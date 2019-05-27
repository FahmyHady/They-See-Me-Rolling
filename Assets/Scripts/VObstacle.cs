using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VObstacle : MonoBehaviour
{
    Vector3[] startPos = new Vector3[3];
    Vector3[] endPos = new Vector3[3];
    bool movingBack;
    public bool movingForward;
    Transform[] mySpheres;
    public float ObstacleSpeed;
    public float height;
    void Start()
    {
        mySpheres = GetComponentsInChildren<Transform>();
        startPos[0] = mySpheres[1].position;
        startPos[1] = mySpheres[2].position;
        startPos[2] = mySpheres[3].position;
        endPos[0] = startPos[0] + Vector3.up * height;
        endPos[1] = startPos[1] - Vector3.up * height;
        endPos[2] = startPos[2] + Vector3.up * height;
        movingForward = true;
    }

    void FixedUpdate()
    {

        if (movingBack)
        {
            for (int i = 1; i < mySpheres.Length; i++)
            {
                mySpheres[i].position = Vector3.MoveTowards(mySpheres[i].position, startPos[i - 1], ObstacleSpeed);
                if (Mathf.Abs(mySpheres[i].position.y - startPos[i - 1].y) < 0.1f)
                {
                    movingBack = false;
                    movingForward = true;
                }

            }
        }
        if (movingForward)
        {
            for (int i = 1; i < mySpheres.Length; i++)
            {
                mySpheres[i].position = Vector3.MoveTowards(mySpheres[i].position, endPos[i - 1], ObstacleSpeed);
                if (Mathf.Abs(mySpheres[i].position.y - endPos[i - 1].y) < 0.1f)
                {
                    movingForward = false;
                    movingBack = true;
                }

            }
        }
    }
}

