using UnityEngine;

namespace PlatformerMVC
{
    public class BulletController
    {
        private Vector3 _velocity;
        private BulletView _bulletView;

        public BulletController(BulletView bulletView)
        {
            _bulletView = bulletView;
            Active(false);
        }

        public void Active(bool value)
        {
            _bulletView.gameObject.SetActive(value);
        }

        public void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            float _angle = Vector3.Angle(Vector3.left, _velocity);
            Vector3 _axis = Vector3.Cross(Vector3.left, _velocity);
            _bulletView.transform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }

        public void Trow(Vector3 position, Vector3 velovity)
        {
            _bulletView.transform.position = position;
            SetVelocity(velovity);
            _bulletView.rigidbody.velocity = Vector2.zero;
            _bulletView.rigidbody.angularVelocity = 0;
            Active(true);
            _bulletView.rigidbody.AddForce(velovity, ForceMode2D.Impulse);
        }
    }
}

