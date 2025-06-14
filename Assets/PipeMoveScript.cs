using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 4;
    public float deadZone = -30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed * Time.deltaTime);

        if (transform.position.x < deadZone)
        {
            Debug.Log("Pipe Deleted");
            Destroy(gameObject);
        }
    }
}
