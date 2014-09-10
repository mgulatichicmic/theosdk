using UnityEngine;
using System.Collections;
using PhysicalApps.Sensors;

public class SpinEvent : MonoBehaviour {

	PASpinEvent spinEvent;
	public float leftRotation, rightRotation, towardsRotation, awayRotation, DistanceReached;
	public float totalLeftRotation , totalRightRotation, totalTowardsRotation, totalAwayRotation;
	public bool isEventDetected;
	// Use this for initialization
	void Start () {
		isEventDetected = false;
		spinEvent = new PASpinEvent();
		spinEvent.Reset();
	}
	
	// Update is called once per frame
	void Update () {


		spinEvent.Update ();
		if( isEventDetected && spinEvent.IsShakedToReset() )
		{
			//Debug.Log( " Shake Event " );

			Reset();
		}
		if( !isEventDetected && spinEvent.IsSpinEventDetected() )
		{
			isEventDetected = true;
			GetRotationalValues();
		
			OnSpin();

		}
	}


	void OnSpin()
	{		
		Debug.Log( " Spin Detectd" );		
		/// Add ur code here //

	}

	void GetRotationalValues()
	{
		leftRotation =  spinEvent.GetLeftRotation();
		totalLeftRotation = spinEvent.GetTotalLeftRotation();
		
		rightRotation = spinEvent.GetRightRotation();
		totalRightRotation = spinEvent.GetTotalRightRotation();
		
		
		towardsRotation = spinEvent.GetTowardsRotation();
		totalTowardsRotation = spinEvent.GetTotalTowardsRotation();
		
		
		awayRotation = spinEvent.GetAwayRotation();
		totalAwayRotation = spinEvent.GetTotalAwayRotation();
		
		DistanceReached = spinEvent.GetMaxHeightReached() ;
	}

	void Reset()
	{
		isEventDetected = false;
		leftRotation =  rightRotation = towardsRotation = awayRotation = DistanceReached = 0f;
		totalLeftRotation = totalRightRotation = totalRightRotation = totalAwayRotation = 0f;
		spinEvent.Reset();
	}
}
