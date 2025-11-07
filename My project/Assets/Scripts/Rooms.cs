using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    Transform player;
    Vector2 playerpos;
    public float dis;
    GameObject room;
    GameObject room1;
    public GameObject[] connectors;
    GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        room = transform.GetChild(0).gameObject;
        Debug.Log(room.name);
        
        room1 = transform.GetChild(0).gameObject;
        Debug.Log(room.name);
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
