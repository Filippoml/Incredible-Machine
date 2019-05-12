using GXPEngine;
using System.Collections.Generic;
using System;
using Colliders;
using System.Threading.Tasks;
using System.Linq;
using GXPEngine.OpenGL;
using Colliders.Objects;

class MyGame : Game {

    public List<Rectangle> mObjects;

    public float time = 1f / 30f;
    public bool paused = true;
    int frame;
    Floor floor;
    public Robot robot;
    public HUD hud;
    Rectangle test;
    public MyGame() : base(1920,1080,true,true) {

        //Fan fan = new Fan();
        //fan.x = 100;
        //fan.y = 300;
        //AddChild(fan);

        ShowMouse(true);
        System.Drawing.Bitmap Bmp = new System.Drawing.Bitmap(width, height);
        System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(Bmp);
        System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(135, 206, 235));
        gfx.FillRectangle(brush, 0, 0, width, height);

        Sprite _background = new Sprite(Bmp);
        AddChild(_background);

        mObjects = new List<Rectangle>();


        Floor floor1 = new Floor(100, 200, 100, 15, "", 0.1f);
        mObjects.Add(floor1);


        Floor floor2 = new Floor(200, 500, 400, 15, "", 0);
        mObjects.Add(floor2);

        Floor floor3 = new Floor(600, 300, 200, 15, "", 0);
        mObjects.Add(floor3);

        //test = new Rectangle(0, 200, 160, 50, 10, 0);
        //mObjects.Add(test);

        // robot = new Robot(800, 250);
        //robot.SetScaleXY(0.8f);
        //AddChild(robot);

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
        time = 1f / 50f;
    }
    public Box box;
    private void add()
    {
        box = new Box(1, 100, 122, 50, 50,0);

        AddChild(box);
        mObjects.Add(box);
    }
    public void Update()
    {
        //if (Input.GetMouseButton(1))
        //{
        //    test.x++;
        //    test.pos.x++;
        //}

            if (Input.GetKeyDown(Key.ESCAPE))
        {
            GL.glfwCloseWindow();
            GL.glfwTerminate();
            System.Environment.Exit(0);
        }
        if(Input.GetKeyDown(Key.A))
        {
            ((MyGame)game).time = 1f / 50f;
            ((MyGame)game).paused = false;
            //ArduinoInterface.SendString("test");
        }
        //time = Input.GetKey(Key.SPACE) ? 1f / 10000f : 1/ 30f;
        if (Input.GetMouseButtonDown(0))
        {
            //time = 1f / 70f;
            //Box box = new Box( 10, Input.mouseX, Input.mouseY, 50, 50, 0);

            //mObjects.Add(box);

            //AddChild(box);

        }

        if (Input.GetKeyDown(Key.R))
        {

            add();
        }

        //floor.loopMovement(time);

        Contact[] contacts = collide();
        
  
        solver(contacts);



        
    }



    public Contact[] collide()
    {
        foreach(Rectangle rect in mObjects)
        {
            rect.canBePlaced = true;
        }

        List<Contact> contacts2 = new List<Contact>();

        for (var ii = 0; ii < mObjects.Count ; ii++)
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
                            if(paused == true)
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
        box.Destroy();
        mObjects.Remove(box);
        add();
    }


    public static void Main (string[] args)
	{
		new MyGame ().Start ();
	}
}
