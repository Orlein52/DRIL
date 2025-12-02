using UnityEngine;
using System.Collections;
using static UnityEngine.Rendering.DebugUI.Table;

public class Enemy : MonoBehaviour
{
    PlayerController plyr;
    GameObject player;
    public bool ranged;
    public GameObject proj;
    public float speed;
    public float health;
    Vector3 fire;
    Vector3 perchance;
    Ray maybe;
    bool fired;
    public float rof;
    Rigidbody2D rb;
    Vector2 direction;
    float dis;
    public float firepow;
    public int detectDis;
    bool detected;
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
        dis = Vector3.Distance(transform.position, player.transform.position);
        if(dis<=detectDis)
        {
            detected = true;
        }
        if(detected)
        {
            if (!ranged)
            {
                rb.linearVelocity = (direction * speed);
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
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Attack")
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
