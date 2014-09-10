using UnityEngine;
using System.Collections;

using PhysicalApps.Sensors;

public class DropEvent : MonoBehaviour {

	PADropEvent dropEvent;
	public bool isEventDetected;
	
	// Use this for initialization
	void Start () {
		dropEvent = new PADropEvent ();
		isEventDetected = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		dropEvent.Update ();
		if( isEventDetected && dropEvent.IsShakedToReset() )
		{
			Debug.Log( " Shake Event " );
			Reset();
		}
		if( !isEventDetected && dropEvent.IsDropEvent() )
		{
			isEventDetected = true;
		
			OnDropEvent();

		}
	}

	void OnDropEvent()
	{
			Debug.Log( " Drop Detected " );
		
		//   Add ur Code Here ///


	}


	void Reset()
	{
		isEventDetected = false;
		dropEvent.Reset();
	}
}
