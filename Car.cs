
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Car : UdonSharpBehaviour
{
	public WheelCollider[] frontWheels;
	public WheelCollider[] backWheels;
	public Rigidbody carBody;
	public float force;
	public float steerAngle;
	public float maxSteerAngle;
	private bool isAccelerating;
	private bool isSteering;
	public TextMeshProUGUI steeringdebug;
	public TextMeshProUGUI motordebug;
	private bool isSeeted;

	[UdonSynced] public float force_sync;
	[UdonSynced] public float steer_sync;
    void Start()
    {
        
    }
	public override void Interact()
	{
		Networking.LocalPlayer.UseAttachedStation();
	}
	public override void OnStationEntered(VRCPlayerApi player)
	{
		isSeeted = true;
		Networking.SetOwner(player, this.gameObject);
	}

	public override void OnStationExited(VRCPlayerApi player)
	{
		isSeeted = false;
	}

	void Update()
	{
		if (Networking.LocalPlayer.IsOwner(this.gameObject))
		{
			if (isSeeted == true)
			{
				// DESKTOP

				// forward, reverse
				isAccelerating = false;
				if (Input.GetKey(KeyCode.W))
				{
					isAccelerating = true;
					//foreach (WheelCollider wheel in backWheels)
					//{
					//	wheel.motorTorque = force;
					//}

					// sync

					force_sync = force;
				}
				if (Input.GetKey(KeyCode.S))
				{
					isAccelerating = true;
					//foreach (WheelCollider wheel in backWheels)
					//{
					//	wheel.motorTorque = -force;
					//}

					// sync

					force_sync = -force;
				}
				if (isAccelerating == false)
				{
					//foreach (WheelCollider wheel in backWheels)
					//{
					//	wheel.motorTorque -= force;
					//	if (wheel.motorTorque < 2)
					//	{
					//		wheel.motorTorque = 0;
					//	}
					//}

					// sync
					force_sync -= force;
					if (force_sync < 2)
					{
						force_sync = 0;
					}

				}



				// steer
				isSteering = false;

				if (Input.GetKey(KeyCode.A))
				{
					isSteering = true;
					//foreach (WheelCollider wheel in frontWheels)
					//{
					//	wheel.steerAngle += -steerAngle;
					//	if (wheel.steerAngle < -maxSteerAngle)
					//	{
					//		wheel.steerAngle = -maxSteerAngle;
					//	}
					//}

					// sync

					steer_sync += -steerAngle;
					if (steer_sync < -maxSteerAngle)
					{
						steer_sync = -maxSteerAngle;
					}
				}
				if (Input.GetKey(KeyCode.D))
				{
					isSteering = true;
					//foreach (WheelCollider wheel in frontWheels)
					//{
					//	wheel.steerAngle += steerAngle;
					//	if (wheel.steerAngle > maxSteerAngle)
					//	{
					//		wheel.steerAngle = maxSteerAngle;
					//	}
					//}

					// sync

					steer_sync += steerAngle;
					if (steer_sync > maxSteerAngle)
					{
						steer_sync = maxSteerAngle;
					}
				}
				if (isSteering == false)
				{
					//foreach (WheelCollider wheel in frontWheels)
					//{
					//	if (wheel.steerAngle > 0 && wheel.steerAngle <= maxSteerAngle)
					//	{
					//		wheel.steerAngle -= steerAngle;
					//	}
					//	if (wheel.steerAngle >= -2 && wheel.steerAngle <= 2)
					//	{
					//		wheel.steerAngle = 0;
					//	}
					//	if (wheel.steerAngle < 0 && wheel.steerAngle >= -maxSteerAngle)
					//	{
					//		wheel.steerAngle += steerAngle;
					//	}
					//}

					// sync

					if (steer_sync > 0 && steer_sync <= maxSteerAngle)
					{
						steer_sync -= steerAngle;
					}
					if (steer_sync >= -2 && steer_sync <= 2)
					{
						steer_sync = 0;
					}
					if (steer_sync < 0 && steer_sync >= -maxSteerAngle)
					{
						steer_sync += steerAngle;
					}
				}
			}
		}
		UpdateCar();
	}
	public void UpdateCar()
	{
		foreach (WheelCollider wheel in frontWheels)
		{
			wheel.steerAngle = steer_sync;
		}
		foreach (WheelCollider wheel in backWheels)
		{
			wheel.motorTorque = force_sync;
		}
		steeringdebug.text = frontWheels[0].steerAngle.ToString();
		motordebug.text = backWheels[0].motorTorque.ToString();
	}
}
