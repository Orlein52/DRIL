using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

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
    public GameObject[] players;
    Transform player;
    float roomNum;
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
    bool spawned;
    GameObject m;
    public int playerNum;
    public int floorNum;
    float roomCount;
    //a is finding out if you have picked a character
    bool a;
    GameObject charSel;
    void Start()
    {
        conNum = 0;
        charSel = GameObject.FindGameObjectWithTag("UI_Sel");
    }
    void Update()
    {
        if (a)
        {
            if (floorNum == 0)
            {
                m = Instantiate(mazes[0], roomPos, transform.rotation);
                floorNum++;
            }
            dis = Vector2.Distance(player.transform.position, transform.position);

            if (tagNum == roomTag.Length && !spawned)
            {
                tagNum = 0;
                spawned = true;
            }
            if (spawnRooms.Length <= 0 && !spawned)
            {
                spawnRooms = GameObject.FindGameObjectsWithTag(roomTag[tagNum]);
                tagNum++;
                roomCount += spawnRooms.Length;
                RoomSpawn();
            }
            if (!spawned && spawnRooms.Length > 0)
            {
                RoomSpawn();
            }
        }
    }
    public void MazeSpawn()
    {

        if (floorNum > 0)
        {
            ArrayUtility.Clear(ref spawnRooms);
            Destroy(m);
            exits[0].GetComponent<Collider2D>().isTrigger = false;
            exits[1].GetComponent<Collider2D>().isTrigger = false;
            exits[2].GetComponent<Collider2D>().isTrigger = false;
            exits[3].GetComponent<Collider2D>().isTrigger = false;
            if (playerController.exitNum == 1)
            {
                player.transform.position -= Vector3.up * 100;
                m = Instantiate(mazes[0], transform.position, transform.rotation);
            }
            if (playerController.exitNum == 0)
            {
                player.transform.position += Vector3.up * 100;
                Instantiate(mazes[0], transform.position, transform.rotation);
            }
            if (playerController.exitNum == 3)
            {
                player.transform.position += Vector3.left * 100;
                Instantiate(mazes[0], transform.position, transform.rotation);
            }
            if (playerController.exitNum == 2)
            {
                player.transform.position += Vector3.right * 100;
                Instantiate(mazes[0], transform.position, transform.rotation);
            }
            playerController.maybe = false;
            RoomSpawn();
        }
    }
    public void RoomSpawn()
    {
        spawned = false;
        if (spawnRooms.Length > 0)
        {
            if (spawnRooms[0].tag == roomTag[0])
            {
                roomNum = UnityEngine.Random.Range(0, bigRooms.Length - 1);
                GameObject r = Instantiate(bigRooms[0], spawnRooms[0].transform.position, spawnRooms[0].transform.rotation, m.transform);
                ArrayUtility.RemoveAt(ref spawnRooms, 0);
            }
            if (spawnRooms[0].tag == roomTag[1])
            {
                roomNum = UnityEngine.Random.Range(0, smallRooms.Length - 1);
                GameObject r = Instantiate(smallRooms[0], spawnRooms[0].transform.position, spawnRooms[0].transform.rotation, m.transform);
                ArrayUtility.RemoveAt(ref spawnRooms, 0);
            }
            if (spawnRooms[0].tag == roomTag[2])
            {
                roomNum = UnityEngine.Random.Range(0, medRooms.Length - 1);
                GameObject r = Instantiate(medRooms[0], spawnRooms[0].transform.position, spawnRooms[0].transform.rotation, m.transform);
                ArrayUtility.RemoveAt(ref spawnRooms, 0);
            }
            if (spawnRooms[0].tag == roomTag[3])
            {
                GameObject r = Instantiate(bossRoom, spawnRooms[0].transform.position, spawnRooms[0].transform.rotation, m.transform);
                ArrayUtility.RemoveAt(ref spawnRooms, 0);
            }
        }
    }
    public void Bob()
    {
        playerNum = 0;
        PlayerSpawn();
    }
    public void Bill()
    {
        playerNum = 1;
        PlayerSpawn();
    }
    public void Becca()
    {
        playerNum = 2;
        PlayerSpawn();
    }
    public void PlayerSpawn()
    {
        Destroy(charSel);
        Instantiate(players[playerNum], transform.position, transform.rotation);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        MazeSpawn();
        Camera.main.transform.SetParent(player.transform, true);
        Camera.main.transform.position = player.transform.position + (Vector3.back * 10);
        a = true;
    }

}


