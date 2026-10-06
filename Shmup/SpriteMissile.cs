using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shmup
{
    public class SpriteMissile : Sprite
    {
        private static Random rng = new Random();

        private float _speed;
        private float _speedMin = 400f;
        private float _speedMax = 800f;

        public SpriteMissile(Texture2D texture, Vector2 position, Vector2 size, Vector2? origin = null) : base(texture, position, size, origin)
        {
            SetSpeed();
        }

        public SpriteMissile(Texture2D texture, Rectangle screenBounds) : base(texture, new(screenBounds.Width, (float)rng.NextDouble() * screenBounds.Height), texture.Bounds.Size.ToVector2(), new(0f, texture.Bounds.Center.Y))
        {
            SetSpeed();
        }

        private void SetSpeed()
        {
            _speed = (rng.NextSingle() * (_speedMax - _speedMin)) + _speedMin;
        }

        public override void Update(GameTime gameTime)
        {
            _position.X -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_position.X < -_texture.Width) _isDead = true;

            base.Update(gameTime);
        }
    }
}
