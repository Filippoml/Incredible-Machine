using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colliders
{
    public class Contact
    {
        public Rectangle A, B;
        public Vec2 Pa, Pb, normal, ra, rb;
        public float Dist, Impulse, invDenom;

        public Contact(Rectangle A, Rectangle B, Vec2 pa, Vec2 pb, Vec2 n, float dist)
        {
            this.A = A;
            this.B = B;
            this.Pa = pa;
            this.Pb = pb;
            this.normal = n;
            this.Dist = dist;
            this.Impulse = 0;

            this.ra = pa.copy().subtract(A.pos).perp();
            this.rb = pb.copy().subtract(B.pos).perp();


            var aInvMass = A.invMass;
            var bInvMass = B.invMass;
            var ran = this.ra.dotProduct(n);
            var rbn = this.rb.dotProduct(n);

            var c = ran * ran * A.invI;
            var d = rbn * rbn * B.invI;


            this.invDenom = 1 / (aInvMass + bInvMass + c + d);

        }


        public Vec2 getVelPa()
        {
            return this.A.vel.copy().addMultipledVector(this.A.angularVel, this.ra);
        }

        /**
        * @return {Vector2}
        */

        public Vec2 getVelPb()
        {
            return this.B.vel.copy().addMultipledVector(this.B.angularVel, this.rb);
        }

        public void applyImpulses (Vec2 imp)
        {

 

            Vec2 friction = new Vec2(1, 1);
            if (A is Floor)
            {
                Vec2 velocity = B.vel;
                friction = velocity.multiply(A.friction);
                B.vel.subtract(friction);
            }
            else if (B is Floor)
            {
                Vec2 velocity = A.vel;
                friction = velocity.multiply(B.friction);
                A.vel.subtract(friction);
            }

            if(A is SpringObject && B is Box)
            {
          
                Console.WriteLine(imp.x + " " + imp.y);
                
                Console.WriteLine(imp.x + " " + imp.y);


                if (B.vel.y > 0)
                {
                    this.B.vel.subtractMultipledVector(this.B.invMass, new Vec2(0, 1000));
                }
                else
                {
                    

                    this.B.vel.subtractMultipledVector(this.B.invMass, imp);

                }
            }
            else
            {
                this.A.vel.addMultipledVector(this.A.invMass, imp);

                this.B.vel.subtractMultipledVector(this.B.invMass, imp);



            }

            float a = imp.dotProduct(this.ra) * this.A.invI;
            float b = imp.dotProduct(this.rb) * this.B.invI;

            this.A.angularVel += a;
            this.B.angularVel -= b;

            //Console.WriteLine(A+ "  " + B);

        }
    }
}
