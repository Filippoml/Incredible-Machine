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
        Sound play;
        private bool pistonAnimation;
        float xCoord, yCoord;
        EasyDraw easyDraw;
        int numWidth;
        SubPiston test, test2;

        MyGame gameVar;
        public PistonObject(float mass, float x, float y, float wid, float hig, float angle, bool placing) : base(mass, x, y, wid, hig, angle, placing)
        {
            play = new Sound("Sounds/sound_piston.wav", false, true);

            gameVar = ((MyGame)game);

            numWidth = (int) this.width;
            test = new SubPiston(0, 0, 0, 50, 10, 0);
            test2 = new SubPiston(0, 0, 0, 10, 40, 0);
            gameVar.level.AddChild(test);
            gameVar.level.mObjects.Add(test);

            gameVar.level.AddChild(test2);
            gameVar.level.mObjects.Add(test2);

            //((MyGame)game).mObjects.Add(test2);
        }

        void Update()
        {
            
            if(gameVar.paused && pistonAnimation)
            {
                int speed = 10;
                maxXinit--;
                test.x -= speed;
                test.pos.x -= speed;

                test2.x -= speed;
                test2.pos.x -= speed;
                if(maxXinit <= 0)
                {
                    pistonAnimation = false;
                }
            }
            else if(maxXinit< maxXfin && pistonAnimation)
            {
                int speed = 10;
                maxXinit ++;
                test.x += speed;
                test.pos.x += speed;

                test2.x += speed;
                test2.pos.x += speed;

               
            }
            

            if (this.DistanceTo(Input.mouseX, Input.mouseY) < this.width / 2 && Input.GetMouseButtonDown(0) && !placing && !gameVar.level.hud.oneObjectPlacing && gameVar.paused)
            {
                test.placing = test2.placing = placing = true;
                gameVar.level.inventory.counter = 0;
            }
            else if (Input.GetMouseButtonDown(0) && canBePlaced && placing)
            {
                gameVar.level.numPistonObjects++;
                test.placing = test2.placing = placing = false;
                xCoord = x;
                yCoord = y;
            }
            else if (placing)
            {
                this.x = Input.mouseX;
                this.y = Input.mouseY;
                base.pos.x = Input.mouseX;
                base.pos.y = Input.mouseY;

                test.x = this.x;
                test2.x = this.x + (test.width /2 );

                test.pos.x = Input.mouseX + 100;
                test2.pos.x = Input.mouseX + 100;

                UpdateRectangleSize();
                test.UpdateRectangleSize();
                test2.UpdateRectangleSize();
            }
            else
            {
                if(Input.GetKeyDown(Key.C))
                {

                }
                if (gameVar.level.box != null)
                {
                    if (gameVar.level.box.x > this.x + this.width / 2 && Mathf.Abs(gameVar.level.box.y - this.y) < this.height && !gameVar.paused)
                    {
          
                        pistonAnimation = true;
                    }
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
     
            gameVar.level.mObjects.Remove(test);
            test.Destroy();

            gameVar.level.mObjects.Remove(test2);
            test2.Destroy();
        }
    }
}
