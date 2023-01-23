using UnityEngine;

namespace PlatformerMVC
{
    public interface IQuestModel
    {
        bool TryCompleted(GameObject actor);
    }

}