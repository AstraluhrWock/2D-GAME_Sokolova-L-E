using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC 
{
    public class SpriteAnimationController : IDisposable
    {
        private sealed class Animation
        {
            public AnimationState track;
            public List<Sprite> sprites;
            public bool loop;
            public float speed = 10;
            public float counter = 0;
            public bool sleep;

            public void Update()
            {
                if (sleep) return;

                counter += Time.deltaTime * speed;

                if(loop)
                {
                    while (counter > sprites.Count)
                    {
                        counter -= sprites.Count;
                    }
                }
                else if (counter > sprites.Count)
                {
                    counter = sprites.Count;
                    sleep = true;
                }
            }
        }

        private AnimationConfig _animationConfig;
        private Dictionary<SpriteRenderer, Animation> _activeAnimations = new Dictionary<SpriteRenderer, Animation>();

        public SpriteAnimationController(AnimationConfig config)
        {
            _animationConfig = config;
        }

        public void StartAnimation(SpriteRenderer spriteRenderer, AnimationState animationState, bool loop, float speed)
        {
            if (_activeAnimations.TryGetValue(spriteRenderer, out var animation))
            {
                animation.loop = loop;
                animation.speed = speed;
                animation.sleep = false;

                if (animation.track != animationState)
                {
                    animation.track = animationState;
                    animation.sprites = _animationConfig.sequences.Find(sequence => sequence.track == animationState).sprites;
                    animation.counter = 0;
                }
            }

            else
            {
                _activeAnimations.Add(spriteRenderer, new Animation()
                {
                    track = animationState,
                    sprites = _animationConfig.sequences.Find(sequence => sequence.track == animationState).sprites,
                    loop = loop,
                    speed = speed,
                }) ;
            }
        }

        public void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimations.ContainsKey(sprite))
            {
                _activeAnimations.Remove(sprite);
            }
        }

        public void Update()
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Value.Update();

                if (animation.Value.counter < animation.Value.sprites.Count)
                {
                    animation.Key.sprite = animation.Value.sprites[(int)animation.Value.counter];
                }
            }
        }

        public void Dispose()
        {
            _activeAnimations.Clear();
        }
    }
}