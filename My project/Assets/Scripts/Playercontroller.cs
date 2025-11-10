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
    Rooms room;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();

    }

    // Update is called once per frame
    void Update()
    {

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
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.tag == "Room")
        {
            Debug.Log("hit");
            room = other.gameObject.GetComponentInParent<Rooms>();
            gameManager.RoomSpawn(room.gameObject);
        }
    }
}
