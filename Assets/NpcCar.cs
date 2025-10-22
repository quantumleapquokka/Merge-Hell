using System.ComponentModel;
using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class NpcCar : MonoBehaviour
{
    [SerializeField] float minSpeed = 2f;
    [SerializeField] float maxSpeed = 3.5f;
    [SerializeField] private LayerMask obstacleMask;

    private Component<BoxCollider2D> boxCollider = null;

    public float despawnY = 10f;

    Rigidbody2D rb;
    float speed;

    void Start()
    {
        boxCollider = transform.find("CollisionPrevention").GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.up * speed * Time.fixedDeltaTime);
        // if (transform.position.y > despawnY) Destroy(gameObject);
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
