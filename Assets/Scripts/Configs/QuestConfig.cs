using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public enum QuestType
    { 
        Coins,
        Buttons
    }

    [CreateAssetMenu(fileName ="QuestCfg", menuName = "Configs / QuestSystem / QuestCfg", order = 1)]
    public class QuestConfig : ScriptableObject
    {
        public int id;
        public QuestType type;
    }
}