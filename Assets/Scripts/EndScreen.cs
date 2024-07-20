using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Jam {
    public class EndScreen : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _score;
        private void OnEnable() {
            _score.text = "Score: " + GameManager.Instance.Score;
        }
    }
}
