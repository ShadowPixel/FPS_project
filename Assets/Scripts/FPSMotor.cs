using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPSMotor : MonoBehaviour
{
    public event Action Land = delegate { };

    [SerializeField] Camera _camera = null;
    [SerializeField] float _camerAngleLimit = 70f;
    [SerializeField] GroundDetector _groundDetector = null;
    [SerializeField] GameObject muzzleFlash = null;
    [SerializeField] GameObject gunshotSound = null;

    private float _currentCameraRotationX = 0;
    bool _isGrounded = false;

    Vector3 _movementThisFrame = Vector3.zero;
    Vector3 _sprintThisFrame = Vector3.zero;
    float _turnAmountThisFrame = 0;
    float _lookAmountThisFrame = 0;

    Rigidbody _rigidbody = null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        muzzleFlash.SetActive(false);
        gunshotSound.SetActive(false);
    }

    private void OnEnable()
    {
        _groundDetector.GroundDetected += OnGroundDetected;
        _groundDetector.GroundVanished += OnGroundVanished;
    }

    private void OnDisable()
    {
        _groundDetector.GroundDetected -= OnGroundDetected;
        _groundDetector.GroundVanished -= OnGroundVanished;
    }

    private void FixedUpdate()
    {
        ApplyMovement(_movementThisFrame);
        ApplyTurn(_turnAmountThisFrame);
        ApplyLook(_lookAmountThisFrame);
        ApplySprint(_sprintThisFrame);
    }

    public void Move(Vector3 requestedMovement)
    {
        //store movement till next tick
        _movementThisFrame = requestedMovement;
    }

    public void Sprint(Vector3 requestedSprint)
    {
        //store sprint till next tick
        _sprintThisFrame = requestedSprint;
    }

    public void Turn(float turnAmount)
    {
        //store rotation till next tick
        _turnAmountThisFrame = turnAmount;
    }

    public void Look(float lookAmount)
    {
        //store look rotaion till next tick
        _lookAmountThisFrame = lookAmount;
    }

    public void Jump(float jumpForce)
    {
        //only jump if player is on ground
        if(_isGrounded == false)
        {
            return;
        }
        _rigidbody.AddForce(Vector3.up * jumpForce);
    }
    public void Shoot(bool shootState)
    {
        //store player has shot till next tick
        muzzleFlash.SetActive(shootState);
        gunshotSound.SetActive(shootState);
    }

    void ApplyMovement(Vector3 moveVector)
    {
        //confirm we have movement, exit otherwise
        if (moveVector == Vector3.zero)
        {
            return;
        }
        //move the rigidbody
        _rigidbody.MovePosition(_rigidbody.position + moveVector);
        //clear movement request
        _movementThisFrame = Vector3.zero;
    }

    void ApplySprint(Vector3 sprintVector)
    {
        //confirm we have srint input, exit otherwise
        if (sprintVector == Vector3.zero)
        {
            return;
        }
        //move the rigidbody
        _rigidbody.MovePosition(_rigidbody.position + sprintVector);
        //clear movement request
        _sprintThisFrame = Vector3.zero;
    }

    void ApplyTurn(float rotateAmount)
    {
        //confirm we have rotation, exit otherwise
        if (rotateAmount == 0)
        {
            return;
        }
        //rotate the body, convert x,y,z to quaternion for rotation
        Quaternion newRotation = Quaternion.Euler(0, rotateAmount, 0);
        _rigidbody.MoveRotation(_rigidbody.rotation * newRotation);
        //clear rotaion request
        _turnAmountThisFrame = 0;
    }

    void ApplyLook(float lookAmount)
    {
        //confirm we have rotation, exit otherwise
        if (lookAmount == 0)
        {
            return;
        }
        //calculate and clamp camera rotaion before applying
        _currentCameraRotationX -= lookAmount;
        _currentCameraRotationX = Mathf.Clamp
            (_currentCameraRotationX, -_camerAngleLimit, _camerAngleLimit);
        _camera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0, 0);
        //clear rotaion request
        _lookAmountThisFrame = 0;
    }

    void OnGroundDetected()
    {
        _isGrounded = true;
        //notify others
        Land?.Invoke();
    }

    void OnGroundVanished()
    {
        _isGrounded = false;
    }
}
