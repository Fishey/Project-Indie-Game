using UnityEngine;


public class SmoothFollow : MonoBehaviour
{

	// The target we are following
	[SerializeField]
	private Transform target;
	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	public float TargetLead = 5f;
	public float FollowSpeed = 15f;
	public float FollowSpeedVert = 10f;
	public float Height = 10f;

	// Use this for initialization
	void Start() { }

	void LateUpdate()
	{
			if (!target)
				return;

			Vector3 newPos = target.position - (Vector3.forward * distance) + (Vector3.right * TargetLead) + (Vector3.up * Height);

			float distToTarget =  1 + (Vector3.Distance(transform.position, newPos) / 10);
			transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * (FollowSpeed * distToTarget));

	}
}