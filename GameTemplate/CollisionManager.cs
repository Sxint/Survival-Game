using GameTemplate;
using Microsoft.Xna.Framework;

namespace Survive
{
    public class CollisionManager : GameComponent
    {
        private Player player;
        private Platform platform;
        public bool onPlatform;


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

                player.position = new Vector2(platformRectLeft.Right, player.position.Y);
                player.isCollideLeft = true;

            }
            else
            {
                player.isCollideLeft = false;
            }
            //right side of platform
            if (playerRect.Intersects(platformRectRight))
            {
                player.position = new Vector2(platformRectRight.Left - player.tex.Width, player.position.Y);
                player.isCollideRight = true;
            }
            else
            {
                player.isCollideRight = false;
            }


            //TODO: Fix jump function
            if (playerRect.Intersects(platformRectTop))
            {
                if(player.position.Y != platformRectTop.Top)
                {
                    player.gravity = 0f;
                    player.isCollideUp = true;
                    player.position.Y = platformRectTop.Top - (player.tex.Height + 1);
                    player.hasjumped = false;
                }
            }

            base.Update(gameTime);
        }
    }
}
