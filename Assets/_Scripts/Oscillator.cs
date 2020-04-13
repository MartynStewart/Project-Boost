using UnityEngine;

[DisallowMultipleComponent]

public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 0f);
    [SerializeField] float period = 2f;

    private Vector3 startingPos;
    private Vector3 offset;
    private float movementFactor;


    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        float position = cycles - Mathf.Floor(cycles);
        float sinInput = Mathf.PI * 2 * position;

        movementFactor = Mathf.Sin(sinInput)/2+0.5f;
        offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
