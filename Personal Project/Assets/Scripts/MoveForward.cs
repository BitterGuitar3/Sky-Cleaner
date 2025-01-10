using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private float speed = 20.0f;
    private float zBoundary = 17.0f;
    private AudioSource collision;
    public AudioClip pop;
    public AudioClip birdNoise;
    public AudioClip thunk;
    // Start is called before the first frame update
    void Start()
    {
        collision = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.z > zBoundary)
        {
            Destroy(gameObject);
        }
    }

 /*   private void OnTriggerEnter(Collider other) //NEED TO FIX
    {
        if (other.CompareTag("Balloon"))
        {
            collision.PlayOneShot(pop);
            Debug.Log("Pop");
        }
        else if (other.CompareTag("Enemy"))
        {
            collision.PlayOneShot(birdNoise);
            Debug.Log("Caw");
        }
        else if (other.CompareTag("Civillian"))
        {
            collision.PlayOneShot(thunk);
            Debug.Log("Thunk");
        }
    }*/
}
