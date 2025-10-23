using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionJoy : MonoBehaviour
{
    UnityEngine.Object explosion;
    void Start()
    {
       explosion = Resources.Load("Explosion");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            GameObject target = collision.gameObject;

            UnityEngine.Object explosionInstance = Instantiate(explosion, target.transform.position, target.transform.rotation);
            Destroy(collision.gameObject);
        }
    }
}
