using Microsoft.Xna.Framework;
using Quaver.API.Enums;

namespace Quaver.Shared.Screens.Edit.Rulesets
{
    public abstract class EditorRuleset : IGameScreenComponent
    {
        /// <summary>
        ///     The game mode the editor is for.
        /// </summary>
        public GameMode Mode { get; }

        /// <summary>
        ///     Reference to the editor screen itself.
        /// </summary>
        public EditorScreen Screen { get; }

        /// <summary>
        ///     The editor playfield itself.
        /// </summary>
        public IEditorPlayfield Playfield { get; private set; }

        /// <summary>
        ///     Ctor -
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="mode"></param>
        protected EditorRuleset(EditorScreen screen, GameMode mode)
        {
            Screen = screen;
            Mode = mode;

            Initialize();
        }

        /// <summary>
        ///     Initializes the playfield.
        /// </summary>
        private void Initialize() => Playfield = CreatePlayfield();

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime) => Playfield.Update(gameTime);

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime) => Playfield.Draw(gameTime);

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public void Destroy() => Playfield.Destroy();

        /// <summary>
        ///     Creates the editor playfield.
        /// </summary>
        /// <returns></returns>
        public abstract IEditorPlayfield CreatePlayfield();
    }
}