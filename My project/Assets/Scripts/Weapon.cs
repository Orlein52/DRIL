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
    Rigidbody2D r;
    public float dmg;
    float dmgmod;
    GameManager gameManager;
    public bool flask;
    public bool voodoo;
    public bool melee = true;
    float dexScale;
    bool f;
    GameObject a;
    public GameObject flaskProj;
    float flaskSpeed;
    void Start()
    {
        flaskSpeed = projSpeed;
    }
    void Update()
    {
        if (f)
        {
            r.linearVelocity = (a.transform.up * projSpeed);
            projSpeed--;
        }
        if (flask && projSpeed <= 0)
        {
            f = false;
            r.linearVelocity = Vector2.zero;
            FlaskBreak();
            projSpeed = flaskSpeed;
        }
    }
    public void LVLUP()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        if (gameManager.playerNum == 0)
        {
            dmgmod = (1.9f * plyer.DEX);
        }
        if (gameManager.playerNum == 1)
        {
            dmgmod = (2.5f * plyer.STR);
        }
        if (gameManager.playerNum == 2)
        {
            dmgmod = (2.35f * plyer.intelligence);
        }
        plyer.tempdmg = dmgmod + dmg;
        if (gameManager.playerNum == 0)
        {
            dexScale = (1 /(-0.01f * (plyer.DEX + 95))) + 1;
        }
        else
            dexScale = (1 / (-0.05f * (plyer.DEX + 15))) + 1;
        rof = rof * (1 - dexScale);
        if (atkCool > 0)
        {
            atkCool = (1 / (plyer.DEX * 0.2f));
        }
    }
    public void Attack()
    {
        plyer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (!cool && melee)
        {
            cool = true;
            a = Instantiate(attack, (weaponSlot.transform.position + weaponSlot.transform.up), weaponSlot.transform.rotation, weaponSlot.transform);
            a.transform.rotation = Quaternion.Euler(0f, 0f, plyer.angleDeg);
            Destroy(a, projLife);
            plyer.c = true;
        }
        if (!cool &&  ranged)
        {
            cool = true;
            a = Instantiate(attack, (weaponSlot.transform.position + weaponSlot.transform.up), weaponSlot.transform.rotation);
            r = a.GetComponent<Rigidbody2D>();
            r.linearVelocity = (a.transform.up * projSpeed);
            Destroy(a, projLife);
            plyer.c = true;
        }
        if (!cool && voodoo)
        {
            cool = true;
            GameObject a = Instantiate(attack, (weaponSlot.transform.position + weaponSlot.transform.up), weaponSlot.transform.rotation);
            Rigidbody2D r = a.GetComponent<Rigidbody2D>();

        }
        if (!cool && flask)
        {
            cool = true;
            a = Instantiate(attack, (weaponSlot.transform.position + weaponSlot.transform.up), weaponSlot.transform.rotation);
            r = a.GetComponent<Rigidbody2D>();
            f = true;
            plyer.c = true;
        }
    }
    public void FlaskBreak()
    {
        GameObject fl = Instantiate(flaskProj, a.transform.position, a.transform.rotation);
        Destroy(a);
        Destroy(fl, projLife);
    }
}
