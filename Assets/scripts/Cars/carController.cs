using UnityEngine;

public class carController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 180f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get arrow key input
        float moveInput = Input.GetAxis("Vertical");   // Up/Down arrows (or W/S)
        float turnInput = Input.GetAxis("Horizontal"); // Left/Right arrows (or A/D)

        // Move the car forward/backward
        transform.Translate(Vector3.up * moveInput * moveSpeed * Time.deltaTime);

        // Rotate the car
        transform.Rotate(Vector3.forward * -turnInput * turnSpeed * Time.deltaTime);
    }
}
