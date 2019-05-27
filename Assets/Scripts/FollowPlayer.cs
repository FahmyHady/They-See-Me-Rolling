using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FollowPlayer : MonoBehaviour
{
    CinemachineVirtualCamera myCamera;
    PlayerControl myPlayer;
    void Start()
    {
        myPlayer = FindObjectOfType<PlayerControl>();
        myCamera = GetComponent<CinemachineVirtualCamera>();
        myCamera.Follow = myPlayer.gameObject.transform;
    }

}
