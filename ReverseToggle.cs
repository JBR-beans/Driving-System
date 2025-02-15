
using UdonSharp;
using UdonSharp.Examples.Utilities;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ReverseToggle : UdonSharpBehaviour
{
    public UdonBehaviour car;
    public override void Interact()
    {
        car.SetProgramVariable("isReversing", !(bool)car.GetProgramVariable("isReversing"));
    }
}
