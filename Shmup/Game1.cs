using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Shmup
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Texture2D _backgroundTxr, _saucerTxr, _missileTxr;
        public Rectangle _screenBounds = new(0, 0, 1280, 720);

        public List<Sprite> _spriteList = [];

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = _screenBounds.Width;
            _graphics.PreferredBackBufferHeight = _screenBounds.Height;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _backgroundTxr = Content.Load<Texture2D>("background");
            _saucerTxr = Content.Load<Texture2D>("saucer");
            _missileTxr = Content.Load<Texture2D>("missile");
        }

        protected override void Update(GameTime gameTime)
        {
            if (_spriteList.Count == 0)
            {
                _spriteList.Add(new Sprite(_backgroundTxr, _screenBounds.Location.ToVector2(), _screenBounds.Size.ToVector2()));
            }

            _spriteList.ForEach(eachSprite => eachSprite.Update(gameTime));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteList.ForEach(eachSprite => eachSprite.Draw(_spriteBatch));
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
