using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRutePacman : MonoBehaviour
{
    public float speed = 7f;
    public bool ArrivedToDestiny = false;
    public Transform[] waypoints;
    public int currentWaypoint = 0;
    public bool shouldAwakeHome = false;

    private void Update()
    {
        if (GameManager.sharedInstance.invincibleTime > 0)
        {
            GetComponent<Animator>().SetBool("PacmanInvincible", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("PacmanInvincible", false);
        }
    }

    private void FixedUpdate()
    {

        if (!ArrivedToDestiny)
        {
            GetOutOfBox();
        }
    }

    private void GetOutOfBox()
    {
        if (GameManager.sharedInstance.gameStarted && !GameManager.sharedInstance.gamePaused)
        {
            GetComponent<AudioSource>().volume = 0.1f;

            if (!shouldAwakeHome)
            {
                float distance = Vector2.Distance((Vector2)this.transform.position, (Vector2)waypoints[currentWaypoint].position);

                if (distance < 0.1)
                {
                    currentWaypoint++;
                    if (currentWaypoint == waypoints.Length)
                    {
                        ArrivedToDestiny = true;
                    }
                }
                else
                {
                    Vector2 newPosition = Vector2.MoveTowards(this.transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);
                    GetComponent<Rigidbody2D>().MovePosition(newPosition);
                }
            }
            
        }
        else
        {
            GetComponent<AudioSource>().volume = 0.0f;
        }
    }


    IEnumerator AwakeFromHome()
    {
        yield return new WaitForSeconds(4);
        this.shouldAwakeHome = false;
        this.speed *= 1.1f;
        PacmanRute pacmanRute = FindObjectOfType<PacmanRute>();
        pacmanRute.oldSpeed = pacmanRute.speed + 0.6f;
    }
}
