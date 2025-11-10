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
    float roomDisN;
    float roomDisS;
    float roomDisW;
    float roomDisE;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        height = Vector2.Distance(connectors[0].transform.position, connectors[1].transform.position);
        length = Vector2.Distance(connectors[2].transform.position, connectors[3].transform.position);
        area = length * height;
        room.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        playerpos = player.transform.position;
        dis = Vector2.Distance(transform.position, playerpos);
        if (dis >= length)
        {
            room.SetActive(false);
        }
        else
        {
            room.SetActive(true);
        }
    }
    public void roomdisplace(GameObject collision)
    {
            
            roomDisN = Vector2.Distance(connectors[0].transform.position, collision.transform.position);
            roomDisS = Vector2.Distance(connectors[1].transform.position, collision.transform.position);
            roomDisW = Vector2.Distance(connectors[2].transform.position, collision.transform.position);
            roomDisE = Vector2.Distance(connectors[3].transform.position, collision.transform.position);
            if ((roomDisN > roomDisS) && (roomDisN > roomDisW) && (roomDisN > roomDisE))
            {
                Debug.Log("hit");
                room.transform.position += (Vector3.up * (height / 2));
            }
            if ((roomDisE > roomDisS) && (roomDisE > roomDisW) && (roomDisE > roomDisN))
            {
                Debug.Log("hit");
                room.transform.position += (Vector3.right * (length / 2));
            }
            if ((roomDisS > roomDisN) && (roomDisS > roomDisW) && (roomDisS > roomDisE))
            {
                Debug.Log("hit");
                room.transform.position += (Vector3.down * (height / 2));
            }
            if ((roomDisW > roomDisS) && (roomDisW > roomDisN) && (roomDisW > roomDisE))
            {
                Debug.Log("hit");
                room.transform.position += (Vector3.left * (length / 2));
            }
    }
}
