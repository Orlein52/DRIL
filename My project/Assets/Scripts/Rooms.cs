using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    Transform player;
    Vector2 playerpos;
    public float dis;
    public GameObject room;
    GameManager gameManager;
    public float area;
    public float height;
    public float length;
    public bool spawned;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerpos = player.transform.position;
        dis = Vector2.Distance(transform.position, playerpos);
        if (dis >= length && spawned)
        {
            room.SetActive(false);
        }
        if (dis < length && spawned)
        {
            room.SetActive(true);
        }
    }
}