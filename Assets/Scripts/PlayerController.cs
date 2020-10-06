using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FPSInput))]
[RequireComponent(typeof(FPSMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject deadMenu;

    public int maxHealth;
    public Text currentHealthLabel;
    private int currentHealth;
    private bool isDead;

    FPSInput _input = null;
    FPSMotor _motor = null;

    [SerializeField] float _moveSpeed = .1f;
    [SerializeField] float _turnSpeed = 6f;
    [SerializeField] float _jumpStrength = 10f;
    [SerializeField] float _sprintSpeed = .2f;
    bool _playerShot = true;

    private void Awake()
    {
        deadMenu.SetActive(false);
        _input = GetComponent<FPSInput>();
        _motor = GetComponent<FPSMotor>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentHealth = maxHealth;
        isDead = false;
        UpdateGUI();
    }

    private void OnEnable()
    {
        _input.MoveInput += OnMove;
        _input.RotateInput += OnRotate;
        _input.JumpInput += OnJump;
        _input.SprintInput += OnSprint;
        _input.ShootInput += OnShoot;
    }

    private void OnDisable()
    {
        _input.MoveInput -= OnMove;
        _input.RotateInput -= OnRotate;
        _input.JumpInput -= OnJump;
        _input.SprintInput -= OnSprint;
        _input.ShootInput -= OnShoot;
    }

    void UpdateGUI()
    {
        currentHealthLabel.text = currentHealth.ToString();
    }

    public void AlterHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        CheckDead();
        UpdateGUI();
    }

    private void CheckDead()
    {
        //don't redo if already dead
        if (isDead)
        {
            return;
        }
        if (currentHealth == 0)
        {
            isDead = true;
            GetComponent<PlayerController>().enabled = false;
            Died();
        }
    }

    public void Died()
    {
        deadMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void OnMove(Vector3 movement)
    {
        //incorporate move speed
        _motor.Move(movement * _moveSpeed);
    }

    void OnSprint(Vector3 sprint)
    {
        //incorporates sprint speed
        _motor.Sprint(sprint * _sprintSpeed);
    }

    void OnRotate(Vector3 rotation)
    {
        //camera looks vertical, plauer rotates left/right
        _motor.Turn(rotation.y * _turnSpeed);
        _motor.Look(rotation.x * _turnSpeed);
    }

    void OnJump()
    {
        //apply jump force
        _motor.Jump(_jumpStrength);
    }

    void OnShoot()
    {
        StartCoroutine(Gunshot());
    }

    IEnumerator Gunshot()
    {
        //activate muzzle flash and gunshot sound
        _motor.Shoot(_playerShot);
        //timer to wait and then return player weapon to original state
        yield return new WaitForSeconds(1);
        _motor.Shoot(false);
    }
}
