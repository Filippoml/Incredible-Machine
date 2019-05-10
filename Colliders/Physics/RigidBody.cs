using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace Colliders
{
    public class RigidBody : EasyDraw
    {
        public int frame;

        public float mass, invMass, width, height, angle;

        public float angularVel, friction;
        public AABB motionBounds;
        public Vec2 pos, vel, force;
       
        public Matrix matrix;

        public RigidBody(float mass, float width, float height, Vec2 pos, Vec2 vel, float angle ) : base((int)width, (int)height)
        {

            this.SetOrigin(width / 2, height / 2);
            this.x = pos.x;
            this.y = pos.y;
            this.mass = mass;
            if (this.mass == 0) this.invMass = 0;
            else this.invMass = 1 / mass;

            this.matrix = new Matrix();


            this.width = width;
            this.height = height;

            this.pos = pos;

            matrix.setAngleAndPos(angle, pos);

            this.angle = angle;
            this.vel = vel;

            this.force = new Vec2(0, 0);

        }

        public void Update()
        {

            if(this is PistonObject)
            {
                Console.WriteLine(this.width);
            }

            float dt = ((MyGame)game).time;
            if(this is Box && ((MyGame)game).paused == true)
            {
                dt = 0;
            }
            if (dt != 0)
            {
                setGravity();
            }

            this.vel.x += (this.force.x);
            this.vel.y += (this.force.y);

            


        
            // --------------------

            this.angle += this.angularVel * dt;


            this.pos.x += this.vel.x * dt;
            this.pos.y += this.vel.y * dt;

            // ====================

            this.force.set(0, 0);


            matrix.setAngleAndPos(angle, pos);

            this.rotation = angle * (180 / Mathf.PI);
         

            this.x = pos.x;
            this.y = pos.y;

        }

        private void setGravity()
        {
          
            this.force.y += this.mass * 9.8f;

        }



        public float Angle
        {
            get
            {
    
                return this.angle;
            }
            set
            {
           
                rotation = value;
                this.angle = value;

            }
        }

        public int Frame
        {
            get
            {

                return this.frame;
            }
            set
            {


                this.frame = value;

            }
        }
    }
}
