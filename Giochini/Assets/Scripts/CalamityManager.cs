using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CalamityStage {
    public float ragePoint;
    public GameObject calamity1;
    public GameObject calamity2;
};

public class CalamityManager : MonoBehaviour {

    [Tooltip("From higher to lower value")]
    public CalamityStage[] rageStates;

    public void ManageCalamity(int playerIndex, float happiness) {
        for (int i = 0; i < rageStates.Length; i++) {
            bool result = happiness < rageStates[i].ragePoint;
            if (playerIndex == 1)
                rageStates[i].calamity1.SetActive(result);
            else if (playerIndex == 2) {
                rageStates[i].calamity2.SetActive(result);
            }

        }
    }
}
