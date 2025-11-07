using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    Transform player;
    Vector2 playerpos;
    public float dis;
    public GameObject room;
    public GameObject[] connectors;
    GameManager gameManager;
    public float area;
    public float height;
    public float length;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        height = Vector2.Distance(connectors[0].transform.position, connectors[1].transform.position);
        length = Vector2.Distance(connectors[2].transform.position, connectors[3].transform.position);
        area = length * height;
    }

    // Update is called once per frame
    void Update()
    {
        playerpos = player.transform.position;
        dis = Vector2.Distance(transform.position, playerpos);
        if (dis >= 7.8f)
        {
            room.SetActive(false);
        }
        else
        {
            room.SetActive(true);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        return;
    }
}
