using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private InputActions InputActions;

    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;
    private int jumpct = 0;

    [Header("attributes:")]
    public float jumpSpeed = 1.0f;
    public float maxJumps = 1.0f;

    // input variables below
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    private void Awake()
    {
        InputActions = new InputActions();
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    void ProcessingInputs()
    {
        if (InputActions.Player.jump.triggered) //button presses
        {
            if (jumpct < maxJumps)
            {
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                jumpct += 1;
            }
        }
        
    }

    void OnMove(InputValue movementValue) 
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "count: " + count.ToString();
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void Update()
    {
        ProcessingInputs();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }
        if(other.gameObject.CompareTag("ground"))
        {
            jumpct = 0;
        }
        
    }

}
