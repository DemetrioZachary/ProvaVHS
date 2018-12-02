using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Offer : MonoBehaviour {

    public string code;
    public float witheringTime = 6f;
    public Sprite iconOn, iconOff;
    public ParticleSystem bloodPS;

    private float startY;
    private Transform offersContainer;
    private Collider coll;

    [HideInInspector]
    public bool isGrabbed = false;

    private void Start() {
        startY = transform.position.y;
        coll = GetComponent<Collider>();
        offersContainer = GameObject.Find("OFFERS").transform;
    }

    public void StartWithering() {
        StartCoroutine(Wither());
    }

    private IEnumerator Wither() {
        while (witheringTime > 0) {
            yield return new WaitForEndOfFrame();
            if (!isGrabbed) {
                witheringTime -= Time.deltaTime;
            }
        }
        DestroyOffer();
    }

    public void Grab() {
        coll.enabled = false;
        isGrabbed = true;
    }

    public void Drop() {
        transform.parent = offersContainer;
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        coll.enabled = true;
        isGrabbed = false;
    }

    public void DestroyOffer() {
        Instantiate(bloodPS, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
