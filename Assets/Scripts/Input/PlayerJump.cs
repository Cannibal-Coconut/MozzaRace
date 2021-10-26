using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D _rigidbody; // Rigidbody of the player
    [SerializeField] private Transform feetTransform;// Feet transform of the player
    
    [Range(0,1)]
    [SerializeField] private float radiusFeet = 0.3f; // Radius used to check if the feet of the player is touching the ground

    private bool _isJumping; // Check if the player is jumping or not
    private bool _isGrounded; // Check if the player is touching the ground or not
    private bool _canDoubleJump; // Check if the player can make a jump in mid-air
    private bool _hasDoubleJump; // Check if the player has made a jump in mid-air

    [SerializeField] private float jumpForce; // Jump force of the player
    [SerializeField] private float doubleJumpForce; // Jump force of the second jump of the player

    [SerializeField] private float jumpTime; // Time that the player can stay on air
    [SerializeField] private float jumpTimeCounter; // Timer that controls the time the player can stay on air

    [SerializeField] private LayerMask layerGround;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(feetTransform.position, radiusFeet, layerGround);
    }

    private void FixedUpdate()
    {
        ControlJumpDuration();
    }

    private void OnDrawGizmos()
    {
        if (!feetTransform) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetTransform.position, radiusFeet);

    }

    private void ControlJumpDuration()
    {
        if (!(jumpTimeCounter > 0)) return;
        
        _rigidbody.velocity = Vector2.up * jumpForce;
        jumpTimeCounter -= Time.fixedDeltaTime;
    }

    public void OnJumpInput(float isJumpingF)
    {
        _isJumping = (isJumpingF == 1);

        switch (_isGrounded)
        {
            case true when _isJumping:
                jumpTimeCounter = jumpTime;
                _rigidbody.velocity = Vector2.up * jumpForce;
                _hasDoubleJump = _canDoubleJump = false;
                break;
            case false when !_isJumping && !_hasDoubleJump:
                jumpTimeCounter = 0;
                _canDoubleJump = true;
                break;
            case false when _isJumping && _canDoubleJump:
                _rigidbody.velocity = Vector2.up * doubleJumpForce;
                _canDoubleJump = false;
                _hasDoubleJump = true;
                break;
        }
    }
}