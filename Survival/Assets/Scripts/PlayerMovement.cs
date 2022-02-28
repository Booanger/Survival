using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Components
    public float speed = 1f;
    private CharacterController characterController;
    float gravity = -9.8f;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;
        movement *= Time.deltaTime;

        movement = transform.TransformDirection(movement);
        characterController.Move(movement);

        //transform.Translate(deltaX * Time.deltaTime, 0, deltaZ * Time.deltaTime);
    }

    private void LateUpdate()
    {
        //CameraRotation();
    }
}
