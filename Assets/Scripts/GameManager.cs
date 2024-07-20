using System;
using UnityEngine;
using UnityEngine.Events;

namespace Jam {
    public class GameManager : MonoBehaviour {
        public static bool InGame;
        [SerializeField] private UnityEvent OnStart;
        [SerializeField] private UnityEvent OnDeath;
        public event Action Cleanup;
        public event Action HealthChanged;
        public event Action MoneyChanged;
        public event Action<Turret> TurretSelected;
        public static GameManager Instance { get; private set; }
        private int _health;
        public int Health {
            get => _health;
            set {
                _health = value;
                if (_health <= 0) {
                    InGame = false;
                    OnDeath.Invoke();
                }
                HealthChanged?.Invoke();
            }
        }
        private int _money;
        public int Money {
            get => _money;
            set {
                _money = value;
                MoneyChanged?.Invoke();
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
        private Turret _selected;
        private float _timer;
        [SerializeField] private Spawner _spawner;
        private void Awake() {
            Instance = this;
            Turret.Clicked += Turret_Clicked;
            InGame = false;
            //StartGame();
        }

        private void Turret_Clicked(Turret obj) {
            if (!InGame)
                return;
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
            Money = 0;
            _timer = 0;
            _spawner.SpawnMod = 1f;
            Unit.GlobalSpeedMod = 1f;
            InGame = true;
            OnStart.Invoke();
        }
        public void DoCleanup() {
            Cleanup?.Invoke();
        }
        public void QuitGame() {
            Application.Quit();
        }
        private void Update() {
            if (!InGame)
                return;
            if (Input.GetKeyDown(KeyCode.Mouse1)) {
                Selected = null;
            }
            _timer += Time.deltaTime;
            if (_timer > 30) {
                float step = (_timer - 30) / 120;
                Unit.GlobalSpeedMod = Mathf.Lerp(1, 1.5f, step);
                _spawner.SpawnMod = Mathf.Lerp(1, 3f, step);
            }
        }
    }
}

