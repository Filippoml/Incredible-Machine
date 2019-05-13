using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
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

        private EasyDraw _easyDraw;
        private Font _font;
        bool opacityPlus;

        public float arrowY;
        public Sprite arrow;
        float animCounter;
        public List <Rectangle> mObjects;
        private bool paused;
        MyGame gameVar;
        public Level()
        {
            gameVar = ((MyGame)game);

            mObjects = new List<Rectangle>();

            paused = gameVar.paused;

           
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
                    LoadLevel2();
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }

        void LoadLevel0() {
            _easyDraw = new EasyDraw(game.width, game.height);
            AddChild(_easyDraw);

            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("font.ttf");
            _font = new Font(new FontFamily(pfc.Families[0].Name), 25, FontStyle.Regular);
            _easyDraw.graphics.DrawString("Waiting tick", _font, new SolidBrush(Color.White), new PointF((game.width / 2) - 300, game.height - 350));
        }

        void LoadLevel1()
        {
            //Background
            System.Drawing.Bitmap Bmp = new System.Drawing.Bitmap(game.width, game.height);
            System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(Bmp);
            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(135, 206, 235));
            gfx.FillRectangle(brush, 0, 0, game.width, game.height);
            Sprite _background = new Sprite(Bmp);
            AddChild(_background);

            Floor floor1 = new Floor(100, 200, 300, 15, "", 0.1f);
            mObjects.Add(floor1);


            Floor floor2 = new Floor(550, 500, 400, 15, "", 0);
            mObjects.Add(floor2);

            Floor floor3 = new Floor(850, 300, 200, 15, "", 0);
            mObjects.Add(floor3);

            robot = new Robot(1100, 250);
            robot.SetScaleXY(0.8f);
            AddChild(robot);

            Inventory inventory = new Inventory();
            AddChild(inventory);

            hud = new HUD();
            AddChild(hud);

            arrow = new Sprite("arrow.png");
            arrow.SetXY(300 - arrow.width/2, 600);
 
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

        void LoadLevel2()
        {
            System.Drawing.Bitmap Bmp = new System.Drawing.Bitmap(game.width, game.height);
            System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(Bmp);
            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(135, 206, 235));
            gfx.FillRectangle(brush, 0, 0, game.width, game.height);
            Sprite _background = new Sprite(Bmp);
            AddChild(_background);

            Floor floor1 = new Floor(100, 200, 500, 15, "", 0.1f);
            mObjects.Add(floor1);


            Floor floor2 = new Floor(550, 500, 400, 15, "", 0);
            mObjects.Add(floor2);

            Floor floor3 = new Floor(850, 300, 200, 15, "", 0);
            mObjects.Add(floor3);

            robot = new Robot(1100, 250);
            robot.SetScaleXY(0.8f);
            AddChild(robot);

            Inventory inventory = new Inventory();
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
            box = new Box(1, 100, 122, 50, 50, 0);

            AddChild(box);
            mObjects.Add(box);
        }
        public void Update()
        {
            if(numLevel == 0)
            {
                if(Input.GetKeyDown(Key.RIGHT))
                {
                    gameVar.NextLevel();
                }

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




                    if (rigidBodyA.mass != 0 || rigidBodyB.mass != 0)
                    {

                        var aabb = new AABB();



                        if (aabb.overlap(rigidBodyA.motionBounds, rigidBodyB.motionBounds))
                        {
                            if (paused)
                            {
                                if (objectinPlacing != null)
                                {
                                    objectinPlacing.canBePlaced = false;
                                }
                            }

                            if (rigidBodyA is SubPiston && rigidBodyB is Box || rigidBodyB is SubPiston && rigidBodyA is Box)
                            {
                                if (paused == true)
                                {
                                    continue;
                                }
                            }


                            Geometry geometry = new Geometry();


                            Contact[] _contacts = geometry.rectRectClosestPoints(rigidBodyA, rigidBodyB);



                            contacts2 = contacts2.Union(_contacts.ToArray()).ToList();
                            colliding = true;

                        }


                    }
                }
                if (rigidBodyA.width == 50)
                {
                    if (colliding == false)
                    {
                        rigidBodyA.angularVel = 0;

                    }

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
    }
}
