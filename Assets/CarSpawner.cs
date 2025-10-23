using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour
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

    private Transform[] lastCarInLane;

    void Awake()
    {
        if (laneXs == null || laneXs.Length == 0)
        {
            Debug.LogWarning("CarSpawner: lane is empty. Add lane center X positions.");
            laneXs = new float[0];
        }
        lastCarInLane = new Transform[laneXs.Length];
    }

    void Start()
    {
        // start an independent loop per lane
        for (int i = 0; i < laneXs.Length; i++)
            StartCoroutine(PerLaneLoop(i));
    }

    IEnumerator PerLaneLoop(int laneIndex)
    {
        var shortWait = new WaitForSeconds(0.1f);

        while (true)
        {
            if (npcCarPrefab == null || laneXs.Length == 0) yield return shortWait;

            if (!IsPlayerBlockingLane(laneIndex) && IsSpacingOk(laneIndex))
            {
                SpawnInLane(laneIndex, spawnY);
                yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
            }
            else
            {
                yield return shortWait;
            }
        }
    }

    bool IsPlayerBlockingLane(int laneIndex)
    {
        if (player == null) return false;

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
        float x = laneXs[laneIndex];
        var pos = new Vector3(x, yPos, 0f);
        var car = Instantiate(npcCarPrefab, pos, Quaternion.identity);

        NPCcarSound soundScript = car.GetComponent<NPCcarSound>();
        soundScript.player = player;

        lastCarInLane[laneIndex] = car.transform; // track the newest one for spacing
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (laneXs != null)
            foreach (var x in laneXs)
                Gizmos.DrawLine(new Vector3(x, spawnY - 0.2f, 0f), new Vector3(x, spawnY + 0.2f, 0f));
    }
}
