using UnityEngine;
using System.Collections;

namespace PhysicalApps
{
	namespace Sensors
	{
		public class PADeviceRest //: MonoBehaviour 
		{
			int m_checkResetCount;
			bool m_DeviceRest;

			public PADeviceRest()
			{
				Reset();
			}

			void CheckDeviceRest()
			{
				PAAccelerometer.Update();
				if( !m_DeviceRest )
				{
					if(  PAAccelerometer.GetCurValue() == 0 &&   PAAccelerometer.GetCurValue() ==  PAAccelerometer.GetLastValue()  )
					{
						m_checkResetCount++;
						if( m_checkResetCount > 25 )
						{
							m_DeviceRest = true;
						}
					}
					else
					{
						m_checkResetCount = 0;
					}
				}
			}

			public bool IsDeviceRest()
			{			
				if( !m_DeviceRest )
					CheckDeviceRest();
				return m_DeviceRest;
			}

			public void Reset()
			{
				m_DeviceRest = false;
				m_checkResetCount = 0;
			}
		}
	}
}