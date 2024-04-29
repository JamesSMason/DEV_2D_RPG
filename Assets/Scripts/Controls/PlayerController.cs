using CustomizableCharacters;
using JSM.RPG.Environment;
using JSM.RPG.Party;
using JSM.Utilities;
using UnityEngine;

namespace JSM.RPG.Controls
{
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] private float _moveSpeed = 4.0f;

        [Header("References")]
        [SerializeField] private CustomizableCharacter _customizableCharacter;
        [SerializeField] private Animator _animator;

        private PlayerControls _playerControls = null;
        private Rigidbody2D _rb = null;

        private Vector2 _movement = Vector2.zero;
        private Vector2 _previousDirection;
        private GameObject _currentDirectionGameObject;
        private int _animatorDirection;

        private readonly Vector2[] _directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        #region Unity Messages

        protected override void Awake()
        {
            base.Awake();
            _playerControls = new PlayerControls();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _playerControls.Enable();
            RandomCombatGenerator.OnCombatStart += RandomCombatGenerator_OnCombatStart;
        }

        private void Start()
        {
            if (PartyHandler.Instance.PlayerPosition != Vector3.zero)
            {
                SetPosition(PartyHandler.Instance.PlayerPosition);
                PartyHandler.Instance.SetPosition(Vector3.zero);
            }

            ResetRigs();
            HandleDirection();
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
            RandomCombatGenerator.OnCombatStart -= RandomCombatGenerator_OnCombatStart;
        }

        #endregion

        #region Public

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        #endregion

        #region Private

        private void ResetRigs()
        {
            _customizableCharacter.UpRig.SetActive(false);
            _customizableCharacter.SideRig.SetActive(false);
            _customizableCharacter.DownRig.SetActive(false);
        }

        private void PlayerInput()
        {
            _movement = _playerControls.Movement.Move.ReadValue<Vector2>().normalized;
        }

        private void Move()
        {
            _animator.SetFloat("Direction", _animatorDirection);

            if (_movement == Vector2.zero)
            {
                _animator.SetFloat("Speed", 0);
                return;
            }

            _animator.SetFloat("Speed", 1);
            Vector3 movement = _movement * _moveSpeed * Time.fixedDeltaTime;
            Vector3 newPosition = transform.position + movement;
            _rb.MovePosition(newPosition);
            HandleDirection();
        }

        private void HandleDirection()
        {
            var direction = GetClosestDirection(_movement);
            if (direction == _previousDirection)
                return;

            _currentDirectionGameObject?.SetActive(false);

            if (direction == Vector2.right)
            {
                _currentDirectionGameObject = _customizableCharacter.SideRig;
                var scale = _customizableCharacter.SideRig.transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                _currentDirectionGameObject.transform.localScale = scale;
                _animatorDirection = 1;
            }
            else if (direction == Vector2.left)
            {
                _currentDirectionGameObject = _customizableCharacter.SideRig;
                var scale = _customizableCharacter.SideRig.transform.localScale;
                scale.x = Mathf.Abs(scale.x) * -1;
                _currentDirectionGameObject.transform.localScale = scale;
                _animatorDirection = 1;
            }
            else if (direction == Vector2.up)
            {
                _currentDirectionGameObject = _customizableCharacter.UpRig;
                _animatorDirection = 0;
            }
            else if (direction == Vector2.down)
            {
                _currentDirectionGameObject = _customizableCharacter.DownRig;
                _animatorDirection = 2;
            }

            _currentDirectionGameObject?.SetActive(true);
            _previousDirection = direction;
        }

        private Vector2 GetClosestDirection(Vector2 from)
        {
            var maxDot = -Mathf.Infinity;
            var ret = Vector3.zero;

            for (int i = 0; i < _directions.Length; i++)
            {
                var t = Vector3.Dot(from, _directions[i]);
                if (t > maxDot)
                {
                    ret = _directions[i];
                    maxDot = t;
                }
            }

            return ret;
        }

        #endregion

        #region Events

        private void RandomCombatGenerator_OnCombatStart()
        {
            PartyHandler.Instance.SetPosition(transform.position);
        }

        #endregion
    }
}