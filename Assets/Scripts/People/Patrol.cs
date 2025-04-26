using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Animator anim;
    public Transform[] patrolPoints;

    [SerializeField] private int targetPoint;
    [SerializeField] private float speed;
    [SerializeField] private float pauseTime = 0.0f;

    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        //Only get a reference to the animator if it already exists
        if(this.GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }

        targetPoint = 0;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == patrolPoints[targetPoint].position)
        {
            canMove = false;
            increaseTargetInt();

            if (anim != null)
            {
                anim.SetBool("isMoving", false);
            }

            StartCoroutine(Rest(pauseTime));
        }

        if (canMove)
        {
            if (anim != null)
            {
                anim.SetBool("isMoving", true);
            }

            // Move to next target
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
            // Turn towards target location
            transform.LookAt(patrolPoints[targetPoint], Vector3.up);
        }
        
    }

    // Increment target location index on arrival
    void increaseTargetInt()
    {
        targetPoint++;
        if (targetPoint == patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
    
    // Wait before being able to move again
    IEnumerator Rest(float pauseTimer)
    {
        yield return new WaitForSeconds(pauseTimer);
        canMove = true;
    }
}

