using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace Colliders
{
    public class Robot : AnimationSprite
    {
        bool wait;
        public Robot(float x, float y) : base ("robotIdle.png", 10, 3)
        {
            this.x = x;
            this.y = y;
        }

        void Update()
        {
            
            if(wait)
            {
                wait = false;
            }
            else
            {
                NextFrame();
                wait = true;
            }
        }
    }
}
