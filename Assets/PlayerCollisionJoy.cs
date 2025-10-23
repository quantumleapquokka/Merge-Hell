using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionJoy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Destroy(collision.gameObject);
        }
    }
}
