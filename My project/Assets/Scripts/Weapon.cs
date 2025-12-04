using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    bool atking;
    bool cool;
    public bool ranged;
    public float rof;
    public float atkCool;
    public float projSpeed;
    float angleDeg;
    float angleRad;
    public GameObject attack;
    public Transform weaponSlot;
    public PlayerController plyer;
    void Start()
    {

    }
    void Update()
    {
        
    }
    public void Attack()
    {
        if (!cool && !ranged)
        {
            cool = true;
            GameObject a = Instantiate(attack, (weaponSlot.transform.position + weaponSlot.transform.up), weaponSlot.transform.rotation, weaponSlot.transform);
            a.transform.rotation = Quaternion.Euler(0f, 0f, plyer.angleDeg);
            Destroy(a, rof);
            StartCoroutine("Atkcool");
        }
        if (!cool &&  ranged)
        {
            GameObject a = Instantiate(attack, (weaponSlot.transform.position + weaponSlot.transform.up), weaponSlot.transform.rotation);
            Rigidbody2D r = a.GetComponent<Rigidbody2D>();
            r.linearVelocity = (a.transform.up * projSpeed);
            Destroy(a, rof);
            StartCoroutine("Atkcool");
        }
    }
    IEnumerator Atkcool()
    {
        yield return new WaitForSeconds(rof + atkCool);
        cool = false;

    }
}
