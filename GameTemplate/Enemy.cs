using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameTemplate
{
    public class Enemy : DrawableGameComponent
    {
        public SpriteBatch spriteBatch { get; set; }
        public Texture2D tex { get; set; }
        public Vector2 position;
        public int speed { get; set; }
        public int jump { get; set; }
        public bool hasjumped { get; set; }
        public string test { get; set; }
        public float gravity { get; set; }

        public bool isCollideLeft;

        public bool isCollideRight;

        public bool isCollideUp;

        public double time = 0f;

        public Player player;     



        public Enemy(Game game, SpriteBatch spriteBatch,
            Texture2D tex, int Speed, int Jump, Vector2 newPosition,
            string test, Player Player) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            position = newPosition;
            this.speed = Speed;
            this.jump = Jump;
            this.test = test;
            this.player = Player;
            //playerOrigin = new Vector2(tex.Width / 2, tex.Height / 2);
            //playerPosition = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        }




        public override void Update(GameTime gameTime)
        {
            if (position.X > player.position.X && !isCollideLeft)
            {
                position.X -= 1;
            }
            else if (position.X < player.position.X)
            {
                position.X += 1;
            }


            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteFont regular = Game.Content.Load<SpriteFont>("fonts/regularFont");
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, null, Color.White);

            //spriteBatch.Draw(tex, position, null, Color.White, playerRotation, playerOrigin, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(regular, position.Y.ToString(), new Vector2(position.X, position.Y), Color.White);
            spriteBatch.DrawString(regular, "isCollideUp: " + isCollideUp.ToString(), new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(regular, "isCollideLeft: " + isCollideLeft.ToString(), new Vector2(0, 20), Color.White);
            spriteBatch.DrawString(regular, "isCollideRight: " + isCollideRight.ToString(), new Vector2(0, 40), Color.White);
            spriteBatch.DrawString(regular, "HasJump: " + hasjumped.ToString(), new Vector2(0, 60), Color.White);
            spriteBatch.DrawString(regular, "Player X Position: " + position.X.ToString(), new Vector2(0, 80), Color.White);
            spriteBatch.DrawString(regular, "Player Y Position: " + position.Y.ToString(), new Vector2(0, 100), Color.White);


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
