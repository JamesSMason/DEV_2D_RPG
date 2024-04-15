using System;
using UnityEngine;

namespace JSM.RPG.Controls
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1.0f;

        private PlayerControls _playerControls = null;
        private Rigidbody2D _rb = null;

        private Vector2 _movement = Vector2.zero;

        #region Unity Messages

        private void Awake()
        {
            _playerControls = new PlayerControls();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void Update()
        {
            PlayerInput();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }

        #endregion

        #region Private

        private void PlayerInput()
        {
            _movement = _playerControls.Movement.Move.ReadValue<Vector2>().normalized;
        }

        private void Move()
        {
            Vector3 movement = _movement * _moveSpeed * Time.fixedDeltaTime;
            Vector3 newPosition = transform.position + movement;
            _rb.MovePosition(newPosition);
        }

        #endregion
    }
}