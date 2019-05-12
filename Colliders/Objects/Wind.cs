using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colliders.Objects
{
    public class Wind : Rectangle
    {
        FanObject fakeParent;
        public Wind(FanObject fakeparent, float mass, float x, float y, float wid, float hig, float angle) : base(mass, x, y, wid, hig, angle, false)
        {
            this.fakeParent = fakeparent;

        }

        void Update()
        {
            float distance = this.DistanceTo(fakeParent);

            if (vel.y > 0)
            {
                this.Remove();
            }
            //if(this.y < ((MyGame)game).box.y)
            //{
            //    this.Remove();
            //}
            base.Update();
        }
    }
}
