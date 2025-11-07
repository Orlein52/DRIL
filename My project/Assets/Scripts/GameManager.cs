using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject[] mazes;
    public GameObject badRoom;
    public GameObject bossRoom;
    Transform player;
    int roomNum;
    public GameObject connector;
    int conNum;
    Ray2D conRay;
    Rooms room;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        conNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RoomSpawn(GameObject currentRoom)
    {
        room = currentRoom.GetComponent<Rooms>();
        if (conNum < 4)
        {
            
            roomNum = Random.Range(0, rooms.Length);
            
            connector = room.connectors[conNum];
            RaycastHit2D conRayHit = Physics2D.Raycast(connector.transform.position, connector.transform.up, 1f);
            if (conRayHit)
            {
                Debug.Log("hit");
                conNum++;
                RoomSpawn(currentRoom);
            }
            if (!conRayHit)
            {
                Instantiate(rooms[roomNum].gameObject, connector.transform.position, transform.rotation);
                conNum++;
                RoomSpawn(currentRoom);
            }
        }
        if (conNum == 4)
        {
            conNum = 0;
        }
    }
}
