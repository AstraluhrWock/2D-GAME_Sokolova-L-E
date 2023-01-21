using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectView _playerView;
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private LevelGenerationView _levelView;

        private PlayerController _playerController;
        private CannonController _cannonController;
        private EmitterController _emitterController;
        private LocationController _locationController;
        private CameraController _cameraController;


       /* private ParalaxManager _paralaxManager;
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _back;*/


        private void Awake()
        {
            _playerController = new PlayerController(_playerView);
            _cameraController = new CameraController(_playerView, Camera.main.transform);
            _cannonController = new CannonController(_cannonView.muzzleTransform, _playerView.transform);
            _emitterController = new EmitterController(_cannonView.bullets, _cannonView.emitterTransform);
            // _paralaxManager = new ParalaxManager(_camera, _back); 
            _locationController = new LocationController(_levelView);
            _locationController.Start();
            
        }

        void Update()
        {
            _playerController.Update();
            _cannonController.Update();
            _emitterController.Update();
            _cameraController.Update();
        //  _paralaxManager.Update();
    }
    }
}
