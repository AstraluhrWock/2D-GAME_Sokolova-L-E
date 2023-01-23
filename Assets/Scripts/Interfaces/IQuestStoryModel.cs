using System;

namespace PlatformerMVC
{
    public interface IQuestStoryModel : IDisposable
    {
        bool IsDone { get;  }
    }
}
