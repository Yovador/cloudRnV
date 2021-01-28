using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSmoothVelocity;
    [SerializeField] private float turnSmoothTime;
    [SerializeField] private float groundRadius;
    [SerializeField] private float playerRange;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask raycastMask;
    private Transform groundCheck;
    private Transform raycastSource;
    [SerializeField] private float gravity;

    private GameManager gameManager;

    private CharacterController characterController;
    private Transform cameraTransform;
    private Vector3 actualVelocity;

    protected AudioSource audioSource;
    [SerializeField] protected AudioClip walkingSound;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        groundCheck = GameObject.Find("GroundCheck").transform;
        raycastSource = GameObject.Find("RaycastSource").transform;
        Cursor.lockState = CursorLockMode.Locked;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if(gameManager.GetGameStatus())
        {
            Action();
        }
        else
        {
            audioSource.Stop();
        }

    }

    private void Action()
    {
        Move();

        if (Input.GetButtonDown("Interract"))
        {
            DetectObject();
        }



    }

    void DetectObject()
    {
        raycastSource.rotation = cameraTransform.rotation;

        RaycastHit hit;
        Ray forwardRay = new Ray(raycastSource.position, raycastSource.forward * playerRange);
        Debug.DrawRay(raycastSource.position, raycastSource.forward * playerRange, Color.red);


        if (Physics.Raycast(forwardRay, out hit, 10f, raycastMask))
        {
            Interract(hit);
        }
    }

    void Interract(RaycastHit hit)
    {
        Debug.Log("Hit : " + hit.collider.name);

        if (hit.collider.CompareTag( "NPC" ) )
        {
            hit.collider.gameObject.GetComponent<NPCBehaviour>().OnInterraction();
        }

        else if (hit.collider.CompareTag("Collectibles"))
        {
            hit.collider.gameObject.GetComponent<Collectibles>().OnInterraction();
        }

        else if (hit.collider.CompareTag("Telescope"))
        {
            hit.collider.gameObject.GetComponent<TelescopeController>().OnInterraction();
        }
    }

    private void Move()
    {
        characterController.Move( PlayerMovement() + Gravity() );
    }

    private Vector3 PlayerMovement()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");
        Vector3 directionWOCam = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

        if (directionWOCam.magnitude >= 0.1f)
        {
            if (!audioSource.isPlaying)
            {
                Debug.Log("NoSound");
                audioSource.clip = walkingSound;
                audioSource.time = 0;
                audioSource.Play();
            }

            float targetAngle = Mathf.Atan2(directionWOCam.x, directionWOCam.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            return moveDirection.normalized * moveSpeed * Time.deltaTime;
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            return Vector3.zero;
        }
    }

    private bool CheckGround()
    {
        bool isOnGround;
        isOnGround = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);
        return isOnGround;
    }

    private Vector3 Gravity()
    {
        actualVelocity.y += gravity * Time.deltaTime;


        if (CheckGround() && actualVelocity.y < 0)
        {
            actualVelocity.y = -2f;
        }


        return actualVelocity * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Outside"))
        {
            gameManager.ChangeAmbient("Outside");
        }

        if (other.CompareTag("Inside"))
        {
            gameManager.ChangeAmbient("Inside");
        }
    }
}
