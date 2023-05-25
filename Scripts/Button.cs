using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Boolean isPlayer = true; 
    public GameObject player;
    public UnityEngine.Events.UnityEvent function;
    public UnityEngine.Events.UnityEvent letgo;
    private GameObject pressedby;
    IEnumerator LerpTo(GameObject obj, Vector3 target, float speed) {
        while (obj.transform.position != target) {
            obj.transform.position = Vector3.Lerp(obj.transform.position, target, Time.deltaTime * speed);
        }
        yield return null;
    }
    void OnTriggerEnter(Collider other) {
        if (isPlayer && other.gameObject == player) {
            lerp(true, other.gameObject);
            function.Invoke();
        }
        else if (!isPlayer && other.gameObject.tag == "Prop") {
            pressedby = other.gameObject;
            lerp(true, other.gameObject);
            function.Invoke();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (!isPlayer && other.gameObject == pressedby) {
            pressedby = null;
            lerp(false, other.gameObject);
            letgo.Invoke();
        }
        else if (isPlayer && other.gameObject == player) {
            lerp(false, other.gameObject);
            letgo.Invoke();
        }
    }

    public void lerp(Boolean InOut, GameObject obj) {
        if (InOut) transform.position -= new Vector3(0, 0.115f, 0);
        else transform.position += new Vector3(0, 0.115f, 0);
        /*if (InOut) {
            StartCoroutine(LerpTo(gameObject, transform.position - new Vector3(0, 0.375f, 0), 0.1f));
            
        }
        else {
            var goal = transform.position = new Vector3(0, 0.375f, 0);
            while (transform.position != goal) {
                transform.position = Vector3.Lerp(transform.position, goal, 0.1f);
            }
        } */
        /*RigidbodyInterpolation intertype = RigidbodyInterpolation.None;
        if (isPlayer) intertype = obj.GetComponent<Rigidbody>().interpolation;
        if (isPlayer) obj.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
        if (InOut) obj.transform.position -= new Vector3(0, 0.375f, 0);
        else obj.transform.position += new Vector3(0, 0.375f, 0);
         if (isPlayer) obj.GetComponent<Rigidbody>().interpolation = intertype;*/
    }   

    public void TestFunc() {
        Debug.Log("Button Pressed!");
    }
}
