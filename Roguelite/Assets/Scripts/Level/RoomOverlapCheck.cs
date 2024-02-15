using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEditor.MemoryProfiler;
using UnityEngine;



public class RoomOverlapCheck : MonoBehaviour
{
    public static List<GameObject> OverlappingObjects = new List<GameObject>();

    public List<GameObject> LocalOverlappingObjects;

    public static List<RoomSpawn.Direction> directions = new List<RoomSpawn.Direction>();

    public int TileID = 0;

    public bool Leader = false;

    bool isOverlapping = false;

    public GameObject Connection;

    private void Awake()
    {
        TileID = Random.Range(0, int.MaxValue);
    }

    private void Start()
    {
        LocalOverlappingObjects = new List<GameObject>();
        Invoke ("SetLeader",0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Room") && other.gameObject!=this.gameObject)
        {
            if (!LocalOverlappingObjects.Contains(other.gameObject))
                LocalOverlappingObjects.Add(other.gameObject);

            if(!OverlappingObjects.Contains(other.gameObject))
                OverlappingObjects.Add(other.gameObject);

            isOverlapping = true;
        }

      
    }

    void SetLeader()
    {
        foreach (GameObject obj in LocalOverlappingObjects)
        {
            obj.GetComponent<RoomOverlapCheck>().LocalOverlappingObjects.OrderBy(go => go.GetComponent<RoomOverlapCheck>().TileID);
        }

        foreach (GameObject obj in OverlappingObjects)
        {
            if(LocalOverlappingObjects.Count>=1 && obj == LocalOverlappingObjects[0])
            {
                obj.GetComponent<RoomOverlapCheck>().Leader = true;

                foreach (GameObject room in LocalOverlappingObjects)
                {
                    
                    //obj.GetComponent<RoomConnections>().OpenDirections.Add(room.GetComponent<RoomSpawn>().direction);
                }
            }
        }

        DestroyOverlaps();
    }

    public void DestroyOverlaps()
    {
       if(!Leader && isOverlapping)
       {
            UnityEngine.Debug.Log("Overlap");
            Connection.GetComponent<RoomSpawn>().DestroyWalkWay();
            RoomSpawn.amountSpawned--;
            Destroy(gameObject);
       }
    }
}
