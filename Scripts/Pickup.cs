using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool holding = false;
    public GameObject cam;
    RigidbodyInterpolation oldval;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (holding) {
            gameObject.layer = 6;
            oldval = gameObject.GetComponent<Rigidbody>().interpolation;
            gameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.position = cam.transform.position + cam.transform.forward * 2;
        }
        else {
            gameObject.layer = 0;
            gameObject.GetComponent<Rigidbody>().interpolation = oldval;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
