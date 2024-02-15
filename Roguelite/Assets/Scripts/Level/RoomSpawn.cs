using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomSpawn : MonoBehaviour
{
    public enum Direction { None,N, E, S, W };

    public Direction direction;

    public static int TotalRooms;

    public static bool EndPointSpawned = false;
   public static int amountSpawned = 0;

    private RoomPrefabArrays prefabArrays;
    public  GameObject Walkway;

    public GameObject Room;
  
    private bool occupiedSpace = false;


    public RoomConnections roomConnections;


    // Start is called before the first frame update
    void Start()
    {
        prefabArrays = GameObject.Find("Level Manager").GetComponent<RoomPrefabArrays>();

        Invoke("Spawn", 0.2f);
    }

    

    // Update is called once per frame
   public void Spawn()
   {
        GameObject newRoom = null;
        if (amountSpawned < TotalRooms && !occupiedSpace)
        {
            GameObject[] spawnableRooms = prefabArrays.Rooms;

            int random = Random.Range(0, spawnableRooms.Length);

            newRoom = Instantiate(spawnableRooms[random], this.transform.position, this.transform.rotation);
            newRoom.GetComponent<RoomOverlapCheck>().Connection = this.gameObject;
             amountSpawned++;
            
        }
        else if (amountSpawned >= TotalRooms)
        {
            if(!EndPointSpawned)
            {
                EndPointSpawned = true;
            }


            DestroyWalkWay();
        }
        else
        {
            DestroyWalkWay();
        }
       
       
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Room"))
        {
            occupiedSpace = true;
        }

    }

   public void DestroyWalkWay()
    {
        
        roomConnections.WalkwayDestroyed(Room);
    }

}
