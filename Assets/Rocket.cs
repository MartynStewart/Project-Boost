using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    private Rigidbody rigidBody;
    private AudioSource audio;
    public float speed = 2f;
    public float rotSpeed = 20f;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        ProcessInput();
    }

    void ProcessInput() {
        if (Input.GetKey(KeyCode.Space)) {
            print("Space Pressed");
            rigidBody.AddRelativeForce(Vector3.up * speed);
            if (!audio.isPlaying) {
                audio.Play();
            }
        } else {
            audio.Stop();
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * rotSpeed * Time.deltaTime);
        }

    }
}
