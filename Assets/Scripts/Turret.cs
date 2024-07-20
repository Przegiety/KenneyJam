using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Jam {
    public class Turret : MonoBehaviour {
        public static event Action<Turret> Clicked;
        public Transform point;
        [SerializeField] private GameObject _content;
        private float _cd = -1;
        private float _cooldown = 4f;
        private void OnMouseDown() {
            Clicked?.Invoke(this);
        }
        public void SetCooldown() {
            _cd = _cooldown;
            _content.SetActive(false);
        }
        private void Update() {
            if (_cd > 0) {
                _cd -= Time.deltaTime;
                if(_cd <= 0) {
                    _content.SetActive(true);
                }
            }
        }
    }
}
