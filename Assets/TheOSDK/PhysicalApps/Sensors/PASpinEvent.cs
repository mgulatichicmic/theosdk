using UnityEngine;
using System.Collections;


namespace PhysicalApps
{
	namespace Sensors
	{
		public class PASpinEvent 
		{
			PACatchEvent catchEvent;
//			PADeviceRest deviceRest;
//			PAShakeScript shakeScript;

			bool m_isShakedReset;
			bool m_startRotations;
			bool isSpinEventDetected = false;

			int totalLeftRotation, totalRightRotation, totalAwayRotation, totalTowardsRotation;
			int leftRotation, rightRotation, awayRotation, towardsRotation;

			public PASpinEvent()
			{
				Start ();
			}

			// Use this for initialization
			void Start () {

				catchEvent = new PACatchEvent ();
				Reset ();

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
							
			}

			int GetTotalRotations( int degrees )
			{
				return  Mathf.FloorToInt( Mathf.Abs( degrees ) / 360 );
			}

			public bool IsShakedToReset()
			{
				return m_isShakedReset;
			}

			public float GetMaxHeightReached()
			{
				return catchEvent.GetMaxHeightReached ();
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

			public bool IsSpinEventDetected()
			{

				return catchEvent.IsShakedToReset();
			}

			public void Reset()
			{
				PAGyroscopeRotations.Reset ();
				catchEvent.Reset();
//				deviceRest.Reset();
//				shakeScript.Reset();

				m_startRotations = false;
				isSpinEventDetected = false;
				m_isShakedReset = false;

				leftRotation = rightRotation = awayRotation = towardsRotation = 0;
				totalLeftRotation = totalRightRotation = totalAwayRotation = totalTowardsRotation = 0;
			}
			
			// Update is called once per frame
			public void Update () 
			{
				catchEvent.Update ();
				if( PAGyroscopeRotations.HasGyroscope() ) 
				{
					if( !isSpinEventDetected )
					{
						if( m_startRotations )
						{
							PAGyroscopeRotations.UpdateRotations();
						}
						if( !m_startRotations && catchEvent.CanStartSpinRotations() )
						{
							m_startRotations = true;
//							Debug.Log( " CanStartSpinRotations" );
							
						}
						if(  catchEvent.IsCatchEvent())
						{
							RotationValues();
							isSpinEventDetected = true;
							m_startRotations = false;
							
						}
					}
				}
			}
		}
	}
}
