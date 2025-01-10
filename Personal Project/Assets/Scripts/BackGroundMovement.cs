using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMovement : MonoBehaviour
{
    private Vector3 startPosition;
    private float repeatWidth;
    private float zBoundaryTree = 16.0f;
    private float zBoundaryCloud = -50.0f;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.z;
    }

    // Update is called once per frame
    void Update()
    {
        //Deals with either destorying background objects that are no longer needed or moving the "Earth" back itself to make it look like its infinite
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if(transform.position.z < startPosition.z - repeatWidth && gameObject.CompareTag("Earth"))
        {
            transform.position = startPosition;
        }

        if (transform.position.z < -zBoundaryTree && gameObject.CompareTag("Tree"))
        {
            Destroy(gameObject);
        }
        if (transform.position.z < zBoundaryCloud && gameObject.CompareTag("Cloud"))
        {
            Destroy(gameObject);
        }
    }
}
