using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject northDoor;
    public GameObject southDoor;
    public GameObject eastDoor;
    public GameObject westDoor;
    
    public bool hasNorthDoor;
    public bool hasSouthDoor;
    public bool hasEastDoor;
    public bool hasWestDoor;
    public bool isBossRoom;
    public bool isKeyRoom;
    public bool isSpawnRoom;

    private void Start()
    {
        northDoor.SetActive(!hasNorthDoor);
        southDoor.SetActive(!hasSouthDoor);
        eastDoor.SetActive(!hasEastDoor);
        westDoor.SetActive(!hasWestDoor);
    }
    
    public void SetDoors(bool north, bool south, bool east, bool west)
    {
        hasNorthDoor = north;
        hasSouthDoor = south;
        hasEastDoor = east;
        hasWestDoor = west;

        northDoor.SetActive(!north);
        southDoor.SetActive(!south);
        eastDoor.SetActive(!east);
        westDoor.SetActive(!west);
    }
}