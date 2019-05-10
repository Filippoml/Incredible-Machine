using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colliders
{
    public class AABB
    {
        Vec2 mCenter, mHalfExtents;

        public AABB()
        {

        }


        public void setVector (Vec2 mCenter, Vec2 mHalfExtents)
        {
            this.mCenter = mCenter;
            this.mHalfExtents = mHalfExtents;
        }

        public void setAABB (AABB a, AABB b)
        {
            var minVectorA = a.mCenter.copy().subtract(a.mHalfExtents);
            var maxVectorA = a.mCenter.copy().add(a.mHalfExtents);

            var minVectorB = b.mCenter.copy().subtract(b.mHalfExtents);
            var maxVectorB = b.mCenter.copy().add(b.mHalfExtents);

            var minVector = minVectorA.copy().min(minVectorB);
            var maxVector = maxVectorA.copy().max(maxVectorB);

            this.mCenter = minVector.copy().add(maxVector).divide(2);
            this.mHalfExtents = maxVector.copy().subtract(minVector).divide(2);
        }


       public bool overlap(AABB a, AABB b)
        {

            float aMaxX = a.mCenter.x + a.mHalfExtents.x;
            float aMinX = a.mCenter.x - a.mHalfExtents.x;

            float aMaxY = a.mCenter.y + a.mHalfExtents.y;
            float aMinY = a.mCenter.y - a.mHalfExtents.y;

            // ----------------------------------------

            float bMaxX = b.mCenter.x + b.mHalfExtents.x;
            float bMinX = b.mCenter.x - b.mHalfExtents.x;

            float bMaxY = b.mCenter.y + b.mHalfExtents.y;
            float bMinY = b.mCenter.y - b.mHalfExtents.y;
        
            if (aMaxX < bMinX || aMinX > bMaxX) return false;
            if (aMaxY < bMinY || aMinY > bMaxY) return false;

            return true;
        }

    }
}
