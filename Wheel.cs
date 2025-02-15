
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Wheel : UdonSharpBehaviour
{

	private WheelCollider wheelCollider;
	private Transform wheelMesh;

	private void Start()
	{
		wheelCollider = this.GetComponent<WheelCollider>();
		wheelMesh = this.transform.GetChild(0).GetComponent<Transform>();
	}
	public void FixedUpdate()
	{
		UpdateWheelVisuals(wheelCollider, wheelMesh);
	}
	void UpdateWheelVisuals(WheelCollider collider, Transform wheelMesh)
	{
		Vector3 pos;
		Quaternion rot;
		collider.GetWorldPose(out pos, out rot); // Get collider position & rotation
		wheelMesh.position = pos; // Apply to the mesh
		wheelMesh.rotation = rot; // Apply to the mesh
	}
}
