using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    [SerializeField] private float mainthrust;
    [SerializeField] private float rotSpeed;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        Thrust();
        Rotate();
    }

    private void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up * mainthrust * Time.deltaTime);
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        } else {
            audioSource.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision) {

        string tag = collision.gameObject.tag;

        if (tag == "Friendly") {
            //Rest and do nothing
        } else if (tag == "Finish") {
            //Run level win outcome
            Debug.Log("A winrar is you");
        } else {
            //Colided with a baddie
        }   
    }

    private void OnTriggerEnter(Collider other) {
        string tag = other.gameObject.tag;

        if (tag == "Fuel") {
            Destroy(other.gameObject);
            Debug.Log("Collected fuel");
        }
    }

    void Rotate() {

        if (Input.GetKey(KeyCode.A)) {
            rigidBody.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D)) {
            rigidBody.freezeRotation = true;
            transform.Rotate(-Vector3.forward * rotSpeed * Time.deltaTime);
        }
        else if(transform.rotation.z != 0) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0),  Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        rigidBody.freezeRotation = false;
    }

}
