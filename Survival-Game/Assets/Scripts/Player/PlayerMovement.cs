using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Parameters
    private InputManager _input;
    private CharacterController _controller;

    //[SerializeField] float currentSpeed = 2.0f;
    public float currentSpeed { get; private set; }
    [SerializeField] float sprintSpeed = 6.0f;
    [SerializeField] float normalSpeed = 2.0f;
    private float targetSpeed = 0.0f;

    [SerializeField] float turnSmoothTime = 0.1f;
    float targetAngle = 0.0f;
    float turnSmoothVelocity;

    // Jump and Gravity
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -15f;
    private Vector3 playerVelocity;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;
    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.28f;
    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;

    [Header("Cinemachine")]
    public Transform cinemachineCameraTarget;
    private Vector2 currentRotation;
    public float cameraSensitivity = 120f;

    Vector3 spherePosition;
    #endregion

    #region Functions
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<InputManager>();
    }

    void Update()
    {
        //CameraRotation();
        GroundedCheck();
        JumpAndGravity();
        Move();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void Move()
    {
        targetSpeed = _input.isSprinting ? sprintSpeed : normalSpeed;
        targetSpeed = _input.isAiming || _input.isCrouching ? normalSpeed : targetSpeed;
        //targetSpeed = _input.isCrouching ? normalSpeed : targetSpeed;

        if (_input.move.magnitude < 0.1f) targetSpeed = 0f;

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 10f);

        #region Rotation
        float angle;

        // Diagonal
        if (_input.isAiming)
        {
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                                          cinemachineCameraTarget.eulerAngles.y,
                                          ref turnSmoothVelocity,
                                          turnSmoothTime);
            targetAngle = cinemachineCameraTarget.eulerAngles.y;
            _input.isSprinting = false;
        }
        // Directional
        else
        {
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                                          targetAngle,
                                          ref turnSmoothVelocity,
                                          turnSmoothTime);
        }
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        #endregion

        if (_input.move.magnitude >= 0.1f)
        {
            targetAngle = (Mathf.Atan2(_input.move.x, _input.move.y) * Mathf.Rad2Deg)
                            + cinemachineCameraTarget.eulerAngles.y;

            #region Movement
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Move the Player
            _controller.Move(currentSpeed * Time.deltaTime * moveDir);
            #endregion

        }
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(spherePosition, GroundedRadius);
    }

    private void JumpAndGravity()
    {
        if (Grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Changes the height position of the player..
        if (_input.isJumping && Grounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);
    }

    private void CameraRotation()
    {
        // Get the mouse delta. This is not in the range -1...1
        float h = _input.look.x * Time.deltaTime;
        float v = _input.look.y * Time.deltaTime;

        currentRotation.x += h * cameraSensitivity;
        currentRotation.y -= v * cameraSensitivity;

        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -90, 90);

        cinemachineCameraTarget.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        //cinemachineCameraTarget.rotation = Quaternion.Slerp(cinemachineCameraTarget.rotation, Quaternion.Euler(currentRotation.y, currentRotation.x, 0), 0.5f);
    }
    #endregion
}