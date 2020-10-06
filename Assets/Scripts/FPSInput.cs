using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    [SerializeField] bool _invertVertical = false;

    public event Action<Vector3> MoveInput = delegate { };
    public event Action<Vector3> RotateInput = delegate { };
    public event Action JumpInput = delegate { };
    public event Action<Vector3> SprintInput = delegate { };
    public event Action ShootInput = delegate { };

    void Update()
    {
        DetectMoveInput();
        DetectRotateInput();
        DetectJumpInput();
        DetectSprintInput();
        DetectShootInput();
    }

    void DetectMoveInput()
    {
        //process input as a 0 or 1 if avalabile
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        //if we have either one
        if (xInput != 0 || yInput != 0)
        {
            //convert local directions bas on player rotation
            Vector3 _horizontalMovement = transform.right * xInput;
            Vector3 _forwardMovement = transform.forward * yInput;
            //combine vectors into single vector
            Vector3 movement = (_horizontalMovement + _forwardMovement).normalized;
            //notify that movement has occured
            MoveInput?.Invoke(movement);
        }
    }

    void DetectRotateInput()
    {
        //get inputs
        float xInput = Input.GetAxisRaw("Mouse X");
        float yInput = Input.GetAxisRaw("Mouse Y");

        if (xInput != 0 || yInput != 0)
        {
            //check to see in camera movement is inverted
            if (_invertVertical == true)
            {
                yInput = -yInput;
            }
            //mouse left/right should be y axis, up/down x axis
            Vector3 rotation = new Vector3(yInput, xInput, 0);
            //notify that rotation has occured
            RotateInput?.Invoke(rotation);
        }
    }

    void DetectJumpInput()
    {
        //spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpInput?.Invoke();
        }
    }

    void DetectShootInput()
    {
        //left click is pressed
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootInput?.Invoke();
        }
    }

    void DetectSprintInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //process input as a 0 or 1 if avalabile
            float xInput = Input.GetAxisRaw("Horizontal");
            float yInput = Input.GetAxisRaw("Vertical");
            //if we have either one
            if (xInput != 0 || yInput != 0)
            {
                //convert local directions bas on player rotation
                Vector3 _horizontalMovement = transform.right * xInput;
                Vector3 _forwardMovement = transform.forward * yInput;
                //combine vectors into single vector
                Vector3 sprint = (_horizontalMovement + _forwardMovement).normalized;
                //notify that movement has occured
                SprintInput?.Invoke(sprint);
            }
        }
    }
}
