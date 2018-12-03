using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float spawnDelay = 10f;
    [Space]
    public Offer offerprefab;
    public Transform spawnTransform;

    private Offer offer;

	void Start () {
        if (Random.Range(0, 10) > 4) {
            StartCoroutine(SpawnOffer());
        }
        else {
            offer = Instantiate(offerprefab, spawnTransform) as Offer;
            StartCoroutine(CheckIfGrabbed());
        }
	}

    private IEnumerator SpawnOffer() {
        float time = 0;
        while (time < spawnDelay) {
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
        offer = Instantiate(offerprefab, spawnTransform) as Offer;
        StartCoroutine(CheckIfGrabbed());
    }

    private IEnumerator CheckIfGrabbed() {
        while(!offer.isGrabbed){
            yield return new WaitForEndOfFrame();
        }

        offer.StartWithering();
        offer = null;
        StartCoroutine(SpawnOffer());
    }
}
