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
        Sprite spriteSpring, spriteFan;
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

            FanObject pistonObject = new FanObject(1, Input.mouseX, Input.mouseY, 50, 50, 0);
            AddChild(pistonObject);
            ((MyGame)game).mObjects.Add(pistonObject);
        }

        void Update()
        {
            if (spriteFan.DistanceTo(Input.mouseX, Input.mouseY) < spriteFan.width && Input.GetMouseButtonDown(0))
            {
                FanObject springObject = new FanObject(0.3f, Input.mouseX, Input.mouseY, 50, 50, 0);
                AddChild(springObject);

                ((MyGame)game).mObjects.Add(springObject);
            }


            if (spriteSpring.DistanceTo(Input.mouseX, Input.mouseY) < spriteSpring.width && Input.GetMouseButtonDown(0))
            {
                SpringObject springObject = new SpringObject(0.3f, Input.mouseX, Input.mouseY, 50, 50, 0);
                AddChild(springObject);

                ((MyGame)game).mObjects.Add(springObject);
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
