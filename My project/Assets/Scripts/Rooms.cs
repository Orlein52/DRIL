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
    public bool spawned;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        gameObject.tag = "Room";
    }

    // Update is called once per frame
    void Update()
    {

    }
}