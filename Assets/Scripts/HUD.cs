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
        [SerializeField] private TextMeshProUGUI _scoreText;
        private void Start() {
            GameManager.Instance.HealthChanged += OnHealthChanged;
            GameManager.Instance.MoneyChanged += OnMoneyChanged;
            GameManager.Instance.ScoreChanged += OnScoreChanged;
            OnHealthChanged();
            OnMoneyChanged();
            OnScoreChanged();
        }


        private void OnDestroy() {
            GameManager.Instance.HealthChanged -= OnHealthChanged;
            GameManager.Instance.MoneyChanged -= OnMoneyChanged;
            GameManager.Instance.ScoreChanged -= OnScoreChanged;
        }
        private void OnHealthChanged() {
            _healthText.text = "Lives: " + GameManager.Instance.Health;
        }
        private void OnMoneyChanged() {
            _moneyText.text = "Money: " + GameManager.Instance.Money;
        }
        private void OnScoreChanged() {
            _scoreText.text = GameManager.Instance.Score.ToString();
        }
    }
}
