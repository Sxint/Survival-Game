using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

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
        public float gravity { get; set; }

        public bool isCollideLeft;

        public bool isCollideRight;

        public bool isCollideUp;

        public double time = 0f;

        public int spriteSizeX = 3;

        public int spriteSizeY = 3;

        public int playerWidth;

        public int playerHeight;

        public int frameX = 0;

        public int frameY = 0;

        public int framePause = 6;

        public int frameTime = 0;

        Texture2D rect;



        private SoundEffect jumpSound;



        public Player(Game game, SpriteBatch spriteBatch,
            Texture2D tex, int Speed, int Jump, Vector2 newPosition,
            string test, SoundEffect jumpSound) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            position = newPosition;
            this.speed = Speed;
            this.jump = Jump;
            this.test = test;
            this.jumpSound = jumpSound;
            playerWidth = tex.Width / 3;
            playerHeight = 34;
            this.rect = new Texture2D(game.GraphicsDevice, playerWidth, 5);
            Color[] data = new Color[playerWidth * 5];

            for (int i = 0; i < data.Length; i++) data[i] = Color.Chocolate;
            rect.SetData(data);

        }




        public override void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalMilliseconds;

            //very common mistake - never write the following line
            //KeyboardState x = new KeyboardState();
            //------------------------------------------

            //MouseState mouse = Mouse.GetState();

            //Make the player face the mouse

            //var distance = new Vector2(mouse.X - playerPosition.X, mouse.Y - playerPosition.Y);

            //playerRotation = (float)Math.Atan2(distance.Y, distance.X);

            position.Y += gravity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if(!isCollideLeft)
                {
                    position.X += speed;
                }    
                //isCollideLeft = false;
                //position.X += speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (!isCollideRight)
                {
                    position.X -= speed;
                }
                isCollideRight = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasjumped == false)
            {
                position.Y -= jump;
                gravity = -10f;
                hasjumped = true;
                jumpSound.Play(); 
            }

            if (hasjumped == true)
            {
                float i = jump;
                gravity += 0.15f * i;

                if (position.Y + playerHeight >= 800)
                {
                    hasjumped = false;
                }
            }

            //NOTE: there are some weird edge cases with this but it works
            else if (hasjumped == false)
            {
                if (position.Y < Shared.stage.Y && !isCollideUp)
                {
                    position.Y = Shared.stage.Y - playerHeight;
                }
                else
                {
                    gravity = 0f;
                }
            }

            frameTime++;
            if(frameTime >= framePause)
            {
                frameX++;
                if(frameX == 1 && frameY == 2)
                {
                    frameX = 0;
                    frameY = 0;
                }
                else if(frameX >= spriteSizeX)
                {
                    frameX = 0;
                    frameY++;   
                    if(frameY >= spriteSizeY)
                    {
                        frameY = 0;
                    }
                }
                frameTime = 0;

            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteFont regular = Game.Content.Load<SpriteFont>("fonts/regularFont");
            spriteBatch.Begin();
            //spriteBatch.Draw(tex, position, null, Color.White);
            spriteBatch.Draw(tex, position, new Rectangle((tex.Width / spriteSizeX * frameX) + 9 ,(tex.Height / spriteSizeY * frameY) + 9 ,playerWidth, playerHeight), Color.White);

            //healthbar
            spriteBatch.Draw(rect, new Vector2(position.X - 15, position.Y - 30), Color.White);


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
           // return new Rectangle((int)position.X, (int)position.Y, playerWidth, playerHeight);
           return new Rectangle((tex.Width / spriteSizeX * frameX) + 9, 9, playerWidth, playerHeight);

        }
    }
}
