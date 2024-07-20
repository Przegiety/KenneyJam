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
        [SerializeField] private TextMeshProUGUI _moneyText;
        private void Start() {
            GameManager.Instance.HealthChanged += OnHealthChanged;
            GameManager.Instance.MoneyChanged += OnMoneyChanged;
            OnHealthChanged();
            OnMoneyChanged();
        }
        private void OnDestroy() {
            GameManager.Instance.HealthChanged -= OnHealthChanged;
            GameManager.Instance.MoneyChanged -= OnMoneyChanged;
        }
        private void OnHealthChanged() {
            _healthText.text = "Lives: " + GameManager.Instance.Health;
        }
        private void OnMoneyChanged() {
            _moneyText.text = "Money: " + GameManager.Instance.Money;
        }
    }
}
