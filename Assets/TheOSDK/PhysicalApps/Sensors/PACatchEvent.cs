using UnityEngine;
using System.Collections;

namespace PhysicalApps
{
	namespace Sensors
	{
		public class PACatchEvent
		{
			PAThrowEvent throwEvent;

			// Check For Catch Event
			bool m_throwEvent;
//			bool isRecordingStarted;
			int m_checkCatchEvent;
			bool m_checkForCatchEvent;
			bool m_catchEventDetected;
			bool m_isThrowStarted;
			//

			public PACatchEvent()
			{
				PAAccelerometer.Init ();
				throwEvent = new PAThrowEvent ();
				Reset ();
			}

			public void Reset()
			{
				throwEvent.Reset ();

				m_checkCatchEvent = 0;
				m_checkForCatchEvent = false;
				m_catchEventDetected = false;
				m_throwEvent = false;
				m_isThrowStarted = false;
			}

			public void Update ()
			{
				throwEvent.Update ();
				if( !m_catchEventDetected )
				{
					if( !m_isThrowStarted && throwEvent.IsThrowStarted() )
					{
						m_isThrowStarted = true;
					}
					if( !m_throwEvent && throwEvent.IsThrowEvent() )
					{
						m_throwEvent = true;
					}

					if( m_throwEvent && !m_catchEventDetected && CheckForCatchEvent())
					{
						m_catchEventDetected = true;
					}
				}
			}

						
			public bool IsShakedToReset()
			{
				return throwEvent.IsShakedToReset();
			}

			public bool CanStartSpinRotations()
			{
				return m_isThrowStarted;
			}

			public float GetMaxHeightReached()
			{
				return throwEvent.GetMaxHeightReached ();
			} 

			public bool IsCatchEvent()
			{
				return m_catchEventDetected;
			}

			bool CheckForCatchEvent()
			{
				if (!m_checkForCatchEvent) {
					if( PAAccelerometer.GetCurValue() > 0 && PAAccelerometer.GetPreLastValue() <= 0 && PAAccelerometer.GetLastValue() <= 0 && PAAccelerometer.GetPreLastLastValue() <= 0 )
					{
						m_checkForCatchEvent = true;
					}
				}
				if( m_checkForCatchEvent ) 
				{
					if ( PAAccelerometer.GetCurValue() >= 0 && PAAccelerometer.GetLastValue() >= 0 && PAAccelerometer.GetPreLastValue() >= 0 && PAAccelerometer.GetPreLastLastValue() >= 0) 
					{
						if( m_checkCatchEvent >= 3 )
						{
							if ( PAAccelerometer.GetCurValue() == 0 && PAAccelerometer.GetLastValue() == 0 && PAAccelerometer.GetLastValue() == PAAccelerometer.GetPreLastLastValue() && PAAccelerometer.GetCurValue() == PAAccelerometer.GetPreLastValue()) {
								return true;
							}
						}
						m_checkCatchEvent++;
					} 
					else {
						m_checkCatchEvent = 0;
						return false;
					}
				}
				return false;
			}
		}
	}
}
