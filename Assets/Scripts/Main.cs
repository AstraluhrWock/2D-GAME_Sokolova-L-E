using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField]
        private LevelObjectView _playerView;
        private AnimationConfig _config;
        private SpriteAnimationController _playerAnimator;


        private void Awake()
        {
            _config = Resources.Load<AnimationConfig>("SpriteAnimatorCfg");
            _playerAnimator = new SpriteAnimationController(_config);
            _playerAnimator.StartAnimation(_playerView.spriteRenderer, AnimationState.Run, true, 10f);
        }

        void Update()
        {
            _playerAnimator.Update();
        }
    }
}
