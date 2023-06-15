using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
/// <summary>
/// º‹≥µ
/// </summary>
public class BracketCar : MonoBehaviour
{
    public LinearDrive linearDrive;
    public LocoingNut locoingNutLeft;
    public LocoingNut locoingNutRight;
    // Start is called before the first frame update


    public void UpdateState()
    {
        if (!locoingNutLeft.isTighted&&!locoingNutRight.isTighted)
        {
            linearDrive.GetComponent<BoxCollider>().enabled = true;
            Debug.Log("≈°À…");
        }
        else
        {
            linearDrive.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
