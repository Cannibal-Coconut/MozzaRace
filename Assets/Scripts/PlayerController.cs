using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private bool _isJumping; // Check if the player is jumping or not
    private bool _isGrounded; // Check if the player is touching the ground or not
    private bool _canDoubleJump; // Check if the player can make a jump in mid-air
    private bool _hasDoubleJump; // Check if the player has made a jump in mid-air

    private Rigidbody2D _rigidbody; // Rigidbody of the player

    private float _jumpTimeCounter; // Counter for the time that the player can stay on air

    [SerializeField] private float _jumpForce; // Jump force of the player
    [SerializeField] private float _doubleJumpForce; // Jump force of the player
    [SerializeField] private float _jumpTime; // Time that the player can stay on air

    [SerializeField] private Transform _feetPosition; // Feet position of the player
    [SerializeField] private float _radiusFeet; // Radius used to check if the feet of the player is touching the ground
    [SerializeField] private LayerMask _groundLayerMask; // Ground Layer Mask


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGrounded();
    }

    private void CheckIsGrounded()
    {
        _isGrounded = Physics2D.OverlapCircle(_feetPosition.position, _radiusFeet, _groundLayerMask);


        if (!(_jumpTimeCounter > 0)) return;
        
        _rigidbody.velocity = Vector2.up * _jumpForce;
        _jumpTimeCounter -= Time.deltaTime;
    }

    public void OnJumpInput(float isJumpingF)
    {
        _isJumping = (isJumpingF == 1);

        switch (_isGrounded)
        {
            case true when _isJumping:
                _jumpTimeCounter = _jumpTime;
                _rigidbody.velocity = Vector2.up * _jumpForce;
                _hasDoubleJump = _canDoubleJump = false;
                break;
            case false when !_isJumping && !_hasDoubleJump:
                _jumpTimeCounter = 0;
                _canDoubleJump = true;
                break;
            case false when _isJumping && _canDoubleJump:
                _rigidbody.velocity = Vector2.up * _doubleJumpForce;
                _canDoubleJump = false;
                _hasDoubleJump = true;
                break;
        }
    }
}