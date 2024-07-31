using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{

    public Transform[] waypoints;
    int currentWaypoint = 0;
    public float speed;
    public AnimationClip up;
    private Animation currentAnimation;
    public bool shouldAwakeHome = false;
    private float timeWaiting;
    


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
        if (GameManager.sharedInstance.gameStarted && !GameManager.sharedInstance.gamePaused)
        {
            GetComponent<AudioSource>().volume = 0.1f;

            if (!shouldAwakeHome)
            {
                float distance = Vector2.Distance((Vector2)this.transform.position, (Vector2)waypoints[currentWaypoint].position);

                if (distance < 0.1)
                {
                    currentWaypoint = (currentWaypoint + 1) % waypoints.Length;


                    Vector2 newDirection = waypoints[currentWaypoint].position - this.transform.position;
                    GetComponent<Animator>().SetFloat("DirX", newDirection.x);
                    GetComponent<Animator>().SetFloat("DirY", newDirection.y);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GameManager.sharedInstance.invincibleTime <= 0)
            {
                GameManager.sharedInstance.gameStarted = false;
                Destroy(collision.gameObject);
                StartCoroutine("RestartGame");
            }
            else
            {
                GameObject home = GameObject.Find("Home");
                this.transform.position = home.transform.position;
                this.currentWaypoint = 0;
                shouldAwakeHome = true;
                UImanager.sharedInstance.ScorePoints(1500);
                StartCoroutine(AwakeFromHome());
            }
        }
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5);
        GameManager.sharedInstance.RestartGame();
    }

    IEnumerator AwakeFromHome()
    {
        yield return new WaitForSeconds(4);
        this.shouldAwakeHome = false;
        this.speed *= 1.1f;
    }
}
