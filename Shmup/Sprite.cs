using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shmup
{
    public class Sprite
    {
        public Texture2D _texture;
        public Rectangle _drawBounds;
        public Vector2 _position, _size, _origin;

        public Sprite(Texture2D texture, Vector2 position, Vector2 size, Vector2? origin = null)
        {
            _texture = texture;
            _origin = origin ?? Vector2.Zero;
            _position = position;
            _size = size;
        }

        public void Update(GameTime gameTime)
        {
            _drawBounds = new(_position.ToPoint(), _size.ToPoint());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _drawBounds, null, Color.White, 0f, _origin, SpriteEffects.None, 0f);
        }
    }
}
