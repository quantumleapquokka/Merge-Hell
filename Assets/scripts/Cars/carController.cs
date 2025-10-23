using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class carController : MonoBehaviour
{
    public float speedLimiter = 1.0f;
    public float moveSpeed = 5f;
    public float turnSpeed = 180f;

    public bool invincible = false;

    public GameObject winTextObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // GameObject winText = transform.Find("Canvas").gameObject;
        // winText.SetActive(true);
        // winText.GetComponent<TextMeshPro>().enabled = true;
        if (winTextObject != null)
        {
            winTextObject.SetActive(true);
            var text = winTextObject.GetComponent<TextMeshPro>();
            if (text != null)
                text.enabled = true;
        }
        else
        {
            Debug.LogWarning("winTextObject not assigned in carController!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get arrow key input
        float moveInput = Input.GetAxis("Vertical");   // Up/Down arrows (or W/S)
        float turnInput = Input.GetAxis("Horizontal"); // Left/Right arrows (or A/D)

        // Move the car forward/backward
        transform.Translate(Vector3.up * moveInput * moveSpeed * speedLimiter * Time.deltaTime);

        // Rotate the car
        transform.Rotate(Vector3.forward * -turnInput * turnSpeed * Time.deltaTime);
    }
    public void displayWin()
    {
        GameObject winText = transform.Find("Canvas").gameObject;
        winText.SetActive(true);
        winText.GetComponent<TextMeshPro>().enabled = true;
        invincible = true;
    }
}