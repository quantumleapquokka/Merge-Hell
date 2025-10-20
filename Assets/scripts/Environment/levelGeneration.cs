using UnityEngine;

public class levelGeneration : MonoBehaviour
{
    GameObject laneSprite;
    public SpriteRenderer lanes;
    public float width = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lanes = GetComponentInChildren<SpriteRenderer>();
        lanes.size = new Vector2(width * 0.1f, 0.5f);
        //Debug.Log("Start!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
