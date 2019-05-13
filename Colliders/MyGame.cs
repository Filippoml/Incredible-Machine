using GXPEngine;
using System.Collections.Generic;
using System;
using Colliders;
using System.Threading.Tasks;
using System.Linq;
using GXPEngine.OpenGL;
using Colliders.Objects;
using Colliders.Extra;

class MyGame : Game {


    public bool nextLevel;
    public float time = 1f / 30f;
    public bool paused = true;
    public Level level;

    int numLevel = 0;

    public MyGame() : base(1920,1080,false,true) {

        level = new Level();
        level.LoadLevel(0);
        AddChild(level);

        time = 1f / 50f;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(Key.ESCAPE))
        {
            GL.glfwCloseWindow();
            GL.glfwTerminate();
            System.Environment.Exit(0);
        }

        if(Input.GetKeyDown(Key.RIGHT) && nextLevel)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        paused = true;
        nextLevel = false;
        ShowMouse(true);
        level.Destroy();
        level = new Level();
        AddChild(level);


        numLevel++;
        level.LoadLevel(numLevel);
    }

    public static void Main (string[] args)
	{
		new MyGame ().Start ();
	}
}
