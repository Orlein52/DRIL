using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject[] smallRooms;
    public GameObject[] medRooms;
    public GameObject[] bigRooms;
    public GameObject[] mazes;
    public GameObject badRoom;
    public GameObject bossRoom;
    public GameObject startRoom;
    public GameObject[] exits;
    PlayerController playerController;
    public GameObject[] spawnRooms;
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
    public string[] roomTag;
    int tagNum;
    Vector3 roomPos;
    bool roomCollect;
    bool spawned;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        conNum = 0;
        
        Instantiate(mazes[0], roomPos, transform.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector2.Distance(player.transform.position, transform.position);
        if (tagNum == roomTag.Length)
        {
            tagNum = 0;
            roomCollect = true;
            spawned = true;
        }
        if (!roomCollect)
        {
            spawnRooms = GameObject.FindGameObjectsWithTag(roomTag[tagNum]);
            tagNum++;
            roomCollect = true;
        }
        if (!spawned)
        {
            RoomSpawn();
        }
    }
    public void MazeSpawn(GameObject maze)
    {
        ArrayUtility.Clear(ref spawnRooms);
        Destroy(maze);
        exits[playerController.exitNum].GetComponent<Collider2D>().isTrigger = false;
        if (playerController.exitNum == 1)
        {
            player.transform.position -= Vector3.up * 48;
            Instantiate(mazes[0], transform.position, transform.rotation);
            StartCoroutine("Trigcool");
        }
        if (playerController.exitNum == 0)
        {
            player.transform.position += Vector3.up * 48;
            Instantiate(mazes[0], transform.position, transform.rotation);
            StartCoroutine("Trigcool");
        }
        if (playerController.exitNum == 3)
        {
            player.transform.position += Vector3.left * 80;
            Instantiate(mazes[0], transform.position, transform.rotation);
            StartCoroutine("Trigcool");
        }
        if (playerController.exitNum == 2)
        {
            player.transform.position += Vector3.right * 80;
            Instantiate(mazes[0], transform.position, transform.rotation);
            StartCoroutine("Trigcool");
        }
        playerController.maybe = false;
        RoomSpawn();
    }
    public void RoomSpawn()
    {
        roomCollect = false;
        if (spawnRooms.Length > 0)
        {
            if (spawnRooms[0].tag == roomTag[0])
            {
                roomNum = UnityEngine.Random.Range(0, bigRooms.Length);
                GameObject r = Instantiate(bigRooms[roomNum], spawnRooms[0].transform.position, spawnRooms[0].transform.rotation);
                ArrayUtility.RemoveAt(ref spawnRooms, 0);
                room = r.GetComponent<Rooms>();
                room.spawned = true;
            }
            if (spawnRooms[0].tag == roomTag[1])
            {
                roomNum = UnityEngine.Random.Range(0, smallRooms.Length);
                GameObject r = Instantiate(smallRooms[roomNum], spawnRooms[0].transform.position, spawnRooms[0].transform.rotation);
                ArrayUtility.RemoveAt(ref spawnRooms, 0);
                room = r.GetComponent<Rooms>();
                room.spawned = true;
            }
            if (spawnRooms[0].tag == roomTag[2])
            {
                roomNum = UnityEngine.Random.Range(0, medRooms.Length);
                GameObject r = Instantiate(medRooms[roomNum], spawnRooms[0].transform.position, spawnRooms[0].transform.rotation);
                ArrayUtility.RemoveAt(ref spawnRooms, 0);
                room = r.GetComponent<Rooms>();
                room.spawned = true;
            }
            if (spawnRooms[0].tag == roomTag[3])
            {
                GameObject r = Instantiate(bossRoom, spawnRooms[0].transform.position, spawnRooms[0].transform.rotation);
                ArrayUtility.RemoveAt(ref spawnRooms, 0);
                room = r.GetComponent<Rooms>();
                room.spawned = true;
            }
        }
        if (spawnRooms.Length == 0)
        {
            roomCollect = false;
        }
    }
    IEnumerator Trigcool()
    {
        yield return new WaitForSeconds(1);
        exits[playerController.exitNum].GetComponent<Collider2D>().isTrigger = true;
    }
}


