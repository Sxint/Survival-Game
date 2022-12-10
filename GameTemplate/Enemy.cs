﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Survive;

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

        public bool isHit;

        public double time = 0f;

        public Player player;

        public int enemyWidth;

        public int enemyHeight;

        public int frameX = 0;

        public int frameY = 0;

        public int framePause = 7;

        public int frameTime = 0;

        public int spriteSizeX = 3;

        public int spriteSizeY = 3;


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
            enemyWidth = tex.Width / 3;
            enemyHeight = 50;
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
            if (getBounds().Contains(player.position))
            {
                player.isHit = true;
                player.health -= 1;
            }
            else
            {
                player.isHit = false;
            }

            if (player.position.X <= position.X)
            {
                if (!isCollideLeft)
                {
                    position.X -= 4;
                }
                isCollideLeft = false;
            }

            if (player.position.X >= position.X)
            {
                if (!isCollideLeft)
                {
                    position.X += 4;
                }
                isCollideRight = false;
            }

            if (hasjumped == true)
            {
                float i = jump;
                gravity += 0.15f * i;

                if (position.Y + enemyHeight >= 800)
                {
                    hasjumped = false;
                }
            }

            //NOTE: there are some weird edge cases with this but it works
            else if (hasjumped == false)
            {
                if (position.Y < Shared.stage.Y && !isCollideUp)
                {
                    position.Y = Shared.stage.Y - enemyHeight;
                }
                else
                {
                    gravity = 0f;
                }
            }

            frameTime++;
            if (frameTime >= framePause)
            {
                frameX++;
                if (frameX == 1 && frameY == 2)
                {
                    frameX = 0;
                    frameY = 0;
                }
                else if (frameX >= spriteSizeX)
                {
                    frameX = 0;
                    frameY++;
                    if (frameY >= spriteSizeY)
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

            spriteBatch.Draw(tex, position, new Rectangle((tex.Width / spriteSizeX * frameX) +9, (tex.Height / spriteSizeY * frameY) +9, enemyWidth, enemyHeight), Color.White);

            //spriteBatch.Draw(tex, position, null, Color.White, playerRotation, playerOrigin, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(regular, position.Y.ToString(), new Vector2(position.X, position.Y), Color.White);
            spriteBatch.DrawString(regular, "isCollideUp: " + isCollideUp.ToString(), new Vector2(0, 120), Color.White);
            spriteBatch.DrawString(regular, "isCollideLeft: " + isCollideLeft.ToString(), new Vector2(0,140), Color.White);
            spriteBatch.DrawString(regular, "isCollideRight: " + isCollideRight.ToString(), new Vector2(0, 160), Color.White);
            spriteBatch.DrawString(regular, "HasJump: " + hasjumped.ToString(), new Vector2(0, 180), Color.White);
            spriteBatch.DrawString(regular, "x Position: " + position.X.ToString(), new Vector2(700, 200), Color.White);





            spriteBatch.End();
            base.Draw(gameTime);
        }


    //hitbox
    public Rectangle getBounds()
        {
           return new Rectangle((int)position.X, (int)position.Y, enemyWidth, enemyHeight);
           //return new Rectangle((tex.Width / spriteSizeX * frameX) + 9, 9, enemyWidth, enemyHeight);

        }
    }
}
