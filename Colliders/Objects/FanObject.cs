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

        public FanObject(float mass, float x, float y, float wid, float hig, float angle) : base(mass, x, y, wid, hig, angle)
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
                //if(Mathf.Abs(((MyGame)game).box.x) - this.x < 100)
                //{
                //    float distance = ((MyGame)game).box.DistanceTo(this);
                //    float value = 20000 * 1 / (distance * 20);
                //    Console.WriteLine(value); 
                //    ((MyGame)game).box.vel.y = -value;
                //}

                bool isPresent = false;
                foreach (GameObject gameObject in game.GetChildren())
                {
                    if (gameObject is Wind)
                    {
                        isPresent = true;
                        break;
                    }
                }


                    if (wait >= 30)
                    {
                        wait = 0;
                        float widthFan = this.width * 2;
                        Wind rect = new Wind(this, 2f, this.x - widthFan / 2, this.y - this.height - this.height / 2, widthFan,10, 0);
                        rect.vel.y = -250;
                        ((MyGame)game).AddChild(rect);
                        ((MyGame)game).mObjects.Add(rect);

                        Wind rect2 = new Wind(this, 2f, this.x - widthFan * 1.42f + 2, this.y - this.height - 2, widthFan, 10, -0.5f);
                        rect2.vel.y = -250;
                        ((MyGame)game).AddChild(rect2);
                        ((MyGame)game).mObjects.Add(rect2);
                }
                    else
                    {
                        wait++;
                    }
                
                base.Update();
            }
        }
    }
}
