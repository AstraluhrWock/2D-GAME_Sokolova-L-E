using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestController : IQuest
    {
        private InteractiveObjectView _player;
        private bool _active;
        private IQuestModel _model;
        private QuestObjectView _quest;

        public event EventHandler<IQuest> QuestCompleted;
        public bool IsCompleted { get; private set; }

        public QuestController(InteractiveObjectView player, IQuestModel model, QuestObjectView view)
        {
            _player = player;
            _active = false;
            _model = model;
            _quest = view;
        }

        public void OnContact(QuestObjectView questObject)
        { 
            if(questObject != null)
            {
                if (_model.TryCompleted(questObject.gameObject))
                {
                    if (questObject == _quest)
                    {
                        Completed();
                    }
                }
            }
        }

        public void Completed()
        {
            if (!_active) return;
            _active = false;
            _player.OnQuestComplete -= OnContact;
            _quest.ProcessComplete();
            QuestCompleted?.Invoke(this, this);
        }

        public void Reset()
        {
            if (_active) return;
            _active = true;
            _player.OnQuestComplete += OnContact;
            _quest.ProcessActivate();
        }

        public void Dispose()
        {
            _player.OnQuestComplete -= OnContact;
        }
    }
}
