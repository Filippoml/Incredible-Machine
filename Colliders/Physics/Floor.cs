using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace Colliders
{
    public class Floor : Rectangle
    {
        float time;

        public Floor(float x, float y, float wid, float hig, string name, float angle) : base(0, x, y, wid, hig, angle)
        {
            this.time = 0;
            this.names = name;
        }

        public void loopMovement (float dt)
        {
            this.time += dt;

            this.pos.x = game.width / 2 + 200 * Mathf.Sin(this.time / 2);
        }
    }
}
