using UnityEngine;
using System.Collections;


namespace PhysicalApps
{
	namespace Sensors
	{
		public class PAForceAtImpact 
		{

			PABounceEvent bounceEvent;

			public PAForceAtImpact()
			{
				bounceEvent = new PABounceEvent ();

				Reset ();
			}

			public void Update()
			{
				bounceEvent.Update ();
			}

			public bool IsShakedToReset()
			{
				return bounceEvent.IsShakedToReset();
			}

			public bool IsForceAtImpact()
			{
				return bounceEvent.IsForceAtImpactCalculated();
			}

			public void Reset()
			{
				if( bounceEvent != null )
					bounceEvent.Reset ();
			}

			public float GetForceAtImpact()
			{
				return bounceEvent.GetForceAtImpact();
			}

		}
	}
}