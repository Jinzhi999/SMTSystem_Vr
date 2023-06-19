using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¼ìÐÞ¸Ç¿Ú
/// </summary>
public class Hatch : MonoBehaviour
{
    public bool inUse = true;

    public Transform hatchPlace;
    public NutTypeHatchCover hatchCover;
    void Start()
    {
        hatchCover = transform.GetComponentInChildren<NutTypeHatchCover>();
        inUse = (bool)hatchCover;
    }

    public void Place(NutTypeHatchCover hatch_cover)
    {
        hatch_cover.transform.SetParent(hatchPlace);
        hatch_cover.transform.localPosition = Vector3.zero;
        hatch_cover.transform.localEulerAngles = Vector3.zero;
        hatchCover = hatch_cover;
        inUse = true;
    }
}
