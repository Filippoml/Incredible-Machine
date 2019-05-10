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
        public HUD() : base(MyGame.main.width, MyGame.main.height)
        {
            Button playButton = new Button(70, 800, "play.png");
            AddChild(playButton);
        }
    }
}
