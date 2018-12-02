using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temple : MonoBehaviour {

    public const int MAX_HAPPINESS = 100;

    public int templeIndex = 1;
    private float happiness = MAX_HAPPINESS;

    [Space]
    public float happySpeed;
    public float happyAcceleration;

    [Space]
    public float sacrificeReward = 40f;
    public float mistakeInjury = 20f;

    [Space]
    public SpriteRenderer[] requestsSR;
    public Light divineLight;

    private string[] requests = new string[3];
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

        AddHappiness(-Time.deltaTime * (happySpeed + 0.5f * happyAcceleration * Time.deltaTime));
        happySpeed += happyAcceleration * Time.deltaTime;
    }

    private void GenerateRequests() {
        for(int i = 0; i < 3; i++) {
            int rand = Random.Range(0, DataContainer.instance.offers.Length);
            requests[i] = DataContainer.instance.offers[rand].code;
            requestsSR[i].sprite = DataContainer.instance.offers[rand].icon;
        }
        //print(requests[0].ToString() + " " + requests[1].ToString() + " " + requests[2].ToString());
    }

    private void CheckOffers() {
        Collider[] overlapping = Physics.OverlapBox(transform.position + coll.center, coll.size, Quaternion.identity, LayerMask.GetMask("Offer"));

        if (overlapping.Length > 0) {
            foreach (Collider otherColl in overlapping) {

                bool match = false;
                Offer offer = otherColl.GetComponent<Offer>();

                for (int i = 0; i < 3; i++) {
                    if (validation[i] == 1) { continue; }
                    if (offer.code == requests[i]) {
                        validation[i] = 1;
                        match = true;
                        break;
                    }
                }
                offer.DestroyOffer();

                if (!match) {
                    AddHappiness(-mistakeInjury);
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
        divineLight.intensity = 1f - happiness / 100f;
        UIManager.instance.SetHappyBar(templeIndex, happiness);
        if (happiness <= 0) {
            GameManager.instance.EndGame(templeIndex);
        }
    }

    public void DoBloodSacrifice() {
        happiness = MAX_HAPPINESS;
    }
}
