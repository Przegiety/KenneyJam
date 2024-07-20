using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Jam {
    public class Spawner : MonoBehaviour {
        public float SpawnMod = 1f;
        [SerializeField] private List<Unit> _pool;
        [SerializeField] private List<Transform> _spawnPoints;

        private float _timer = 1f;

        public void Spawn() {
            if (!GameManager.InGame)
                return;
            int pointIndex = Random.Range(0, _spawnPoints.Count);
            var unit = _pool.Where(u => !u.gameObject.activeSelf).OrderBy(s=> Random.Range(0,100)).FirstOrDefault();
            if (!unit)
                return;
            unit.transform.position = _spawnPoints[pointIndex].position;
            unit.gameObject.SetActive(true);
        }
        private void Update() {
            _timer -= Time.deltaTime;
            if (_timer <= 0) {
                Spawn();
                _timer = Random.Range(0.5f, 2f) / SpawnMod;
            }
        }
        private void OnEnable() {
            _timer = -1;
            foreach (var u in _pool)
                u.gameObject.SetActive(false);
        }
    }
}