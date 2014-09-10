using UnityEngine;
using System.Collections;


namespace PhysicalApps
{
	namespace Sensors
	{
		public class PATiltEvent// : MonoBehaviour 
		{
		//	public float tiltTextX, tiltTextY;
		//	public float speedTextX, speedTextY;

			int inclinationX, inclinationY;
			int SpeedX, SpeedY;
			int lastInclinationX, lastInclinationY;
			Vector3 acc;//, last;
			float pitch, roll;
			float m_time = 0.0f;
			float m_lastTime = 0.0f;
			// Use this for initialization
			void Start () {
		//		InvokeRepeating("Wait", 1f, 1f);
			
				m_time = m_lastTime = 0.0f;
			}


			void Reset()
			{
				SpeedX = SpeedY = 0;
			}

			void Display()
			{
//				tiltTextX.text = "Tilted In X = " + inclinationX;
//				tiltTextY.text = "Tilted In Y = " + inclinationY;
			}

			// Update is called once per frame
			public void Update () {

				//last = acc;
				#if !UNITY_EDITOR
					m_lastTime = m_time;
					m_time = Time.time;
					acc = Input.acceleration;

					roll  =   Mathf.Atan (acc.x / Mathf.Sqrt (acc.y * acc.y + acc.z * acc.z)) ; 
					roll = (roll * 180) / Mathf.PI;
					inclinationY = Mathf.RoundToInt (roll);
					
					pitch  =   Mathf.Atan (acc.y / Mathf.Sqrt (acc.x * acc.x + acc.z * acc.z)) ; 
					pitch = (pitch * 180) / Mathf.PI;
					inclinationX = Mathf.RoundToInt (pitch);


					m_time += Mathf.Abs(Time.time - m_lastTime);
					if( m_time > 0.5f )
					{
						m_time  = 0.0f;
						Wait ();
					}
				#endif
			}

			void Wait()
			{
				//Debug.Log (Time.time);

				if( lastInclinationX != inclinationX )
					SpeedX = Mathf.Abs( Mathf.Abs( inclinationX ) - Mathf.Abs(SpeedX));
				if( lastInclinationY != inclinationY )
					SpeedY = Mathf.Abs( Mathf.Abs(inclinationY) - Mathf.Abs(SpeedY) );

				lastInclinationX = inclinationX;
				lastInclinationY = inclinationY;

//				speedTextX.text = "Speed In X = " + SpeedX;
//				speedTextY.text = "Speed In Y = " + SpeedY;
			}

			public int GetTiltX()
			{
				return inclinationX;
			}

			public int GetTiltY()
			{
				return inclinationY;
			}

			public int GetSpeedTextX()
			{
				return SpeedX;
			}
			
			public int GetSpeedTextY()
			{
				return SpeedY;
			}
		}
	}
}
