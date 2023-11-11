using UnityEngine;

public class CameraControlWithOrbit : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 3.0f;

    public Transform[] stacks; 
    private int currentStackIndex = 0;
    private Transform currentStack;

    private bool isOrbiting = false;
    private Vector3 lastMousePosition;

    void Start()
    {
        if (stacks.Length > 0)
        {
            currentStack = stacks[currentStackIndex];
            SetCameraFocus(currentStack);
        }
    }

    void Update()
    {
        // Camera movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Camera rotation
        if (Input.GetMouseButton(1)) // Right mouse button for rotation
        {
            if (!isOrbiting)
            {
                isOrbiting = true;
                lastMousePosition = Input.mousePosition;
            }
            else
            {
                Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
                transform.Rotate(Vector3.up * deltaMousePosition.x * rotationSpeed);
                transform.Rotate(Vector3.left * deltaMousePosition.y * rotationSpeed);
                lastMousePosition = Input.mousePosition;
            }
        }
        else
        {
            isOrbiting = false;
        }

        // Switch camera focus
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8))
        {
            int requestedIndex = -1;

            if (Input.GetKeyDown(KeyCode.Alpha6))
                requestedIndex = 0;
            else if (Input.GetKeyDown(KeyCode.Alpha7))
                requestedIndex = 1;
            else if (Input.GetKeyDown(KeyCode.Alpha8))
                requestedIndex = 2;

            if (requestedIndex >= 0 && requestedIndex < stacks.Length)
            {
                currentStackIndex = requestedIndex;
                currentStack = stacks[currentStackIndex];
                SetCameraFocus(currentStack);
            }
        }
    }

    void SetCameraFocus(Transform target)
    {
        transform.LookAt(target);
    }
}

