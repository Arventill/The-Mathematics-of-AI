using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public GameObject fuel;
    public float distance;
    bool autopilot = false;

    void Start()
    {

    }

    void CalculateAngle()
    {
        Vector3 tF = this.transform.up;
        Vector3 fD = fuel.transform.position - this.transform.position;

        float angle = Mathf.Acos((tF.x * fD.x + tF.y * fD.y)/(tF.magnitude * fD.magnitude));

        Debug.DrawRay(this.transform.position, tF * 10, Color.green, 2);
        Debug.DrawRay(this.transform.position, fD, Color.red, 2);

        Debug.Log("Angle: " + angle * Mathf.Rad2Deg);
        Debug.Log("UnityAngle: " + Vector3.Angle(tF, fD));

        short clockwise = 1;
        if (Cross(tF, fD).z < 0)
        {
            clockwise = -1;
        }

        this.transform.Rotate(new Vector3(0, 0, angle * Mathf.Rad2Deg * clockwise * 0.02f));
    }

    Vector3 Cross(Vector3 v, Vector3 w)
    {
        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.z * w.x - v.x * w.z;
        float zMult = v.x * w.y - v.y * w.x;

        return new Vector3(xMult, yMult, zMult);
    }

    float CalculateDistance()
    {
        Vector3 tankPosition = this.transform.position;
        Vector3 fuelPosition = fuel.transform.position;

        distance = Mathf.Sqrt(
            Mathf.Pow(tankPosition.x - fuelPosition.x, 2) 
            + Mathf.Pow(tankPosition.y - fuelPosition.y, 2)
            + Mathf.Pow(tankPosition.z - fuelPosition.z, 2));
        float unityDistance = Vector3.Distance(tankPosition, fuelPosition);

        Debug.Log("Distance: " + distance);
        Debug.Log("UnityDistance: " + unityDistance);

        return unityDistance;
    }

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CalculateDistance();
            CalculateAngle();
        }

        if (Input.GetKeyDown(KeyCode.T))
            autopilot = autopilot == false ? true : false;

        
    }

    private void LateUpdate()
    {


        if (autopilot && CalculateDistance() > 2.2f)
        {
            CalculateAngle();
            var destination2 = fuel.transform.position - this.transform.position;
            this.transform.Translate(this.transform.up.normalized * speed * Time.deltaTime, Space.World);
        }
    }
}