using UnityEngine;
using System.Collections;
using PhysicalApps.Sensors;

public class CatchEvent : MonoBehaviour {

	PACatchEvent catchEvent;
	public bool isEventDetected;
	// Use this for initialization
	void Start () {
	
		isEventDetected = false;
		catchEvent = new PACatchEvent ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		catchEvent.Update();
		if( isEventDetected && catchEvent.IsShakedToReset()  )
		{
			Debug.Log( " Shake Event " );
			Reset();
		}

		if( !isEventDetected && catchEvent.IsCatchEvent() )
		{
			isEventDetected = true;

			OnCatch();
		}
	}

	void OnCatch()
	{
		Debug.Log( " Catch Detected " );
		// Add ur code here //
	}

	void Reset()
	{
		isEventDetected = false;
		catchEvent.Reset();
	}
}
