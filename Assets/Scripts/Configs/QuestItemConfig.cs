using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    [CreateAssetMenu(fileName = "QuestItemCfg", menuName = "Configs / QuestSystem / QuestItemCfg", order = 1)]
    public class QuestItemConfig : ScriptableObject
    {
        public int questId;
        public List<int> questItemID;
    }
}
