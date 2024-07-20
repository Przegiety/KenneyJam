using System.Collections;
using UnityEngine;
namespace Jam {
    public class Unit : MonoBehaviour {
        public static float GlobalSpeedMod = 1f;
        [SerializeField] private float _baseSpeed = 5f;
        [SerializeField] private int _baseHp = 100;
        private int _currentHp;
        private Vector3 _direction = Vector3.left;
        private Rigidbody _rigidbody;
        private bool _isDead;
        [SerializeField] private UnitType _type;
        public UnitType Type => _type;
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void Update() {
            _rigidbody.linearVelocity = GlobalSpeedMod * _baseSpeed * _direction;
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
            switch (_type) {
                case UnitType.Enemy:
                    player.TakeDamage(this);
                    break;
                case UnitType.Heal:
                    GameManager.Instance.Health++;
                    break;
                case UnitType.Money:
                    GameManager.Instance.Money++;
                    break;
            }
            Kill();
        }
        public void Kill() {
            if (_isDead)
                return;
            _isDead = true;
            var audio = GetComponent<AudioSource>();
            if (audio) {
                audio.pitch = Random.Range(0.5f, 1.02f);
                audio.Play();
            }
            StartCoroutine(KillCoroutine());
        }
        private IEnumerator KillCoroutine() {
            float duration = 0.5f;
            var renderers = GetComponentsInChildren<Renderer>();
            var block = new MaterialPropertyBlock();

            for (float t = 0; t < duration; t += Time.deltaTime) {
                foreach (var renderer in renderers) {
                    renderer.GetPropertyBlock(block);
                    block.SetFloat("_Dissolve", t / duration);
                    renderer.SetPropertyBlock(block);
                }
                yield return null;
            }
            gameObject.SetActive(false);
        }
        private IEnumerator SpawnCoroutine() {
            float duration = 0.5f;
            var renderers = GetComponentsInChildren<Renderer>();
            var block = new MaterialPropertyBlock();

            for (float t = 0; t < duration; t += Time.deltaTime) {
                foreach (var renderer in renderers) {
                    renderer.GetPropertyBlock(block);
                    block.SetFloat("_Dissolve", 1 - (t / duration));
                    renderer.SetPropertyBlock(block);
                }
                yield return null;
            }
            foreach (var renderer in renderers) {
                renderer.GetPropertyBlock(block);
                block.SetFloat("_Dissolve", 0);
                renderer.SetPropertyBlock(block);
            }
        }
        public enum UnitType {
            Enemy, Heal, Money
        }
    }
}