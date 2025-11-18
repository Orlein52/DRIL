using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject[] mazes;
    public GameObject badRoom;
    public GameObject bossRoom;
    public GameObject startRoom;
    public GameObject[] exits;
    PlayerController playerController;
    Grid grid;
    Transform player;
    int roomNum;
    public GameObject connector;
    public int conNum;
    Rooms room;
    float dis;
    public float floorsize;
    bool bossSpawn;
    public float bossChance;
    public float chanceBoss;
    
    Vector3 roomPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grid = GetComponent<Grid>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        conNum = 0;
        
        Instantiate(rooms[1], roomPos, transform.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector2.Distance(player.transform.position, transform.position);
    }
    public void RoomSpawn(GameObject room)
    {
        roomNum = Random.Range(0, 3);
        if(playerController.exitNum == 1)
        {
            Destroy(room);
            exits[1].SetActive(false);
            player.transform.position -= Vector3.up * 6;
            Instantiate(rooms[roomNum], transform.position, transform.rotation);
        }
    }

}


