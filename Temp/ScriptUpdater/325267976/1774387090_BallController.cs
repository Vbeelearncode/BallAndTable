using UnityEngine;

public class BallController : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 initialVelocity;
    private Rigidbody rigidBody;

    public AudioClip ballRollClip, ballHitClip;
    private AudioSource tableCollisionAudioSource;

    private float jumpForce = 5.0f;
    private bool isTouchingTable=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tableCollisionAudioSource = gameObject.AddComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
        initialPosition = rigidBody.transform.position;
        initialVelocity = rigidBody.linearVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R)) {
            rigidBody.transform.position = initialPosition;
            rigidBody.linearVelocity = initialVelocity;
        } 

        if (Input.GetKey(KeyCode.Space) && isTouchingTable)
        {
            rigidBody.linearVelocity = new Vector3(rigidBody.linearVelocity.x, jumpForce, rigidBody.linearVelocity.z);
        }

        tableCollisionAudioSource.volume = rigidBody.linearVelocity.magnitude/5.0f;

        if (isTouchingTable && rigidBody.linearVelocity != Vector3.zero && !tableCollisionAudioSource.isPlaying) {
            tableCollisionAudioSource.PlayOneShot(ballRollClip);
        }
    }

    public void OnCollisionEnter(Collision collision) {
        Debug.Log("touched the table");
        if (collision.gameObject.tag == "Table" && isTouchingTable == false) {
            isTouchingTable = true;
            tableCollisionAudioSource.PlayOneShot(ballHitClip);
        }
    }

    public void OnCollisionExit(Collision collision) {
        Debug.Log("exitted the table");
        isTouchingTable = false;
    }
}
