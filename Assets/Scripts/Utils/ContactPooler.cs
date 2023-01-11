using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class ContactPooler
    {
        private Collider2D _collider;
        private ContactPoint2D[] _contactPoint = new ContactPoint2D[5];
        private int _contactCount = 0;
        private float _treshold = 0.2f;

        public bool IsGrounded { get; private set; }
        public bool LeftContact { get; private set; }
        public bool RightContact { get; private set; }

        public ContactPooler(Collider2D collider)
        {
            _collider = collider;
        }
        public void Update()
        {
            IsGrounded = true;
            LeftContact = false;
            RightContact = false;

            _contactCount = _collider.GetContacts(_contactPoint);

            for (int i = 0; i < _contactCount; i++)
            { 
                if(_contactPoint[i].normal.y > _treshold )
                {
                    IsGrounded = true;
                }

                if (_contactPoint[i].normal.x > _treshold)
                {
                    LeftContact = true;
                }

                if (_contactPoint[i].normal.x < -_treshold)
                {
                    RightContact = true;
                }
            }
        }
    }
}