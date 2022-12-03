using GameTemplate;
using Microsoft.Xna.Framework;

namespace Survive
{

    public class CollisionManager : GameComponent
    {
        private Player player;
        private Platform platform;
        public bool hasCollided;

        public CollisionManager(Game game, Player player, Platform platform) : base(game)
        {
            this.player = player;
            this.platform = platform;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerRect = player.getBounds();
            // find the area of the bat;
            Rectangle platformRectLeft = platform.getLeftBounds();

            Rectangle platformRectRight = platform.getRightBounds();

            Rectangle platformRectTop = platform.getTopBounds();
            // check if the intersect
            if (playerRect.Intersects(platformRectLeft))
            {
                player.position = new Vector2(platformRectLeft.Right, player.position.Y);
            }

            if (playerRect.Intersects(platformRectRight))
            {
                player.position = new Vector2(platformRectRight.Right - player.tex.Width, player.position.Y);
            }


            //TODO: Fix jump function
            if (playerRect.Intersects(platformRectTop))
            {
                player.position = new Vector2(player.position.X, platformRectTop.Top - player.tex.Width);
                player.hasjumped = false;
            }


            base.Update(gameTime);
        }
    }
}
