using UnityEditor;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public Collider2D col;
    Rigidbody2D rb;
    public GameObject enemy;
    Vector2 direction;
    Ray2D ray;
    public float speed;
    PlayerController plyct;
    GameObject plyer;
    public float minDmg;
    float dis;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        plyer = GameObject.FindGameObjectWithTag("Player");
        plyct = plyer.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        minDmg = plyct.tempdmg;
        if (enemy)
        {
            float angleRad = Mathf.Atan2(enemy.transform.position.y - transform.position.y, enemy.transform.position.x - transform.position.x);
            float angleDeg = (180 / Mathf.PI) * angleRad - 0;
            transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
            direction = (enemy.transform.position - transform.position);
            ray = new Ray2D(transform.position, direction);
            rb.linearVelocity = (ray.direction * speed);
        }
        if (enemy == null)
        {
            dis = Vector3.Distance(transform.position, plyer.transform.position);
            col.enabled = true;
            col.offset = Vector2.right * 1000;
            col.offset = Vector2.zero;
            float angleRad = Mathf.Atan2(plyer.transform.position.y - transform.position.y, plyer.transform.position.x - transform.position.x);
            float angleDeg = (180 / Mathf.PI) * angleRad - 0;
            transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
            if (dis > 1)
            {
                direction = (plyer.transform.position - transform.position);
                ray = new Ray2D(transform.position, direction);
                rb.linearVelocity = (ray.direction * speed);
            }
            if (dis <= 1)
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
    }
}
