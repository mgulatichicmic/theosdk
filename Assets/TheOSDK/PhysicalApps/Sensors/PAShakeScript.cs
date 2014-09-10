using UnityEngine;
using System.Collections;


namespace PhysicalApps
{
	namespace Sensors
	{
		public class PAShakeScript //: MonoBehaviour 
		{
			// Hierarchy DRAG E DROP over var GUI Text in Inspector
			
			
			float accelerometerUpdateInterval = 1.0f / 60.0f;
			
			// The greater the value of LowPassKernelWidthInSeconds, the slower the filtered value will converge towards current input sample (and vice versa).
			float lowPassKernelWidthInSeconds = 1.0f;
			
			// This next parameter is initialized to 2.0 per Apple's recommendation, or at least according to Brady! <img src="http://www.lucedigitale.com/blog/wp-includes/images/smilies/icon_wink.gif" alt=";)" class="wp-smiley"> 
			float shakeDetectionThreshold = 2.5f ;
			
			private float lowPassFilterFactor; 
			private Vector3 lowPassValue = Vector3.zero;
			private Vector3 acceleration;
			private Vector3 deltaAcceleration;
			private bool shakeEvent;
			public PAShakeScript()
			{
				Start ();
			}

			void Start()
			{

				//Debug.Log (" Call Came ");
				lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds; 
//				shakeDetectionThreshold *= shakeDetectionThreshold;
				lowPassValue = Input.acceleration;
			}
			
			
			public void CheckForShake()
			{
				acceleration = Input.acceleration;
				lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
				deltaAcceleration = acceleration - lowPassValue;

//				Debug.Log( deltaAcceleration.sqrMagnitude + ">= "+ shakeDetectionThreshold );

				if ( !shakeEvent && deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
				{
					Debug.Log( " Shake Event Detected" );
					shakeEvent = true;
					// Perform your "shaking actions" here, with suitable guards in the if check above, if necessary to not, to not fire again if they're already being performed.
					// scoreTextX.text = "Shake event detected at time "+Time.time;
				}

//				if( deltaAcceleration.sqrMagnitude < shakeDetectionThreshold && shakeEvent )
//				{
//					shakeEvent = false;
//				}
			}
			
			public bool IsShaking()
			{
				if( !shakeEvent )
				{
					//Debug.Log( "Came here " );
					CheckForShake();
				}
				return shakeEvent;
			}

			public void Reset( )
			{
				shakeEvent = false;
			}

		}
	}
}