using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace Colliders
{
    public class Geometry
    {
        float distA;
        Rectangle faceRect;
        Vec2 worldEdge0, worldEdge1;
        Vec2[] pointA, pointB;


        public Geometry()
        {

        }

        private Vec2 projectPointOntoEdge (Vec2 p, Vec2 e0, Vec2 e1)
        {
            var v = p.copy().subtract(e0);
            var e = e1.copy().subtract(e0);

            var t = e.dotProduct(v) / e.getSquareLength();

            t = Mathf.Max(Mathf.Min(1, t), 0);

            return e0.copy().addMultipledVector(t, e);
        }

        private Most [] featurePairJudgement (float dist, float centerDist, int edge, int supportV, float fpc, Most mostSeparated, Most mostPenetrating, Vec2 e0, Vec2 e1 )
        {

            if (dist > 0)
            {
                Vec2 p = this.projectPointOntoEdge(new Vec2(0,0), e0, e1);
                dist = p.getLength();


                if (dist < mostSeparated.dist)
                {
                    mostSeparated = new Most ( dist, supportV, edge, fpc, centerDist);
                }
                else if (dist == mostSeparated.dist && fpc == mostSeparated.fp)
                {
                    if (centerDist < mostSeparated.centerDist)
                    {
                        mostSeparated = new Most(dist, supportV, edge, fpc, centerDist);
                    }
                }
            }
            else
            {
                //
                if (dist > mostPenetrating.dist)
                {
                    mostPenetrating = new Most(dist, supportV, edge, fpc, centerDist);
                }
                else if (dist == mostPenetrating.dist && fpc == mostPenetrating.fp)
                {
                    // got to the pick the right one - pick one closest to center of A
                    if (centerDist < mostPenetrating.centerDist)
                    {
                        mostPenetrating = new Most(dist, supportV, edge, fpc, centerDist);
                    }
                }
            }
            Most[] mosts = new Most[] { mostSeparated, mostPenetrating } ;
            return mosts;
        }

        public Contact[] rectRectClosestPoints(Rectangle rectangleA, Rectangle rectangleB)
        {

            List<Contact> contacts = new List<Contact>();

            Most mostSeparated = new Most(99999, -1, -1, -1, 99999);

            Most mostPenetrating = new Most(-99999, -1, -1, -1, 99999);

            for (var ii = 0; ii < rectangleA.localSpacePoints.Length; ii++) {

                ///rectangleA.localSpacePoints[ii]
                var wsN = rectangleA.getWorldSpaceNormal(ii);
                var wsV0 = rectangleA.getWorldSpacePoint(ii);
                var wsV1 = rectangleA.getWorldSpacePoint((ii + 1) % rectangleA.localSpacePoints.Length);

                // get supporting vertices of B
                Vertex[] s = rectangleB.getSupportVertices(wsN.copy().multiply(-1));


                for (var jj = 0; jj < s.Length; jj++) {
                    if (s[jj] != null)
                    {
                        //var m
                        var mfp0 = s[jj].mV.copy().subtract(wsV0);
                        var mfp1 = s[jj].mV.copy().subtract(wsV1);

                        // distance from the origin of the face
                        var dist = mfp0.dotProduct(wsN);

                        // distance to the center of A
                        var centerDist = s[jj].mV.copy().subtract(rectangleA.pos).getSquareLength();


                        if (ii == 0) distA = dist;

                        Most[] most = this.featurePairJudgement(dist, centerDist, ii, s[jj].mI, 1, mostSeparated, mostPenetrating, mfp0, mfp1);
                        mostSeparated = most[0];
                        mostPenetrating = most[1];
                    }
                }
            }


            for (var ii = 0; ii < rectangleB.localSpacePoints.Length; ii++) {

                var wsN = rectangleB.getWorldSpaceNormal(ii);
                var wsV0 = rectangleB.getWorldSpacePoint(ii);
                var wsV1 = rectangleB.getWorldSpacePoint((ii + 1) % rectangleB.localSpacePoints.Length);

                Vertex[] s = rectangleA.getSupportVertices(wsN.copy().multiply(-1));

                for (var jj = 0; jj < s.Length; jj++) {
                    if (s[jj] != null)
                    {
                        var mfp0 = s[jj].mV.copy().subtract(wsV0);
                        var mfp1 = s[jj].mV.copy().subtract(wsV1);

                        // distance from the origin of the face
                        var dist = mfp0.dotProduct(wsN);

                        // distance to the center of A
                        var centerDist = s[jj].mV.copy().subtract(rectangleB.pos).getSquareLength();
                        //window.distB = dist;

                        Most[] most = this.featurePairJudgement(dist, centerDist, ii, s[jj].mI, 0, mostSeparated, mostPenetrating, mfp0, mfp1);
                        mostSeparated = most[0];
                        mostPenetrating = most[1];


                    }
                }
            }


            Most featureToUse = new Most(0, 0, 0, 0, 0);
            Rectangle vertexRect, faceRect;
            if (mostSeparated.dist > 0 && mostSeparated.fp != -1) {
                featureToUse = mostSeparated;
            } else if (mostPenetrating.dist <= 0) {
                featureToUse = mostPenetrating;
            } else {
                //Console.WriteLine(RectRectClosestPoints() + "Impossible condition!");
            }

            //edited
            if (featureToUse.fp == 0) {
                vertexRect = rectangleA;
                faceRect = rectangleB;
            } else {
          
                vertexRect = rectangleB;
                faceRect = rectangleA;
            }



            var worldN = faceRect.getWorldSpaceNormal(featureToUse.face);

            if(featureToUse.vertex == -1)
            {

            }
            // other vertex adjcent which makes most parallel normal with the collision normal
            var worldV = vertexRect.getSecondSupport(featureToUse.vertex, worldN);


            // world space edge
            Vec2 worldEdge0 = faceRect.getWorldSpacePoint(featureToUse.face);
            this.faceRect = faceRect;

            Vec2 worldEdge1 = faceRect.getWorldSpacePoint((featureToUse.face + 1) % faceRect.localSpacePoints.Length);

            this.worldEdge0 = worldEdge0;
            this.worldEdge1 = worldEdge1;
            Vec2 [] pointsOnRectangleA = new Vec2 [2];
            Vec2[] pointsOnRectangleB = new Vec2[2];


            if (featureToUse.fp == 0) {
                pointsOnRectangleA[0] = this.projectPointOntoEdge(worldEdge0, worldV[0], worldV[1]);
                pointsOnRectangleA[1] = this.projectPointOntoEdge(worldEdge1, worldV[0], worldV[1]);

                pointsOnRectangleB[0] = this.projectPointOntoEdge(worldV[1], worldEdge0, worldEdge1);
                pointsOnRectangleB[1] = this.projectPointOntoEdge(worldV[0], worldEdge0, worldEdge1);

                worldN.multiply(-1);
            } else {
                pointsOnRectangleB[0] = this.projectPointOntoEdge(worldEdge0, worldV[0], worldV[1]);
                pointsOnRectangleB[1] = this.projectPointOntoEdge(worldEdge1, worldV[0], worldV[1]);

                pointsOnRectangleA[0] = this.projectPointOntoEdge(worldV[1], worldEdge0, worldEdge1);
                pointsOnRectangleA[1] = this.projectPointOntoEdge(worldV[0], worldEdge0, worldEdge1);
            }


            this.pointA = pointsOnRectangleA;
            this.pointB = pointsOnRectangleB;



            float d0 = pointsOnRectangleB[0].copy().subtract(pointsOnRectangleA[0]).dotProduct(worldN);
            float d1 = pointsOnRectangleB[1].copy().subtract(pointsOnRectangleA[1]).dotProduct(worldN);

            

           
            contacts.Add(new Contact(rectangleA, rectangleB, pointsOnRectangleA[0], pointsOnRectangleB[0], worldN, d0));
            contacts.Add(new Contact(rectangleA, rectangleB, pointsOnRectangleA[1], pointsOnRectangleB[1], worldN, d1));
            


            return contacts.ToArray();

        }
    }
}
