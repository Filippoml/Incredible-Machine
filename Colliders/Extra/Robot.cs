using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace Colliders
{
    public class Robot : Sprite
    {
        
        public Robot(float x, float y) : base ("robot.png")
        {
            this.x = x;
            this.y = y;
        }
    }
}
