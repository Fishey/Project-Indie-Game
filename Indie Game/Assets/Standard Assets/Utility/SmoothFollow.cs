using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{

		// The target we are following
		[SerializeField]
		private Transform target;
		// The distance in the x-z plane to the target
		[SerializeField]
		private float distance = 10.0f;
		private float targetLead = 0f;

		// Use this for initialization
		void Start() { }

		void LateUpdate()
		{
			if (!target)
				return;

			transform.position = target.position;
			transform.position -= (Vector3.forward * distance) + (Vector3.right * targetLead);


		}
	}
}