using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
{
    public GameObject Feet, AboveFeet;
    public bool isCollider;
    public List<GameObject> objects;  
    public List<GameObject> exclude;
    void OnTriggerEnter(Collider other)
    {
        if (exclude.Contains(other.gameObject)) return;
        objects.Add(other.gameObject);
        //Debug.Log(other.gameObject + " entered");
    }
    void OnTriggerExit(Collider other)
    {
        if (isCollider) objects.Remove(other.gameObject);
    }
    void Update()
    {
        if (!isCollider)
        {
            List<GameObject> flist = Feet.GetComponent<Vault>().objects;
            List<GameObject> alist = AboveFeet.GetComponent<Vault>().objects;
            if (flist.Count > 0) {
                foreach (var obj in flist) {
                    if (!alist.Contains(obj))
                    {
                        RigidbodyInterpolation oldval = gameObject.GetComponent<Rigidbody>().interpolation;
                        gameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
                        transform.position += obj.transform.InverseTransformDirection(Vector3.up);
                    }
                }
            }
        }
        else Debug.Log(gameObject + "'s List: " + objects);
    } 
}