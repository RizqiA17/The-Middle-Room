using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform povCamera;

    public float speed = 5f;
    public float gravity = -15f;
    public float jumpForce = 7f;

    bool isGrounded;
    bool mouseLocked;

    public float mouseXSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckGround();
        MouseRotate();

        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z) * speed;

        if (Input.GetButton("Jump")) Jump(move);

        if (isGrounded) Move(move);
        else Gravity(move);
    }

    void Jump(Vector3 velocity)
    {
        velocity.y = jumpForce;
        Move(velocity);
    }

    void Gravity(Vector3 velocity)
    {
        velocity.y += gravity * Time.deltaTime;
        Move(velocity);
    }

    void Move(Vector3 velocity)
    {
        controller.Move(velocity * Time.deltaTime);
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }
    void MouseRotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseXSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(0f, yRotation , 0f);
        povCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (Input.GetKeyDown(KeyCode.Slash))
        {
            if (mouseLocked)
            {
                mouseLocked = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                mouseLocked = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
