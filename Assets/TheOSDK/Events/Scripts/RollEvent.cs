using UnityEngine;
using System.Collections;
using PhysicalApps.Sensors;

public class RollEvent : MonoBehaviour {

	PAShakeScript ss;
	PARollEventScript rollevet;
	public float leftRotation, rightRotation, towardsRotation, awayRotation, DistanceRolled;
	public float totalLeftRotation , totalRightRotation, totalTowardsRotation, totalAwayRotation;
	public bool isEventDetected;
	// Use this for initialization
	void Start () {
		isEventDetected = false;
		rollevet = new PARollEventScript();
		rollevet.ResetRollValues();
		ss = new PAShakeScript ();
	}
	
	// Update is called once per frame
	void Update () {

		if( isEventDetected && ss.IsShaking() )
		{
			//Debug.Log( " Shake Event " );

			Reset();
		}
		if( !isEventDetected && rollevet.CheckForRollEvent() )
		{
			isEventDetected = true;

			GetRotationalValues();

			OnRoll();
		}
	}

	void OnRoll()
	{
		Debug.Log( "Roll Event Detected "); 
		// Add ur code here //

	}

	void GetRotationalValues()
	{
		leftRotation = rollevet.GetLeftRotation();
		totalLeftRotation = rollevet.GetTotalLeftRotation();
		
		
		rightRotation = rollevet.GetRightRotation();
		totalRightRotation = rollevet.GetTotalRightRotation();
		
		
		towardsRotation = rollevet.GetTowardsRotation();
		totalTowardsRotation =  rollevet.GetTotalTowardsRotation();
		
		
		awayRotation = rollevet.GetAwayRotation();
		totalAwayRotation =  rollevet.GetTotalAwayRotation();
		
		DistanceRolled = rollevet.GetDistanceRolled();
	}

	void Reset()
	{
		isEventDetected = false;
		leftRotation =  rightRotation = towardsRotation = awayRotation = DistanceRolled = 0f;
		totalLeftRotation = totalRightRotation = totalRightRotation = totalAwayRotation = 0f;
		rollevet.ResetRollValues();
		ss.Reset();
	}
}
