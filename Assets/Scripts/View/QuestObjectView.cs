using UnityEngine;

namespace PlatformerMVC
{
    public class QuestObjectView : LevelObjectView
    {
        public Color completedColor;
        public Color defaultColor;

        private void Awake()
        {
            defaultColor = spriteRenderer.color;
        }

        public void ProcessComplete()
        {
            spriteRenderer.color = completedColor;

        }

        public void ProcessActivate()
        {
            spriteRenderer.color = defaultColor;

        }
    }
      
}
