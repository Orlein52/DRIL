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
    Quaternion rot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        rb = GetComponent<Rigidbody2D>();
        rot = new Quaternion();
        plyr = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float angleRad = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad - 0;
        transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
        if (!ranged)
        {
            direction = (player.transform.position - transform.position);
            rb.linearVelocity = (direction * speed);
        }
        if (ranged && !fired)
        {
            fired = true;
            perchance = transform.position - player.transform.position;
            maybe = new Ray(player.transform.position, perchance);
            GameObject p = Instantiate(proj, transform.position, transform.rotation);
            p.GetComponent<Rigidbody>().AddForce(Vector3.forward);
            Destroy(p, 3);
            StartCoroutine("fireCooldown");
        }
        if (health <= 0)
        {
            Destroy(gameObject);
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
