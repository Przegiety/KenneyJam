using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Jam {
    public class Turret : MonoBehaviour {
        public static event Action<Turret> Clicked;
        public Transform point;
        [SerializeField] private GameObject _content;
        public int rowId;
        private float _cd = -1;
        private float _cooldown = 4f;
        [SerializeField] private Image _timer;
        private Renderer[] _renderers;
        private MaterialPropertyBlock _block;
        private void OnMouseDown() {
            if (_cd > 0)
                return;
            Clicked?.Invoke(this);
        }
        private void OnMouseEnter() {
            SetHover(true);
        }
        private void OnMouseExit() {
            SetHover(false);
        }
        
        public void Fire() {
            _cd = _cooldown;
            SetUsed(true);
            StartCoroutine(FireCoroutine());
        }
        private void Update() {
            if (_cd > 0) {
                _cd -= Time.deltaTime;
                if (_cd <= 0) {
                    SetUsed(false);
                }
                _timer.fillAmount = _cd / _cooldown;
            }
        }
        private void Awake() {
            _renderers = GetComponentsInChildren<Renderer>();
            _block = new MaterialPropertyBlock();
        }
        private void Start() {
            GameManager.Instance.TurretSelected += Instance_TurretSelected;
        }
        private void OnDestroy() {
            GameManager.Instance.TurretSelected -= Instance_TurretSelected;
        }
        private void Instance_TurretSelected(Turret obj) {
            foreach (var r in _renderers) {
                r.GetPropertyBlock(_block);
                _block.SetFloat("_Selected", obj == this ? 1f : 0f);
                r.SetPropertyBlock(_block);
            }
        }

        private void SetUsed(bool value) {
            foreach (var r in _renderers) {
                r.GetPropertyBlock(_block);
                _block.SetFloat("_Used", value ? 1f : 0f);
                r.SetPropertyBlock(_block);
            }
        }
        private void SetHover(bool value) {
            foreach (var r in _renderers) {
                r.GetPropertyBlock(_block);
                _block.SetFloat("_Highlight", value ? 1f : 0f);
                r.SetPropertyBlock(_block);
            }
        }
        private IEnumerator FireCoroutine() {
            float duration = 0.2f;
            for (float t = 0; t < duration; t += Time.deltaTime) {
                yield return null;
                foreach (var r in _renderers) {
                    r.GetPropertyBlock(_block);
                    _block.SetFloat("_Overlay",1- t/duration);
                    r.SetPropertyBlock(_block);
                }
            }
            foreach (var r in _renderers) {
                r.GetPropertyBlock(_block);
                _block.SetFloat("_Overlay", 0);
                r.SetPropertyBlock(_block);
            }
        }
    }
}
