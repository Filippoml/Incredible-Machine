using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace Colliders.Objects
{
    public class ConveyorBeltObject : Rectangle
    {

        private bool placing = true;
        float xCoord, yCoord;

        public ConveyorBeltObject(float mass, float x, float y, float wid, float hig, float angle) : base(mass, x, y, wid, hig, angle)
        {

        }


        void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                placing = false;
                xCoord = x;
                yCoord = y;
            }
            if (placing)
            {
                this.x = Input.mouseX;
                this.y = Input.mouseY;
                base.pos.x = Input.mouseX;
                base.pos.y = Input.mouseY;

            }
            else
            {
                base.Update();
            }
        }
    }
}
