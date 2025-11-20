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
    GameObject maze;
    public int exitNum;
    Vector3 mousePos;
    public GameObject weaponSlot;
    Camera cam;
    public GameObject tempHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        float angleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad - 0;
        weaponSlot.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
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
    public void Attack(InputAction.CallbackContext context)
    {
        weaponSlot.transform.right = weaponSlot.transform.up;
        weaponSlot.transform.position = (weaponSlot.transform.position - weaponSlot.transform.up);
        GameObject a = Instantiate(tempHit, weaponSlot.transform.position, weaponSlot.transform.rotation);
        Destroy(a, 2f);
        weaponSlot.transform.position = (weaponSlot.transform.position + weaponSlot.transform.up);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Maze")
        {
            maze = other.gameObject;
        }
        if(other.tag == "Exit_N")
        {
            exitNum = 1;
            gameManager.MazeSpawn(maze);
        }
        if (other.tag == "Exit_S")
        {
            exitNum = 0;
            gameManager.MazeSpawn(maze);
        }
        if (other.tag == "Exit_W")
        {
            exitNum = 2;
            gameManager.MazeSpawn(maze);
        }
        if (other.tag == "Exit_E")
        {
            exitNum = 3;
            gameManager.MazeSpawn(maze);
        }
    }
}
