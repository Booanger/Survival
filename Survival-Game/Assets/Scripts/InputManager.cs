using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool changeCameraPosition;

    // Update is called once per frame
    void Update()
    {
        Look();
        Move();
    }

    private void Move()
    {
        jump = Input.GetButtonDown("Jump");
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        sprint = Input.GetKey(KeyCode.LeftShift);
    }

    private void Look()
    {
        //changeCameraPosition = Input.GetKeyDown(KeyCode.V);
        changeCameraPosition = Input.GetKeyDown(KeyCode.V) ? !changeCameraPosition : changeCameraPosition;
        look.x = Input.GetAxis("Mouse X");
        look.y = Input.GetAxis("Mouse Y");
    }
}
