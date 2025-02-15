
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GrabbingWheel : UdonSharpBehaviour
{
    public UdonBehaviour car;
    public void OnPickup()
    {
        car.SetProgramVariable("isGrabbingWheel", true);
    }
    public void OnDrop()
    {
		car.SetProgramVariable("isGrabbingWheel", false);
	}

    public void OnPickupUseDown()
    {
		car.SetProgramVariable("isAccelerating", true);
	}
	public void OnPickupUseUp()
	{
		car.SetProgramVariable("isAccelerating", false);
	}
}
