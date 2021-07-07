using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosPlusAngleOffset : MonoBehaviour
{
    public Transform startPos;
    public Transform _targetPos;
    public float offsetAngle;



    // Update is called once per frame
    void Update()
    {
        CalculateAngle(_targetPos, startPos, offsetAngle);
    }

    private Vector3 CalculateAngle(Transform target, Transform startpos, float angle)
    {
        float radius = Vector3.Distance(startPos.position, target.position);

        float x = Mathf.Abs(startPos.transform.position.x - target.position.x);
        float z = Mathf.Abs(startPos.transform.position.z - target.position.z);


        float initialAngle = Mathf.Atan2(z, x) * Mathf.Rad2Deg;

        float desiredAngle = (initialAngle + angle) * Mathf.Deg2Rad;

        float xResult = radius * Mathf.Cos(desiredAngle);
        float zResult = radius * Mathf.Sin(desiredAngle);

        if (_targetPos.position.x < startPos.position.x)
        {
            xResult = xResult * -1;
        }

        if (_targetPos.position.z < startPos.position.z)
        {
            zResult = zResult * -1;
        }

        Vector3 targetPos = new Vector3(startPos.position.x + xResult, startPos.position.y, startPos.position.z + zResult);
        Debug.DrawLine(startPos.position, _targetPos.position, Color.red);
        Debug.DrawLine(startPos.position, targetPos, Color.green);
        return targetPos;
    }
}
