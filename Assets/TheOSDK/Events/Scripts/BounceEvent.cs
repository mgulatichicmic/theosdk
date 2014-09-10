using UnityEngine;
using System.Collections;
using PhysicalApps.Sensors;

public class BounceEvent : MonoBehaviour {

	PABounceEvent bounceEvent;
	public bool isEventDetected;
	public float forceAtImpact;
	// Use this for initialization
	void Start () {
	
		isEventDetected = false;
		bounceEvent = new PABounceEvent ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		bounceEvent.Update();

		if( isEventDetected  && bounceEvent.IsShakedToReset())
		{
			Debug.Log( " Shake Event " );
			Reset();
		}
		if( !isEventDetected && bounceEvent.IsBounceEvent() )
		{
			isEventDetected = true;
			OnBounce();
		}
	}

	void OnBounce()
	{
		forceAtImpact =  bounceEvent.GetForceAtImpact();		
		Debug.Log(  " Bounce Detected ");

		///  Add ur code here and get the force at impact //


	}

	void Reset()
	{
		isEventDetected = false;
		forceAtImpact = 0f;
		bounceEvent.Reset();
	}
}
