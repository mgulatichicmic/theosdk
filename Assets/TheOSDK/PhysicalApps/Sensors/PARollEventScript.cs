
using UnityEngine;
using System.Collections;

namespace PhysicalApps
{
	namespace Sensors
	{
		public class PARollEventScript //: MonoBehaviour 
		{

		//	public TextMesh leftRotationText, rightRotationText, towardsRotationText, awayRotationText, DistanceRolledText, IsGyroText;//, UpText, GradualText, TotalTime;
			//public TextMesh rollEventText;
			float totalLeftRotation, totalRightRotation, totalAwayRotation, totalTowardsRotation;
			int gradualCheckCount;
			bool isRollEventStarted, isRollEventDetected, isReleased;
			const float ballDiameter  = 20f; // cm
			const float feetConversion = 0.0328083989501f;
			const float completeRotation = 75f;  // cm
			float distanceRolled = 0f;
			float totalHighRotations = 0f;
			int leftRotation, rightRotation, awayRotation, towardsRotation;
			// Use this for initialization

			public PARollEventScript()
			{
				Start ();
			}

			void Start () {
				if( PAGyroscopeRotations.HasGyroscope() )
				{
					ResetRollValues ();
				}
			}

			// Update is called once per frame
			void RollUpdate () {

				if( PAGyroscopeRotations.HasGyroscope() )
				{
					if( !isRollEventDetected && !isReleased && CheckForRelease() )
					{
						ResetRollValues ();
						isReleased = true;
					}
					if( isReleased )
					{
						if( !isRollEventStarted && CheckForRollEventStarted() )
						{
							isRollEventStarted = true;
							Debug.Log( "Roll Started" );
							//rollEventText.text = "" + isRollEvent ;
						}
						
						if( isRollEventStarted )
						{
							//GyroscopeRotations.UpdateRotations();
							if( CheckForBallStopped() )
							{
								RotationValues();
								//					isRollEvent  = false;
								isReleased = false;
								isRollEventDetected = true;
								
								
								Debug.Log( "Roll Stopped" );
								//StartCoroutine( Wait( 2.0f ) );
							}
						}
					}
				}
			}
			
			bool CheckForRelease()
			{

				if (Input.acceleration.magnitude > 2f)
					return true;
				return false;
			}
			
			void OnGUI()
			{
				if (GUI.Button (new Rect (Screen.width / 2 + Screen.width / 3, Screen.height / 3, Screen.width / 8, Screen.height / 9), "Reset")) {
					isRollEventDetected = false;
					ResetRollValues ();
				}
			}
			
//			IEnumerator Wait( float waitTime ) {
//				yield return new WaitForSeconds(waitTime);
//				isRollEventDetected = false;
//				
//			} 
			
			public void ResetRollValues()
			{
				PAGyroscopeRotations.Reset ();
				
				isRollEventDetected = false;
				isRollEventStarted = false;
				isReleased = false;
				gradualCheckCount = 0;
				
				distanceRolled = 0f;
				totalHighRotations = 0f;
				
				leftRotation = rightRotation = awayRotation = towardsRotation = 0;
				totalLeftRotation = totalRightRotation = totalAwayRotation = totalTowardsRotation = 0;
				
			}


			int GetTotalRotations( int degrees )
			{
				return  Mathf.FloorToInt( Mathf.Abs( degrees ) / 360 );
			}

			void RotationValues()
			{
				int rollDegrees = PAGyroscopeRotations.GetRoll ();
				int pitchDegrees = PAGyroscopeRotations.GetPitch ();
				
				if( rollDegrees >= 0 )
				{
					leftRotation = rollDegrees;
					totalLeftRotation = GetTotalRotations( rollDegrees );
				}
				if( rollDegrees < 0)
				{
					rightRotation = Mathf.Abs( rollDegrees );
					totalRightRotation = GetTotalRotations( rollDegrees );
				}


				if( pitchDegrees >= 0 )
				{
					towardsRotation = pitchDegrees;
					totalTowardsRotation = GetTotalRotations( pitchDegrees );
				}
				if( pitchDegrees < 0 )
				{
					awayRotation = Mathf.Abs( pitchDegrees );
					totalAwayRotation = GetTotalRotations( pitchDegrees );
				}
				
				FindDistanceRolled ();
				
			}

			public bool CheckForRollEvent()
			{
				//if( !isRollEventDetected )
				RollUpdate ();
				return isRollEventDetected;
			}

			public float GetDistanceRolled()
			{
				return distanceRolled;
			}

			public float GetTotalLeftRotation()
			{
				return totalLeftRotation;
			}

			public float GetTotalRightRotation()
			{
				return totalRightRotation;
			}

			public float GetTotalAwayRotation()
			{
				return totalAwayRotation;
			}

			public float GetTotalTowardsRotation()
			{
				return totalTowardsRotation;
			}

			public float GetLeftRotation()
			{
				return leftRotation;
			}
			
			public float GetRightRotation()
			{
				return rightRotation;
			}
			
			public float GetAwayRotation()
			{
				return awayRotation;
			}
			
			public float GetTowardsRotation()
			{
				return towardsRotation;
			}

			void FindDistanceRolled()
			{
				int totalRollRotations, totalPitchRotations;
				totalRollRotations = GetTotalRotations( PAGyroscopeRotations.GetRoll() );
				totalPitchRotations = GetTotalRotations( PAGyroscopeRotations.GetPitch() );

				totalHighRotations = totalRollRotations > totalPitchRotations ? totalRollRotations : totalPitchRotations;
				
				distanceRolled = completeRotation * totalHighRotations * feetConversion;
				
				distanceRolled = Mathf.Round (distanceRolled);
				
			}
			
			bool CheckForBallStopped()
			{
				if (Input.gyro.rotationRateUnbiased.magnitude < 0.3f)   // 0.2f
					return true;
				return false;
			}
			
			bool CheckForRollEventStarted()
			{
				PAGyroscopeRotations.UpdateRotations ();
				if( Input.gyro.rotationRateUnbiased.magnitude > 0.2f && Input.acceleration.magnitude > 1f )   //   1f
				{
					gradualCheckCount++;
					if( gradualCheckCount > 10 )
					{
						return true;
					}
				}
				else
				{
					gradualCheckCount = 0;
				}
				return false;
			}
		}
	}
}
