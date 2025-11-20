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
        
        Instantiate(mazes[0], roomPos, transform.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector2.Distance(player.transform.position, transform.position);
    }
    public void MazeSpawn(GameObject maze)
    {
        Destroy(maze);
        exits[playerController.exitNum].SetActive(false);
        if (playerController.exitNum == 1)
        {
            player.transform.position -= Vector3.up * 50;
            Instantiate(mazes[0], transform.position, transform.rotation);
            StartCoroutine("Trigcool");
        }
        if (playerController.exitNum == 0)
        {
            player.transform.position += Vector3.up * 50;
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
    }
    IEnumerator Trigcool()
    {
        yield return new WaitForSeconds(1);
        exits[playerController.exitNum].SetActive(true);
    }
}


