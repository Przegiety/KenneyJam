using UnityEngine;
namespace Jam {
    public class Unit : MonoBehaviour {
        [SerializeField] private float _baseSpeed = 5f;
        [SerializeField] private int _baseHp = 100;
        private int _currentHp;
        private Vector3 _direction = Vector3.left;
        private Rigidbody _rigidbody;
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void Update() {
            _rigidbody.linearVelocity = _baseSpeed * _direction;
        }
        private void OnEnable() {
            _currentHp = _baseHp;
        }
        private void OnTriggerEnter(Collider other) {
            var player = other.GetComponent<PlayerTrigger>();
            player.TakeDamage(this);
        }
    }
}