﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerJump : MonoBehaviour{

    Rigidbody _rb;
    JumpButton _jumpButton;
    PlayerColliders _playerColliders;
    TestableInputsScript _testableInputsScript;

    float _fallMultiplier = 2f;
    float _lowJumpMultiplier = 1.3f;
    float _jumpForce = 5f;
    bool _isReadyToJump = true;
    
    void Start(){
        _rb = GetComponent<Rigidbody>();
        _jumpButton = FindObjectOfType<JumpButton>();
        _playerColliders = GetComponent<PlayerColliders>();
        //_testableInputsScript = FindObjectOfType<TestableInputsScript>();
        //_jumpButton.OnJumped += Jump;  //used from controller - commented for tests
        //_testableInputsScript.OnJumped += Jump;
    }

    void Update(){
       // FallingController();
    }


    public bool IsColliding(){
        if (_playerColliders._hitInfoLeft.collider == null && _playerColliders._hitInfoRight.collider == null){
            return false;
        }
        else{
            return true;
        }
    }

    public void Jump(){
        if (/*_jumpButton.Jump &&*/  IsColliding() && _isReadyToJump){
            _isReadyToJump = false;
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
            StartCoroutine(JumpCoolDown());
        }
    }

    void FallingController(){
        if (_rb.velocity.y < 0){
            _rb.velocity += Vector3.up * Physics.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0 && !_jumpButton.Jump){
            _rb.velocity += Vector3.up * Physics.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    IEnumerator JumpCoolDown(){
        yield return new WaitForSeconds(0.1f);
        _isReadyToJump = true;
    }
}
