using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Survive;

namespace GameTemplate
{
    public class Player : DrawableGameComponent
    {
        public SpriteBatch spriteBatch { get; set; }
        public Texture2D tex { get; set; }
        public Vector2 position;
        public int speed { get; set; }

        public int jump { get; set; }

        public bool hasjumped { get; set; }
        public string test { get; set; }

        public float testJump;




        public Player(Game game, SpriteBatch spriteBatch,
            Texture2D tex, int Speed, int Jump, Vector2 newPosition,
            string test, bool hasJumped) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            position = newPosition;
            this.speed = Speed;
            this.jump = Jump;
            this.test = test;
            this.hasjumped = hasJumped;
        }



        public override void Update(GameTime gameTime)
        {
            //very common mistake - never write the following line
            //KeyboardState x = new KeyboardState();
            //------------------------------------------
            position.Y += testJump;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //speed.X = 3f;
                position.X += speed;

            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                //speed.X = 3f;
                position.X -= speed;
            }
            //else
            //{
            //    speed.X = 0f;
            //}

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasjumped == false)
            {
                position.Y -= 1f;
                testJump = -10f;
                hasjumped = true;
            }

            if (hasjumped == true)
            {
                float i = 1;
                testJump += 0.15f * i;

                if (position.Y + tex.Height >= 800)
                {
                    hasjumped = false;
                }
            }

            else if (hasjumped == false)
            {
                testJump = 0f;
            }

            base.Update(gameTime);
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
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
