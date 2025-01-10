using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed = 4.0f;
    private float zBoundary = 17.0f;
    public int pointValue;
    private AudioSource collision;
    public AudioClip sound;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        collision = gameManager.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * -speed * Time.deltaTime);

        if(transform.position.z < -zBoundary)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && CompareTag("Balloon"))
        {
            Debug.Log("Player hit a balloon");
            gameManager.UpdateScore(3);
            collision.PlayOneShot(sound);
            Destroy(gameObject);
        }
        else if(other.CompareTag("Player") && CompareTag("Enemy"))
        {
            Debug.Log("Player hit an enemy");
            gameManager.GameOver();
            collision.PlayOneShot(sound);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Bullet") && CompareTag("Balloon"))
        {
            Debug.Log("Player shot down balloon");
            gameManager.UpdateScore(5);
            collision.PlayOneShot(sound);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Bullet") && !CompareTag("Civillian"))
        {
            Debug.Log("Player shot an enemy");
            collision.PlayOneShot(sound);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if(other.CompareTag("Bullet") && CompareTag("Civillian"))
        {
            Debug.Log("Player shot a civillian");
            collision.PlayOneShot(sound);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player") && CompareTag("Civillian"))
        {
            Debug.Log("Player crashed into a civillian");
            collision.PlayOneShot(sound);
            gameManager.GameOver();
        }

    }
}
