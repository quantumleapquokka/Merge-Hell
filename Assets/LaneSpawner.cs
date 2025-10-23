using System.Collections.Generic;
using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    [Header("What to spawn")]
    public GameObject lanePrefab;

    [Header("Placement (Y axis)")]
    public float startY = 0f;
    public float spacingY = 6f;
    public int prewarm = 4;

    [Header("Follow target (camera or player)")]
    public Transform follow;
    public float aheadDistance = 40f;
    public float behindCullDistance = 25f;

    [Header("Limits")]
    [Min(1)] public int maxActive = 10;

    [Header("Parenting (optional)")]
    public Transform container;

    private readonly Queue<Transform> active = new Queue<Transform>();
    private float nextY;

    void Start()
    {
        if (lanePrefab == null)
        {
            Debug.LogWarning("LaneSpawner: lanePrefab not set.");
            enabled = false;
            return;
        }

        nextY = startY;

        // prewarm initial lanes
        for (int i = 0; i < prewarm; i++)
            SpawnNext();
    }

    void Update()
    {
        float refY = follow ? follow.position.y : transform.position.y;

        while (active.Count < maxActive && nextY <= refY + aheadDistance)
        {
            SpawnNext();
        }

        while (active.Count > 0 &&
              (active.Count > maxActive ||
               active.Peek().position.y < refY - behindCullDistance))
        {
            var oldest = active.Dequeue();
            if (oldest) Destroy(oldest.gameObject);
        }
    }

    void SpawnNext()
    {
        var pos = new Vector3(transform.position.x, nextY, 0f);
        var go = Instantiate(lanePrefab, pos, Quaternion.identity, container ? container : null);
        active.Enqueue(go.transform);
        nextY += spacingY;
    }

    void OnDrawGizmosSelected()
    {
        if (!follow) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector3(transform.position.x - 0.5f, follow.position.y + aheadDistance, 0f),
                        new Vector3(transform.position.x + 0.5f, follow.position.y + aheadDistance, 0f));
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(transform.position.x - 0.5f, follow.position.y - behindCullDistance, 0f),
                        new Vector3(transform.position.x + 0.5f, follow.position.y - behindCullDistance, 0f));
    }
}
