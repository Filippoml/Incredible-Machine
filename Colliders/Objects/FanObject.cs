using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colliders.Objects;
using GXPEngine;

namespace Colliders
{
    public class FanObject : Rectangle
    {
        private bool placing = true;
        float xCoord, yCoord;

        public FanObject(float mass, float x, float y, float wid, float hig, float angle, bool placing) : base(mass, x, y, wid, hig, angle, placing)
        {

        }

        int wait = 200;
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
                if (Mathf.Abs(((MyGame)game).box.x - this.x)< this.width)
                {
                    float distance = ((MyGame)game).box.DistanceTo(this);
                    float number = -9.8f;

                    if (distance < 200)
                    {
                        number = 6;
                    }
                    else if (distance < 300)
                    {
                        number = 6;
                    }
                    else if (distance < 400)
                    {
                        number = 6;
                    }
                    else if (distance < 500)
                    {
                        number = 4;
                    }
                    else if (distance < 600)
                    {
                        number = 4;
                    }
                    else if (distance < 700)
                    {
                        number = 4;
                    }




                    ((MyGame)game).box.gravity = -number;
                    //float distance = ((MyGame)game).box.DistanceTo(this);
                    //float value = 20000 * 1 / (distance * 20);
                    //Console.WriteLine(value);
                    //((MyGame)game).box.vel.y = -value;
                }
                else
                {
                    ((MyGame)game).box.gravity = 9.8f;
                }

                //bool isPresent = false;
                //foreach (GameObject gameObject in game.GetChildren())
                //{
                //    if (gameObject is Wind)
                //    {
                //        isPresent = true;
                //        break;
                //    }
                //}


                //    if (wait >= 40)
                //    {
                //        wait = 0;
                //        float widthFan = this.width / 1.5f;
                //        Wind rect = new Wind(this, 2f, this.x - widthFan / 2, this.y - this.height - this.height / 2, widthFan,10, 0);
                //        rect.vel.y = -100;
                //        ((MyGame)game).AddChild(rect);
                //        ((MyGame)game).mObjects.Add(rect);

                //        Wind rect2 = new Wind(this, 2f, this.x - widthFan + 4, this.y - this.height * 1.5f + 3, widthFan/2, 10, -0.5f);
                //         rect2.vel.y = rect.vel.y;
                //        ((MyGame)game).AddChild(rect2);
                //        ((MyGame)game).mObjects.Add(rect2);


                //        Wind rect3 = new Wind(this, 2f, this.x +  widthFan /2 - 4, this.y - this.height * 1.5f + 3, widthFan / 2, 10, 0.5f);
                //         rect3.vel.y = rect.vel.y;
                //        ((MyGame)game).AddChild(rect3);
                //        ((MyGame)game).mObjects.Add(rect3);
                //}
                //    else
                //    {
                //        wait++;
                //    }

                base.Update();
            }
        }
    }
}
