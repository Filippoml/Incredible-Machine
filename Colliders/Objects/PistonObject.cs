using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colliders.Objects;
using GXPEngine;

namespace Colliders
{
    public class PistonObject : Rectangle
    {
        int maxXinit, maxXfin = 5;

        private bool pistonAnimation;
        float xCoord, yCoord;
        EasyDraw easyDraw;
        int numWidth;
        SubPiston test, test2;
        public PistonObject(float mass, float x, float y, float wid, float hig, float angle, bool placing) : base(mass, x, y, wid, hig, angle, placing)
        {

            numWidth = (int) this.width;
            test = new SubPiston(0, 0, 0, 50, 10, 0);
            test2 = new SubPiston(0, 0, 0, 10, 40, 0);
            ((MyGame)game).AddChild(test);
            ((MyGame)game).mObjects.Add(test);

            ((MyGame)game).AddChild(test2);
            ((MyGame)game).mObjects.Add(test2);

            //((MyGame)game).mObjects.Add(test2);
        }

        void Update()
        {
            if(maxXinit< maxXfin && pistonAnimation)
            {
                int speed = 10;
                maxXinit ++;
                test.x += speed;
                test.pos.x += speed;

                test2.x += speed;
                test2.pos.x += speed;

                if (maxXinit == maxXfin)
                {
                    pistonAnimation = false;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                pistonAnimation = true;


                //test2.x++;
                //test2.pos.x++;




                //test.UpdateRectangleSize(1, test.x, test.y, test.width, test.height, 0);
                //test2.UpdateRectangleSize(1, test2.x, test2.y, test2.width, test2.height, 0);
                //numWidth++;
                //width = numWidth;

                //easyDraw = new EasyDraw(numWidth, (int)height);
                //easyDraw.SetOrigin(numWidth / 2, easyDraw.height / 2);
                ////easyDraw.x = (easyDraw.width / 2) - 24;
                ////easyDraw.y = 0.1f;
                //easyDraw.Fill(100, 100, 100);
                //easyDraw.NoStroke();

                //easyDraw.ShapeAlign(CenterMode.Min, CenterMode.Min);
                //easyDraw.Rect(0, 0, numWidth, height);


                //AddChild(easyDraw);
                //base.UpdateRectangleSize(1, x , y, numWidth, height, 0);


            }


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

                test.x = this.x;
                test2.x = this.x + (test.width /2 );

                test.pos.x = Input.mouseX + 100;
                test2.pos.x = Input.mouseX + 100;

            }
            else
            {
                if (((MyGame)game).box.x  > this.x + this.width + 13 && ((MyGame)game).box.x - this.x < 100 && Mathf.Abs(((MyGame)game).box.y - this.y) < this.height)
                {
                    pistonAnimation = true;
                }
                base.Update();
                test.UpdateRectangleSize();
                test2.UpdateRectangleSize();

            }
            test.y = test.pos.y = this.y;
            test2.y = test2.pos.y = this.y;
  
        }

        public void DestroyChildren()
        {
            var gameVar = ((MyGame)game);
            gameVar.mObjects.Remove(test);
            test.Destroy();

            gameVar.mObjects.Remove(test2);
            test2.Destroy();
        }
    }
}
