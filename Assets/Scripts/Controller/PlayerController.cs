using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerController
    {
        private AnimationConfig _config;
        private SpriteAnimationController _playerAnimator;
        private LevelObjectView _playerView;
        private Transform _playerTransform;

        private float _xAxisInput;
        private bool _isJump;

        private float _walkSpeed = 3f;
        private float _animationSpeed = 10f;
        private float _movingThreshold = 0.1f;

        private Vector3 _rightScale = new Vector3(1, 1, 1);
        private Vector3 _leftScale = new Vector3(-1, 1, 1);

        private bool _isMoving;

        private float _jumpForce = 9f;
        private float _jumpThreshold = 1f;
        private float _g = -9.8f;
        private float _groundLevel = 0.5f;
        private float _yVelocity;

        public PlayerController(LevelObjectView player)
        {
            _config = Resources.Load<AnimationConfig>("SpriteAnimatorCfg");
            _playerAnimator = new SpriteAnimationController(_config);
            _playerAnimator.StartAnimation(player.spriteRenderer, AnimationState.Idle, true, _animationSpeed);
            _playerView = player;
            _playerTransform = player.transform;
        }


        private void MoveTowards()
        {
            _playerTransform.position += Vector3.right * (Time.deltaTime * _walkSpeed*(_xAxisInput < 0 ? -1:1));
            _playerTransform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }

        public bool IsGrounded()
        {
            return _playerTransform.position.y <= _groundLevel && _yVelocity <= 0;
        }

        public void Update()
        {
            _playerAnimator.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingThreshold;

            if (_isMoving)
            {
                MoveTowards();
            }

            if (IsGrounded())
            {
                _playerAnimator.StartAnimation(_playerView.spriteRenderer, _isMoving ? AnimationState.Run : AnimationState.Idle, true, _animationSpeed);

                if (_isJump && _yVelocity <= 0)
                {
                    _yVelocity = _jumpForce;
                }
                else if (_yVelocity < 0)
                {
                    _yVelocity = 0;
                    _playerTransform.position = new Vector3(_playerTransform.position.x, _groundLevel, _playerTransform.position.z);
                }
            }

            else
            {
                if (Mathf.Abs(_yVelocity) > _jumpThreshold)
                {
                    _playerAnimator.StartAnimation(_playerView.spriteRenderer, AnimationState.Jump, true, _animationSpeed);
                }
                _yVelocity += _g * Time.deltaTime;
                _playerTransform.position += Vector3.up * (Time.deltaTime * _yVelocity);
            }
                
        }
    }
}
