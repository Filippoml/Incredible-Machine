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



    public float time = 1f / 30f;
    public bool paused = true;
    public Level level;

    int numLevel = 0;

    public MyGame() : base(1920,1080,true,true) {

        //Sound backgroundSound = new Sound("Sounds/sound_incredible_machine.wav", true, false);
        //backgroundSound.Play();

        level = new Level();
        level.LoadLevel(numLevel);
        AddChild(level);

        time = 1f / 50f;
        ShowMouse(true);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(Key.ESCAPE))
        {
            GL.glfwCloseWindow();
            GL.glfwTerminate();
            System.Environment.Exit(0);
        }
        if (numLevel == 0 && Input.GetKey(Key.ZERO) )
        {
    

            NextLevel();

        }
    }

    public void NextLevel()
    {
        paused = true;

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
