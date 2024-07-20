using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Jam {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }
        public int Health { get; set; }
        private Turret _selected;
        [SerializeField] private LineRenderer _shotPrefab;
        private void Awake() {
            Instance = this;
            Turret.Clicked += Turret_Clicked;
        }

        private void Turret_Clicked(Turret obj) {
            if (_selected && _selected != obj) {
                var shot = Instantiate(_shotPrefab);
                shot.gameObject.SetActive(true);
                shot.SetPositions(new Vector3[] { _selected.point.position,obj.point.position });
                Destroy(shot.gameObject, 0.2f);
                var results = Physics.OverlapCapsule(_selected.point.position, obj.point.position, 0.3f);
                foreach (var r in results) {
                    var u = r.GetComponent<Unit>();
                    if (u)
                        u.gameObject.SetActive(false);
                }
                _selected.SetCooldown();
                obj.SetCooldown();
                _selected = null;
            } else {
                _selected = obj;
            }
        }

        public void StartGame() {
            Health = 5;
        }
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Mouse1)) {
                _selected = null;
            }
        }
    }
}

