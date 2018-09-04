using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Quaver.API.Enums;
using Quaver.Assets;
using Quaver.Database.Maps;
using Quaver.Skinning;
using Wobble;
using Wobble.Graphics;
using Wobble.Graphics.Sprites;

namespace Quaver.Screens.Gameplay.UI
{
    public class HitErrorBar : Container
    {
        /// <summary>
        ///     The middle 0ms line for the hit error bar.
        /// </summary>
        public Sprite MiddleLine { get; }

        /// <summary>
        ///     The size of the hit error object pool.
        /// </summary>
        private int PoolSize { get; } = 32;

        /// <summary>
        ///     The list of lines that are currently in the hit error.
        /// </summary>
        public List<Sprite> LineObjectPool { get; }

        /// <summary>
        ///     The current index we're in within the object pool.
        ///     Initialized to -1 because we add to it each time we add a judgement.
        /// </summary>
        private int CurrentLinePoolIndex { get; set; } = -1;

        /// <summary>
        ///     the last hit chevron.
        /// </summary>
        public Sprite LastHitCheveron { get; }

        /// <inheritdoc />
        /// <summary>
        ///   Ctor -  
        /// </summary>
        /// <param name="type"></param>
        /// <param name="size"></param>
        public HitErrorBar(ScalableVector2 size)
        {
            Size = size;

            MiddleLine = new Sprite()
            {
                Size = new ScalableVector2(2, 0, 0, 1),
                Alignment = Alignment.MidCenter,
                Parent = this
            };

            // Create the object pool and initialize all of the sprites.
            LineObjectPool = new List<Sprite>();
            for (var i = 0; i < PoolSize; i++)
            {
                LineObjectPool.Add(new Sprite()
                {
                    Parent = this,
                    Size = new ScalableVector2(4, 0, 0, 1),
                    Alignment = Alignment.MidCenter,
                    Alpha = 0
                });
            }

            // Create the hit chevron.
            LastHitCheveron = new Sprite()
            {
                Parent = this,
                Alignment = Alignment.MidCenter,
                Alpha = 1,
                Image = FontAwesome.CaretDown,
                Y = -Height - 3,
                Size = new ScalableVector2(8, 8)
            };
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            var dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            // Gradually fade out the line.
            foreach (var line in LineObjectPool)
                line.Alpha = MathHelper.Lerp(line.Alpha, 0, (float) Math.Min(dt / 960, 1));

            // Tween the chevron to the last hit
            if (CurrentLinePoolIndex != -1)
                LastHitCheveron.X = MathHelper.Lerp(LastHitCheveron.X, LineObjectPool[CurrentLinePoolIndex].X, (float) Math.Min(dt / 360, 1));

            base.Update(gameTime);
        }

        /// <summary>
        ///     Adds a judgement to the hit error at a given hit time.
        /// </summary>
        public void AddJudgement(Judgement j, double hitTime)
        {
            CurrentLinePoolIndex++;

            if (CurrentLinePoolIndex >= PoolSize)
                CurrentLinePoolIndex = 0;

            LineObjectPool[CurrentLinePoolIndex].Tint = SkinManager.Skin.Keys[MapManager.Selected.Value.Mode].JudgeColors[j];
            LineObjectPool[CurrentLinePoolIndex].X = -(float)hitTime;
            LineObjectPool[CurrentLinePoolIndex].Alpha = 0.5f;
        }
    }
}