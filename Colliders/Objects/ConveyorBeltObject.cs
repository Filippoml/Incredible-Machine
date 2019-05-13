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
        float xCoord, yCoord;

        public ConveyorBeltObject(float mass, float x, float y, float wid, float hig, float angle, bool placing) : base(mass, x, y, wid, hig, angle, placing)
        {

        }


        void Update()
        {
            if (this.DistanceTo(Input.mouseX, Input.mouseY) < this.width / 2 && Input.GetMouseButtonDown(0) && !placing)
            {
                placing = true;
            }
            else if (Input.GetMouseButtonDown(0) && canBePlaced)
            {
                placing = false;
                xCoord = x;
                yCoord = y;
            }
            else if (placing)
            {
                this.x = Input.mouseX;
                this.y = Input.mouseY;
                base.pos.x = Input.mouseX;
                base.pos.y = Input.mouseY;
                UpdateRectangleSize();
            }
            else
            {
                base.Update();
            }
        }
    }
}
