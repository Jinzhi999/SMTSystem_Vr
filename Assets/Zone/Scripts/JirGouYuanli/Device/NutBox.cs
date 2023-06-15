using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutBox : MonoBehaviour
{
    public bool inUse = true;

    public Vector3 nutPos;
    public Vector3 nutRot;
    public Nut nut;
    void Start()
    {
        nut = transform.parent.GetComponentInChildren<Nut>();
        nutPos = nut.transform.localPosition;
        nutRot = new Vector3(0,0,120);
    }

    private void OnTriggerStay(Collider other)
    {
        if (inUse) return;
        Nut nut = other.GetComponent<Nut>();
        if (nut == null)
        {
            return;
        }
        nut.Place(this);
        nut.transform.SetParent(transform.parent);
        nut.transform.localPosition = nutPos;
        nut.transform.localEulerAngles = nutRot;

        inUse = true;
    }
}
