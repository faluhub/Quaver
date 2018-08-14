﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using osu_database_reader;
using Quaver.Audio;
using Quaver.Database.Maps;
using Quaver.Database.Scores;
using Quaver.Screens.Gameplay;
using Quaver.Screens.Loading;
using Wobble.Discord;
using Wobble.Graphics;
using Wobble.Input;
using Wobble.Screens;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Quaver.Screens.Select
{
    public class SelectScreen : Screen
    {
        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        public sealed override ScreenView View { get; protected set; }

        /// <summary>
        ///     The list of available mapsets in the screen.
        ///     This value can change as the user searches for maps.
        /// </summary>
        public List<Mapset> AvailableMapsets { get; set; }

        /// <summary>
        /// </summary>
        public SelectScreen()
        {
            // Default the available mapsets to all of them since the user
            // hasn't searched or filtered by anything.
            AvailableMapsets = MapManager.Mapsets;

            DiscordManager.Client.CurrentPresence.Details = "Selecting a song";
            DiscordManager.Client.CurrentPresence.State = "In the menus";
            DiscordManager.Client.SetPresence(DiscordManager.Client.CurrentPresence);

            View = new SelectScreenView(this);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            HandleInput();
            KeepPlayingAudioTrackAtPreview();

            base.Update(gameTime);
        }

        private void HandleInput()
        {
            var screenView = (SelectScreenView) View;

            if (KeyboardManager.IsUniqueKeyPress(Keys.Enter))
                ScreenManager.ChangeScreen(new MapLoadingScreen(new List<LocalScore>()));
        }

        /// <summary>
        ///     Plays the audio track at the preview time if it has stopped
        /// </summary>
        private static void KeepPlayingAudioTrackAtPreview()
        {
            if (AudioEngine.Track == null)
                return;

            if (AudioEngine.Track.HasPlayed && AudioEngine.Track.IsStopped)
                AudioEngine.PlaySelectedTrackAtPreview();
        }
    }
}