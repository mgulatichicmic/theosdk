using UnityEngine;
using System.Collections;

namespace PhysicalApps
{
	namespace Sensors
	{
		public class PAAccelerometer  
		{
			static float m_preLastLastValue ,m_preLastValue,m_lastValue, m_curValue;

			public static void Init()
			{
				m_preLastLastValue = m_preLastValue = m_lastValue = m_curValue = Mathf.Round(Input.acceleration.magnitude - 1 );
			}

			public static void Update()
			{
				m_preLastLastValue = m_preLastValue;
				m_preLastValue = m_lastValue;
				m_lastValue = m_curValue;
				m_curValue = Mathf.Round (Input.acceleration.magnitude - 1);
			}

			public static float GetCurValue()
			{
				return m_curValue;
			}

			public static float GetLastValue()
			{
				return m_lastValue;
			}

			public static float GetPreLastValue()
			{
				return m_preLastValue;
			}

			public static float GetPreLastLastValue()
			{
				return m_preLastLastValue;
			}
			
		}
	}
}
