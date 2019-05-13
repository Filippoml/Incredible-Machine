﻿using System;
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

            gameVar = ((MyGame)game);



            spriteSpring  = new Sprite("invSpring.png");
            spriteSpring.SetOrigin(spriteSpring.width / 2, spriteSpring.height / 2);
            spriteSpring.x = 300;
            spriteSpring.y = 795;

            AddChild(spriteSpring);

            spriteFan = new Sprite("invFan.png");
            spriteBelt = new Sprite("invBelt.png");
            spritePiston = new Sprite("invPiston.png");
            if (gameVar.level.numLevel > 1)
            {
         
                spriteFan.SetOrigin(spriteFan.width / 2, spriteFan.height / 2);
                spriteFan.x = 400;
                spriteFan.y = 795;
                spriteFan.Mirror(false, true);
                AddChild(spriteFan);


            
                spriteBelt.SetOrigin(spriteBelt.width / 2, spriteBelt.height / 2);
                spriteBelt.x = 500;
                spriteBelt.y = 795;
                AddChild(spriteBelt);

             
                spritePiston.SetOrigin(spritePiston.width / 2, spritePiston.height / 2);
                spritePiston.x = 600;
                spritePiston.y = 795;
                AddChild(spritePiston);
            }
            //FanObject pistonObject = new FanObject(1, Input.mouseX, Input.mouseY, 50, 50, 0);
            //AddChild(pistonObject);
            //((MyGame)game).mObjects.Add(pistonObject);
        }

        void Update()
        {
            if (gameVar.level.numLevel == 1)
            {
                switch (gameVar.level.tutorialPoint)
                {
                    case 0:

                        if (spriteSpring.DistanceTo(Input.mouseX, Input.mouseY) < spriteSpring.width / 2 && Input.GetMouseButtonDown(0))
                        {
                            gameVar.level.arrow.x = 550;
                            gameVar.level.arrowY = gameVar.level.arrow.y = 300;

                            gameVar.level.tutorialPoint = 1;
                        }
                        break;
                    case 1:

                        if (gameVar.level.numSpringObjects == 1)
                        {
                            gameVar.level.arrow.x = 25;
                            gameVar.level.arrowY = gameVar.level.arrow.y = 610;

                            gameVar.level.tutorialPoint++;
                            break;
                        }


                        break;
                }

            }


            if (spriteSpring.DistanceTo(Input.mouseX, Input.mouseY) < spriteSpring.width/2 && Input.GetMouseButtonDown(0))
            {
                if (gameVar.level.numLevel == 1 )
                {
                    if (gameVar.level.numSpringObjects == 0)
                    {
                        gameVar.level.hud.destroyPlacingObject();
                        SpringObject springObject = new SpringObject(0.3f, Input.mouseX, Input.mouseY, 50, 50, 0, true);
                        AddChild(springObject);

                        gameVar.level.mObjects.Add(springObject);
                    }
                }
                else
                {
                    gameVar.level.hud.destroyPlacingObject();
                    SpringObject springObject = new SpringObject(0.3f, Input.mouseX, Input.mouseY, 50, 50, 0, true);
                    AddChild(springObject);

                    gameVar.level.mObjects.Add(springObject);
                }
            }
            
            else if (spriteFan.DistanceTo(Input.mouseX, Input.mouseY) < spriteFan.width/2 && Input.GetMouseButtonDown(0))
            {
                gameVar.level.hud.destroyPlacingObject();
                FanObject springObject = new FanObject(0.3f, Input.mouseX, Input.mouseY, 50, 50, 0, true);
                AddChild(springObject);

                gameVar.level.mObjects.Add(springObject);
            }


            else if (spriteBelt.DistanceTo(Input.mouseX, Input.mouseY) < spriteBelt.width/2 && Input.GetMouseButtonDown(0))
            {
                gameVar.level.hud.destroyPlacingObject();
                ConveyorBeltObject beltObject = new ConveyorBeltObject(0, Input.mouseX, Input.mouseY, 230, 45, 0, true);
                AddChild(beltObject);
                gameVar.level.mObjects.Add(beltObject);
            }

            else if (spritePiston.DistanceTo(Input.mouseX, Input.mouseY) < spritePiston.width/2 && Input.GetMouseButtonDown(0))
            {
                gameVar.level.hud.destroyPlacingObject();
                PistonObject pistonObject = new PistonObject(0, Input.mouseX, Input.mouseY, 70, 45, 0, true);
                AddChild(pistonObject);
                gameVar.level.mObjects.Add(pistonObject);
            }

            if (Input.GetKeyDown(Key.P))
            {
                SpringObject springObject = new SpringObject(0.3f, 200, 322, 50, 50, 0, true);
                AddChild(springObject);

                gameVar.level.mObjects.Add(springObject);
            }
        }
    }
}
