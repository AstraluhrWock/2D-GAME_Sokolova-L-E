using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public enum AnimationState
    { 
        Idle = 0,
        Run = 1,
        Jump = 2
    }

    [CreateAssetMenu(fileName = "SpriteAnimatorCfg", menuName = "Configs / Animation", order = 1)]
    public class AnimationConfig : ScriptableObject
    {
        [Serializable]
        public class SpriteSequence
        {
            public AnimationState track;
            public List<Sprite> sprites = new List<Sprite>();
        }

        public List<SpriteSequence> sequences = new List<SpriteSequence>();
    }
}
