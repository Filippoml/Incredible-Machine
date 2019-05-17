using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace Colliders.Objects
{
    class PlankObject : Rectangle
    {
        float xCoord, yCoord;

        MyGame gameVar;

        public PlankObject(float mass, float x, float y, float wid, float hig, float angle, bool plancing) : base(mass, x, y, wid, hig, angle, plancing)
        {

            gameVar = ((MyGame)game);
        }


        void Update()
        {
            if (rectRect(this.x, this.y, this.width, this.height) && Input.GetMouseButtonDown(0) && !placing && !gameVar.level.hud.oneObjectPlacing && gameVar.paused)
            {
                gameVar.level.inventory.counter = 0;

                placing = true;
            }
            else if (Input.GetMouseButtonDown(0) && canBePlaced && placing)
            {
                gameVar.level.numPlankObjects++;
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


        bool rectRect(float r2x, float r2y, float r2w, float r2h)
        {
            float r1x = Input.mouseX + this.width / 2;
            float r1y = Input.mouseY;
            float r1w = 10;
            float r1h = 10;
            // are the sides of one rectangle touching the other?

            if (r1x + r1w >= r2x &&    // r1 right edge past r2 left
                r1x <= r2x + r2w &&    // r1 left edge past r2 right
                r1y + r1h >= r2y &&    // r1 top edge past r2 bottom
                r1y <= r2y + r2h)
            {    // r1 bottom edge past r2 top
                return true;
            }
            return false;
        }
    }
}
