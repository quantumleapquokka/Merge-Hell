using System.ComponentModel;
using UnityEngine;

public class NpcCar : MonoBehaviour
{
    [SerializeField] float minSpeed = 2f;
    [SerializeField] float maxSpeed = 3.5f;
    [SerializeField] private LayerMask obstacleMask;

    private BoxCollider2D boxCollider = null;

    public float despawnY = 10f;

    Rigidbody2D rb;
    float speed;

    void Start()
    {
        boxCollider = transform.Find("CollisionPrevention").GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.up * speed * Time.fixedDeltaTime);
        // if (transform.position.y > despawnY) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with " + collision.name);
        speed = Vector2.Distance(transform.position, collision.transform.position) * 0.01f;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exited collision with " + other.name);
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);

        // randomize car color
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Random.ColorHSV(0f, 1f, 0.2f, 0.7f, 0.8f, 1f); 
            //Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax)
        }
    }

}
