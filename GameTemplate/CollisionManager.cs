using GameTemplate;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.WIC;

namespace Survive
{
    public class CollisionManager : GameComponent
    {
        private Player player;
        private Platform platform;
        public CollisionManager(Game game, Player player, Platform platform) : base(game)
        {
            this.player = player;
            this.platform = platform;
        }

        public override void Update(GameTime gameTime)
        {
            // find the area of ball
            Rectangle playerRect = player.getBounds();
            // find the area of the bat;
            Rectangle platformRect = platform.getBounds();
            // check if the intersect
            if (playerRect.Intersects(platformRect))
            {
                player.speed = new Vector2(0, 0);
            }

            base.Update(gameTime);
        }
    }
}
