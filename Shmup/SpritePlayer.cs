using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shmup
{
    public class SpritePlayer : Sprite
    {
        private Vector2 _moveSpeed = new(16f, 9f);

        public SpritePlayer(Texture2D texture, Vector2 position, Vector2 size, Vector2? origin = null) : base(texture, position, size, origin) { }

        public SpritePlayer(Texture2D texture, Rectangle screenBounds) : base(texture, new(screenBounds.Width / 4, screenBounds.Height / 2), texture.Bounds.Size.ToVector2(), new(0f, texture.Bounds.Center.Y)) { }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W)) _position.Y -= _moveSpeed.Y;
            if (keyboardState.IsKeyDown(Keys.S)) _position.Y += _moveSpeed.Y;

            if (keyboardState.IsKeyDown(Keys.A)) _position.X -= _moveSpeed.X;
            if (keyboardState.IsKeyDown(Keys.D)) _position.X += _moveSpeed.X;

            base.Update(gameTime);
        }
    }
}
