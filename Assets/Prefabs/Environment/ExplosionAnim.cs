using UnityEngine;

public class ExplosionAnim : MonoBehaviour
{
    ParticleSystem explosion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        explosion = GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (explosion.isStopped)
        {
            Destroy(this.gameObject);
        }
    }
}
