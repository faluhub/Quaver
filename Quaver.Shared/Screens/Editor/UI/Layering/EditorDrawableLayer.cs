﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Quaver.API.Maps.Structures;
using Quaver.Shared.Assets;
using Quaver.Shared.Database.Maps;
using Quaver.Shared.Graphics.Containers;
using Quaver.Shared.Screens.Menu.UI.Jukebox;
using Wobble.Graphics;
using Wobble.Graphics.Sprites;
using Wobble.Graphics.UI.Buttons;

namespace Quaver.Shared.Screens.Editor.UI.Layering
{
    public class EditorDrawableLayer : PoolableSprite<EditorLayerInfo>
    {
        /// <summary>
        /// </summary>
        private EditorLayerCompositor LayerCompositor { get; }

        /// <summary>
        /// </summary>
        private EditorLayerVisiblityCheckbox VisibilityCheckbox { get; set; }

        /// <summary>
        /// </summary>
        private JukeboxButton EditLayerNameButton { get; set; }

        /// <summary>
        /// </summary>
        private SpriteTextBitmap LayerName { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public sealed override int HEIGHT { get; } = 40;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="layerCompositor"></param>
        /// <param name="item"></param>
        /// <param name="index"></param>
        public EditorDrawableLayer(EditorLayerCompositor layerCompositor, EditorLayerInfo item, int index) : base(item, index)
        {
            LayerCompositor = layerCompositor;
            Tint = Color.White;

            Alpha = layerCompositor.SelectedLayerIndex.Value == index ? 0.45f : 0;
            Size = new ScalableVector2(LayerCompositor.Width, HEIGHT);

            CreateVisibilityCheckbox();
            CreateEditNamePencil();
            CreateLayerName();
        }

        /// <summary>
        /// </summary>
        private void CreateVisibilityCheckbox() => VisibilityCheckbox = new EditorLayerVisiblityCheckbox(Item)
        {
            Parent = this,
            Alignment = Alignment.MidLeft,
            X = 12,
            Size = new ScalableVector2(16, 16),
        };

        /// <summary>
        /// </summary>
        private void CreateEditNamePencil() => EditLayerNameButton = new JukeboxButton(FontAwesome.Get(FontAwesomeIcon.fa_pencil))
        {
            Parent = this,
            Alignment = Alignment.MidLeft,
            X = VisibilityCheckbox.X + VisibilityCheckbox.Width + 10,
            Size = VisibilityCheckbox.Size
        };

        /// <summary>
        /// </summary>
        private void CreateLayerName() => LayerName = new SpriteTextBitmap(FontsBitmap.AllerRegular, Item.Name)
        {
            Parent = this,
            FontSize = 16,
            Alignment = Alignment.MidLeft,
            X = EditLayerNameButton.X + EditLayerNameButton.Width + 10
        };

        /// <summary>
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="index"></param>
        public override void UpdateContent(EditorLayerInfo layer, int index)
        {
        }
    }
}