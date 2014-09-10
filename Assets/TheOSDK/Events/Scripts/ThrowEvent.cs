using UnityEngine;
using System.Collections;
using PhysicalApps.Sensors;

public class ThrowEvent : MonoBehaviour {

	PAThrowEvent throwEvent;
	public bool isEventDetected;
	public float maxHeight;
	
	// Use this for initialization
	void Start () {
	
		isEventDetected = false;
		throwEvent = new PAThrowEvent ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		throwEvent.Update ();
		if( isEventDetected && throwEvent.IsShakedToReset()  )
		{
//			Debug.Log( " Shake Event " );
			Reset();
		}

		if( !isEventDetected && throwEvent.IsThrowEvent() )
		{
			isEventDetected = true;
		
			OnThrow();
		}
	}

	void OnThrow()
	{
		Debug.Log( " Throw Detected " );
		maxHeight =  throwEvent.GetMaxHeightReached();
		/// Add ur code here //
		/// 
	}

	void Reset()
	{
		isEventDetected = false;
		maxHeight = 0f;
		throwEvent.Reset();
	}
}
