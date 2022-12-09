using GameTemplate;
using Microsoft.Xna.Framework;
using SharpDX.MediaFoundation;

namespace Survive
{
    public class CollisionManager : GameComponent
    {
        private Player player;
        private Platform platform;
        private Enemy enemy;
        private Rectangle playerRect;
        private Rectangle enemyRect;



        public CollisionManager(Game game, Player player, Platform platform) : base(game)
        {
            this.player = player;
            this.platform = platform;
        }



        public override void Update(GameTime gameTime)
        {
        
            Rectangle playerRect = player.getBounds();
            
            Rectangle platformRectLeft = platform.getLeftBounds();

            Rectangle platformRectRight = platform.getRightBounds();

            Rectangle platformRectTop = platform.getTopBounds();

        


            if (playerRect.Intersects(platformRectLeft))
            {

                //player.position = new Vector2(platformRectLeft.Right, player.position.Y);
                player.isCollideLeft = true;
            }

            

           

            ////right side of platform
            if (playerRect.Intersects(platformRectRight))
            {
                //player.position = new Vector2(platformRectRight.Left - player.tex.Width, player.position.Y);
                player.isCollideRight = true;
            }

            //TODO: Fix jump function
            if (playerRect.Intersects(platformRectTop) && player.hasjumped && player.isCollideLeft == false && player.isCollideRight == false)
            {

                if (player.position.Y != platformRectTop.Top)
                {
                    player.gravity = 0f;
                    player.isCollideUp = true;
                    player.position.Y = platformRectTop.Top - (player.tex.Height);
                    player.hasjumped = false;
                }
            }

          
            
            if (player.isCollideLeft)
            {
                player.position.X = platformRectLeft.Right;
                if (player.position.Y - player.tex.Height > platformRectLeft.Height)
                {
                    player.isCollideLeft = false;
                }
            }

            if (player.isCollideRight)
            {
                player.position.X = platformRectRight.Left - player.tex.Width;
                if (player.position.Y - player.tex.Height > platformRectLeft.Height)
                {
                    player.isCollideRight = false;
                }
            }

            if (player.isCollideUp)
            {
                if (player.position.X > platformRectTop.Right)
                {
                    player.hasjumped = true;

                    player.isCollideUp = false;
                }
                if (player.position.X + player.tex.Width < platformRectTop.X)
                {
                    player.hasjumped = true;

                    player.isCollideUp = false;
                }
            }
            

            base.Update(gameTime);
        }
    }
}
