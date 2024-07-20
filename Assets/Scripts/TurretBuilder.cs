using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Jam {
    public class TurretBuilder : MonoBehaviour {
        [SerializeField] private GameObject _turret;
        [SerializeField] private GameObject _debris;
        private void OnMouseDown() {
            if (GameManager.Instance.Money > 0) {
                _debris.SetActive(false);
                _turret.SetActive(true);
                GameManager.Instance.Money--;
            }
        }
    }
}
