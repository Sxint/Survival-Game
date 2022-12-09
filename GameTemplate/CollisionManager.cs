using GameTemplate;
using Microsoft.Xna.Framework;
using SharpDX.MediaFoundation;
using System.Collections.Generic;

namespace Survive
{
    public class CollisionManager : GameComponent
    {
        private Player player;
        private Platform platform;
        private Enemy enemy;
        private Rectangle playerRect;
        private Rectangle enemyRect;
        private List<Platform> platforms;



        public CollisionManager(Game game, Player player, List<Platform> platforms) : base(game)
        {
            this.player = player;
            this.platforms = platforms;
        }



        public override void Update(GameTime gameTime)
        {
        

        
            foreach(Platform item in platforms) 
            { 
                
                Rectangle playerRect = player.getBounds();
            
                Rectangle platformRectLeft = item.getLeftBounds();

                Rectangle platformRectRight = item.getRightBounds();

                Rectangle platformRectTop = item.getTopBounds();

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
                        player.position.Y = platformRectTop.Top - (player.playerHeight);
                        player.hasjumped = false;
                    }
                }

                 
                if (player.isCollideLeft)
                {
                    player.position.X = platformRectLeft.Right;
                    if (player.position.Y - player.playerHeight > platformRectLeft.Height)
                    {
                        player.isCollideLeft = false;
                    }
                }

                if (player.isCollideRight)
                {
                    player.position.X = platformRectRight.Left - player.playerWidth;
                    if (player.position.Y - player.playerHeight > platformRectLeft.Height)
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
                    if (player.position.X + player.playerWidth < platformRectTop.X)
                    {
                        player.hasjumped = true;

                        player.isCollideUp = false;
                    }
                }
            

            
            }

         

            

           

         
           

            base.Update(gameTime);
        }
    }
}
