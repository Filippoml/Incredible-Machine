using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace Colliders
{
    public class Button : Sprite
    {
        public Button(float x, float y, string filename) : base(filename)
        {
            this.x = x;
            this.y = y;
        }

        void Update()
        {
            if(DistanceTo(Input.mouseX, Input.mouseY) < this.width && Input.GetMouseButtonDown(0))
            {
                ((MyGame)game).ResetGame();
                ((MyGame)game).time = 1f / 50f;
                ((MyGame)game).paused = false;
            }

            
        }
    }
}
