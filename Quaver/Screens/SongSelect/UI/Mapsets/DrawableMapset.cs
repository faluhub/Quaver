﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Quaver.Database.Maps;
using Quaver.Graphics;
using Quaver.Assets;
using Quaver.Scheduling;
using Quaver.Screens.Loading;
using Quaver.Skinning;
using Wobble;
using Wobble.Assets;
using Wobble.Graphics;
using Wobble.Graphics.Animations;
using Wobble.Graphics.Shaders;
using Wobble.Graphics.Sprites;
using Wobble.Graphics.UI.Buttons;
using Wobble.Input;
using Wobble.Logging;
using Wobble.Window;

namespace Quaver.Screens.SongSelect.UI.Mapsets
{
    public class DrawableMapset : Button
    {
        /// <summary>
        ///     The container to scroll for maps.
        /// </summary>
        public MapsetScrollContainer Container { get; }

        /// <summary>
        ///     The mapset this drawable is currently representing.
        /// </summary>
        public Mapset Mapset { get; private set; }

        /// <summary>
        ///     The index of the set in Screen.AvailableMapsets
        /// </summary>
        public int MapsetIndex { get; set; }

        /// <summary>
        ///    The title of the song.
        /// </summary>
        public SpriteText Title { get; }

        /// <summary>
        ///     The artist of the song
        /// </summary>
        public SpriteText Artist { get; }

        /// <summary>
        ///     The creator of the mapset.
        /// </summary>
        public SpriteText Creator { get; }

        /// <summary>
        ///     The height of the drawable mapset.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static int HEIGHT { get; } = 84;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public DrawableMapset(MapsetScrollContainer container)
        {
            Container = container;
            Size = new ScalableVector2(410, HEIGHT);
            Image = UserInterface.SelectButtonBackground;

            Title = new SpriteText(BitmapFonts.Exo2SemiBold, " ", 13)
            {
                Parent = this,
                Alignment = Alignment.TopLeft,
                Position = new ScalableVector2(15, 12)
            };

            Artist = new SpriteText(BitmapFonts.Exo2SemiBold, " ", 12, false)
            {
                Parent = this,
                Alignment = Alignment.TopLeft,
                Position = new ScalableVector2(Title.X, Title.Y + Title.Height + 3)
            };

            Creator = new SpriteText(BitmapFonts.Exo2Medium, " ", 10, false)
            {
                Parent = this,
                Alignment = Alignment.TopRight,
                Position = new ScalableVector2(-10, Artist.Y + Artist.Height + 2)
            };

            Clicked += OnClicked;
        }

        /// <summary>
        ///     Updates the mapset this drawable represents.
        /// </summary>
        public void UpdateWithNewMapset(Mapset set, int mapsetIndex)
        {
            Mapset = set;
            MapsetIndex = mapsetIndex;

            Title.Text = Mapset.Title;
            Artist.Text = Mapset.Artist;
            Creator.Text = "By: " + Mapset.Creator;
        }

        /// <summary>
        ///     Displays the mapset as selected.
        /// </summary>
        public void DisplayAsSelected(Map map)
        {
            // Change the width of the set outwards to appear it as selected.
            Animations.Clear();
            ChangeWidthTo(500, Easing.OutQuint, 600);
            FadeToColor(new Color(68, 174, 221), Easing.OutQuint, 300);

            Title.Animations.Clear();
            Artist.Animations.Clear();
            Creator.Animations.Clear();

            Title.Animations.Add(new Animation(AnimationProperty.Alpha, Easing.OutQuint, Title.Alpha, 1f, 400));
            Artist.Animations.Add(new Animation(AnimationProperty.Alpha, Easing.OutQuint, Artist.Alpha, 1f, 400));
            Creator.Animations.Add(new Animation(AnimationProperty.Alpha, Easing.OutQuint, Creator.Alpha, 1f, 400));
        }

        /// <summary>
        ///     Displays the mapset as deselected
        /// </summary>
        public void DisplayAsDeselected()
        {
            Animations.Clear();
            ChangeWidthTo(410,Easing.OutQuint, 600);
            FadeToColor(Color.Black, Easing.OutQuint, 300);

            Title.Animations.Clear();
            Artist.Animations.Clear();
            Creator.Animations.Clear();

            Title.Animations.Add(new Animation(AnimationProperty.Alpha, Easing.OutQuint, Title.Alpha, 0.65f, 400));
            Artist.Animations.Add(new Animation(AnimationProperty.Alpha, Easing.OutQuint, Artist.Alpha, 0.65f, 400));
            Creator.Animations.Add(new Animation(AnimationProperty.Alpha, Easing.OutQuint, Creator.Alpha, 0.65f, 400));
        }

        /// <summary>
        ///     Called when the button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClicked(object sender, EventArgs e)
        {
            // Whenever someone clicks the set, switch containers to the difficulties.
            if (Container.SelectedMapsetIndex == MapsetIndex)
            {
                Container.View.SwitchToContainer(SelectContainerStatus.Difficulty);
                return;
            }

            var map = Mapset.PreferredMap ?? Mapset.Maps.First();
            Container.SelectMap(MapsetIndex, map);
        }

        /// <inheritdoc />
        /// <summary>
        ///     In this case, we only want buttons to be clickable if they're in the bounds of the scroll container.
        /// </summary>
        /// <returns></returns>
        protected override bool IsMouseInClickArea()
        {
            var newRect = Rectangle.Intersect(ScreenRectangle.ToRectangle(), Container.ScreenRectangle.ToRectangle());
            return GraphicsHelper.RectangleContains(newRect, MouseManager.CurrentState.Position);
        }
    }
}