using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    public float speed = 0.4f;
    Vector2 destination = Vector2.zero;
    private Rigidbody2D rigidbody;
    private Animator animator;
    public CircleCollider2D rute;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        destination = this.transform.position;
    }
    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.gameStarted && !GameManager.sharedInstance.gamePaused)
        {
            GetComponent<AudioSource>().volume = 0.1f;

            float distanceToDestination = Vector2.Distance(this.transform.position, destination);

            if (distanceToDestination < 2.0f)
            {
                if (Input.GetKey(KeyCode.W) && CanMoveTo(Vector2.up))
                {
                    destination = (Vector2)this.transform.position + Vector2.up;
                }

                if (Input.GetKey(KeyCode.D) && CanMoveTo(Vector2.right))
                {
                    destination = (Vector2)this.transform.position + Vector2.right;
                }

                if (Input.GetKey(KeyCode.S) && CanMoveTo(Vector2.down))
                {
                    destination = (Vector2)this.transform.position + Vector2.down;
                }

                if (Input.GetKey(KeyCode.A) && CanMoveTo(Vector2.left))
                {
                    destination = (Vector2)this.transform.position + Vector2.left;
                }

                Vector2 newPos = Vector2.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);
                rigidbody.MovePosition(newPos);
            }


            Vector2 dir = destination - (Vector2)(this.transform.position);

            animator.SetFloat("DirX", dir.x);
            animator.SetFloat("DirY", dir.y);
        }
        else
        {
            GetComponent<AudioSource>().volume = 0.0f;
        }


    }

    private bool CanMoveTo(Vector2 direction)
    {
        Vector2 pacmanPos = this.transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pacmanPos + direction, pacmanPos);
        //return hit.collider == GetComponent<Collider2D>();

        Collider2D pacmanCollider = GetComponent<Collider2D>();
        Collider2D hitCollider = hit.collider;

        if (hitCollider == pacmanCollider)
        {
            //no tengo nada más enmedio -> puedo moverme allí
            return true;
        }
        else
        {
            //tengo un collider delante que NO es el de pacman -> no puedo moverme allí
            return false;
        }
    }

    


}
