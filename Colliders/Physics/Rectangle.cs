using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace Colliders
{
    public class Rectangle : RigidBody
    {
        public string names;
        Vec2 halfExtents, halfExtentsMinus;
        public Vec2[] localSpacePoints;
        List<Vec2> localSpaceNormals;
        public float invI;
        bool canPlayAnimation;
        AnimationSprite spriteSpring;
        public Rectangle(float mass, float x, float y, float wid, float hig, float angle) : base(mass, wid, hig, new Vec2(x + wid / 2, y + hig / 2), new Vec2(0, 0), angle)
        {

            if(this is Box)
            {
                Sprite sprite = new Sprite("bomb.png");
                sprite.height = (int)hig;
                sprite.width = (int)wid;
                sprite.SetOrigin(hig * 2 , wid * 2);
                AddChild(sprite);
            }
            else if(this is SpringObject)
            {
                spriteSpring = new AnimationSprite("spring.png",5,3);
                spriteSpring.height = (int)hig;
                spriteSpring.width = (int)wid;
                spriteSpring.SetOrigin(hig * 2, wid * 2);
                AddChild(spriteSpring);
            }
            else if(this is FanObject)
            {
                Sprite sprite = new Sprite("fan.png");
                sprite.rotation = 180;
                sprite.height = (int)hig;
                sprite.width = (int)wid;
                sprite.SetOrigin(hig * 2, wid * 2);
                AddChild(sprite);
            }
            else
            {
                Fill(100, 100, 100);
                NoStroke();
                ShapeAlign(CenterMode.Min, CenterMode.Min);
                Rect(0, 0, width, height);
            }

            this.halfExtents = new Vec2(this.width / 2, this.height / 2);
            this.halfExtentsMinus = new Vec2(-this.width / 2, -this.height / 2);


            this.localSpacePoints = new Vec2[] {
                new Vec2(this.halfExtents.x, -this.halfExtents.y),
                new Vec2(-this.halfExtents.x, -this.halfExtents.y),
                new Vec2(-this.halfExtents.x, this.halfExtents.y),
                new Vec2(this.halfExtents.x, this.halfExtents.y)
            };

            localSpaceNormals = new List<Vec2>();

            for (var ii = 0; ii < this.localSpacePoints.Length; ii++)
            {
                var nextNum = (ii + 1) % this.localSpacePoints.Length;
                localSpaceNormals.Add(this.localSpacePoints[nextNum].copy().subtract(this.localSpacePoints[ii]).getNormal().perp());
            }

            // calculate the inverse inertia tensor
            if (this.invMass > 0)
            {
                var I = this.mass * (this.width * this.width + this.height * this.height) / 12;
                this.invI = 1 / I;
            }
            else
            {
                this.invI = 0;
            }
            generateMotionAABB();
        }

        public void UpdateRectangleSize(float mass, float x, float y, float wid, float hig, float angle)
        {
            
            base.mass = mass;
            base.width = wid;
            base.height = hig;

            //base.pos = new Vec2(x + wid / 2, y + hig / 2);
            
            //matrix.setAngleAndPos(angle, new Vec2 (2000, pos.y));
            //pos.x += 500;
            //base.pos.x += 500;

            //matrix.setAngleAndPos(this.angle, this.pos);
            //base.matrix.setAngleAndPos(this.angle, this.pos);

            this.halfExtents = new Vec2(this.width / 2, this.height / 2);
            this.halfExtentsMinus = new Vec2(-this.width / 2, -this.height / 2);


            this.localSpacePoints = new Vec2[] {
                new Vec2(this.halfExtents.x, -this.halfExtents.y),
                new Vec2(-this.halfExtents.x, -this.halfExtents.y),
                new Vec2(-this.halfExtents.x, this.halfExtents.y),
                new Vec2(this.halfExtents.x, this.halfExtents.y)
            };

            localSpaceNormals = new List<Vec2>();

            for (var ii = 0; ii < this.localSpacePoints.Length; ii++)
            {
                var nextNum = (ii + 1) % this.localSpacePoints.Length;
                localSpaceNormals.Add(this.localSpacePoints[nextNum].copy().subtract(this.localSpacePoints[ii]).getNormal().perp());
            }

            // calculate the inverse inertia tensor
            if (this.invMass > 0)
            {
                var I = this.mass * (this.width * this.width + this.height * this.height) / 12;
                this.invI = 1 / I;
            }
            else
            {
                this.invI = 0;
            }
            generateMotionAABB();
        }

        public void UpdateCoord(float x, float y)
        {
            base.x = x;
            base.y = y;
        }

        public void Update()
        {
            if (this.pos.y > game.height + Math.Sqrt(this.width * this.width + this.height * this.height) * 2)
            {
                this.Destroy();
                ((MyGame)game).mObjects.Remove(this);
            }

           
            base.Update();
            generateMotionAABB();

            if (this is SpringObject)
            {
                if (canPlayAnimation)
                {
                    spriteSpring.NextFrame();
                    if (spriteSpring.currentFrame >= 14)
                    {
                        canPlayAnimation = false;
                    }
                }
            }
        }

        public void PlayAnimation()
        {
            canPlayAnimation = true;
        }

        public void generateMotionAABB()
        {
            var boundsNow = buildAABB(this.localSpacePoints, this.matrix);
            Matrix matrixNextFrame = new Matrix();

            matrixNextFrame.setAngleAndPos(this.Angle + this.angularVel * ((MyGame)game).time, this.pos.copy().addMultipledVector(((MyGame)game).time, this.vel));
            var boundsNextFrame = buildAABB(this.localSpacePoints, matrixNextFrame);

            this.motionBounds = new AABB();
            this.motionBounds.setAABB(boundsNow, boundsNextFrame);
        }


        public AABB buildAABB(Vec2[] points, Matrix m)
        {

            var minVec = new Vec2(99999, 99999);
            var maxVec = new Vec2(-99999, -99999);

            for (var ii = 0; ii < points.Length; ii++)
            {
                minVec = m.transformBy(points[ii]).min(minVec);
                maxVec = m.transformBy(points[ii]).max(maxVec);
            }


            var aabbCenter = minVec.copy().add(maxVec).divide(2);
            var aabbHalfExtents = maxVec.copy().subtract(minVec).divide(2);

            var aabb = new AABB();
            if (this is PistonObject)
            {
                aabbCenter = new Vec2(aabbCenter.x, aabbCenter.y);

            }
            aabb.setVector(aabbCenter, aabbHalfExtents);

            return aabb;
        }

        public Contact[] getClosestPoints(Rectangle rBody)
        {

            Geometry geometry = new Geometry();


            return geometry.rectRectClosestPoints(this, rBody);

        }

        public Vec2 getWorldSpaceNormal(int ii)
        {
            if(ii < 0)
            {
                ii = 0;
            }
 
            return this.matrix.rotateBy(localSpaceNormals.ToArray()[ii]);
        }

        public Vec2 getWorldSpacePoint(int ii)
        {

           return this.matrix.transformBy(this.localSpacePoints[ii]);
        }


        public Vertex[] getSupportVertices(Vec2 direction)
        {
            // rotate into rectangle space
            var v = this.matrix.RotateIntoSpaceOf(direction.copy());

            // get axis bits
            int closestI = -1;
            int secondClosestI = -1;
            float closestD = -99999;
            float secondClosestD = -99999;

            // first support
            for (int ii = 0; ii < this.localSpacePoints.Length; ii++)
            {
                float d = v.copy().dotProduct(this.localSpacePoints[ii]);

                if (d > closestD)
                {
                    closestD = d;
                    closestI = ii;
                }
            }

            // second support
            var num = 1;
            for (int ii = 0; ii < this.localSpacePoints.Length; ii++)
            {
                float d = v.copy().dotProduct(this.localSpacePoints[ii]);

                if (ii != closestI && d == closestD)
                {
                    secondClosestD = d;
                    secondClosestI = ii;
                    num++;
                    break;
                }
            }

            // closest vertices
            Vertex[] spa = new Vertex[2];

            spa [0] = (new Vertex(closestI, this.matrix.transformBy(this.localSpacePoints[closestI])));
            if (num > 1)
            {
                spa [1] = (new Vertex(secondClosestI, this.matrix.transformBy(this.localSpacePoints[secondClosestI])));
            }

            return spa;
        }


        public Vec2[] getSecondSupport(int v, Vec2 n)
        {

            Vec2 va = this.getWorldSpacePoint((v - 1 + this.localSpacePoints.Length) % this.localSpacePoints.Length);
            Vec2 vb = this.getWorldSpacePoint(v);
            Vec2 vc = this.getWorldSpacePoint((v + 1) % this.localSpacePoints.Length);

            var na = vb.copy().subtract(va).perp().getNormal();
            var nc = vc.copy().subtract(vb).perp().getNormal();

            Vec2[] support = new Vec2[2];

            if (na.dotProduct(n) < nc.dotProduct(n))
            {
                support[0] = va;
                support[1] = vb;
            }
            else
            {
                support[0] = vb;
                support[1] = vc;
            }

            return support;
        }


    }
}
