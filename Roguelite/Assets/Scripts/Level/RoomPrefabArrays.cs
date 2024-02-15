using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPrefabArrays : MonoBehaviour
{
    // Start is called before the first frame update
    public  GameObject[] bottomRoom;
    public  GameObject[] leftRoom;
    public  GameObject[] topRoom;
    public  GameObject[] rightRoom;

    public  GameObject[] Rooms;

    public  GameObject blockRoom;

    public int RoomAmount;


    private void Awake()
    {
        RoomSpawn.amountSpawned = 0;

        RoomOverlapCheck.OverlappingObjects.Clear();

        RoomSpawn.TotalRooms = RoomAmount;

        RoomSpawn.EndPointSpawned = false;
    }

}
