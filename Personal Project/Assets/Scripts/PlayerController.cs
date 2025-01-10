using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float xSpeed = 25.0f;
    private float zSpeed = 10.0f;
    //private float rotationZ = 0f;
    private float horizontalBoundary = 20;
    private float verticalBoundary = 14;
    private float heightBoundary = 0.86f;
    //private float newZ;
    private int bulletCount;
    private int bulletLimit = 2;
    public float horizontalInput;
    public float verticalInput;

    //private Vector3 startRotation;

    private Rigidbody rb;
    public GameObject bullet;
    private GameManager gameManager;
    private AudioSource playerAudio;
    public AudioClip gunFire;
    public AudioClip explosion;
    public GameObject explosionParticles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        bulletCount = 0;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletCount = GameObject.FindGameObjectsWithTag("Bullet").Length;
        if (gameManager.isGameActive)
        {
            MovePlayer();
            KeepInBounds();
            Shoot();
            LockedRotation();
        }
    }

    //Keeps the player inside the game boundaries
    private void KeepInBounds()
    {
        if (transform.position.x > horizontalBoundary)
        {
            transform.position = new Vector3(horizontalBoundary, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -horizontalBoundary)
        {
            transform.position = new Vector3(-horizontalBoundary, transform.position.y, transform.position.z);
        }

        if (transform.position.z > verticalBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, verticalBoundary);
        }
        else if (transform.position.z < -verticalBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -verticalBoundary);
        }

        if (transform.position.y != heightBoundary)
        {
            transform.position = new Vector3(transform.position.x, heightBoundary, transform.position.z);
        }
    }

    //Moves the player when WASD or the arrow keys are used
    private void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * zSpeed * verticalInput * Time.deltaTime);
        transform.Translate(Vector3.right * xSpeed * horizontalInput * Time.deltaTime);

    }

    //Shoot when the space bar is pressed
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bulletCount <= bulletLimit)
        {
            //Debug.Log("Pew");
            Vector3 bulletPos = transform.position + new Vector3(0, 0, 1.0f);
            Instantiate(bullet, bulletPos, bullet.transform.rotation);
            playerAudio.PlayOneShot(gunFire, 0.5f);
        }
    }

    //Locks the planes rotation between -15 and 15 degrees
    private void LockedRotation()
    {
        /*
         rotationZ += Input.GetAxis("Horizontal");
         rotationZ = Mathf.Clamp(rotationZ, -15, 15);

         transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -rotationZ); 

        if(Input.GetAxis("Horizontal") == 0)
        {
            var ang = transform.localEulerAngles;
            var newZ = Mathf.Lerp(ang.z, startRotation.z, Time.deltaTime * 10.0f);
            transform.localEulerAngles = new Vector3(startRotation.x, startRotation.y, newZ);
            Debug.Log(transform.localEulerAngles.z);
        }*/

        /*
        var ang = transform.localEulerAngles;
        var rotationSpeed = 10.0f;
        float newZ = Mathf.Lerp(ang.z, Input.GetAxisRaw("Horizontal") * -15f, Time.deltaTime * rotationSpeed);
        transform.localEulerAngles = new Vector3(ang.x, ang.y, newZ);
        Debug.Log(transform.localEulerAngles.z); */

        /*
        var ang = transform.localEulerAngles;
        var rotationSpeed = 50.0f;
        newZ = ang.z + (rotationSpeed * Time.deltaTime * -Input.GetAxis("Horizontal"));
        newZ = Mathf.Min(newZ, 15);
        newZ = Mathf.Max(newZ, -15);
        transform.localEulerAngles = new Vector3(ang.x, ang.y, newZ);
        Debug.Log(transform.localEulerAngles.z);
        */

        var newZ = Input.GetAxis("Horizontal") * -30.0f;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, newZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Balloon") && !other.CompareTag("Bullet") && !other.CompareTag("Cloud"))
        {
            Debug.Log("Player Died");
            gameManager.gameAudio.PlayOneShot(explosion, 1.0f);
            Instantiate(explosionParticles, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
