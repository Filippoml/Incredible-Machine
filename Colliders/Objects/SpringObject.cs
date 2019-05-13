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
        MyGame gameVar;
        float xCoord, yCoord;

        public SpringObject(float mass, float x, float y, float wid, float hig, float angle, bool placing) : base(mass, x, y, wid, hig, angle, placing)
        {
            gameVar = ((MyGame)game);


        }

        void Update()
        {
            Console.WriteLine(Input.mouseX + "  " + Input.mouseY);
            if (this.DistanceTo(Input.mouseX, Input.mouseY) < this.width / 2 && Input.GetMouseButtonDown(0) && !placing && gameVar.level.numLevel > 1)
            {
                gameVar.level.numSpringObjects--;
                placing = true;
            }
            else if (Input.GetMouseButtonDown(0) && canBePlaced)
            {
                if(gameVar.level.numLevel == 1)
                {
                    if(this.DistanceTo(593,471) < 50)
                    {
                        gameVar.level.numSpringObjects++;
                        placing = false;
                        xCoord = x;
                        yCoord = y;

                    }
                }
                else
                {
                    gameVar.level.numSpringObjects++;
                    placing = false;
                    xCoord = x;
                    yCoord = y;
                }

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
