using UnityEngine;

public enum MovementTypes { Keyboard, Xbox, UAC }
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody _rigidbody;
	private CapsuleCollider _collider;
    private int _movementSpeed = 1000;
    private int _playerNum = 1;
    private float _maxSpeed = 10.0f;
    private PlayerClass _owner;
    private string[] _names;
	private float distToGround;
	private Transform mainCam;
    public MovementTypes mType;
	public float jumpHeight = 25;
    //public GameObject inGameMenu;
    string horizontalRightString, horizontalLeftString, verticalRightString, verticalLeftString, fireString, jumpString;


    float utime = 0;

    // Use this for initialization
    void Start()
    {
		mainCam = Camera.main.transform;
		Time.timeScale = 2.0f;
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<CapsuleCollider> ();
		setStrings ();
		distToGround = _collider.bounds.extents.y;
    }

	private bool isGrounded()
	{
		Debug.DrawLine (transform.position, transform.position+Vector3.up);
		//return Physics.Raycast (transform.position, -Vector3.up, distToGround + 0.1f);
		bool canJump = false;
		if (Physics.Raycast (transform.position, Vector3.up, distToGround + 0.1f) || Physics.Raycast (transform.position, -Vector3.up, distToGround + 0.1f))
			canJump = true;
		return canJump;
	}

    public void SetMovementType(int type)
    {
        mType = (MovementTypes)type;
		setStrings ();
    }

    public void SetPlayer(int Player)
    {
        _playerNum = Player;
		_names = Input.GetJoystickNames ();	
		if (_playerNum <= _names.GetLength (0)) {
			switch (_names [_playerNum - 1]) {
			case "Controller (Xbox 360 Wireless Receiver for Windows)":
				SetMovementType ((int)MovementTypes.Xbox);
				break;
			case "Controller (Xbox One For Windows)":
				SetMovementType((int)MovementTypes.Xbox);
				break;
			case "Controller (Xbox 360 For Windows)":
				SetMovementType((int)MovementTypes.Xbox);
				break;
			default:
				break;
			}
		}
		setStrings ();
    }

	void setStrings()
	{
		horizontalRightString = "P" + _playerNum + "_HorizontalRight";
		verticalRightString = "P" + _playerNum + "_VerticalRight";
		horizontalLeftString = "P" + _playerNum + "_HorizontalLeft";
		verticalLeftString = "P" + _playerNum + "_VerticalLeft";
		fireString = "P" + _playerNum + "_Fire1";
		jumpString = "P" + _playerNum + "_Jump";
		
		if (mType == MovementTypes.Xbox) {
			horizontalRightString += "Xbox";
			verticalRightString += "Xbox";
			verticalLeftString += "Xbox";
			horizontalLeftString += "Xbox";
			fireString += "Xbox";
			jumpString += "Xbox";
		}
	}
	
	public void SetMovementSpeed(int Movement)
	{
		_movementSpeed = Movement * (int)_owner.GetComponent<Rigidbody>().mass * 20;
        _maxSpeed = _movementSpeed / 100f;
    }

    public void SetOwner(PlayerClass owner)
    {
        _owner = owner;
    }

    public void Pause()
    {
        if (Time.timeScale == 1.0f) {
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}
    }

	public void unPause()
	{
		Time.timeScale = 1.0f;
	}
	
	// Update is called once per frame
	bool ready = true;

	void Update()
	{
		Debug.Log (isGrounded ());
		/*
		if (Input.GetButtonDown("Cancel") && playerNum == 1 && !inGameMenu.activeInHierarchy) // May be used later to open Pause Menu
		{
			inGameMenu.SetActive(true);
			Pause();
			return;
		}
		*/

		utime += Time.deltaTime;
		
		if (utime >= 6) {
			ready = true;
		}
		
		if (Input.GetButtonDown(fireString) && ready == true)
		{
			utime = 0;
			ready = false;
		}
		

	}
	void FixedUpdate()
	{
		Movement();
		Aim();
	}
	
	void Movement()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        float horizontalspeed = 0f;
        float verticalspeed = 0f;
			if ((screenPos.x >= Screen.width / 7) && (screenPos.x <= Screen.width - Screen.width / 7))
				horizontalspeed = Input.GetAxisRaw (horizontalLeftString) * Time.deltaTime;
		else if ((screenPos.x < Screen.width / 7) && (Input.GetAxisRaw (horizontalLeftString) * _movementSpeed * Time.deltaTime) > 0f)
			horizontalspeed = Input.GetAxisRaw (horizontalLeftString) * Time.deltaTime;
		else if ((screenPos.x > Screen.width - Screen.width / 7) && (Input.GetAxisRaw (horizontalLeftString) * _movementSpeed * Time.deltaTime) < 0f)
			horizontalspeed = Input.GetAxisRaw (horizontalLeftString) * Time.deltaTime;
			
			if ((screenPos.y >= Screen.height / 5) && (screenPos.y <= Screen.height - Screen.height / 5))
				verticalspeed = Input.GetAxisRaw (verticalLeftString) * Time.deltaTime;
			else if ((screenPos.y < Screen.height / 5) && (Input.GetAxisRaw (verticalLeftString) * _movementSpeed * Time.deltaTime) > 0f)
				verticalspeed = Input.GetAxisRaw (verticalLeftString) * Time.deltaTime;
			else if ((screenPos.y > Screen.height - Screen.height / 5) && (Input.GetAxisRaw (verticalLeftString) * _movementSpeed * Time.deltaTime) < 0f)
				verticalspeed = Input.GetAxisRaw (verticalLeftString) * Time.deltaTime;
		Vector3 movement = horizontalspeed * mainCam.right;
		_rigidbody.AddForce(movement.normalized * _movementSpeed * Time.deltaTime);

        if (_rigidbody.velocity.magnitude > _maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
        }

		if (Input.GetButtonDown(jumpString) && isGrounded() && ready == true)
		{
			Debug.Log(jumpHeight);
			_rigidbody.velocity += transform.up*jumpHeight;
			utime = 0;
		}
    }

    void Aim()
    {


    }

    public int PlayerNum
    {
        get { return this._playerNum; }
        set { this._playerNum = value; }
    }
}