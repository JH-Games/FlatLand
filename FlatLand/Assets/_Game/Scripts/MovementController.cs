using System;
using UnityEngine;

public class MovementController : MonoBehaviour {
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float rotationSpeed;
    private float _speed;
    private Vector3 _inputDirection;
    private CharacterController _characterController;
    private bool _isSprinting;
    private Rigidbody _rigidbody;

    private void Start() => _rigidbody = GetComponent<Rigidbody>();

    private void Update() {
        _inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if (_inputDirection.magnitude < 0.1f) {
            _speed = Mathf.Lerp(_speed, 0, Time.deltaTime * 10f);
            animationController.SetBlendValue(_speed / sprintSpeed);
            return;
        }

        _isSprinting = Input.GetKey(KeyCode.LeftShift);
        _speed = Mathf.Lerp(_speed, _isSprinting ? sprintSpeed : moveSpeed, Time.deltaTime * 10f);
        animationController.SetBlendValue(_speed / sprintSpeed);

        float targetAngle = Mathf.Atan2(_inputDirection.x, _inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), rotationSpeed * Time.deltaTime);

        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f).normalized * Vector3.forward;
        
        _rigidbody.velocity = moveDirection * (_speed);
    }
}