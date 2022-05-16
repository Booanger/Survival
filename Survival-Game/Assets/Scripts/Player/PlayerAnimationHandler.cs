using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private InputManager _input;
    Animator _animator;
    private bool isSprinting;
    PlayerMovement _movement;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<InputManager>();
        _animator = GetComponent<Animator>();
        isSprinting = false;
        _movement = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        BasicMovement();
        Jumping();
        Crouching();
    }

    void IsSprinting()
    {
        // Is this condition necessary?
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        // If Aiming Button Pressed
        if (_input.isAiming) isSprinting = false;
        // If Sprinting Button Pressed
        else if (_input.isSprinting) isSprinting = true;
        else isSprinting = false;


        _animator.SetBool("IsSprinting", isSprinting);
    }

    void BasicMovement()
    {
        if (_input.isAiming)
        {
            _animator.SetFloat("VelocityX", _input.move.x * _movement.currentSpeed);
            _animator.SetFloat("VelocityZ", _input.move.y * _movement.currentSpeed);
        }
        else
        {
            _animator.SetFloat("VelocityX", 0);
            _animator.SetFloat("VelocityZ", _input.move.magnitude * GetComponent<PlayerMovement>().currentSpeed);
        }
    }

    void Crouching()
    {
        if (_input.isCrouching)
        {
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1, Time.deltaTime * 10f));
        }
        else
        {
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0, Time.deltaTime * 10f));
        }
    }

    void Jumping()
    {
        _animator.SetBool("IsGrounded", _movement.Grounded);
    }
}
