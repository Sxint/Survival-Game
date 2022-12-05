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
            

            if (playerRect.Intersects(platformRectRight))
            {
                if (!player.hasjumped)
                {
                    player.position = new Vector2(platformRectRight.Left - player.tex.Width, player.position.Y);
                }
                //player.position = new Vector2(platformRectRight.Left - player.tex.Width, player.position.Y);
                player.isCollideRight = true;
            }


            //TODO: Fix jump function
            if (playerRect.Intersects(platformRectTop))
            {
                player.hasjumped = false;
                player.gravity = 0f;
            }

            base.Update(gameTime);
        }
    }
}
