using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConnections : MonoBehaviour
{
    public GameObject[] PossibleDorways;

    public int ExitCount = 0;

    public GameObject DoorWay;
    public GameObject Wall;
    public GameObject Connection;

    bool exitCreated = false;

    private void Start()
    {
        foreach (GameObject p in PossibleDorways)
        {
            int r = Random.Range(0, 2);

            if(r == 0)
            {
                CreateConnection(p);
            }
        }
        TrySpawnRoom();
        
    }

    void TrySpawnRoom()
    {
        if (ExitCount > 0 ) return;
        foreach (GameObject p in PossibleDorways)
        {
            if (p != null)
            {
                CreateConnection(p);
                exitCreated = true;
            }
        }
    }

    void CreateConnection(GameObject WallToReplace)
    {
        GameObject CreatedDoorway = Instantiate(DoorWay, WallToReplace.transform.position, WallToReplace.transform.rotation);
        GameObject newConnection = Instantiate(Connection, CreatedDoorway.transform.localPosition, CreatedDoorway.transform.rotation);

        CreatedDoorway.transform.SetParent(transform);


        newConnection.transform.SetParent(CreatedDoorway.transform);
        newConnection.transform.localPosition = new Vector3(-2.552f, 0f, -5.99f);
        newConnection.transform.localRotation = Quaternion.Euler(0, 180, 0);

        newConnection.GetComponentInChildren<RoomSpawn>().roomConnections = this;
        newConnection.GetComponentInChildren<RoomSpawn>().Room = CreatedDoorway;
       
        Destroy(WallToReplace);
        ExitCount++;
    }

    public void WalkwayDestroyed(GameObject door)
    {
        if (ExitCount <= 0)
            ExitCount = 0;
        else
        {
            ExitCount--;
        }

        
        TrySpawnRoom();
        GameObject newWall = Instantiate(Wall, door.transform.position, door.transform.rotation);
        newWall.transform.SetParent(transform);
        Destroy(door);
    }


}
