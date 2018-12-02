using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int playerIndex = 1;
    public float speed = 3;
    public float bloodSacrificeMultiplier = 0.75f;
    public Transform offerTransform;

    private Rigidbody rigidBody;
    private Offer offer;

    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update() {
        ManageMovement();
        ManageInteraction();
        ManageSacrifice();
        //Debug.DrawLine(transform.position, transform.position + 2*transform.forward, Color.blue);
    }

    private void ManageMovement() {
        float x = Input.GetAxis("Hor" + playerIndex);
        float z = Input.GetAxis("Ver" + playerIndex);

        if (Mathf.Abs(x) > 0 || Mathf.Abs(z) > 0) {
            Vector3 vec = new Vector3(x, 0, z);
            rigidBody.velocity = vec.normalized * Mathf.Clamp01(vec.magnitude) * speed;


            transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.forward, vec, Vector3.up), 0);
        }
    }

    private void ManageInteraction() {
        if (Input.GetButtonDown("Grab" + playerIndex)) {

            Offer newOffer = null;
            RaycastHit hit = new RaycastHit();

            if (Physics.SphereCast(new Ray(transform.position, transform.forward), 0.6f, out hit, 1f, LayerMask.GetMask("Offer"))) {
                //print(hit.collider.gameObject);
                newOffer = hit.collider.GetComponent<Offer>();
            }

            if (offer) {
                offer.Drop();
            }

            if (newOffer) {
                newOffer.transform.parent = transform;
                newOffer.transform.localPosition = offerTransform.localPosition;
                newOffer.transform.localRotation = offerTransform.localRotation;
                newOffer.Grab();
            }

            offer = newOffer;
        }
    }

    private void ManageSacrifice() {
        if (Input.GetButtonDown("Sacrifice" + playerIndex)) {
            Collider[] overlapping = Physics.OverlapBox(transform.position, Vector3.one, Quaternion.identity, LayerMask.GetMask("Temple"));
            if (overlapping.Length > 0) {
                Temple temple = overlapping[0].GetComponent<Temple>();
                if (temple.templeIndex == playerIndex) {
                    temple.DoBloodSacrifice();
                    speed *= bloodSacrificeMultiplier;

                    print("BLOOD!");
                }
            }
        }
    }
}
