using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerController
    {
        private AnimationConfig _config;
        private SpriteAnimationController _playerAnimator;
        private ContactPooler _contactPooler;
        private LevelObjectView _playerView;

        private Transform _playerTransform;
        private Rigidbody2D _playerRigitbody;
        private int _health = 100;

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
        private float _xVelocity = 0f;
        private float _yVelocity = 0f;

        public PlayerController(InteractiveObjectView player)
        {
            _playerView = player;
            _playerTransform = player.transform;
            _playerRigitbody = player.rigidbody;
            _config = Resources.Load<AnimationConfig>("SpriteAnimatorCfg");
            _playerAnimator = new SpriteAnimationController(_config);
            _playerAnimator.StartAnimation(player.spriteRenderer, AnimationState.Idle, true, _animationSpeed);
            _contactPooler = new ContactPooler(_playerView.collider);

            player.TakeDamage += DamageOn;
           
        }
       public void DamageOn(BulletView bullet)
        {
            _health -= bullet.DamagePoint;
        }

        private void MoveTowards()
        {
            _xVelocity += Time.fixedDeltaTime * _walkSpeed *(_xAxisInput < 0 ? -1:1);
            _playerRigitbody.velocity = new Vector2(_xVelocity, _yVelocity);
            _playerTransform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }


        public void Update()
        {
            if (_health <= 0)
            {
                _health = 0;
                _playerView.spriteRenderer.enabled = false;
            }
            _playerAnimator.Update();
            _contactPooler.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;
            _yVelocity = _playerRigitbody.velocity.y;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingThreshold;
            _playerAnimator.StartAnimation(_playerView.spriteRenderer, _isMoving ? AnimationState.Run : AnimationState.Idle, true, _animationSpeed);

            if (_isMoving)
            {
                MoveTowards();
            }

            else 
            {
                _xVelocity = 0;
                _playerRigitbody.velocity = new Vector2 (_xVelocity, _yVelocity); // _playerRigitbody.velocity.y
            }

            if (_contactPooler.IsGrounded)
            {
              
                if (_isJump && _yVelocity <= _jumpThreshold)
                {
                    _playerRigitbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
            }

            else
            {
                if (Mathf.Abs(_yVelocity) > _jumpThreshold)
                {
                    _playerAnimator.StartAnimation(_playerView.spriteRenderer, AnimationState.Jump, true, _animationSpeed);
                }
            }
                
        }
    }
}
