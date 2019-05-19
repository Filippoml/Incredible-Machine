using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colliders.Objects;
using GXPEngine;

namespace Colliders
{
    public class HUD : EasyDraw
    {
        Sprite playStatus, pauseStatus;
        Button deleteButton, playButton, pauseButton;
        Rectangle objectPlacing;
        MyGame gameVar;
        public bool oneObjectPlacing;
        public HUD() : base(MyGame.main.width, MyGame.main.height)
        {
            gameVar = ((MyGame)game);
            playButton = new Button(70, 800, "play.png");
            playButton.SetOrigin(playButton.width / 2, playButton.height / 2);
            playButton.width = playButton.height = 70;

            AddChild(playButton);

            pauseButton = new Button(70, 800, "pause.png");
            pauseButton.SetOrigin(pauseButton.width / 2, pauseButton.height / 2);
            pauseButton.width = pauseButton.height = 70;
            pauseButton.visible = false;
            AddChild(pauseButton);

            deleteButton = new Button(160, 800, "delete.png");
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

            if (gameVar.paused)
            {
                pauseButton.visible = false;
                playButton.visible = true;

                if (playButton.DistanceTo(Input.mouseX, Input.mouseY) < playButton.width / 2 && Input.GetMouseButtonDown(0))
                {
                    if (gameVar.level.numLevel == 1)
                    {
                        if (gameVar.level.tutorialPoint == 2)
                        {
                            gameVar.level.arrow.visible = false;
                        }
                    }
                    gameVar.paused = false;
                }
            }
            else
            {
                pauseButton.visible = true;
                playButton.visible = false;

                if (pauseButton.DistanceTo(Input.mouseX, Input.mouseY) < pauseButton.width / 2 && Input.GetMouseButtonDown(0))
                {
                    gameVar.level.ResetGame();
       
                }
            }

            pauseStatus.visible = gameVar.paused;
            playStatus.visible = !gameVar.paused;

            oneObjectPlacing = false; //Are you placing objects
            objectPlacing = null;
  

            
            getPlacingObject();

            deleteButton.visible = oneObjectPlacing;

            if (deleteButton.visible)
            {
                if (Input.GetMouseButtonDown(1))
                {
  
                    destroyPlacingObject();

                }
                else if (deleteButton.DistanceTo(Input.mouseX, Input.mouseY) < deleteButton.width / 2 && Input.GetMouseButtonDown(0))
                {

                    if (objectPlacing is PistonObject)
                    {
                        PistonObject pistonObject = objectPlacing as PistonObject;
                        pistonObject.DestroyChildren();

                       
                        gameVar.level.numPistonObjects--;
                        
                    }
                    else if (objectPlacing is SpringObject)
                    {
                   
                            gameVar.level.numSpringObjects--;
                        
                    }
                    else if (objectPlacing is ConveyorBeltObject)
                    {
    
                            gameVar.level.numBeltObjects--;
                        
                    }
                    else if (objectPlacing is PlankObject)
                    {

                            gameVar.level.numPlankObjects--;
                        
                    }
                    else if (objectPlacing is FanObject)
                    {

                            gameVar.level.numFanObjects--;
                        
                    }
                    destroyPlacingObject();
                }
            }
        }

        public void destroyPlacingObject()
        {
            if (deleteButton.visible)
            {
                if (objectPlacing is PistonObject)
                {
                    PistonObject pistonObject = objectPlacing as PistonObject;
                    pistonObject.DestroyChildren();

 

                    if (gameVar.level.numPistonObjects > 0)
                    {
                        gameVar.level.numPistonObjects--;
                    }
                }
                else if(objectPlacing is SpringObject)
                {
                    if (gameVar.level.numSpringObjects > 0)
                    {
                        gameVar.level.numSpringObjects--;
                    }
                }
                else if (objectPlacing is ConveyorBeltObject)
                {
                    if (gameVar.level.numBeltObjects > 0)
                    {
                        gameVar.level.numBeltObjects--;
                    }
                }
                else if (objectPlacing is PlankObject)
                {
                    if (gameVar.level.numPlankObjects > 0)
                    {
                        gameVar.level.numPlankObjects--;
                    }
                }
                else if (objectPlacing is FanObject)
                {
                    if (gameVar.level.numFanObjects > 0)
                    {
                        gameVar.level.numFanObjects--;
                    }
                }

                gameVar.level.mObjects.Remove(objectPlacing);
                objectPlacing.Destroy();
            }
        }
    

        void getPlacingObject()
        {
            foreach (Rectangle rect in gameVar.level.mObjects)
            {
                if (rect.placing)
                {
                    oneObjectPlacing = true;
                    objectPlacing = rect;
                    break;
                }
            }
        }
    }
}
