using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    public class HealthBar
    {
        public Rectangle health(Vector2 position, int hitPoints)
        {
            return new Rectangle((int)position.X, (int)position.Y, hitPoints, 5);
        }
    }
}
