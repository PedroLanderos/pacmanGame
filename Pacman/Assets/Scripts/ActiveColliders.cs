using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveColliders : MonoBehaviour
{
    private GhostMovement ghostMovement;
    private FollowRutePacman followRutePacman;
    private PacmanRute pacmanRute;


    private void Awake()
    {
        ghostMovement = FindObjectOfType<GhostMovement>();
        pacmanRute = FindObjectOfType<PacmanRute>();
        followRutePacman = FindObjectOfType<FollowRutePacman>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GameManager.sharedInstance.invincibleTime <= 0)
            {
                Destroy(collision.gameObject);
                GameManager.sharedInstance.gameStarted = false;
                ghostMovement.StartCoroutine("RestartGame");
            }
            else
            {
                GameObject home = GameObject.Find("Home");
                this.transform.position = home.transform.position;
                followRutePacman.currentWaypoint = 0;
                followRutePacman.ArrivedToDestiny = false;
                followRutePacman.shouldAwakeHome = true;
                UImanager.sharedInstance.ScorePoints(1500);
                followRutePacman.StartCoroutine("AwakeFromHome");
            }

        }



    }

}
