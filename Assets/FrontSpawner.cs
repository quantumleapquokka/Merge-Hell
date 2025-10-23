using UnityEngine;
using System.Collections;

public class FrontSpawner : MonoBehaviour
{
    [Header("What to spawn")]
    public NpcCar npcCarPrefab;

    [Header("Where to spawn")]
    public float[] laneXs;
    public float spawnY = -6f;
    public float laneTolerance = 0.5f;

    [Header("Flow / spacing")]
    public float gapDistance = 4f;
    public float minInterval = 0.6f; 
    public float maxInterval = 1.3f;

    [Header("Player blocking (pause)")]
    public Transform player; 
    public float playerBlockAhead = 2.5f;

    [Header("Limits")]
    [Tooltip("How many cars this lane is allowed to spawn before stopping.")]
    public int perLaneSpawnLimit = 2;

    public float speedLimiter = 0.02f;

    private Transform[] lastCarInLane;

    public int wavesToSpawn = 3;

    void Awake()
    {
        if (laneXs == null || laneXs.Length == 0)
        {
            Debug.LogWarning("FrontSpawner: laneXs is empty. Add lane center X positions.");
            laneXs = new float[0];
        }
        lastCarInLane = new Transform[laneXs.Length];
    }

    void Start()
    {
        if (npcCarPrefab == null)
        {
            Debug.LogWarning("FrontSpawner: npcCarPrefab is not set.");
            return;
        }

        // start an independent loop per lane
        for (int i = 0; i < laneXs.Length; i++)
        {
            StartCoroutine(PerLaneLoop(i));
        }
    }

    IEnumerator PerLaneLoop(int laneIndex)
    {
        var shortWait = new WaitForSeconds(0.1f);
        int spawnCount = 0;

        while ((wavesToSpawn > 0) && (spawnCount < perLaneSpawnLimit))
        {
            if (npcCarPrefab == null || laneXs.Length == 0)
            {
                yield return shortWait;
                continue;
            }

            if (!IsPlayerBlockingLane(laneIndex) && IsSpacingOk(laneIndex))
            {
                SpawnInLane(laneIndex, spawnY);
                spawnCount++;
                wavesToSpawn--;

                float wait = Mathf.Max(0f, Random.Range(minInterval, maxInterval));
                yield return new WaitForSeconds(wait);
            }
            else
            {
                yield return shortWait;
            }
        }

        Debug.Log($"FrontSpawner: Lane {laneIndex} finished spawning {spawnCount}/{perLaneSpawnLimit} cars.");
    }

    bool IsPlayerBlockingLane(int laneIndex)
    {
        if (player == null || laneIndex < 0 || laneIndex >= laneXs.Length) return false;

        bool sameLane = Mathf.Abs(player.position.x - laneXs[laneIndex]) <= laneTolerance;

        bool playerAheadNearSpawn = (player.position.y >= spawnY) &&
                                    (player.position.y - spawnY <= playerBlockAhead);

        return sameLane && playerAheadNearSpawn;
    }

    bool IsSpacingOk(int laneIndex)
    {
        var t = lastCarInLane[laneIndex];
        if (!t) return true;

        float dist = t.position.y - spawnY;
        return dist >= gapDistance;
    }

    void SpawnInLane(int laneIndex, float yPos)
    {
        if (laneIndex < 0 || laneIndex >= laneXs.Length) return;

        float x = laneXs[laneIndex];
        var pos = new Vector3(x, yPos, 0f);
        var car = Instantiate(npcCarPrefab, pos, Quaternion.identity);

        NpcCar carScript = car.GetComponent<NpcCar>();
        carScript.maxSpeed = 0.5f;

        lastCarInLane[laneIndex] = car != null ? car.transform : null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (laneXs != null)
        {
            foreach (var x in laneXs)
            {
                Gizmos.DrawLine(new Vector3(x, spawnY - 0.2f, 0f),
                                new Vector3(x, spawnY + 0.2f, 0f));
            }
        }
    }
}
