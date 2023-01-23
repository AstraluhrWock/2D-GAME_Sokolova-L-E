using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class CameraController 
    {
        private LevelObjectView _player;
        private Transform _playerTransform;
        private Transform _cameraTransform;

        private float _cameraSpeed = 1.2f;

        private float _X;
        private float _Y;

        private float _offsetX;
        private float _offsetY;

        private float _xAxisInput;
        private float _yAxisInput;

        private float _treshold;

        public CameraController(LevelObjectView player, Transform cameraTransform)
        {
            _player = player;
            _playerTransform = player.transform;
            _cameraTransform = cameraTransform;
            _treshold = 0.2f;

        }
        public void Update()
        {
            _xAxisInput = Input.GetAxis("Horizontal");
            _yAxisInput = _player.rigidbody.velocity.y;

            _X = _playerTransform.position.x;
            _Y = _playerTransform.position.y;

            if (_xAxisInput > _treshold)
            {
                _offsetX = 4;
            }
            else if (_xAxisInput < -_treshold)
            {
                _offsetX = -4;
            }
            else
            {
                _offsetX = 0;
            }

            if (_yAxisInput > _treshold)
            {
                _offsetY = 4;
            }
            else if (_yAxisInput < -_treshold)
            {
                _offsetY = -4;
            }
            else
            {
                _offsetY = 0;
            }

            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, new Vector3(_X + _offsetX, _Y + _offsetY, _cameraTransform.position.z), Time.deltaTime * _cameraSpeed);
        }
    }
}
