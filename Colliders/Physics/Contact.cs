using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colliders.Objects;
using GXPEngine;


namespace Colliders
{
    public class Contact : GameObject
    {
        public Rectangle A, B;
        public Vec2 Pa, Pb, normal, ra, rb;
        public float Dist, Impulse, invDenom;
        public bool prova; 
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

        public void applyImpulses(Vec2 imp)
        {

            bool verticalFloor = false;

            //Vec2 friction = new Vec2(100, 100);
            //if (A is ConveyorBeltObject)
            //{
            //    Vec2 velocity = B.vel;
            //    friction = velocity.multiply(A.friction);
            //    B.vel.subtract(friction);
            //}
            //else if (B is ConveyorBeltObject)
            //{
            //    Vec2 velocity = A.vel;
            //    friction = velocity.multiply(B.friction);
            //    A.vel.subtract(friction);
            //}


            if (A.placing == false && B.placing == false)
            {

                if (B is Box && A is PistonObject)
                {
                    this.B.vel.subtractMultipledVector(this.B.invMass, imp);

                }
                else if (A is Floor && B is Box && A.height > A.width && B.vel.x > 0)
                {
                    float test = -B.vel.x * 0.5f;
                    this.B.vel.subtractMultipledVector(this.B.invMass, imp);
                    this.B.vel.x = test;
                    if ((Mathf.Abs(B.vel.x) > 0 && Mathf.Abs(B.vel.y) > 0))
                    {
                        verticalFloor = true;
                    }

                }
                else if (B is Floor && A is Box && B.height > B.width && A.vel.x > 0)
                {
                    float test = -A.vel.x * 0.5f;
                    this.A.vel.addMultipledVector(this.B.invMass, imp);
                    this.A.vel.x = test;
                    if ((Mathf.Abs(A.vel.x) > 0 && Mathf.Abs(A.vel.y) > 0))
                    {
                        verticalFloor = true;
                    }
                }

                else if (A is FanObject && B is Box)
                {
                    this.B.vel.subtractMultipledVector(this.B.invMass, imp);
                }
                else if (A is Box && B is FanObject)
                {
                    this.A.vel.addMultipledVector(this.A.invMass, imp);
                }
                else if (B is SpringObject && A is Box)
                {


                    if (A.vel.y > 0 && A.y < B.y - B.height)
                    {
                        B.PlayAnimation();
                        this.A.vel.addMultipledVector(this.A.invMass, new Vec2(0, -1000));
                    }
                    else
                    {
                        this.A.vel.addMultipledVector(this.A.invMass, imp);
                    }
                }
               
                else if (A is SpringObject && B is Box)
                {


                    if (B.vel.y > 0 && B.y < A.y - A.height)
                    {
                        A.PlayAnimation();
                        this.B.vel.subtractMultipledVector(this.B.invMass, new Vec2(0, 1000));
                    }
                    else
                    {
                        this.B.vel.subtractMultipledVector(this.B.invMass, imp);
                    }
                }
                else if(A is Floor && B is Box)
                {
                    if(B.vel.x == 100)
                    {
                        B.vel.x = 0;
                    }
                 
                        this.A.vel.addMultipledVector(this.A.invMass, imp);
                        this.B.vel.subtractMultipledVector(this.B.invMass, imp);
                    
                    
                }
                else if(A is Box && B is Floor)
                {
                    this.A.vel.addMultipledVector(this.A.invMass, imp);
                    this.B.vel.addMultipledVector(this.B.invMass, imp);
                    
                }
                else if (B is Box && A is ConveyorBeltObject && B.y <= A.y)
                {
                    if (this.prova == false)
                    {
                        this.B.vel.x = 100;
                    }
                    else
                    {
                        this.B.vel.x = 0;
                    }
                    this.B.vel.subtractMultipledVector(this.B.invMass, imp);
                }
                else if (A is Box && B is ConveyorBeltObject && A.y <= A.y)
                {
                    if (this.prova == false)
                    {
                        this.A.vel.x = 100;
                    }
                    else
                    {
                        this.A.vel.x = 0;
                    }
                    this.A.vel.addMultipledVector(this.A.invMass, imp);

                }
                else
                {
                    this.A.vel.addMultipledVector(this.A.invMass, imp);
                    this.B.vel.subtractMultipledVector(this.B.invMass, imp);
                }



                //if (A is Wind && B is Box)
                //{
                //    A.Remove(); 
                //}
                if (!(A is SpringObject || B is SpringObject || A is FanObject || B is FanObject || A is SubPiston || B is SubPiston || verticalFloor || A is ConveyorBeltObject || B is ConveyorBeltObject))
                {
                    float a = imp.dotProduct(this.ra) * this.A.invI;
                    float b = imp.dotProduct(this.rb) * this.B.invI;

                    this.A.angularVel += a;
                    this.B.angularVel -= b;

                }

                //Console.WriteLine(A+ "  " + B);
            }
        }
    }
}
