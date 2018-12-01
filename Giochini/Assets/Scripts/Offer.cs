using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OfferType {A,B,C }

public class Offer : MonoBehaviour {

    public OfferType type;

    private float startY;
    private Transform offersContainer;
    private Collider coll;

    [HideInInspector]
    public bool isPicked = false;

    private void Start() {
        startY = transform.position.y;
        coll = GetComponent<Collider>();
        offersContainer = GameObject.Find("OFFERS").transform;
    }

    public void Grab() {
        coll.enabled = false;
        isPicked = true;
    }

    public void Drop() {
        transform.parent = offersContainer;
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        coll.enabled = true;
        isPicked = false;
    }

    public void DestroyOffer() {
        // TEMP
        Destroy(gameObject);
    }
}
