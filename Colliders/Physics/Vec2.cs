using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace Colliders
{
    public struct Vec2
    {
        public float x, y;

        public Vec2(float xx, float yy)
        {
            this.x = xx;
            this.y = yy;
        }

        public Vec2 copy ()
        {
            var vec = new Vec2(this.x, this.y);

            return vec;
        }

        public Vec2 subtract (Vec2 vec)
        {
            this.x = this.x - vec.x;
            this.y = this.y - vec.y;

            return this;
        }

        public Vec2 set(float x, float y)
        {
            this.x = x;
            this.y = y;

            return this;
        }

        public Vec2 subtractMultipledVector (float value, Vec2 vector)
        {
            this.x -= value * vector.x;
            this.y -= value * vector.y;

            return this;
        }

        public Vec2 addMultipledVector (float value, Vec2 vector)
        {
            this.x += value * vector.x;
            this.y += value * vector.y;

            return this;
        }

        public Vec2 multiply (float value)
        {

            this.x = this.x * value;
            this.y = this.y * value;

            return this;
        }

        /**
         *
         * @param {Vec2} value
         * @return {Vec2}
         */

        public Vec2 divide (float value)
        {
            this.x = this.x / value;
            this.y = this.y / value;

            return this;
        }


        /**
        * @
        * @desc add vector
        *
        * @param {Vec2} vec
        */

        public Vec2 add (Vec2 vec)
        {

            this.x = this.x + vec.x;
            this.y = this.y + vec.y;

            return this;
        }

        /**
         * @
         * @param {Vec2} theta
         * @return {Vec2}
         */

        public Vec2 fromAngle (float theta)
        {
            this.x = Mathf.Cos(theta);
            this.y = Mathf.Sin(theta);

            return this;
        }


        /**
         * @
         * @return {Vec2}
         */
        public Vec2 perp ()
        {
            return new Vec2(-this.y, this.x);
        }

        // =============================

        /**
        * @
        * @desc get the length of the vector
        *
        * @return {Number}
        */

        public float getLength ()
        {
            return Mathf.Sqrt(this.getSquareLength());
        }

        /**
         *
         * @return {Number}
         */
        public float getSquareLength ()
        {
            return this.dotProduct(this);
        }

        /**
        * @
        * @desc get the normalized vector
        *
        * @return {Vec2}
        */

        public Vec2 getNormal ()
        {
            float length = this.getLength();
            return new Vec2(this.x / length, this.y / length);
        }


        public Vec2 min (Vec2 minVec )
        {
            if (this.x > minVec.x) this.x = minVec.x;
            if (this.y > minVec.y) this.y = minVec.y;

            return this;
        }

        /**
        * @function
        * @desc get the vector compared with the vector of maxVector to see which is bigger
        *
        * @param {Vector2} maxVec
        * @return {Vector2}
        */

        public Vec2 max (Vec2 maxVec )
        {
            if (this.x < maxVec.x) this.x = maxVec.x;
            if (this.y < maxVec.y) this.y = maxVec.y;

            return this;
        }

        /**
        * @function
        * @desc get the vector compare with vector between minVec and maxVec
        *
        * @param {Vector2} minVec
        * @param {Vector2} maxVec
        */
        public Vec2 clamp (Vec2 minVec, Vec2 maxVec )
        {
            return this.max(minVec).min(maxVec);
        }

        /**
        * @function
        * @desc
        * @see http://mathworld.wolfram.com/RotationMatrix.html
        *
        * @param {Number} theta
        */

        public Vec2 rotate (float theta)
        {
            var rotatedX = this.x * Mathf.Cos(theta) - this.y * Mathf.Sin(theta);
            var rotatedY = this.x * Mathf.Sin(theta) + this.y * Mathf.Cos(theta);

            return this.set(rotatedX, rotatedY);
        }


        /**
        * @function
        * @see http://en.wikipedia.org/wiki/Dot_product
        *
        * @param  {Vector2} vec
        * @return {Number}
        */

        public float dotProduct (Vec2 vec)
        {
            return this.x * vec.x + this.y * vec.y;
        }

        /**
         *
         * @return {Vector2}
         */
        public Vec2 abs()
        {
            return new Vec2(Mathf.Abs(this.x),Math.Abs(this.y));
        }

    }
}
