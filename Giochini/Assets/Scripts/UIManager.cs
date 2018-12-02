using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public RawImage P1_HappyBar, P2_HappyBar;

    // -----------------------------------------------------
    private static UIManager s_Instance = null;

    public static UIManager instance {
        get {
            if (s_Instance == null) {
                Debug.LogError("Missing UI Manager");
            }
            return s_Instance;
        }
    }

    private void Awake() {
        s_Instance = FindObjectOfType<UIManager>();
    }
    // ----------------------------------------------------

    public void SetHappyBar(int playerIndex, float valuePercent) {
        if (playerIndex == 1) {
            P1_HappyBar.rectTransform.localScale = new Vector3(valuePercent / 100f, 1, 1);
        }
        else if (playerIndex == 2) {
            P2_HappyBar.rectTransform.localScale = new Vector3(valuePercent / 100f, 1, 1);
        }
    }
}
