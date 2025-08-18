using UnityEngine;

public class RoomData
{
    public Vector3Int GridPos;
    public bool North, South, East, West;
    public bool IsBossRoom;
    public bool IsKeyRoom;
    public bool IsSpawnRoom;

    public RoomData(Vector3Int pos) { GridPos = pos; }
}