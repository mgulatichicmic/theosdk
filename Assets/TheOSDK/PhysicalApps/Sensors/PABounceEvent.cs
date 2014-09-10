using UnityEngine;
using System.Collections;

namespace PhysicalApps
{
	namespace Sensors
	{
		public class PABounceEvent
		{
			PADropEvent dropEvent;
			
			// Drop Values 			
			bool m_bounceEvent;
			bool m_dropEvent;

			//

			//  variables for Force at Imapact
			const float m_massOfBall = .612f;   //  612 gms / 0.612 kg
			const float m_massOfDevicesAvg = .130f;   //  130 gms / 0.13 kg
			float m_totalAirRecordTime;
			float m_forceAtImpact;	
			//

			public PABounceEvent()
			{
				PAAccelerometer.Init ();
				dropEvent = new PADropEvent ();
				Reset();
			}

			public void Reset()
			{
				dropEvent.Reset ();

				m_bounceEvent = false;
				m_dropEvent = false;

			}

			public void Update ()
			{
				dropEvent.Update ();

				if( !m_bounceEvent )
				{
					if( !m_dropEvent && dropEvent.IsDropEvent() )
					{
						m_dropEvent = true;
						m_totalAirRecordTime = Time.realtimeSinceStartup;
						
					}
					if(m_dropEvent && CheckForBouncePeak() ) 
					{
						m_bounceEvent = true;
						m_totalAirRecordTime = Time.realtimeSinceStartup - m_totalAirRecordTime ;
						FindForceAtImpact();
					}
				}
			}

			public bool IsForceAtImpactCalculated()
			{
				return m_bounceEvent;
			}

			public float GetForceAtImpact()
			{
				return m_forceAtImpact;
			}

			void FindForceAtImpact()
			{
				m_forceAtImpact = ( (m_massOfBall + m_massOfDevicesAvg ) * ( 9.8f * 9.8f ) *  ( m_totalAirRecordTime * m_totalAirRecordTime )) / ( 2.0f * 0.4f  ); // consider m_slowDownDistance = 0.05 // * 100 
//				Debug.Log ("Force At Impact = " + m_forceAtImpact);
			}

			public bool IsShakedToReset()
			{
				return dropEvent.IsShakedToReset();
			}

			public bool IsBounceEvent()
			{
				return m_bounceEvent;
			}
					
			bool CheckForBouncePeak()
			{
				if ( PAAccelerometer.GetCurValue() >= 0 && PAAccelerometer.GetLastValue() == -1 && PAAccelerometer.GetPreLastValue() == -1 && PAAccelerometer.GetPreLastLastValue() == -1)
				{
					return true;
				}
				return false;
			}
		}
	}
}
