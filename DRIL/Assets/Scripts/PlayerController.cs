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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();

    }

    // Update is called once per frame
    void Update()
    {

        tempmove = rb.linearVelocity;
        tempmove.x = inputX * speed;
        tempmove.y = inputY * speed;
        rb.linearVelocity = (tempmove.x * Vector2.right, tempmove.y * Vector2.up);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 InputAxis = context.ReadValue<Vector2>();
        inputX = InputAxis.x;
        inputY = InputAxis.y;

    }

}
