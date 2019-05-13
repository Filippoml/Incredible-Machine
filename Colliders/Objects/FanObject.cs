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
        float xCoord, yCoord;

        MyGame gameVar;

        public FanObject(float mass, float x, float y, float wid, float hig, float angle, bool placing) : base(mass, x, y, wid, hig, angle, placing)
        {
            gameVar = ((MyGame)game);
        }

        int wait = 200;
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
                if (Mathf.Abs(gameVar.level.box.x - this.x)< this.width)
                {
                    float distance = gameVar.level.box.DistanceTo(this);
                    float number = -9.8f;


                    if (distance < 200)
                    {
                        number = 15;
                    }
                    else if (distance < 300)
                    {
                        number = 10;
                    }
                    else if (distance < 400)
                    {
                        number = 2;
                    }




                    gameVar.level.box.gravity = -number;
                    //float distance = ((MyGame)game).box.DistanceTo(this);
                    //float value = 20000 * 1 / (distance * 20);
                    //Console.WriteLine(value);
                    //((MyGame)game).box.vel.y = -value;
                }
                else
                {
                    gameVar.level.box.gravity = 9.8f;
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
