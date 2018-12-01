using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temple : MonoBehaviour {

    public int templeIndex = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (other.GetComponent<PlayerController>().playerIndex == templeIndex) {

            }
        }
    }
}
