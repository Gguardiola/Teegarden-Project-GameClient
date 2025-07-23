using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool lerpCrouch = false;
    public float gravity;
    public float speed;
    public float jumpHeight;
    private bool crouching = false;
    private bool sprinting = false;
    private float crouchTimer = 0f;
    public GameObject itemHolder;
    private float bobTimer = 0f;
    private Vector3 defaultArmPos;
    private float armBobSpeed = 6f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = PlayerConfig.Instance.speed;
        jumpHeight = PlayerConfig.Instance.jumpHeight;
        gravity = WorldConfig.Instance.gravity;
        defaultArmPos = itemHolder.transform.localPosition;
    }
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
            itemHolder.transform.localPosition = Vector3.Lerp(itemHolder.transform.localPosition, new Vector3(0, -0.7f, 0), Time.deltaTime * 5);

            itemHolder.transform.localPosition = Vector3.Lerp(itemHolder.transform.localPosition, new Vector3(0, -0.3f, 0), Time.deltaTime * 5);
        }
        controller.Move(playerVelocity * Time.deltaTime);

        if (isGrounded && input != Vector2.zero)
        {
            bobTimer += Time.deltaTime * armBobSpeed;
            float bobX = Mathf.Sin(bobTimer) * 0.05f;
            float bobY = Mathf.Cos(bobTimer * 2) * 0.05f;

            itemHolder.transform.localPosition = defaultArmPos + new Vector3(bobX, bobY, 0f);
        }
        else
        {
            bobTimer = 0f;

            itemHolder.transform.localPosition = Vector3.Lerp(itemHolder.transform.localPosition, defaultArmPos, Time.deltaTime *  5f);
        }
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
            armBobSpeed = 10f;

        }
        else
        {
            speed = 5f;
            armBobSpeed = 6f;

        }
    }

}

