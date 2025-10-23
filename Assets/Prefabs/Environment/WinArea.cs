using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WinArea : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            carController player = collision.gameObject.GetComponent<carController>();
            player.invincible = true;
            player.displayWin();
        }
    }
}
