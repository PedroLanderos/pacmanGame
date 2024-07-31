using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGhostFollower : MonoBehaviour
{
    public float speed;

    public GameObject[] pointsToFollow;
    private int currentPoint;

    public bool shouldWaitInHome;

    private void Update()
    {
        if (!GameManager.sharedInstance.gamePaused && GameManager.sharedInstance.gameStarted)
        {

        }
    }
}
