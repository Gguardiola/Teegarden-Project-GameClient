using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public bool lerpCrouch = false;
    public float gravity = -9.81f;
    public float speed = 5.0f;
    public float jumpHeight = 1.5f;
    public bool crouching = false;
    public bool sprinting = false;
    private float crouchTimer = 0f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
            } else if (!crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    //Recieves the input for our InputManager and processes it into characterController.
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        controller.Move(moveDirection * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0f;
        lerpCrouch = true;
    }
    
    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = 8f;
        }
        else
        {
            speed = 5f;
        }
    }
}

