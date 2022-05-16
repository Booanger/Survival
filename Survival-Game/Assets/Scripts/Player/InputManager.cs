using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 move = Vector2.zero;
    public Vector2 look = Vector2.zero;

    public bool isSprinting = false;
    public bool isJumping = false;
    public bool isAiming = false;
    public bool isCrouching = false;

    // Update is called once per frame
    void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"),
                           Input.GetAxis("Vertical"));

        look = new Vector2(Input.GetAxis("Mouse X"),
                           Input.GetAxis("Mouse Y"));

        isSprinting = Input.GetKey(KeyCode.LeftShift);
        isJumping = Input.GetButtonDown("Jump");
        isAiming = Input.GetMouseButton(1);
        isCrouching= Input.GetKey(KeyCode.LeftControl);
    }
}
