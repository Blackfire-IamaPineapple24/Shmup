using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shmup
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Random rng = new();

        public SpriteFont _uiFont;
        public Texture2D _backgroundTxr, _saucerTxr, _missileTxr;
        public Rectangle _screenBounds = new(0, 0, 1280, 720);

        public List<Sprite> _spriteList = [];
        public SpritePlayer _player;

        private int _maxMissiles = 16;
        private int _minMissiles = 8;

        public int _lives = 3;
        public int _score = 0;

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

            _uiFont = Content.Load<SpriteFont>("ShmupUIFont");

            _backgroundTxr = Content.Load<Texture2D>("background");
            _saucerTxr = Content.Load<Texture2D>("saucer");
            _missileTxr = Content.Load<Texture2D>("missile");
        }

        protected override void Update(GameTime gameTime)
        {
            if (_spriteList.Count == 0)
            {
                _spriteList.Add(new Sprite(_backgroundTxr, _screenBounds.Location.ToVector2(), _screenBounds.Size.ToVector2()));

                _player = new SpritePlayer(_saucerTxr, _screenBounds);
                _spriteList.Add(_player);
            }

            if (_spriteList.OfType<SpriteMissile>().Count() < _minMissiles)
            {
                if (rng.NextDouble() < gameTime.ElapsedGameTime.TotalSeconds * 4.0)
                {
                    _spriteList.Add(new SpriteMissile(_missileTxr, _screenBounds));
                }
            }

            if (!_player._isDead)
            {
                if (_spriteList.OfType<SpriteMissile>().Count() < _maxMissiles)
                {
                    if (rng.NextDouble() < gameTime.ElapsedGameTime.TotalSeconds * (float.Sqrt(_score) + 1))
                    {
                        _spriteList.Add(new SpriteMissile(_missileTxr, _screenBounds));
                    }
                }
            }

            _spriteList.ForEach(eachSprite => eachSprite.Update(gameTime));

            if (!_player._isDead)
            {
                foreach (SpriteMissile eachMissile in _spriteList.OfType<SpriteMissile>())
                {
                    if (eachMissile._drawBounds.Intersects(_player._drawBounds))
                    {
                        eachMissile._isDead = true;
                        _lives--;
                        if (_lives <= 0)
                        {
                            _player._isDead = true;
                            break;
                        }
                    }
                }    
            }

            if (!_player._isDead)
            {
                _score += _spriteList.OfType<SpriteMissile>().Where(eachSprite => eachSprite._position.X < -eachSprite._texture.Width).Count();
            }

            _spriteList.RemoveAll(deadSprites => deadSprites._isDead);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteList.ForEach(eachSprite => eachSprite.Draw(_spriteBatch));

            _spriteBatch.DrawString(_uiFont, $"Score: {_score}", new Vector2(8f, 8f), Color.White);
            _spriteBatch.DrawString(_uiFont, $"Lives: {_lives}", new Vector2(8f, 80f), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
