using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject[] mazes;
    public GameObject badRoom;
    public GameObject bossRoom;
    Transform player;
    int roomNum;
    GameObject connector;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        roomNum = Random.Range(0, 4);
    }
    public void RoomSpawn(GameObject currentRoom)
    {
        connector = currentRoom.transform.GetChild(1).gameObject;
        //if (connector == 
    }
}
