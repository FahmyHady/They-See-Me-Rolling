using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HObstacleOne : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    bool movingBack;
    public bool movingForward;
    public float ObstacleSpeed;

    void FixedUpdate()
    {
        if (movingBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, ObstacleSpeed);
            if (Mathf.Abs(transform.position.z - startPos.z) < 0.1f)
            {
                movingBack = false;
                movingForward = true;
            }
        }
        if (movingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, ObstacleSpeed);
            if (Mathf.Abs(transform.position.z - endPos.z) < 0.1f)
            {
                movingForward = false;
                movingBack = true;
            }
        }
    }
}
