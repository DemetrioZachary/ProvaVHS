using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // -----------------------------------------------------
    private static GameManager s_Instance = null;

    public static GameManager instance {
        get {
            if (s_Instance == null) {
                Debug.LogError("Missing Game Manager");
            }
            return s_Instance;
        }
    }

    private void Awake() {
        s_Instance = FindObjectOfType<GameManager>();
    }
    // ----------------------------------------------------

}
