using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float intelligence;
    public int STR;
    public int CON;
    public int DEX;
    public int defense;
    public int magic_defense;
    public Weapon currentWeapon;
    Rigidbody2D rb;
    public PlayerInput input;
    Vector2 tempmove;
    public int speed;
    public float inputY;
    public float inputX;
    GameManager gameManager;
    public int exitNum;
    Vector3 mousePos;
    public GameObject weaponSlot;
    Camera cam;
    public GameObject tempHit;
    public float health;
    public float tempdmg;
    bool atking;
    public float rof;
    public bool maybe = false;
    public float atkCool;
    public float angleDeg;
    public float angleRad;
    public GameObject weapon;
    public bool c;
    public float exp;
    public float nextLvl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        cam = Camera.main;
        currentWeapon = weapon.GetComponent<Weapon>();
        currentWeapon.weaponSlot = weaponSlot.transform;
        Instantiate(weapon, weaponSlot.transform.position + weaponSlot.transform.up, weapon.transform.rotation, weaponSlot.transform);
        currentWeapon.LVLUP();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        angleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        angleDeg = (180 / Mathf.PI) * angleRad - 0;
        weaponSlot.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg - 90);
        tempmove = rb.linearVelocity;
        tempmove.x = inputX * speed;
        tempmove.y = inputY * speed;
        rb.linearVelocityX = (tempmove.x);
        rb.linearVelocityY = (tempmove.y);
        if (atking)
        {
            currentWeapon.Attack();
        }
        if (c)
        {
            c = false;
            StartCoroutine("Atkcool");
        }
        if (exp >= nextLvl)
        {
            exp = 0;
            nextLvl = nextLvl * 1.5f;
            gameManager.LVLUP();
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 InputAxis = context.ReadValue<Vector2>();
        inputX = InputAxis.x;
        inputY = InputAxis.y;
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            atking = true;
        }
        else
        {
            atking = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enem_Proj")
        {
            health -= 3;
        }
        if(other.tag == "Exit_N" && !maybe)
        {
            maybe = true;
            exitNum = 1;
            gameManager.MazeSpawn();
        }
        if (other.tag == "Exit_S" && !maybe)
        {
            maybe = true;
            exitNum = 0;
            gameManager.MazeSpawn();
        }
        if (other.tag == "Exit_W" && !maybe)
        {
            maybe = true;
            exitNum = 2;
            gameManager.MazeSpawn();
        }
        if (other.tag == "Exit_E" && !maybe)
        {
            maybe = true;
            exitNum = 3;
            gameManager.MazeSpawn();
        }
        if (other.tag == "Room")
        {
            Rooms r = other.gameObject.GetComponent<Rooms>();
            r.RoomStart();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            health -= 3;
        }
    }
    IEnumerator Atkcool()
    {
        yield return new WaitForSeconds(currentWeapon.rof + currentWeapon.atkCool);
        currentWeapon.cool = false;
    }
}

