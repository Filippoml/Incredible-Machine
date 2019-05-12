using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace Colliders
{
    public class SpringObject : Rectangle
    {
       
        float xCoord, yCoord;

        public SpringObject(float mass, float x, float y, float wid, float hig, float angle, bool placing) : base(mass, x, y, wid, hig, angle, placing)
        {

          
        }

        void Update()
        {
            Console.WriteLine(canBePlaced);
            if (Input.GetMouseButtonDown(0) && canBePlaced)
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
                UpdateRectangleSize();
            }
            else
            {

                base.Update();
            }
            
        }
    }
}
