using UnityEngine;
using System.Collections;
public enum PickupTypes{HEALTH, ENERGY
}

public class PickUpScript : MonoBehaviour {
	
	public PickupTypes PickupType;
	public float AmountRestored = 25;
	
	private PlayerClass _player;
	// Use this for initialization
	void Start () {
		_player = GameObject.Find("Player").GetComponent<PlayerClass>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player"){
			if (PickupType == PickupTypes.HEALTH){
				_player.ChangeHealth(AmountRestored);
				FMOD_StudioSystem.instance.PlayOneShot("event:/HealthPickup", transform.position);
			}
			else if (PickupType == PickupTypes.ENERGY){
				_player.ChangeEnergy(AmountRestored);
				FMOD_StudioSystem.instance.PlayOneShot("event:/EnergyPickup", transform.position);

			}
			gameObject.SetActive(false);
		}
	}
}



