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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        Move();
    }

    private void Move()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        sprint = Input.GetKey(KeyCode.LeftShift);
    }

    private void Look()
    {
        look.x = Input.GetAxis("Mouse X");
        look.y = Input.GetAxis("Mouse Y");
    }
}
