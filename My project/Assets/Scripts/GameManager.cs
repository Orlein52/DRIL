using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject[] mazes;
    public GameObject badRoom;
    public GameObject bossRoom;
    Transform player;
    int roomNum;
    public GameObject connector;
    public int conNum;
    Ray2D conRay;
    Rooms room;
    float dis;
    public float floorsize;
    bool bossSpawn;
    public float bossChance;
    public float chanceBoss;
    Vector2 spawnLoc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        conNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector2.Distance(player.transform.position, transform.position);
    }
    public void RoomSpawn(GameObject currentRoom)
    {
        StartCoroutine("RoomStop");
        room = currentRoom.GetComponent<Rooms>();
        if (conNum < 4)
        {
            bossChance = Random.Range(0, 100);
            
            roomNum = Random.Range(0, rooms.Length);
            
            connector = room.connectors[conNum];
            
            RaycastHit2D conRayHit = Physics2D.Raycast(connector.transform.position, connector.transform.up, 1f);
            if (conRayHit)
            {
                
                conNum++;
                RoomSpawn(currentRoom);
            }
            if (!conRayHit)
            {
                
                if (dis < floorsize && bossSpawn)
                {
                    GameObject r = Instantiate(rooms[roomNum].gameObject, connector.transform.position, transform.rotation);
                    conNum++;
                    RoomSpawn(currentRoom);
                    room = r.gameObject.GetComponent<Rooms>();
                    room.roomdisplace(currentRoom);
                }
                if (dis < floorsize &&  !bossSpawn)
                {
                    if (bossChance <= chanceBoss)
                    {
                        GameObject r = Instantiate(bossRoom.gameObject, connector.transform.position, transform.rotation);
                        conNum++;
                        RoomSpawn(currentRoom);
                        bossSpawn = true;
                        room = r.gameObject.GetComponent<Rooms>();
                        room.roomdisplace(currentRoom);
                    }
                    if (bossChance > chanceBoss)
                    {
                        GameObject r = Instantiate(rooms[roomNum].gameObject, connector.transform.position, transform.rotation);
                        conNum++;
                        RoomSpawn(currentRoom);
                        chanceBoss += 5;
                        room = r.gameObject.GetComponent<Rooms>();
                        room.roomdisplace(currentRoom);
                    }

                }
                if (dis >= floorsize)
                {
                    Instantiate(badRoom, connector.transform.position, transform.rotation);
                    conNum++;
                    RoomSpawn(currentRoom);
                }
            }
        }

        if (conNum == 4)
        {
            conNum = 0;
        }
    }
    IEnumerator RoomStop()
    {
        Debug.Log("called");
        yield return new WaitForSeconds(1);

    }
}

