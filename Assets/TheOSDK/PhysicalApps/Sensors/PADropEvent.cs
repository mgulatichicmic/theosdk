using UnityEngine;
using System.Collections;

namespace PhysicalApps
{
	namespace Sensors
	{
		public class PADropEvent
		{
			PADeviceRest deviceRest;
			PAShakeScript shakeScript;

			// Drop Values 			
			int m_dropCheckCount;
			bool m_dropEvent;
			bool m_checkDropEvent;

			bool m_isShakedReset;
			
			//

			public PADropEvent()
			{
				shakeScript = new PAShakeScript ();
				deviceRest = new PADeviceRest ();
				Reset ();
				PAAccelerometer.Init ();
			}

			public void Reset()
			{
				deviceRest.Reset();
				shakeScript.Reset();

				m_isShakedReset = false;

				m_dropCheckCount = 0;
				m_dropEvent = false;
				m_checkDropEvent = false;

			}

			void CheckForShake()
			{
				if( m_dropEvent )
				{
					if( !m_isShakedReset && deviceRest.IsDeviceRest() &&  shakeScript.IsShaking()  )
					{
						m_isShakedReset = true;
					}
				}
			}

			public void Update ( )
			{

				PAAccelerometer.Update ();

				if( !m_dropEvent )
				{
					if ( PAAccelerometer.GetCurValue() != 0 ) 
					{

						if ( CheckForDrop ()) 
						{   ///     !m_throwEvent &&
//							Debug.Log( "Drop Event Detected" );
							m_dropEvent = true;
						}	
					}
				}
			}

			public bool IsShakedToReset()
			{
				CheckForShake ();
				return m_isShakedReset;
			}

			public bool IsDropEvent()
			{
				return m_dropEvent;
			}

//			public bool CanStartTimer()
//			{
//				return m_checkDropEvent;
//			}

			bool CheckForDrop()
			{

				if( m_checkDropEvent && PAAccelerometer.GetCurValue() == -1 &&  PAAccelerometer.GetCurValue() == PAAccelerometer.GetLastValue()  )
				{
					m_dropCheckCount++;
					if( m_dropCheckCount > 5 )
					{
						return true;
					}
				}
				else
				{
					if( PAAccelerometer.GetCurValue() == -1 && PAAccelerometer.GetPreLastLastValue() == 0 && PAAccelerometer.GetPreLastValue() == PAAccelerometer.GetPreLastLastValue() && PAAccelerometer.GetLastValue() == PAAccelerometer.GetPreLastValue()) // m_lastValue = 0
					{
						m_checkDropEvent = true;
					}
					
					m_dropCheckCount = 0;
					return false;
				}
				return false;
			}
		}
	}
}
