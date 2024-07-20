using System.Collections;
using UnityEngine;
namespace Jam {
    public class Unit : MonoBehaviour {
        [SerializeField] private float _baseSpeed = 5f;
        [SerializeField] private int _baseHp = 100;
        private int _currentHp;
        private Vector3 _direction = Vector3.left;
        private Rigidbody _rigidbody;
        private bool _isDead;
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void Update() {
            _rigidbody.linearVelocity = _baseSpeed * _direction;
        }
        private void OnEnable() {
            _currentHp = _baseHp;
            _isDead = false;
            StartCoroutine(SpawnCoroutine());
        }
        private void OnTriggerEnter(Collider other) {
            if (_isDead)
                return;
            var player = other.GetComponent<PlayerTrigger>();
            player.TakeDamage(this);
            Kill();
        }
        public void Kill() {
            if (_isDead)
                return;
            _isDead = true;
            StartCoroutine(KillCoroutine());
        }
        private IEnumerator KillCoroutine() {
            float duration = 0.5f;
            var renderer = GetComponentInChildren<Renderer>();
            var block = new MaterialPropertyBlock();

            for (float t = 0; t < duration; t += Time.deltaTime) {
                renderer.GetPropertyBlock(block);
                block.SetFloat("_Dissolve", t / duration);
                renderer.SetPropertyBlock(block);
                yield return null;
            }
            gameObject.SetActive(false);
        }
        private IEnumerator SpawnCoroutine() {
            float duration = 0.5f;
            var renderer = GetComponentInChildren<Renderer>();
            var block = new MaterialPropertyBlock();

            for (float t = 0; t < duration; t += Time.deltaTime) {
                renderer.GetPropertyBlock(block);
                block.SetFloat("_Dissolve", 1 - (t / duration));
                renderer.SetPropertyBlock(block);
                yield return null;
            }
            block.SetFloat("_Dissolve", 0);
            renderer.SetPropertyBlock(block);
        }
    }
}