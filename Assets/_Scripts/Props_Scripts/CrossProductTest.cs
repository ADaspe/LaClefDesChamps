using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossProductTest : MonoBehaviour
{
    public Transform point1;
    public Transform point2;

    private Vector3 point1Pos;
    private Vector3 point2Pos;
    private Vector3 distanceBtwn;
    private Vector3 middlePoint;

    private void Update()
    {
        //middlePoint = CheckMiddle();
        //print(middlePoint);
        //CrossProduct Test

        point1Pos = point1.position;
        point2Pos = point2.position;
        distanceBtwn = point2Pos - point1Pos;


        Vector3 cross = Vector3.Cross(point1Pos, point2Pos);
        DrawRays(point1Pos, distanceBtwn, Color.red);
        //DrawRays(middlePoint, Vector3.up * 10, Color.green);
        DrawRays(point1Pos, cross, Color.blue);
    }

    private Vector3 CheckMiddle()
    {
        point1Pos = point1.position;
        point2Pos = point2.position;
        distanceBtwn = point2Pos - point1Pos;


        return distanceBtwn / 2;
    }

    private void DrawRays(Vector3 origin, Vector3 ray, Color color)
    {
        Debug.DrawRay(origin, ray, color);
    }
}
