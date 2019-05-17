using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Colliders.Objects;
using GXPEngine;
using GXPEngine.OpenGL;

namespace Colliders.Extra
{
    public class Level : GameObject
    {

        public int numLevel;
        public Box box;
        public Robot robot;
        public HUD hud;

        public int tutorialPoint;

        public int numSpringObjects;
        public int numPlankObjects;
        public int numPistonObjects;
        public int numFanObjects;
        public int numBeltObjects;


        private EasyDraw _easyDraw;
        private Font _font;
        bool opacityPlus;

        public float arrowY;
        public Sprite arrow;
        float animCounter;

        Floor floatingFloor1, floatingFloor2;
        public List <Rectangle> mObjects;
        MyGame gameVar;
        AnimationSprite explosion, die, intro;
        public Inventory inventory;
        Sound boom, won;
        public Level()
        {
            gameVar = ((MyGame)game);

            mObjects = new List<Rectangle>();
            nextLevel = new AnimationSprite("youWon.png", 5, 6);
            explosion = new AnimationSprite("explosion.png", 10, 3);
            explosion.SetOrigin(explosion.width / 2, explosion.height / 2);
            
            boom = new Sound("Sounds/boom.wav", false, true);

            won = new Sound("Sounds/sound_win.wav", false, true);


            die = new AnimationSprite("robotDie.png", 10, 2);
            //die.SetOrigin(die.width / 2, die.height / 2);
            die.SetScaleXY(0.5f);
        }


        public void LoadLevel(int level)
        {
            numLevel = level;
            switch (level)
            {
                case 0:
                    LoadLevel0();
                    break;
                case 1:
                    LoadLevel1();
                    break;
                case 2:
                    LoadLevel4();
                    break;
                case 3:
                    LoadLevel3();
                    break;
                case 4:
                    LoadLevel0();
                    break;
            }
        }

        void LoadLevel0() {
            intro = new AnimationSprite("intro4.png", 8, 9);
            intro.SetXY(game.width / 2 - intro.width/1.5f - 50, game.height / 2 - intro.height / 1.5f);
            //intro.x = 1000;
            //intro.y = 400;
            AddChild(intro);
            //_easyDraw = new EasyDraw(game.width, game.height);
            //AddChild(_easyDraw);

            //PrivateFontCollection pfc = new PrivateFontCollection();
            //pfc.AddFontFile("font.ttf");
            //_font = new Font(new FontFamily(pfc.Families[0].Name), 25, FontStyle.Regular);
            //_easyDraw.graphics.DrawString("Waiting tick", _font, new SolidBrush(Color.White), new PointF((game.width / 2) - 300, game.height - 350));
           

        }

        void LoadLevel1()
        {
            //Background
            System.Drawing.Bitmap Bmp = new System.Drawing.Bitmap(game.width, game.height);
            System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(Bmp);
            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(135, 206, 235));
            gfx.FillRectangle(brush, 0, 0, game.width, game.height);
            Sprite _background = new Sprite("Levels/Level1.png");
            _background.x = -10;
            _background.SetScaleXY(0.82f);
            AddChild(_background);

            Floor floor1 = new Floor(100, 200, 300, 15, "", 0.1f);
            mObjects.Add(floor1);


            Floor floor2 = new Floor(550, 500, 400, 15, "", 0);
            mObjects.Add(floor2);

            Floor floor3 = new Floor(850, 300, 200, 15, "", 0);
            mObjects.Add(floor3);

            robot = new Robot(1120, 250);
            robot.SetScaleXY(0.5f);
            Sprite building = new Sprite("building.png");
            building.y = 537;
            building.x = 1020;
            AddChild(building);
            AddChild(robot);

            inventory = new Inventory();
            AddChild(inventory);

            hud = new HUD();
            AddChild(hud);

            arrow = new Sprite("arrow.png");
            arrow.SetXY(560 - arrow.width/2, 600);
 
            arrowY = 600;
            AddChild(arrow);


            foreach (Rectangle rectangle in mObjects)
            {
                if (!this.HasChild(rectangle))
                {
                    AddChild(rectangle);
                }
            }

            add();
        }
        


        void LoadLevel3()
        {
            System.Drawing.Bitmap Bmp = new System.Drawing.Bitmap(game.width, game.height);
            System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(Bmp);
            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(135, 206, 235));
            gfx.FillRectangle(brush, 0, 0, game.width, game.height);
            Sprite _background = new Sprite(Bmp);
            AddChild(_background);

            Floor floor1 = new Floor(50, 400, 20, 300, "", 0);
            mObjects.Add(floor1);


            Floor floor2 = new Floor(350, 50, 20, 250, "", 0);
            mObjects.Add(floor2);

            Floor floor3 = new Floor(550, 400, 20, 250, "", 0);
            mObjects.Add(floor3);

            Floor floor4 = new Floor(370, 50, 300, 20, "", 0);
            mObjects.Add(floor4);

            Floor floor5 = new Floor(670, 50, 20, 250, "", 0);
            mObjects.Add(floor5);



            robot = new Robot(1120, 250);
            robot.SetScaleXY(0.5f);
            Sprite building = new Sprite("building.png");
            building.y = 537;
            building.x = 1020;
            AddChild(building);
            AddChild(robot);

            inventory = new Inventory();
            AddChild(inventory);

            hud = new HUD();
            AddChild(hud);



            foreach (Rectangle rectangle in mObjects)
            {
                if (!this.HasChild(rectangle))
                {
                    AddChild(rectangle);
                }
            }

            add();
        }
        Contact test = null;
        void LoadLevel4()
        {
            System.Drawing.Bitmap Bmp = new System.Drawing.Bitmap(game.width, game.height);
            System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(Bmp);
            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(135, 206, 235));
            gfx.FillRectangle(brush, 0, 0, game.width, game.height);
            Sprite _background = new Sprite(Bmp);
            AddChild(_background);

            floatingFloor1 = new Floor(900, 50, 15, 300, "", 0);
            mObjects.Add(floatingFloor1);


            floatingFloor2 = new Floor(550, 400, 15, 300, "", 0);
            mObjects.Add(floatingFloor2);




            robot = new Robot(1120, 250);
            robot.SetScaleXY(0.5f);
            Sprite building = new Sprite("building.png");
            building.y = 537;
            building.x = 1020;
            AddChild(building);
            AddChild(robot);

            inventory = new Inventory();
            AddChild(inventory);

            hud = new HUD();
            AddChild(hud);



            foreach (Rectangle rectangle in mObjects)
            {
                if (!this.HasChild(rectangle))
                {
                    AddChild(rectangle);
                }
            }

            add();
        }



        private void add()
        {
            box = new Box(1, 100, 122, 44, 60, 0);
            //box = new Box(1, 520, 122, 44, 60, 0);

            AddChild(box);
            mObjects.Add(box);
        }
     
        float counter = 0;
        AnimationSprite nextLevel;
        bool wait, animate = false;
        int wait2;
        public void Update()
        {

            if(intro != null)
            {
                if(wait2 == 3)
                {
                    intro.NextFrame();
                    wait2 = 0;
                }
                else
                {
                    wait2++;
                }
               
            }

            if(this.GetChildren().Contains(die))
            {
                if(explosion.currentFrame < 29)
                if(wait)
                {
                    wait = false;
                }
                else
                {
                    wait = true;
                    explosion.NextFrame();
         
                }

                if (explosion.currentFrame > 28)
                {
                    explosion.Destroy();
                    
                }
                if (explosion.currentFrame > 20 && die.currentFrame < 18)
                {

                    if (wait)
                    {
                        wait = false;
                    }
                    else
                    {
                        wait = true;
                        die.NextFrame();

                    }


                }

                if (die.currentFrame > 17 && nextLevel.scale == 1 && counter != 1)
                {
                    counter = 1;

          
                    nextLevel.SetOrigin(nextLevel.width / 2, nextLevel.height / 2);
                    nextLevel.SetXY(game.width / 2 - nextLevel.width/2, game.height / 2 - nextLevel.height/2.5f);
                    nextLevel.SetScaleXY(1);
                    AddChild(nextLevel);
                    won.Play();
                    animate = true;
              
                }


            }
            //if (counter > 0)
            //{
            //    if(counter < 100)
            //    {
            //        nextLevel.SetScaleXY(counter / 100f);
            //    }

            //    if (counter > 400)
            //    {


            //        animate = true;
            //        counter = 0;

            //    }
            //    counter += 2.5f;
            //}

            if (animate)
            {
                
                if (wait)
                {
                    wait = false;
                }
                else
                {
                    wait = true;
                    nextLevel.NextFrame();

                }
                if (nextLevel.currentFrame > 28)
                {
                    gameVar.NextLevel();
                }
            }
            if (numLevel == 0)
            {
                if (_easyDraw != null)
                {
                    if (opacityPlus)
                    {
                        _easyDraw.alpha += 0.01f;
                        if (_easyDraw.alpha >= 1)
                        {
                            opacityPlus = !opacityPlus;
                            _easyDraw.alpha = 1;
                        }

                    }
                    else
                    {
                        _easyDraw.alpha -= 0.01f;
                        if (_easyDraw.alpha <= 0)
                        {
                            opacityPlus = !opacityPlus;
                            _easyDraw.alpha = 0;
                        }
                    }
                }
            }
            //if (Input.GetMouseButton(1))
            //{
            //    test.x++;
            //    test.pos.x++;
            //}
            if (arrow != null)
            {
                animCounter += 0.1f;
                arrow.y = arrowY + 20 * Mathf.Sin(animCounter / 2);
            }
            if (floatingFloor1 != null)
            {
                animCounter += 0.1f;
                floatingFloor1.y = 250 + 50 * Mathf.Sin(animCounter / 6);
                floatingFloor2.y = 500 - 50 * Mathf.Sin(animCounter / 6);

                floatingFloor1.pos.y = floatingFloor1.y;
                floatingFloor2.pos.y = floatingFloor2.y;

                floatingFloor1.UpdateRectangleSize();
                floatingFloor2.UpdateRectangleSize();
            }

            if (Input.GetKeyDown(Key.A))
            {
                gameVar.time = 1f / 50f;
                gameVar.paused = false;
                //ArduinoInterface.SendString("test");
            }

            if (Input.GetKeyDown(Key.R))
            {

                add();
            }



            Contact[] contacts = collide();


            solver(contacts);




        }



        public Contact[] collide()
        {
            foreach (Rectangle rect in mObjects)
            {
                rect.canBePlaced = true;
            }

            List<Contact> contacts2 = new List<Contact>();

            
            foreach(Rectangle rect in mObjects)
            {
                if (rect is SpringObject)
                {
                    if (rect.animationSprite != null)
                    {
                        rect.animationSprite.SetColor(0, 0, 0);
                    }
                    if (rect.sprite != null)
                    {
                        rect.sprite.SetColor(0, 0, 0);
                    }
                }
                else
                {
                    if (rect.animationSprite != null)
                    {
                        rect.animationSprite.SetColor(1, 1, 1);
                    }
                    if (rect.sprite != null)
                    {
                        rect.sprite.SetColor(1, 1, 1);
                    }
                }
                rect.collisions.Clear();
            }

            Rectangle box2 = null, belt2 = null, floor2 = null;
            for (var ii = 0; ii < mObjects.Count; ii++)
            {
                var rigidBodyA = mObjects[ii];
                bool colliding = false;
                for (var jj = ii + 1; jj < mObjects.Count; jj++)
                {
                    Rectangle rigidBodyB = mObjects[jj];
                    Rectangle objectinPlacing = null;
                    if (rigidBodyA.placing)
                    {
                        objectinPlacing = rigidBodyA;
                    }
                    else if (rigidBodyB.placing)
                    {
                        objectinPlacing = rigidBodyB;
                    }

                    if (rigidBodyA is SubPiston && rigidBodyB is Box || rigidBodyB is SubPiston && rigidBodyA is Box)
                    {

                    }
                    else
                    {
                        if (rigidBodyA is SubPiston && rigidBodyB is SubPiston)
                        {
                            continue;
                        }


                        if (rigidBodyA is SubPiston && rigidBodyB is PistonObject)
                        {
                            continue;
                        }

                        if (rigidBodyB is SubPiston && rigidBodyA is PistonObject)
                        {
                            continue;
                        }
                    }




                    if ((rigidBodyA.mass != 0 || rigidBodyB.mass != 0) || gameVar.paused)
                    {

                        var aabb = new AABB();


                        if (aabb.overlap(rigidBodyA.motionBounds, rigidBodyB.motionBounds))
                        {

                            if (rigidBodyA is Box)
                            {
                                box2 = rigidBodyA;

                                if (rigidBodyB is ConveyorBeltObject)
                                {
                                    belt2 = rigidBodyB;

                                }
                                else if (rigidBodyB is Floor)
                                {
                                    floor2 = rigidBodyB;
                                }
                                rigidBodyA.collisions.Add(rigidBodyB);
                            }
                            if (gameVar.paused)
                            {
                                if (objectinPlacing != null)
                                {
                                    objectinPlacing.canBePlaced = false;
                                }
                            }

                            if (rigidBodyA is SubPiston && rigidBodyB is Box || rigidBodyB is SubPiston && rigidBodyA is Box)
                            {
                                if (gameVar.paused == true)
                                {
                                    continue;
                                }
                            }


                            if (rigidBodyA.placing)
                            {
                                if (rigidBodyA.animationSprite != null)
                                {
                                    rigidBodyA.animationSprite.SetColor(1, 0, 0);
                                }
                                if (rigidBodyA.sprite != null)
                                {
                                    rigidBodyA.sprite.SetColor(1, 0, 0);
                                }
                            }


                            if (rigidBodyB.placing)
                            {
                                if (rigidBodyB.animationSprite != null)
                                {
                                    rigidBodyB.animationSprite.SetColor(1, 0, 0);
                                }
                                if (rigidBodyB.sprite != null)
                                {
                                    rigidBodyB.sprite.SetColor(1, 0, 0);
                                }
                            }

                            Geometry geometry = new Geometry();


                            Contact[] _contacts = geometry.rectRectClosestPoints(rigidBodyA, rigidBodyB);



                            contacts2 = contacts2.Union(_contacts.ToArray()).ToList();
                            colliding = true;

                        }

                    }
                }
                if (rigidBodyA is Box)
                {
                    if (colliding == false)
                    {
                        rigidBodyA.angularVel = 0;

                    }

                }
                bool boolean1 = false, boolean2 = false;

                foreach (Contact contact in contacts2)
                {
                    if (contact.A is Box && contact.B is ConveyorBeltObject || contact.B is Box && contact.A is ConveyorBeltObject)
                    {
                        boolean1 = true;
                        test = contact;
                    }
                    if (contact.A is Box && contact.B is Floor || contact.B is Box && contact.A is Floor)
                    {
                        boolean2 = true;
                    }
                }

                if(boolean1 && boolean2)
                {
                    test.prova = true;
                    //contacts2.Remove(test);
                    //box.vel.x = 0;
                }
            }



            return contacts2.ToArray();
        }


        private void solver(Contact[] contacts)
        {
            var numInteraction = 2;
            for (var jj = 0; jj < numInteraction; jj++)
            {

                for (var ii = 0; ii < contacts.Length; ii++)
                {
                    Contact con = contacts[ii];
                    Vec2 n = con.normal;
                    Vec2 a = con.getVelPb();
                    Vec2 b = con.getVelPa();
                    Vec2 c = a.subtract(b);
                    float relNv = c.dotProduct(n);



                    speculativeSolver(con, n, relNv);

                }
            }

        }

        private void speculativeSolver(Contact con, Vec2 n, float relNv)
        {

            float remove = relNv + con.Dist / ((MyGame)game).time;

            if (remove < 0)
            {
                float mag = remove * con.invDenom;
                Vec2 imp = n.copy().multiply(mag);



                con.applyImpulses(imp);

            }
        }

        public void ResetGame()
        {
            gameVar.paused = true;
            box.Destroy();
            mObjects.Remove(box);

            add();

         
        }
        
        public void ShowNextLevelButton()
        {
            boom.Play();
            explosion.x = robot.x;
            explosion.y = robot.y;
            AddChild(explosion);

            robot.visible = false;

            die.x = robot.x;
            die.y = robot.y;
            AddChild(die);
        }

    }
}
