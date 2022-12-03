using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameTemplate
{
    public class Platform : DrawableGameComponent
    {
        public SpriteBatch spriteBatch { get; set; }
        public Texture2D tex { get; set; }
        public Vector2 position;
        public string test { get; set; }

        public Platform(Game game, SpriteBatch spriteBatch,
            Texture2D tex, Vector2 Position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            position = Position;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteFont regular = Game.Content.Load<SpriteFont>("fonts/regularFont");
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.DrawString(regular, position.Y.ToString(), new Vector2(position.X, position.Y), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        //hitbox
        public Rectangle getLeftBounds()
        {
            return new Rectangle((int)position.X + tex.Width, (int)position.Y + 20, 0, tex.Height);
        }
        public Rectangle getRightBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y + 20, 0, tex.Height);
        }
        public Rectangle getTopBounds()
        {
            return new Rectangle((int)position.X + 5, (int)position.Y - 20, tex.Width - 50, tex.Height);
        }
    }
}
