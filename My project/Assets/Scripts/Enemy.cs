using UnityEngine;
using System.Collections;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    PlayerController plyr;
    GameObject player;
    public bool ranged;
    public GameObject proj;
    public float speed;
    public float health;
    Ray maybe;
    bool fired;
    public float rof;
    Rigidbody2D rb;
    Ray2D ray;
    Vector2 direction;
    public float firepow;
    public int detectDis;
    Rooms rooms;
    bool a;
    public float exp;
    public float dmg;
    public float projDMG;
    Vector2 go;
    Vector3 perchance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        rb = GetComponent<Rigidbody2D>();
        plyr = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float angleRad = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad - 0;
        transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
        direction = (player.transform.position - transform.position);
        ray = new Ray2D(transform.position, direction);
        go = ray.GetPoint(1);
        if (a)
        {
            if (rooms.d)
            {
                if (!ranged)
                {
                    rb.linearVelocity = (ray.direction * speed);
                    rb.mass = 2;
                }
                if (ranged && !fired)
                {
                    fired = true;
                    perchance = transform.position - player.transform.position;
                    maybe = new Ray(transform.position, direction);
                    GameObject p = Instantiate(proj, transform.position, transform.rotation);
                    p.GetComponent<Rigidbody2D>().linearVelocity = (maybe.direction * firepow);
                    Destroy(p, 3);
                    StartCoroutine("fireCooldown");
                }
            }
        }
        if (health <= 0)
        {
            plyr.exp += exp;
            ArrayUtility.Remove(ref rooms.enemies, gameObject);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Attack" && rooms.d)
        {
            health -= plyr.tempdmg;
        }
        if (other.tag == "Room")
        {
            rooms = other.gameObject.GetComponent<Rooms>();
            ArrayUtility.Add(ref rooms.enemies, gameObject);
            a = true;
        }
        if (other.tag == "Flask")
        {
            plyr.f = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Flask_Proj")
        {
            health -= plyr.tempdmg;
        }
    }
    IEnumerator fireCooldown()
    {
        yield return new WaitForSeconds(rof);
        fired = false;
    }
}
