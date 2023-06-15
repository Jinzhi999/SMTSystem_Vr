using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ëø½ø»ú¹¹
/// </summary>
public class LocoingNut : Nut
{
    public BracketCar bracketCar;

    protected override void FinishEvent(bool isTughtOrRelax)
    {
        bracketCar.UpdateState();
    }
}
