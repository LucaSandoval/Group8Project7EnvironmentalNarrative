using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarabAI : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float turnSpeed;

    private Vector3 targetPos;
    private Vector3 startPos;
    public float moveRadius;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        timer = Random.Range(0.5f, 4);

        //InvokeRepeating("PickNewPoint", 0, 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveDelta = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        transform.position = Vector3.Lerp(transform.position, moveDelta, Time.deltaTime * speed);

        Vector3 directionTarget = (transform.position - targetPos).normalized;
        directionTarget.y = 0;
        if (directionTarget != Vector3.zero)
        {
            Quaternion lookRoation = Quaternion.LookRotation(directionTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, Time.deltaTime * turnSpeed);
        }     
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            timer = Random.Range(0.5f, 4);
            PickNewPoint();
        }
    }

    void PickNewPoint()
    {
        targetPos = startPos + new Vector3(Random.Range(-moveRadius, moveRadius), 0, Random.Range(-moveRadius, moveRadius));
    }
}
