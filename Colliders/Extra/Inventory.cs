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
        Sprite spriteSpring, spriteFan, spriteBelt, spritePiston;
        MyGame gameVar;
        public Inventory()
        {


            spriteSpring  = new Sprite("invSpring.png");
            spriteSpring.SetOrigin(spriteSpring.width / 2, spriteSpring.height / 2);
            spriteSpring.x = 300;
            spriteSpring.y = 795;

            AddChild(spriteSpring);

            spriteFan = new Sprite("invFan.png");
            spriteFan.SetOrigin(spriteFan.width / 2, spriteFan.height / 2);
            spriteFan.x = 400;
            spriteFan.y = 795;
            AddChild(spriteFan);


            spriteBelt = new Sprite("invBelt.png");
            spriteBelt.SetOrigin(spriteBelt.width / 2, spriteBelt.height / 2);
            spriteBelt.x = 500;
            spriteBelt.y = 795;
            AddChild(spriteBelt);

            spritePiston = new Sprite("invPiston.png");
            spritePiston.SetOrigin(spritePiston.width / 2, spritePiston.height / 2);
            spritePiston.x = 600;
            spritePiston.y = 795;
            AddChild(spritePiston);

            gameVar = ((MyGame)game);
            //FanObject pistonObject = new FanObject(1, Input.mouseX, Input.mouseY, 50, 50, 0);
            //AddChild(pistonObject);
            //((MyGame)game).mObjects.Add(pistonObject);
        }

        void Update()
        {
            if (spriteSpring.DistanceTo(Input.mouseX, Input.mouseY) < spriteSpring.width/2 && Input.GetMouseButtonDown(0))
            {
                gameVar.hud.destroyPlacingObject();
                SpringObject springObject = new SpringObject(0.3f, Input.mouseX, Input.mouseY, 50, 50, 0, true);
                AddChild(springObject);

                ((MyGame)game).mObjects.Add(springObject);
            }

            else if (spriteFan.DistanceTo(Input.mouseX, Input.mouseY) < spriteFan.width/2 && Input.GetMouseButtonDown(0))
            {
                gameVar.hud.destroyPlacingObject();
                FanObject springObject = new FanObject(0.3f, Input.mouseX, Input.mouseY, 50, 50, 0, true);
                AddChild(springObject);

                ((MyGame)game).mObjects.Add(springObject);
            }


            else if (spriteBelt.DistanceTo(Input.mouseX, Input.mouseY) < spriteBelt.width/2 && Input.GetMouseButtonDown(0))
            {
                gameVar.hud.destroyPlacingObject();
                ConveyorBeltObject beltObject = new ConveyorBeltObject(0, Input.mouseX, Input.mouseY, 230, 45, 0, true);
                AddChild(beltObject);
                ((MyGame)game).mObjects.Add(beltObject);
            }

            else if (spritePiston.DistanceTo(Input.mouseX, Input.mouseY) < spritePiston.width/2 && Input.GetMouseButtonDown(0))
            {
                gameVar.hud.destroyPlacingObject();
                PistonObject pistonObject = new PistonObject(0, Input.mouseX, Input.mouseY, 70, 45, 0, true);
                AddChild(pistonObject);
                ((MyGame)game).mObjects.Add(pistonObject);
            }

            if (Input.GetKeyDown(Key.P))
            {
                SpringObject springObject = new SpringObject(0.3f, 200, 322, 50, 50, 0, true);
                AddChild(springObject);

                ((MyGame)game).mObjects.Add(springObject);
            }
        }
    }
}
