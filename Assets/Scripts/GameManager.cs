using System;
using UnityEngine;
namespace Jam {
    public class GameManager : MonoBehaviour {
        public event Action HealthChanged;
        public event Action<Turret> TurretSelected;
        public static GameManager Instance { get; private set; }
        public int Health {
            get => health;
            set {
                health = value;
                HealthChanged?.Invoke();
            }
        }
        private Turret Selected {
            get => _selected;
            set {
                _selected = value;
                TurretSelected?.Invoke(_selected);
            }
        }
        [SerializeField] private LineRenderer _shotPrefab;
        private int health;
        private Turret _selected;

        private void Awake() {
            Instance = this;
            Turret.Clicked += Turret_Clicked;
            StartGame();
        }

        private void Turret_Clicked(Turret obj) {
            if (Selected && Selected.rowId != obj.rowId) {
                var shot = Instantiate(_shotPrefab);
                shot.gameObject.SetActive(true);
                shot.SetPositions(new Vector3[] { Selected.point.position, obj.point.position });
                Destroy(shot.gameObject, 0.2f);
                var results = Physics.OverlapCapsule(Selected.point.position, obj.point.position, 0.3f);
                foreach (var r in results) {
                    var u = r.GetComponent<Unit>();
                    if (u)
                        u.Kill();
                }
                Selected.Fire();
                obj.Fire();
                Selected = null;
            } else {
                Selected = obj;
            }
        }

        public void StartGame() {
            Health = 10;
        }
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Mouse1)) {
                Selected = null;
            }
        }
    }
}

