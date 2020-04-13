using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    private bool isKillable = true;
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private GameManager gameManager;
    [SerializeField] private float mainthrust = 20f;
    [SerializeField] private float rotSpeed = 20f;
    [SerializeField] private int levelLoadWait = 2;

    [SerializeField] public AudioClip mainEngine;
    [SerializeField] public AudioClip objectCollision;
    [SerializeField] public AudioClip successAudio;
    [SerializeField] ParticleSystem partEngine;
    [SerializeField] ParticleSystem partCollision;
    [SerializeField] ParticleSystem partSuccess;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update(){
        Thrust();
        Rotate();
    }

    private void Thrust() {
        if (Input.GetKey(KeyCode.Space) && gameManager.isEngineActive && isKillable) {
            rigidBody.AddRelativeForce(Vector3.up * mainthrust * Time.deltaTime);
            gameManager.BurnFuel(Time.deltaTime);

            if (!audioSource.isPlaying) {
                Debug.Log("Playing Sound");
               audioSource.PlayOneShot(mainEngine);
            }
        }
    }

    void Rotate() {
        if (Input.GetKey(KeyCode.A) && isKillable) {
            rigidBody.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D) && isKillable) {
            rigidBody.freezeRotation = true;
            transform.Rotate(-Vector3.forward * rotSpeed * Time.deltaTime);
        }
        else if(transform.rotation.z != 0) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0),  Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        rigidBody.freezeRotation = false;
    }

    private void OnCollisionEnter(Collision collision) {
        string tag = collision.gameObject.tag;
        if (tag == "Friendly") {
            //Rest and do nothing
        } else if (tag == "Finish") {
            //Run level win outcome
            audioSource.Stop();
            audioSource.PlayOneShot(successAudio);
            Invoke("ChangeLevel", levelLoadWait);
        } else {
            //Colided with a baddie
            if (isKillable) {
                isKillable = false;
                audioSource.Stop();
                audioSource.PlayOneShot(objectCollision);
                Invoke("DestroyShip", levelLoadWait);
            }
        }
    }

    private void DestroyShip() {
        gameManager.DestroyShip();
    }

    private void ChangeLevel() {
        gameManager.ChangeLevel();
    }

    private void OnTriggerEnter(Collider other) {
        string tag = other.gameObject.tag;
        if (tag == "Fuel") {
            Destroy(other.gameObject);
            gameManager.AddFuel(100f);
        }
    }

}
