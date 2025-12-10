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
    //a is finding out if you have picked a character
    bool a;
    GameObject charSel;
    GameObject bobWepSel;
    GameObject billWepSel;
    GameObject beccaWepSel;
    int wepNum;
    public GameObject[] weapons;
    int roomCount;
    GameObject LVL;
    public int LVLpoints;
    public bool l;
    void Start()
    {
        conNum = 0;
        charSel = GameObject.FindGameObjectWithTag("UI_Sel");
        bobWepSel = GameObject.FindGameObjectWithTag("UI_bob_wep");
        billWepSel = GameObject.FindGameObjectWithTag("UI_bill_wep");
        beccaWepSel = GameObject.FindGameObjectWithTag("UI_becca_wep");
        LVL = GameObject.FindGameObjectWithTag("UI_LVL");
        bobWepSel.SetActive(false);
        billWepSel.SetActive(false);
        beccaWepSel.SetActive(false);
        LVL.SetActive(false);
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
        if (!l && LVLpoints == 0)
        {
            LVLEnd();
        }
    }
    public void MazeSpawn()
    {
        ArrayUtility.Clear(ref spawnRooms);
        if (floorNum > 0)
        {
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
                Instantiate(bigRooms[0], spawnRooms[0].transform.position, spawnRooms[0].transform.rotation, m.transform);
                ArrayUtility.RemoveAt(ref spawnRooms, 0);
            }
            if (spawnRooms[0].tag == roomTag[1])
            {
                roomNum = UnityEngine.Random.Range(0, smallRooms.Length - 1);
                Instantiate(smallRooms[0], spawnRooms[0].transform.position, spawnRooms[0].transform.rotation, m.transform);
                ArrayUtility.RemoveAt(ref spawnRooms, 0);
            }
            if (spawnRooms[0].tag == roomTag[2])
            {
                roomNum = UnityEngine.Random.Range(0, medRooms.Length - 1);
                Instantiate(medRooms[0], spawnRooms[0].transform.position, spawnRooms[0].transform.rotation, m.transform);
                ArrayUtility.RemoveAt(ref spawnRooms, 0);
            }
            if (spawnRooms[0].tag == roomTag[3])
            {
                Instantiate(bossRoom, spawnRooms[0].transform.position, spawnRooms[0].transform.rotation, m.transform);
                ArrayUtility.RemoveAt(ref spawnRooms, 0);
            }
        }
    }
    public void Bob()
    {
        playerNum = 0;
        bobWepSel.SetActive(true);
        Destroy(charSel);
    }
    public void Bill()
    {
        playerNum = 1;
        billWepSel.SetActive(true);
        Destroy(charSel);
    }
    public void Becca()
    {
        playerNum = 2;
        beccaWepSel.SetActive(true);
        Destroy(charSel);
    }
    public void WeaponSelect(int a)
    {
        wepNum = a;
        if (playerNum == 0)
        {
            bobWepSel.SetActive(false);
        }
        if (playerNum == 1)
        {
            billWepSel.SetActive(false);
        }
        if (playerNum == 2)
        {
            beccaWepSel.SetActive(false);
        }
        Debug.Log(wepNum);
    }
    public void PlayerSpawn()
    {
        GameObject p = Instantiate(players[playerNum], transform.position, transform.rotation);
        playerController = p.GetComponent<PlayerController>();
        player = p.transform;
        MazeSpawn();
        Camera.main.transform.SetParent(player.transform, true);
        Camera.main.transform.position = player.transform.position + (Vector3.back * 10);
        playerController.weapon = weapons[wepNum];
        a = true;
    }
    public void LVLUP()
    {
        Time.timeScale = 0;
        LVL.SetActive(true);
        LVLpoints = 4;
        l = false;
    }
    public void CON()
    {
        playerController.CON++;
        LVLpoints--;
        playerController.maxHealth += 25;
        playerController.health += 25;
        playerController.speed += 0.2f;
    }
    public void STR()
    {
        if (playerNum == 1)
        {
            playerController.STR += 2;
        }
        else
            playerController.STR++;
        LVLpoints--;
        playerController.defense = (2 * playerController.STR) + 10;
    }
    public void DEX()
    {
        if (playerNum == 0)
            playerController.DEX += 2;
        else
            playerController.DEX++;
        LVLpoints--;
    }
    public void INT()
    {
        if (playerNum == 2)
            playerController.intelligence += 2;
        else
            playerController.intelligence++;
        LVLpoints--;
    }
    public void LVLEnd()
    {
        l = true;
        LVL.SetActive(false);
        Time.timeScale = 1;
        playerController.currentWeapon.LVLUP();
    }
}


