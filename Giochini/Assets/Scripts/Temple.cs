using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temple : MonoBehaviour {

    public const int MAX_HAPPINESS = 100;

    public int templeIndex = 1;
    private float happiness = MAX_HAPPINESS;

    [Space]
    public float happySpeed=0.1f;
    public float happyAcceleration=0.01f;

    [Space]
    public float sacrificeReward = 40f;
    public float mistakeInjury = 20f;

    [Space]
    public SpriteRenderer[] requestsSR;
    public Light divineLight;
    public float minLightIntensity = 1f;
    public float maxLightIntensity = 10f;

    [Space]
    public AudioSource audioSource;

    private string[] requests = new string[3];
    private int[] validation = { 0, 0, 0 };

    private BoxCollider coll;
    private CalamityManager calamityManager;

    void Start() {
        coll = GetComponent<BoxCollider>();
        if (!coll) {
            Debug.LogError("MISSING BOX COLLIDER ON TEMPLE " + templeIndex);
        }
        calamityManager = FindObjectOfType<CalamityManager>();
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
        for (int i = 0; i < 3; i++) {
            int rand = Random.Range(0, DataContainer.instance.offers.Length);
            requests[i] = DataContainer.instance.offers[rand].code;
            requestsSR[i].sprite = DataContainer.instance.offers[rand].iconOn;
            validation[i] = 0;
        }
        //print(requests[0].ToString() + " " + requests[1].ToString() + " " + requests[2].ToString());
    }

    private void CheckOffers() {
        Collider[] overlapping = Physics.OverlapBox(transform.position + coll.center, coll.size / 2f, Quaternion.identity, LayerMask.GetMask("Offer"));
        
        if (overlapping.Length > 0) {
            foreach (Collider otherColl in overlapping) {

                bool match = false;
                Offer offer = otherColl.GetComponent<Offer>();

                for (int i = 0; i < 3; i++) {
                    if (validation[i] == 1) { continue; }
                    else if (offer.code == requests[i]) {
                        validation[i] = 1;
                        requestsSR[i].sprite = offer.iconOff;
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
                GenerateRequests();
                audioSource.Play();
                //print("VERY GOOD!");
            }
        }
    }

    private void AddHappiness(float value) {
        happiness = Mathf.Clamp(happiness + value, 0, MAX_HAPPINESS);
        ChangeLight();
        UIManager.instance.SetHappyBar(templeIndex, happiness);
        if (happiness <= 0) {
            UIManager.instance.EndGame(templeIndex);
        }
        calamityManager.ManageCalamity(templeIndex, happiness);
    }

    private void ChangeLight() {
        divineLight.intensity = maxLightIntensity - happiness / MAX_HAPPINESS * (maxLightIntensity - minLightIntensity);
    }

    public void DoBloodSacrifice() {
        happiness = MAX_HAPPINESS;
    }
}
