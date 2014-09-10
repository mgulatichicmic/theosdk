using UnityEngine;
using System.Collections;

namespace PhysicalApps
{
	namespace Sensors
	{
		public class PAThrowEvent
		{
			PADeviceRest deviceRest;
			PAShakeScript shakeScript;

			// Check For Throw 
			bool m_recording;
			bool m_checkForThrow;
			bool m_throwEvent;
			bool m_checkForFall;
			int m_checkThrowCount;
			float m_timeTakenToReachMaxHeight;
			bool m_cantCheckIntial;
//			bool m_decideMaxHeightTime;
			float m_heightReached;
			//

			bool m_isShakedReset;


			public PAThrowEvent()
			{
				PAAccelerometer.Init ();
				shakeScript = new PAShakeScript ();
				deviceRest = new PADeviceRest ();
				Reset();
			}

			public void Reset()
			{
				deviceRest.Reset();
				shakeScript.Reset();

				m_recording = false;
				m_checkForThrow = false;
				m_throwEvent = false;
				m_checkThrowCount = 0;
				m_checkForFall = false;
				m_timeTakenToReachMaxHeight = 0f;
				m_cantCheckIntial = false;
//				m_decideMaxHeightTime = false;
				m_heightReached = 0f;
				m_isShakedReset = false;
			}

			public void Update()
			{
				PAAccelerometer.Update ();
				if( !m_throwEvent )
				{
					if ( !m_recording && PAAccelerometer.GetCurValue() != 0) 
					{
						m_recording = true;
						if( PAAccelerometer.GetCurValue() == -1 )
								m_cantCheckIntial = true;
					}
					if( m_recording && !m_cantCheckIntial )
						CheckForThrow ();
				}
			}

			void CheckForThrow()
			{			
				if( m_checkForThrow  && !m_checkForFall )
				{
					if( PAAccelerometer.GetCurValue() > 0 && PAAccelerometer.GetCurValue() >= PAAccelerometer.GetLastValue() )
					{
						m_checkThrowCount++;
					}
					if( m_checkThrowCount > 0 && PAAccelerometer.GetCurValue() < PAAccelerometer.GetLastValue() )
					{
//						Debug.Log( Input.acceleration.magnitude );
//						Debug.Log(  " preLastLast " + PAAccelerometer.GetPreLastLastValue()+ " preLast " + PAAccelerometer.GetPreLastValue() +" last "+ PAAccelerometer.GetLastValue()+" last "+ PAAccelerometer.GetCurValue());
						m_checkForFall = true;
					}
				}
				else if( !m_checkForThrow ){
					m_checkThrowCount = 0;
					m_checkForThrow  =false;
					if( PAAccelerometer.GetCurValue() > 0 && PAAccelerometer.GetLastValue() == 0 && PAAccelerometer.GetLastValue() == PAAccelerometer.GetPreLastValue() )
					{

						m_checkForThrow = true;	
						m_timeTakenToReachMaxHeight = Time.realtimeSinceStartup;
//						Debug.Log ("m_timeTakenToReachMaxHeight"+m_timeTakenToReachMaxHeight + " = " + Time.realtimeSinceStartup);
					}				
				}
				
				if( m_checkForFall )
				{
					if( PAAccelerometer.GetLastValue() >= PAAccelerometer.GetCurValue() )
					{	
						if( PAAccelerometer.GetCurValue() == -1 )
						{

							m_timeTakenToReachMaxHeight = Time.realtimeSinceStartup - m_timeTakenToReachMaxHeight;
//							Debug.Log ("m_timeTakenToReachMaxHeight"+m_timeTakenToReachMaxHeight + " = " + Time.realtimeSinceStartup);
							
							m_timeTakenToReachMaxHeight += 0.1f;
										
							FindMaxHeightReached();
							m_throwEvent = true;
						}
					}
					else if( PAAccelerometer.GetCurValue() > PAAccelerometer.GetLastValue() )
					{
						m_checkForThrow = false;
						Reset();
					}
				}
			}

			void CheckForShake()
			{
				if( m_throwEvent )
				{
					if( !m_isShakedReset && deviceRest.IsDeviceRest() &&  shakeScript.IsShaking()  )
					{
						m_isShakedReset = true;
					}
				}
			}

			public bool IsShakedToReset()
			{
				CheckForShake ();
				return m_isShakedReset;
			}

			public bool IsThrowStarted()
			{
				return m_checkForThrow;
			}

			public bool IsThrowEvent()
			{
				return m_throwEvent;
			}

			void FindMaxHeightReached()
			{
				//	V = U+ gt where V = 0 , g = -9.8 m/s , t = time taken
//				Debug.Log ("m_timeTakenToReachMaxHeight"+m_timeTakenToReachMaxHeight);

				float m_u ;
				m_u = (9.8f) * (float)m_timeTakenToReachMaxHeight;
				
				//m_heightReached = Mathf.Abs ((m_u * m_u) / (2 * 9.8f)); 

				m_heightReached = Mathf.Abs ((m_u * m_timeTakenToReachMaxHeight) - (4.9f) * (m_timeTakenToReachMaxHeight * m_timeTakenToReachMaxHeight )); 
			}	

			public float GetMaxHeightReached()
			{
				return m_heightReached;
			}
		}
	}
}
