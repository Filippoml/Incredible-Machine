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
            if (this.DistanceTo(Input.mouseX, Input.mouseY) < this.width / 2 && Input.GetMouseButtonDown(0) && !placing && !gameVar.level.hud.oneObjectPlacing)
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
