using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class CannonController 
    {
        private Transform _muzzleTransform;
        private Transform _targetTransform;

        private Vector3 _direction;
        private Vector3 _axis;
        private float _angle;

        public CannonController(Transform muzzle, Transform target)
        {
            _muzzleTransform = muzzle;
            _targetTransform = target;
        }

        public void Update()
        {
            _direction = _targetTransform.position - _muzzleTransform.position;
            _angle = Vector3.Angle(Vector3.down, _direction);
            _axis = Vector3.Cross(Vector3.down, _direction);

            _muzzleTransform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }
    }
}
