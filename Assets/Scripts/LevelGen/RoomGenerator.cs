using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine.AI;

public class RoomGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs; 
    public GameObject roomBoss;      
    public GameObject roomSpawn;     
    public GameObject roomWithKey;   

    public int roomCount = 10;
    public float roomSpacing = 50.0f; 

    private HashSet<Vector3Int> occupied = new HashSet<Vector3Int>();
    private List<RoomData> rooms = new List<RoomData>();

    private int stepXZ;
    private bool bossPlaced = false;
    private bool keyPlaced = false;
    private NavMeshSurface npcSurface;

    private void Start()
    {
        npcSurface = GetComponent<NavMeshSurface>();
        stepXZ = Mathf.RoundToInt(roomSpacing);

        GenerateRoomsRecursive(Vector3Int.zero, null, true);

        InstantiateRooms();

        StartCoroutine(BakeNavMeshSurface());
        StartCoroutine(InstanciateNPCs());
    }

    private void GenerateRoomsRecursive(Vector3Int pos, RoomData fromRoom, bool isSpawn = false)
    {
        if (occupied.Contains(pos) || rooms.Count >= roomCount) return;

        var newRoom = new RoomData(pos);
        rooms.Add(newRoom);
        occupied.Add(pos);

        if (isSpawn)
        {
            newRoom.IsSpawnRoom = true;
        }
        else
        {
            if (!bossPlaced && rooms.Count > roomCount / 2)
            {
                newRoom.IsBossRoom = true;
                bossPlaced = true;
            }
            else if (!keyPlaced && rooms.Count > roomCount / 3)
            {
                newRoom.IsKeyRoom = true;
                keyPlaced = true;
            }
        }

        if (fromRoom != null)
        {
            Vector3Int delta = pos - fromRoom.GridPos;
            Vector3Int unit = NormalizeDelta(delta);
            ConnectRooms(fromRoom, newRoom, unit);
        }

        var dirs = new List<Vector3Int>
        {
            Vector3Int.forward, Vector3Int.back,
            Vector3Int.left, Vector3Int.right
        };
        Shuffle(dirs);

        foreach (var unitDir in dirs)
        {
            if (rooms.Count >= roomCount) break;

            Vector3Int nextPos = pos + new Vector3Int(unitDir.x * stepXZ, 0, unitDir.z * stepXZ);

            if (!occupied.Contains(nextPos))
            {
                GenerateRoomsRecursive(nextPos, newRoom);
            }
        }
    }

    private void ConnectRooms(RoomData a, RoomData b, Vector3Int unitDir)
    {
        if (unitDir == Vector3Int.forward) { a.North = true; b.South = true; }
        else if (unitDir == Vector3Int.back) { a.South = true; b.North = true; }
        else if (unitDir == Vector3Int.left) { a.West = true; b.East = true; }
        else if (unitDir == Vector3Int.right) { a.East = true; b.West = true; }
    }

    private Vector3Int NormalizeDelta(Vector3Int d)
    {
        int nx = d.x == 0 ? 0 : (d.x > 0 ? 1 : -1);
        int ny = d.y == 0 ? 0 : (d.y > 0 ? 1 : -1);
        int nz = d.z == 0 ? 0 : (d.z > 0 ? 1 : -1);
        return new Vector3Int(nx, ny, nz);
    }

    private void InstantiateRooms()
    {
        foreach (var data in rooms)
        {
            GameObject prefabToUse;

            if (data.IsSpawnRoom) prefabToUse = roomSpawn;
            else if (data.IsBossRoom) prefabToUse = roomBoss;
            else if (data.IsKeyRoom) prefabToUse = roomWithKey;
            else prefabToUse = roomPrefabs[Random.Range(0, roomPrefabs.Length)];

            Vector3 worldPos = new Vector3(data.GridPos.x, data.GridPos.y, data.GridPos.z);
            var room = Instantiate(prefabToUse, worldPos, Quaternion.identity).GetComponent<Room>();

            room.SetDoors(data.North, data.South, data.East, data.West);
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int j = Random.Range(i, list.Count);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
    
    private IEnumerator BakeNavMeshSurface()
    {
        if (npcSurface != null)
        {
            npcSurface.BuildNavMesh();
            yield return null;
        }
    }

    private IEnumerator InstanciateNPCs()
    {
        yield return null;


        GameObject[] spawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
        foreach (GameObject spawner in spawners)
        {
            GameObject enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemies/SecurityRobotEnemy");
            GameObject enemyPathPrefab = Resources.Load<GameObject>("Prefabs/Enemies/EnemyPath");
            GameObject navigationPath = Instantiate(enemyPathPrefab, spawner.transform.position, Quaternion.identity);
            GameObject enemy = Instantiate(enemyPrefab, spawner.transform.position, Quaternion.identity);
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            enemy.GetComponent<EnemyAI>().path = navigationPath.GetComponent<NavigationPath>();
            agent.enabled = false;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(enemy.transform.position, out hit, 5f, NavMesh.AllAreas))
            {
                enemy.transform.position = hit.position;
            }

            agent.enabled = true;
            agent.Warp(enemy.transform.position);
            yield return null;
        }
    }
}
