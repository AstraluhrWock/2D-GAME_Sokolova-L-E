using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{

    public class QuestCoinModel : IQuestModel
    {
        public bool TryCompleted(GameObject actor)
        {
            return actor.CompareTag("QuestCoin");
        }
    }
}
