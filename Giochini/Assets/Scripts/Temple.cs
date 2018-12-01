using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temple : MonoBehaviour {

    public int templeIndex = 1;
    public float happiness = 100f;

    public float sacrificeReward = 40f;
    public float mistakeInjury = 20f;

    [Space]
    public DataContainer dataContainer; 
    public SpriteRenderer[] requestsSR;

    private OfferType[] requests = new OfferType[3];
    private int[] validation = { 0, 0, 0 };

    private BoxCollider coll;

    void Start() {
        coll = GetComponent<BoxCollider>();
        if (!coll) {
            Debug.LogError("MISSING BOX COLLIDER ON TEMPLE " + templeIndex);
        }
        GenerateRequests();
        //print(System.Enum.GetValues(typeof(OfferType)).Length);
    }

    void Update() {
        if (Input.GetButtonDown("Grab" + templeIndex)) {
            CancelInvoke();
            Invoke("CheckOffers", 0.1f);
        }
    }

    private void GenerateRequests() {
        int max = System.Enum.GetValues(typeof(OfferType)).Length;
        for(int i = 0; i < 3; i++) {
            int r = Random.Range(0, max);
            requests[i] = (OfferType)r;
            requestsSR[i].sprite = dataContainer.requestSprites[r];
        }
        //print(requests[0].ToString() + " " + requests[1].ToString() + " " + requests[2].ToString());
    }

    private void CheckOffers() {
        Collider[] overlapping = Physics.OverlapBox(transform.position + coll.center, coll.size, Quaternion.identity, LayerMask.GetMask("Offer"));

        if (overlapping.Length > 0) {
            foreach (Collider c in overlapping) {

                bool match = false;
                Offer offer = c.GetComponent<Offer>();

                for (int i = 0; i < 3; i++) {
                    if (validation[i] == 1) { continue; }
                    if (offer.type == requests[i]) {
                        validation[i] = 1;
                        match = true;
                        break;
                    }
                }
                offer.DestroyOffer();

                if (!match) {
                    AddHappiness(mistakeInjury);
                    //print("SO BAD!");
                }
            }

            if (validation[0] == 1 && validation[1] == 1 && validation[2] == 1) {
                AddHappiness(sacrificeReward);
                //print("VERY GOOD!");
            }
        }
    }

    private void AddHappiness(float value) {
        happiness = Mathf.Clamp(happiness + value, 0, 100);
    }
}
