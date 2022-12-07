using GameTemplate;
using Microsoft.Xna.Framework;
using SharpDX.MediaFoundation;

namespace Survive
{
    public class EnemyCollisionManager : GameComponent
    {
      
        private Platform platform;
        private Enemy enemy;


        public EnemyCollisionManager(Game game, Enemy Enemy, Platform platform) : base(game)
        {
            this.platform = platform;
            this.enemy = Enemy;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerRect = enemy.getBounds();





            Rectangle platformRectLeft = platform.getLeftBounds();

            Rectangle platformRectRight = platform.getRightBounds();

            Rectangle platformRectTop = platform.getTopBounds();




            if (playerRect.Intersects(platformRectLeft))
            {

                //player.position = new Vector2(platformRectLeft.Right, player.position.Y);
                enemy.isCollideLeft = true;
            }





            ////right side of platform
            if (playerRect.Intersects(platformRectRight))
            {
                //player.position = new Vector2(platformRectRight.Left - player.tex.Width, player.position.Y);
                enemy.isCollideRight = true;
            }

            //TODO: Fix jump function
            if (playerRect.Intersects(platformRectTop) && enemy.hasjumped && enemy.isCollideLeft == false && enemy.isCollideRight == false)
            {

                if (enemy.position.Y != platformRectTop.Top)
                {
                    enemy.gravity = 0f;
                    enemy.isCollideUp = true;
                    enemy.position.Y = platformRectTop.Top - (enemy.tex.Height);
                    enemy.hasjumped = false;
                }
            }



            if (enemy.isCollideLeft)
            {
                enemy.position.X = platformRectLeft.Right;
                if (enemy.position.Y - enemy.tex.Height > platformRectLeft.Height)
                {
                    enemy.isCollideLeft = false;
                    enemy.Jump();
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
                if (enemy.position.X > platformRectTop.Right)
                {
                    enemy.hasjumped = true;

                    enemy.isCollideUp = false;
                }
                if (enemy.position.X + enemy.tex.Width < platformRectTop.X)
                {
                    enemy.hasjumped = true;

                    enemy.isCollideUp = false;
                }
            }


            base.Update(gameTime);
        }
    }
}
