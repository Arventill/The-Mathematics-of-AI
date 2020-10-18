using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveGold : MonoBehaviour
{
    public float speed = 40f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown("right"))
            this.transform.Translate(new Vector3(0,0,1).normalized * speed * Time.deltaTime, Space.World);
    }
}
