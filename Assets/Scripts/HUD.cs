using System;
using System.Collections;
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
        [SerializeField] private List<GameObject> _hearts;
        [SerializeField] private List<GameObject> _money;
        private bool _dirty;
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
            int health = GameManager.Instance.Health;
            for (int i = 0; i < _hearts.Count; i++) {
                _hearts[i].SetActive(i < health);
            }
        }
        private void OnMoneyChanged() {
            _moneyText.text = "Money: " + GameManager.Instance.Money;
            int money = GameManager.Instance.Money;
            for (int i = 0; i < _money.Count; i++) {
                _money[i].SetActive(i < money);
            }
        }
        private void OnScoreChanged() {
            _scoreText.text = GameManager.Instance.Score.ToString();
            _dirty = true;
        }
        private IEnumerator ScoreAnim() {
            float duration = 0.2f;
            for (float t = 0; t < duration; t += Time.deltaTime) {
                float step = t / duration;
                _scoreText.transform.localScale = Mathf.Lerp(1.4f, 1, step) * Vector3.one;
                yield return null;
            }
            _scoreText.transform.localScale = Vector3.one;
        }
        private void LateUpdate() {
            if (_dirty) {
                StopAllCoroutines();
                StartCoroutine(ScoreAnim());
                _dirty = false;
            }
        }
    }
}
