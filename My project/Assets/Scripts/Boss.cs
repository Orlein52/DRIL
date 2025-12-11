using UnityEditor;
using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public float health;
    PlayerController plyr;
    GameObject player;
    Rooms rooms;
    public float exp;
    public float dmg;
    public float projDMG;
    bool a;
    public GameObject proj;
    public GameObject minion;
    public GameObject idk;
    public Transform atkTran;
    public float dmgRed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        plyr = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (a)
        {
            if (rooms.d)
            {
                if (health <= 0)
                {
                    plyr.exp += exp;
                    ArrayUtility.Remove(ref rooms.enemies, gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
    public void Spin()
    {

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Room")
        {
            rooms = other.gameObject.GetComponent<Rooms>();
            ArrayUtility.Add(ref rooms.enemies, gameObject);
            a = true;
        }
        if (other.tag == "Attack" && rooms.d)
        {
            health -= (plyr.tempdmg * (1 - dmgRed));
        }
    }
}
