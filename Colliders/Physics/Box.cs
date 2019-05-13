using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace Colliders
{
    public class Box : Rectangle
    {

        Font _font;
        MyGame gameVar;
        float halfWidth, halfHeight;
        public Box(float mass, float x, float y, float wid, float hig, float angle):base(mass,x,y,wid,hig,angle, false)
        {
            gameVar = ((MyGame)game);
            this.width = wid;
            this.height = hig;
            this.halfWidth = this.width / 2;
            this.halfHeight = this.height / 2;

            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("font.ttf");
            _font = new Font(new FontFamily(pfc.Families[0].Name), 50, FontStyle.Regular);
        }

        public void Update()
        {
            if (this.HitTest(gameVar.level.robot))
            {

                Sound backgroundSound = new Sound("Sounds/sound_win.wav", false, true);
                backgroundSound.Play();

                gameVar.level.hud.graphics.DrawString("LEVEL FINISHED!", font, new SolidBrush(Color.Red), new PointF(900, 100));
                this.Destroy();
                gameVar.nextLevel = true;
            }
            base.Update();
            
            
        }

    }
}
