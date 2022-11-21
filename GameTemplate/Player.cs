using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace GameTemplate
{
    public class Player : DrawableGameComponent
    {
        public SpriteBatch spriteBatch { get; set; }
        public Texture2D tex { get; set; }
        public Vector2 position { get; set; }
        public Vector2 speed { get; set; }
        public string test { get; set; }




        public Player(Game game, SpriteBatch spriteBatch,
            Texture2D tex, Vector2 position,
            Vector2 speed, string test) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            this.test = test;
        }



        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            //very common mistake - never write the following line
            //KeyboardState x = new KeyboardState();
            //------------------------------------------

            if (ks.IsKeyDown(Keys.A))
            {
                position -= speed;
                if (position.X < 0)
                {
                    position = new Vector2(0, position.Y);
                }
            }
            if (ks.IsKeyDown(Keys.D))
            {
                position += speed;
                if (position.X > Shared.stage.X - tex.Width)
                {
                    this.position = new Vector2(Shared.stage.X - tex.Width, position.Y);
                }
            }

            //added rudimentary jumping function (still need to fix)
            if (ks.IsKeyDown(Keys.W))
            {
                this.position = new Vector2(this.position.X, this.position.Y - (float)gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteFont regular = Game.Content.Load<SpriteFont>("fonts/regularFont");
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.DrawString(regular, "test", new Vector2(position.X, position.Y), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }


        //hitbox
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
