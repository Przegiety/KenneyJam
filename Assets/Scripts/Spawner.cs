using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] private List<Unit> _pool;
    [SerializeField] private List<Transform> _spawnPoints;

    private float _timer = 1f;
    public void Spawn() {
        int pointIndex = Random.Range(0, _spawnPoints.Count);
        var unit = _pool.FirstOrDefault(u => !u.gameObject.activeSelf);
        if (!unit)
            return;
        unit.transform.position = _spawnPoints[pointIndex].position;
        unit.gameObject.SetActive(true);
    }
    private void Update() {
        _timer -= Time.deltaTime;
        if (_timer <= 0) {
            Spawn();
            _timer = Random.Range(0.5f, 2f);
        }
    }
}

