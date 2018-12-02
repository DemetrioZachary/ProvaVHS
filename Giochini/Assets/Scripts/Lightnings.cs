using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightnings : MonoBehaviour {

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            Offer offer = other.GetComponentInChildren<Offer>();
            if (offer) {
                offer.DestroyOffer();
            }
        }
    }
}
