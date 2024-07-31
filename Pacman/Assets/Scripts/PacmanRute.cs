using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanRute : MonoBehaviour
{
    public GameObject ruteElements;
    private FollowRutePacman followRutePacman;
    private GameObject objectToAdd;
    private float time;
    public float speed = 4f;
    public float oldSpeed;
    public float temporalSpeed;
    private float timeAumentSpeed;
    private float temporalTime;

    public GameObject[] pointsToFollow;
    int currentPoint = 0;
    int pointTouched = 0;
    public bool shouldAwakeHome = false;

    private void Start()
    {
        followRutePacman = FindObjectOfType<FollowRutePacman>();
        temporalSpeed = oldSpeed + 2f;
        oldSpeed = 5f;
    }

    private void FixedUpdate()
    {

        if (GameManager.sharedInstance.gameStarted && !GameManager.sharedInstance.gamePaused)
        {

            Debug.Log(speed);
            GetComponent<AudioSource>().volume = 0.1f;

            temporalTime += Time.deltaTime;
            timeAumentSpeed += Time.deltaTime;

            if (timeAumentSpeed >= 4)
            {
                speed = temporalSpeed;
                if (temporalTime >= 7)
                {
                    speed = oldSpeed;
                    timeAumentSpeed = 0;
                    temporalTime = 0;
                }
            }


            time += Time.deltaTime;

            if (time >= 0.1f)
            {
                objectToAdd = Instantiate(ruteElements, this.transform.position, Quaternion.identity);
                Destroy(objectToAdd.gameObject, 20f);
                pointsToFollow[currentPoint] = objectToAdd.gameObject;
                currentPoint++;
                if (currentPoint == 5000)
                {
                    currentPoint = 0;
                }
                time = 0;
            }


            float distance = Vector2.Distance((Vector2)followRutePacman.transform.position, (Vector2)pointsToFollow[pointTouched].transform.position);

            if (distance < 0.1)
            {
                pointTouched++;
                if (pointTouched == 5000)
                {
                    pointTouched = 0;
                }
            }
            else
            {
                if (followRutePacman.ArrivedToDestiny)
                {
                    Vector2 follow = Vector2.MoveTowards(followRutePacman.transform.position, pointsToFollow[pointTouched].transform.position,
                                                    speed * Time.deltaTime);
                    Rigidbody2D rigidbodyGhost = followRutePacman.GetComponent<Rigidbody2D>();
                    rigidbodyGhost.MovePosition(follow);
                }


            }


            
        }
        else
        {
            GetComponent<AudioSource>().volume = 0.0f;
        }


    }


}
