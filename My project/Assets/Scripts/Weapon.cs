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
    public float dmg;
    float dmgmod;
    GameManager gameManager;
    void Start()
    {

    }
    void Update()
    {
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
            rof -= (plyer.DEX * 0.001f);
        else
            rof -= (plyer.DEX * 0.005f);
        if (atkCool > 0)
        {
            atkCool -= (plyer.DEX * 0.02f);
        }
        if (atkCool < 0)
        {
            atkCool = 0;
        }
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
