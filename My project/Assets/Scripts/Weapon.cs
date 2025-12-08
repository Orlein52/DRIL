using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    bool atking;
    public bool cool;
    public bool ranged;
    public float rof;
    public float atkCool;
    public float projSpeed;
    public float projLife;
    float angleDeg;
    float angleRad;
    public GameObject attack;
    public Transform weaponSlot;
    public PlayerController plyer;
    void Start()
    {
        plyer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        
    }
    public void Attack()
    {
        plyer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (!cool && !ranged)
        {
            cool = true;
            GameObject a = Instantiate(attack, (weaponSlot.transform.position + weaponSlot.transform.up), weaponSlot.transform.rotation, weaponSlot.transform);
            a.transform.rotation = Quaternion.Euler(0f, 0f, plyer.angleDeg);
            Destroy(a, projLife);
            plyer.c = true;
        }
        if (!cool &&  ranged)
        {
            cool = true;
            GameObject a = Instantiate(attack, (weaponSlot.transform.position + weaponSlot.transform.up), weaponSlot.transform.rotation);
            Rigidbody2D r = a.GetComponent<Rigidbody2D>();
            r.linearVelocity = (a.transform.up * projSpeed);
            Destroy(a, projLife);
            plyer.c = true;
        }
    }

}
