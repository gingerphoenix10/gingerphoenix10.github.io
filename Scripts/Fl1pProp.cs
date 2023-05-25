using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Fl1pProp : MonoBehaviour
{
    public GameObject player;
    public GameObject screen;
    public Text text;
    private bool Fl1pperUnlocked = false;
    public GameObject PrevFl1pper;
    public GameObject Fl1pper;
    //public GameObject Fl1pperCam;
    private RaycastHit hit;
    private bool holding;
    private GameObject holdingobj;
    IEnumerator HoldColor(Color start, float seconds, Color end) {
        screen.GetComponent<Image>().color = start;
        yield return new WaitForSeconds(seconds);
        screen.GetComponent<Image>().color = end;
    }
    IEnumerator ThatOneFunction(RigidbodyInterpolation oldval) {
        yield return 0;
        player.GetComponent<Rigidbody>().interpolation = oldval;
    }
    void Flip()
    {
        text.text = "Flip";
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit,200))
        {
            if (hit.collider.gameObject.tag == "Prop") {
                screen.GetComponent<Image>().color = Color.black;
                var proppos = hit.collider.gameObject.transform.position;
                var playerpos = player.transform.position;
                hit.collider.gameObject.transform.position = playerpos;
                RigidbodyInterpolation oldval = player.GetComponent<Rigidbody>().interpolation;
                player.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
                player.transform.position = proppos;
                StartCoroutine(HoldColor(Color.green, 1, Color.black));
                StartCoroutine(ThatOneFunction(oldval));
            }
            else StartCoroutine(HoldColor(Color.red, 1, Color.black));
        }
        else StartCoroutine(HoldColor(Color.red, 1, Color.black));
    }
    void changeTime(float value)
    {
        if (Mathf.Round(Time.timeScale*10)/10 == 0.1 && value < 0) {
            StartCoroutine(HoldColor(Color.red, 0.1f, Color.black));
            return;
        }
        Time.timeScale+=value;
        if (Time.timeScale > 2) {
            Time.timeScale = 2;
            StartCoroutine(HoldColor(Color.red, 1, Color.black));
        }
        text.text = "Time:\n" + Mathf.Round(Time.timeScale*10)/10 + "x";
    }
    void GrabFl1pper() {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit,5))
        {
            if (hit.collider.gameObject == PrevFl1pper) {
                Destroy(hit.collider.gameObject);
                Fl1pper.SetActive(true);
                Fl1pperUnlocked = true;
            }
        }
    }

    void hold() {               //Currently working on holding (must hover infront of player and disable Fl1pperUnlocked)
        if (!holding) {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit,5))
            {
                Debug.Log(hit.collider.gameObject);
                if (hit.collider.gameObject.tag == "Prop") {
                    holding = true;
                    holdingobj = hit.collider.gameObject;
                    holdingobj.GetComponent<Pickup>().holding = true;
                }
            }
        }
        else
        {
            holding = false;
            holdingobj.GetComponent<Pickup>().holding = false;
            holdingobj = null;
        }
    }

    void Update() {
        if (Fl1pperUnlocked && Input.GetKeyDown("f")) Flip();
        if (Gamepad.all.Count > 0) {
            if (Gamepad.current.bButton.wasPressedThisFrame && Fl1pperUnlocked) Flip();
        }

        if (Fl1pperUnlocked && Input.GetKeyDown("left")) changeTime(-0.1f);
        if (Gamepad.all.Count > 0) {
            if (Gamepad.current.leftShoulder.wasPressedThisFrame && Fl1pperUnlocked) changeTime(-0.1f);
        }

        if (Fl1pperUnlocked && Input.GetKeyDown("right")) changeTime(0.1f);
        if (Gamepad.all.Count > 0) {
            if (Gamepad.current.rightShoulder.wasPressedThisFrame && Fl1pperUnlocked) changeTime(0.1f);
        }
        
        if (!Fl1pperUnlocked && Input.GetKeyDown("e")) GrabFl1pper();
        if (Gamepad.all.Count > 0) {
            if (!Fl1pperUnlocked && Gamepad.current.xButton.wasPressedThisFrame) GrabFl1pper();
        }

        if (Fl1pperUnlocked && Input.GetKeyDown("e")) hold();
        if (Gamepad.all.Count > 0) {
            if (!Fl1pperUnlocked && Gamepad.current.xButton.wasPressedThisFrame) hold();
        }
    }
}
