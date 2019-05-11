using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colliders
{
    public class Matrix
    {
        public float angle;
        Vec2 row0, row1, pos;

        public Matrix()
        {
            this.angle = 0;
            this.row0 = new Vec2(1, 0);
            this.row1 = new Vec2(0, 1);
            this.pos = new Vec2(0,0);
        }


        /**
        * @param {Number} angle
        */

        public void setAngle (float angle )
        {
            this.angle = angle;
            this.row0 = this.row0.fromAngle(angle);
            this.row1 = this.row0.perp();
        }


        /**
        * @param {Number} angle
        * @param {Vector2} pos
        */
        public void setAngleAndPos (float angle, Vec2 pos)
        {
            this.setAngle(angle);
            this.pos = pos;
        }

        // this.row0 = (cos(theta), sin(theta));
        // this.row1 = (-sin(theta), cos(theta));

        /**
        * @param {Vector2} v
        * @return {Vector2}
        */

        public Vec2 RotateIntoSpaceOf (Vec2 v)
        {
            return new Vec2(v.copy().dotProduct(this.row0), v.copy().dotProduct(this.row1));
        }

        /**
        * @param {Vector2}
        * @return {Vector2}
        */

        public Vec2 rotateBy (Vec2 v )
        {
            var vec0 = this.row0.copy().multiply(v.x);
            var vec1 = this.row1.copy().multiply(v.y);

            return vec0.add(vec1);
        }

        /**
        * @param {Vector2} vec
        * @return {Vector2}
        */
        public Vec2 transformBy (Vec2 vec)
        {
            return this.rotateBy(vec).add(this.pos);
        }
    }
}
