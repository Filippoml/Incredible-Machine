using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace Colliders
{
    public class HUD : EasyDraw
    {
        Sprite playStatus, pauseStatus;
        Button deleteButton, playButton;
        public HUD() : base(MyGame.main.width, MyGame.main.height)
        {
            playButton = new Button(70, 800, "play.png");
            playButton.SetOrigin(playButton.width / 2, playButton.height / 2);
            AddChild(playButton);

            deleteButton = new Button(140, 800, "delete.png");
            deleteButton.SetOrigin(deleteButton.width / 2, deleteButton.height / 2);
            deleteButton.visible = false;
            AddChild(deleteButton);


            playStatus = new Sprite("play2.png");
            playStatus.visible = false;
            playStatus.SetScaleXY(0.5f);
            playStatus.SetXY(1420, 70);
            playStatus.SetOrigin(playStatus.width / 2, playStatus.height / 2);
            AddChild(playStatus);

            pauseStatus = new Sprite("pause2.png");
            pauseStatus.SetScaleXY(0.5f);
            pauseStatus.SetXY(1420, 70);
            pauseStatus.SetOrigin(pauseStatus.width / 2, pauseStatus.height / 2);
            AddChild(pauseStatus);
        }

        void Update()
        {

            if (playButton.DistanceTo(Input.mouseX, Input.mouseY) < playButton.width/2 && Input.GetMouseButtonDown(0))
            {
                ((MyGame)game).ResetGame();
                ((MyGame)game).time = 1f / 50f;
                ((MyGame)game).paused = false;
            }

            pauseStatus.visible = ((MyGame)game).paused;
            playStatus.visible = !((MyGame)game).paused;

            bool oneObjectPlacing = false; //Are you placing objects
            Rectangle objectPlacing = null;
            var gameVar = ((MyGame)game);
            foreach (Rectangle rect in gameVar.mObjects)
            {
                if (rect.placing)
                {
                    oneObjectPlacing = true;
                    objectPlacing = rect;
                    break;
                }
            }



            deleteButton.visible = oneObjectPlacing;

            if (deleteButton.visible)
            {
                if(Input.GetMouseButtonDown(1))
                {
                    if (deleteButton != null)
                    {
                        if (objectPlacing is PistonObject)
                        {
                            PistonObject pistonObject = objectPlacing as PistonObject;
                            pistonObject.DestroyChildren();
                        }
                        gameVar.mObjects.Remove(objectPlacing);
                        objectPlacing.Destroy();
                    }
                }
                else if (deleteButton.DistanceTo(Input.mouseX, Input.mouseY) < deleteButton.width/2 && Input.GetMouseButtonDown(0))
                {
                    if(deleteButton != null)
                    {
                        if(objectPlacing is PistonObject)
                        {
                            PistonObject pistonObject = objectPlacing as PistonObject;
                            pistonObject.DestroyChildren();
                        }
                        gameVar.mObjects.Remove(objectPlacing);
                        objectPlacing.Destroy();
                    }
                }
            }
        }

        
    }
}
