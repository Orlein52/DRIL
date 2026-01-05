using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boss : MonoBehaviour
{
    public float health;
    PlayerController plyr;
    GameObject player;
    Rooms rooms;
    public float exp;
    public float dmg;
    public float projDMG;
    public bool a;
    public GameObject proj;
    public GameObject minion;
    public GameObject idk;
    public Transform atkTran;
    public float dmgRed;
    bool l;
    bool cool;
    public float atkcool;
    public float projLife;
    public bool start = false;
    public float atkcoolD;
    bool t;
    public int tNum;
    public int rotNum;
    GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        plyr = player.GetComponent<PlayerController>();
        rooms = GetComponentInParent<Rooms>();
        rooms.enemies++;
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rooms.d && a)
        {
            if (start)
            {
                StartCoroutine("CoolD");
                start = false;
            }
            if (l && !cool)
            {
                float angleRad = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);
                float angleDeg = (180 / Mathf.PI) * angleRad - 0;
                atkTran.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg - 90);
                GameObject at = Instantiate(proj, atkTran.position, atkTran.rotation, transform);
                Rigidbody2D r = at.GetComponent<Rigidbody2D>();
                r.linearVelocity = (at.transform.up * 20);
                Destroy(at, projLife);
                StartCoroutine("AtkCool");
            }
            if (t && !cool)
            {
                GameObject at = Instantiate(proj, atkTran.position, atkTran.rotation, transform);
                Rigidbody2D r = at.GetComponent<Rigidbody2D>();
                r.linearVelocity = (at.transform.up * 5);
                Destroy(at, projLife);
                atkTran.transform.rotation = Quaternion.Euler(0f, 0f, (atkTran.transform.rotation.z * rotNum));
                tNum++;
                rotNum += 45;
            }
            if (t &&  tNum > 4)
            {
                cool = true;
                StartCoroutine("AtkCool");
            }
            if (health <= 0)
            {
                plyr.exp += exp;
                rooms.enemies--;
                Camera.main.transform.SetParent(player.transform.transform);
                Camera.main.transform.position = player.transform.position;
                Camera.main.orthographicSize = 5;
                gameManager.BossKill();
                Destroy(gameObject);

            }
        }
    }
    public void Laser()
    {
        l = true;
        projLife = 20;
        cool = false;
        StartCoroutine("CoolD");
    }
    public void Tack()
    {
        t = true;
        projLife = 20;
        cool = false;
        StartCoroutine("CoolD");
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Attack" && rooms.d)
        {
            health -= (plyr.tempdmg * (1 - dmgRed));
        }
        if (other.tag == "Minion" && rooms.d)
        {
            Minion m = other.gameObject.GetComponent<Minion>();
            m.enemy = gameObject;
            m.col.enabled = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Minion")
        {
            Minion m = other.gameObject.GetComponent<Minion>();
            health -= m.minDmg;
        }
    }
    IEnumerator AtkCool()
    {
        cool = true;
        yield return new WaitForSeconds(atkcool);
        cool = false;
    }
    IEnumerator CoolD()
    {
        yield return new WaitForSeconds(atkcoolD);
        l = false;
        t = false;
        yield return new WaitForSeconds(atkcoolD);
        int s = Random.Range(0, 2);
        atkcoolD -= 0.001f;
        if (s == 0)
            Laser();
        if (s == 1)
            Tack();

    }
}
