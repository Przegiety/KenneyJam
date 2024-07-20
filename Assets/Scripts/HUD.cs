using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Jam {
    public class HUD : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _healthText;
        private void Start() {
            GameManager.Instance.HealthChanged += OnHealthChanged;
            OnHealthChanged();
        }
        private void OnDestroy() {
            GameManager.Instance.HealthChanged -= OnHealthChanged;
        }
        private void OnHealthChanged() {
            _healthText.text = "Lives: " + GameManager.Instance.Health;
        }
    }
}
