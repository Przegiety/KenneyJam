using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public int Health { get; set; }

    private void Awake() {
        Instance = this;
        
    }
    public void StartGame() {
        Health = 5;
    }
}

