using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class EmitterController 
    {
        private List<BulletController> _bulletControllers = new List<BulletController>();
        private Transform _transform;
        private int _index;
        private float _timeTillNextBullet;
        private float _startSpeed = 15f;
        private float _delay = 1f;

        public EmitterController(List<BulletView> bulletViews, Transform emmiterTransform)
        {
            _transform = emmiterTransform;
            foreach (BulletView bulletView in bulletViews)
            {
                _bulletControllers.Add(new BulletController(bulletView));
            }
        }

        public void Update()
        {
            if (_timeTillNextBullet > 0)
            {
                _bulletControllers[_index].Active(false);
                _timeTillNextBullet -= Time.deltaTime;
            }
            else
            {
                _timeTillNextBullet = _delay;
                _bulletControllers[_index].Trow(_transform.position, -_transform.up * _startSpeed);
                _index++;
                if (_index >= _bulletControllers.Count)
                {
                    _index = 0;
                }
            }
        }
    }
}
