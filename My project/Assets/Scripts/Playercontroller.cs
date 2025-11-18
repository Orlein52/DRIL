using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public PlayerInput input;
    Vector2 tempmove;
    public int speed;
    public float inputY;
    public float inputX;
    GameManager gameManager;
    GameObject room;
    public int exitNum;
    Vector3 mousePos;
    Vector3 maybe;
    Ray mayRay;
    Quaternion slotRot;
    public GameObject weaponSlot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        slotRot = new Quaternion();
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        mayRay = new Ray(transform.position, Vector3.up);

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Mouse.current.position.ReadValue();
        maybe = mousePos - transform.position;
        mayRay.direction = maybe;
        slotRot = Quaternion.Euler(mayRay.direction);
        weaponSlot.transform.rotation = slotRot;
        tempmove = rb.linearVelocity;
        tempmove.x = inputX * speed;
        tempmove.y = inputY * speed;
        rb.linearVelocityX = (tempmove.x);
        rb.linearVelocityY = (tempmove.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 InputAxis = context.ReadValue<Vector2>();
        inputX = InputAxis.x;
        inputY = InputAxis.y;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Room")
        {
            room = other.gameObject;
        }
        if(other.tag == "Exit_N")
        {
            exitNum = 1;
            gameManager.RoomSpawn(room);
        }
    }
}
