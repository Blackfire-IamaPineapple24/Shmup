using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shmup
{
    internal class SpriteMissile : Sprite
    {
        public SpriteMissile(Texture2D texture, Vector2 position, Vector2 size, Vector2? origin = null) : base(texture, position, size, origin) { }

        public SpriteMissile(Texture2D texture, Rectangle screenBounds) : base(texture, new(screenBounds.Width, (float)rng.NextDouble() * screenBounds.Height), texture.Bounds.Size.ToVector2(), new(0f, texture.Bounds.Center.Y)) {}

        public override void Update(GameTime gameTime)
        {
            _position.X -= 10f;
            base.Update(gameTime);
        }
    }
}
