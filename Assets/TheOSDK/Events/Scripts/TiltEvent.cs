using UnityEngine;
using System.Collections;
using PhysicalApps.Sensors;

public class TiltEvent : MonoBehaviour {

	PATiltEvent tiltEvent;
	public float TiltX;
	public float TiltY;
	
	public float SpeedX;
	public float SpeedY;
	
	// Use this for initialization
	void Start () {
	
		tiltEvent = new PATiltEvent ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		tiltEvent.Update ();
	
		TiltX =  tiltEvent.GetTiltX();
		TiltY = tiltEvent.GetTiltY();

		SpeedX = tiltEvent.GetSpeedTextX();
		SpeedY = tiltEvent.GetSpeedTextY();
	}
}
