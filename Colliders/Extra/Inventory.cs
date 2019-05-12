using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colliders.Objects;
using GXPEngine;

namespace Colliders
{
    public class Inventory : GameObject
    {
        Sprite spriteSpring, spriteFan, spriteBelt;
        public Inventory()
        {


            spriteSpring  = new Sprite("invSpring.png");
            spriteSpring.x = 300;
            spriteSpring.y = 795;
            AddChild(spriteSpring);

            spriteFan = new Sprite("invFan.png");
            spriteFan.x = 400;
            spriteFan.y = 795;
            AddChild(spriteFan);


            spriteBelt = new Sprite("invBelt.png");
            spriteBelt.x = 500;
            spriteBelt.y = 795;
            AddChild(spriteBelt);

            //FanObject pistonObject = new FanObject(1, Input.mouseX, Input.mouseY, 50, 50, 0);
            //AddChild(pistonObject);
            //((MyGame)game).mObjects.Add(pistonObject);
        }

        void Update()
        {
            if (spriteSpring.DistanceTo(Input.mouseX, Input.mouseY) < spriteSpring.width && Input.GetMouseButtonDown(0))
            {
                SpringObject springObject = new SpringObject(0.3f, Input.mouseX, Input.mouseY, 50, 50, 0);
                AddChild(springObject);

                ((MyGame)game).mObjects.Add(springObject);
            }

            else if (spriteFan.DistanceTo(Input.mouseX, Input.mouseY) < spriteFan.width && Input.GetMouseButtonDown(0))
            {
                FanObject springObject = new FanObject(0.3f, Input.mouseX, Input.mouseY, 50, 50, 0);
                AddChild(springObject);

                ((MyGame)game).mObjects.Add(springObject);
            }


            else if (spriteBelt.DistanceTo(Input.mouseX, Input.mouseY) < spriteBelt.width && Input.GetMouseButtonDown(0))
            {
                ConveyorBeltObject beltObject = new ConveyorBeltObject(0, Input.mouseX, Input.mouseY, 230, 45, 0);
                AddChild(beltObject);
                ((MyGame)game).mObjects.Add(beltObject);
            }

            if (Input.GetKeyDown(Key.P))
            {
                SpringObject springObject = new SpringObject(0.3f, 200, 322, 50, 50, 0);
                AddChild(springObject);

                ((MyGame)game).mObjects.Add(springObject);
            }
        }
    }
}
