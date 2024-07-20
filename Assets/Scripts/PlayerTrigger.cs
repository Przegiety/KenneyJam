using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour {
    public void TakeDamage(Unit unit) {
        unit.gameObject.SetActive(false);
        GameManager.Instance.Health--;
        Debug.Log("damage");
    }
}

