using System.ComponentModel;
using UnityEngine;

public class NpcCar : MonoBehaviour
{
    [SerializeField] float minSpeed = .01f;
    [SerializeField] public float maxSpeed = 0.05f;
    [SerializeField] private LayerMask obstacleMask;

    private BoxCollider2D boxCollider = null;

    public float despawnY = 10f;

    Rigidbody2D rb;
    public float speed;

    private bool slowingDown = false;

    private int carCount = 0;

    void Start()
    {
        boxCollider = transform.Find("CollisionPrevention").GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.up * speed * Time.fixedDeltaTime);

        if (carCount >= 1)
        {
            speed = 0.2f;
        }
        else
        {
            speed = Random.Range(minSpeed, maxSpeed);
        }
        // if (transform.position.y > despawnY) Destroy(gameObject);
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        carCount++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        carCount--;
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
