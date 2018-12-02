using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataContainer : MonoBehaviour {

    public Offer[] offers;

    // -----------------------------------------------------
    private static DataContainer s_Instance = null;

    public static DataContainer instance {
        get {
            if (s_Instance == null) {
                Debug.LogError("Missing Data Container");
            }
            return s_Instance;
        }
    }

    private void Awake() {
        s_Instance = FindObjectOfType<DataContainer>();
    }
    // ----------------------------------------------------
}
