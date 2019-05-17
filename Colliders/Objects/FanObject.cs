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

        AnimationSprite wind;

        public FanObject(float mass, float x, float y, float wid, float hig, float angle, bool placing) : base(mass, x, y, wid, hig, angle, placing)
        {
            gameVar = ((MyGame)game);

      
        }

        void Update()
        {
            if (this.DistanceTo(Input.mouseX, Input.mouseY) < this.width / 2 && Input.GetMouseButtonDown(0) && !placing && !gameVar.level.hud.oneObjectPlacing && gameVar.paused)
            {
                gameVar.level.inventory.counter = 0;
                placing = true;
            }
            else if (Input.GetMouseButtonDown(0) && canBePlaced && placing)
            {
                gameVar.level.numFanObjects++;
                placing = false;
                xCoord = x;
                yCoord = y;
                wind = new AnimationSprite("wind.png", 30, 1);
                wind.alpha = 0.5f;
                wind.SetXY(-25, -400);
                AddChild(wind);
            }
            else if (placing)
            {
                if(wind != null)
                {
                    wind.Destroy();
                }
                this.x = Input.mouseX;
                this.y = Input.mouseY;
                base.pos.x = Input.mouseX;
                base.pos.y = Input.mouseY;
                UpdateRectangleSize();
            }
            else
            {

                if (wind != null)
                {
                    wind.NextFrame();
                }
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

                }
                else
                {
                    gameVar.level.box.gravity = 9.8f;
                }
                
                base.Update();
            }
        }
    }
}
