using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colliders
{
    public class Most
    {
        public float dist, fp, centerDist;
        public int vertex, face;
        public Most (float dist, int vertex, int face, float fp, float centerDist)
        {
            this.dist = dist;
            this.vertex = vertex;
            this.face = face;
            this.fp = fp;
            this.centerDist = centerDist;
        }
    }
}
