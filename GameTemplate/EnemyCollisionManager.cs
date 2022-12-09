using GameTemplate;
using Microsoft.Xna.Framework;
using SharpDX.MediaFoundation;
using System;

namespace Survive
{
    public class EnemyCollisionManager : GameComponent
    {

        private Platform platform;
        private Enemy enemy;


        public EnemyCollisionManager(Game game, Enemy Enemy, Platform platform) : base(game)
        {
            this.enemy = Enemy;
            this.platform = platform;
        }



        public override void Update(GameTime gameTime)
        {

            Rectangle enemyRect = enemy.getBounds();

            Rectangle platformRectLeft = platform.getLeftBounds();

            Rectangle platformRectRight = platform.getRightBounds();

            Rectangle platformRectTop = platform.getTopBounds();




            if (enemyRect.Intersects(platformRectLeft))
            {
                enemy.isCollideLeft = true;

                enemy.position.Y -= enemy.jump;
                enemy.gravity = -10f;
                enemy.hasjumped = true;

            }


            ////right side of platform
            if (enemyRect.Intersects(platformRectRight))
            {
                //player.position = new Vector2(platformRectRight.Left - player.tex.Width, player.position.Y);
                enemy.isCollideRight = true;
            }

            //TODO: Fix jump function
            if (enemyRect.Intersects(platformRectTop) && enemy.hasjumped && enemy.isCollideLeft == false && enemy.isCollideRight == false)
            {

                if (enemy.position.Y != platformRectTop.Top)
                {
                    enemy.gravity = 0f;
                    enemy.isCollideUp = true;
                    enemy.position.Y = platformRectTop.Top - (enemy.tex.Height);
                    enemy.hasjumped = false;
                }
                enemy.hasjumped = false;

            }



            if (enemy.isCollideLeft && enemy.hasjumped != true)
            {
                enemy.position.X = platformRectLeft.Right;
                if (enemy.position.Y - enemy.tex.Height > platformRectLeft.Height)
                {
                    enemy.isCollideLeft = false;
                }
            }

            if (enemy.isCollideRight)
            {
                enemy.position.X = platformRectRight.Left - enemy.tex.Width;
                if (enemy.position.Y - enemy.tex.Height > platformRectLeft.Height)
                {
                    enemy.isCollideRight = false;
                }
            }

            if (enemy.isCollideUp)
            {
                if (enemy.position.X > platformRectTop.Right && enemy.hasjumped != true)
                {
                    enemy.hasjumped = true;

                    enemy.isCollideUp = false;
                }
                if (enemy.position.X + enemy.tex.Width < platformRectTop.X && enemy.hasjumped != true)
                {
                    enemy.hasjumped = true;

                    enemy.isCollideUp = false;
                }
            }


            base.Update(gameTime);
        }
    }
}
