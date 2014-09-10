using UnityEngine;
using System.Collections;

namespace PhysicalApps
{
	namespace Sensors
	{
		public class PAGyroscopeRotations {

			const float radian = 57.2958f;
			const float dt = 0.036f;   // 0.034f

			static Vector3 curRateOfRotation ;
			static float totalTime;
			static float leftRotation,  towardsRotation, zClockRotation;
			static int roll,pitch,yaw;

			static char ch;
			static float m_time;
			static bool m_isReachedMaxHeight;
			//int degree = 248;

			// Use this for initialization
			void Start () {
			//	ch = (char)degree;

				Reset ();
			}
			
			public static void Reset()
			{
				curRateOfRotation = Vector3.zero;
				leftRotation = towardsRotation = zClockRotation = 0f;
				roll = pitch = yaw = 0;
				m_time = 0f;
				m_isReachedMaxHeight = false;
				totalTime = 0f; 
			}


			public static bool HasGyroscope()
			{
				return Input.gyro.enabled;
			}

//			public static void Dummy()
//			{
//				if( !m_isReachedMaxHeight ) 
//				{
//					Debug.Log( "Came here" );
//					if ( m_time > 0f && Time.time  > m_time ) {
//						totalTime += 0.05f ;
//						m_time = Time.time + 0.05f;
//						Debug.Log( "Came here time = "+ Time.time+" totalTime = "+ totalTime ); 
//						
//					}
//					if( m_time == 0f )
//					{
//						Debug.Log( "Came here intial" );
//						m_time = Time.time + 0.05f;
//					}
//				}
//			}
			
			public static void UpdateRotations()
			{
				curRateOfRotation = Input.gyro.rotationRateUnbiased;

				if( curRateOfRotation != Vector3.zero )
				{
					if( curRateOfRotation.x > 0f )
					{
						//   + left rotation 
						//   - right rotation
						leftRotation -= Mathf.Abs( curRateOfRotation.x ) * dt * radian ;
						
						//rightRotation += Mathf.Abs( curRateOfRotation.x ) * dt * radian;
					}
					else if( curRateOfRotation.x < 0f )
					{
						// - right rotation
						// + left rotation
						leftRotation += Mathf.Abs( curRateOfRotation.x ) * dt * radian;
						//rightRotation -=  Mathf.Abs( curRateOfRotation.x ) * dt * radian;
						
					}
					
					if( curRateOfRotation.y > 0f )
					{
						// + towards rotation
						// - away rotation
						towardsRotation += Mathf.Abs( curRateOfRotation.y ) *dt * radian;
						//awayRotation -= Mathf.Abs( curRateOfRotation.y ) * dt * radian;
					}
					else if( curRateOfRotation.y < 0f )
					{
						// - towards rotation
						// + away rotation
						towardsRotation -= Mathf.Abs( curRateOfRotation.y ) *dt* radian;
						//awayRotation += Mathf.Abs( curRateOfRotation.y ) * dt * radian;
					}
					
					if( curRateOfRotation.z > 0 )
					{
						// + towards rotation
						// - away rotation
						zClockRotation -= Mathf.Abs( curRateOfRotation.z )  *dt* radian;
						//zAntiRotation -= Mathf.Abs( curRateOfRotation.z ) * dt * radian;
					}
					else if( curRateOfRotation.z < 0 )
					{
						// - towards rotation
						// + away rotation
						zClockRotation += Mathf.Abs( curRateOfRotation.z ) *dt* radian;
						//zAntiRotation += Mathf.Abs( curRateOfRotation.z ) * dt * radian;
					}
					
					roll = Mathf.FloorToInt (leftRotation);
					pitch = Mathf.FloorToInt (towardsRotation);
					yaw = Mathf.FloorToInt (zClockRotation);

				}
			}
			
//			public static float GetTime()
//			{
//				return totalTime;
//			}
//			
//			public static float FindMaximumHeight()
//			{
//				return (4.9f * totalTime * totalTime);
//			}
			
//			public static void SetAsReached( bool flag )
//			{
//				m_isReachedMaxHeight = flag;
//			}
			
			public static int GetPitch()
			{
				return pitch;
			}
			
			public static int GetRoll()
			{
				return roll;
			}
			
			public static int GetYaw()
			{
				return yaw;
			}
		}
	}
}
