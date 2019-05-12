using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace Colliders
{
    public class PistonObject : Rectangle
    {
        private bool placing = true;
        float xCoord, yCoord;
        EasyDraw easyDraw;
        int numWidth;
        public PistonObject(float mass, float x, float y, float wid, float hig, float angle) : base(mass, x, y, wid, hig, angle)
        {

            numWidth = (int) this.width;
        }

        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                Clear(System.Drawing.Color.Transparent);
                numWidth++;
                width = numWidth;
                Console.WriteLine(width);
                if (easyDraw != null)
                {
                    RemoveChild(easyDraw);
                }
                easyDraw = new EasyDraw(numWidth, (int)height);
                easyDraw.SetOrigin(numWidth / 2, easyDraw.height / 2);
                //easyDraw.x = (easyDraw.width / 2) - 24;
                //easyDraw.y = 0.1f;
                easyDraw.Fill(100, 100, 100);
                easyDraw.NoStroke();
                easyDraw.ShapeAlign(CenterMode.Min, CenterMode.Min);
                easyDraw.Rect(0, 0, numWidth, height);



                AddChild(easyDraw);
                 base.UpdateRectangleSize(1, x , y, numWidth, height, 0);


            }

           
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
