using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToGoal : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform goal;
    public float accuracy = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.LookAt(goal.position);
    }

    // Update is called once per frame
    private void Update()
    {
        this.transform.LookAt(goal.position);
    }


    void LateUpdate()
    {
        Vector3 destination = goal.position - this.transform.position;
        Debug.DrawRay(this.transform.position, destination, Color.red);
        if(destination.magnitude >= accuracy)
            this.transform.Translate(destination.normalized * speed * Time.deltaTime, Space.World);
    }
}
