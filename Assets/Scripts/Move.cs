using System.IO;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 goal = new Vector3(5, 0, 4);
    public float speed = 0.2f;

    void Start()
    {

    }

    private void Update()
    {
        this.transform.Translate(goal.normalized * speed * Time.deltaTime);
    }
}
