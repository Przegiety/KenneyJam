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
        [SerializeField] private AudioSource _audio;
        private Renderer[] _renderers;
        private MaterialPropertyBlock _block;
        private void OnMouseDown() {
            if (GameManager.Instance.Money > 0) {
                _debris.SetActive(false);
                _turret.SetActive(true);
                GameManager.Instance.Money--;
                _audio.Play();
            }
        }
        private void OnMouseEnter() {
            SetHover(true);
        }
        private void OnMouseExit() {
            SetHover(false);
        }
        private void SetHover(bool value) {
            foreach (var r in _renderers) {
                r.GetPropertyBlock(_block);
                _block.SetFloat("_Highlight", value ? 1f : 0f);
                r.SetPropertyBlock(_block);
            }
        }
        private void Start() {
            _block = new();
            _renderers = GetComponentsInChildren<Renderer>();
            GameManager.Instance.Cleanup += Instance_Cleanup;
        }

        private void Instance_Cleanup() {
            _debris.SetActive(true);
            _turret.SetActive(false);
        }
    }
}
