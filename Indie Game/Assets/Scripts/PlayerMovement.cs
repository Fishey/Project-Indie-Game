using UnityEngine;
using UnityStandardAssets.Utility;

public enum MovementTypes { Keyboard, Xbox }
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody _rigidbody;
	private MeshCollider _collider;
    private int _movementSpeed = 1000;
    private int _playerNum = 1;
    private float _maxSpeed = 10.0f;
    private PlayerClass _owner;
    private string[] _names;
	private float _distToGround;
	private Transform mainCam;
	private bool jumping = false;
	private float dir = 0;
    public MovementTypes mType;
	public float jumpHeight = 25;
	private bool facingRight = true;

	private float lastTimeMovementPressed;
	
    //public GameObject inGameMenu;
    string horizontalRightString, horizontalLeftString, verticalRightString, verticalLeftString, fireString, jumpString, flipString, swingString;


    float utime = 0;

    void Start()
    {
		mainCam = Camera.main.transform;
		Time.timeScale = 2.0f;
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<MeshCollider> ();
		setStrings ();
		_distToGround = _collider.bounds.extents.y;
    }

	private bool isGrounded() // Currently checks if the player is either on the floor or on the ceiling... that could cause problems later
	{
		bool canJump = false;
		Vector3 cameraUp = Camera.main.transform.TransformDirection(Vector3.up);
		if (Physics.Raycast (transform.position, -cameraUp, _distToGround + 0.1f) || Physics.Raycast (transform.position, -transform.up, _distToGround + 0.1f))
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
		flipString = "P" + _playerNum + "_Flip";
		jumpString = "P" + _playerNum + "_Jump";
		swingString = "P" + _playerNum + "_Swing";
		
		if (mType == MovementTypes.Xbox) {
			horizontalRightString += "Xbox";
			verticalRightString += "Xbox";
			verticalLeftString += "Xbox";
			horizontalLeftString += "Xbox";
			fireString += "Xbox";
			flipString += "Xbox";
			jumpString += "Xbox";
			swingString += "Xbox";
		}
	}
	
	public void SetMovementSpeed(int Movement)
	{
		_maxSpeed = Movement;
		_movementSpeed = Movement * (int)_owner.GetComponent<Rigidbody>().mass * 20;
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
		utime += Time.deltaTime;
		
		if (utime >= 1) {
			ready = true;
		}

		if (Input.GetButtonDown(jumpString) && isGrounded())
		{
			Debug.Log("yes");
			jumping = true;
		}

		if (Input.GetButtonDown(swingString))
		{
			_owner.Attack();
		}

	}
	void FixedUpdate()
	{
		dir = Movement();
		Aim(dir);
	}
	
	float Movement()
    {
		if (Input.GetAxisRaw(horizontalLeftString) == 1)
		{
			if (Time.time - lastTimeMovementPressed < 0.2f){
				Ray ray = new Ray(transform.position, mainCam.right);
				
				//dash
			}
		}
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
		if (horizontalspeed > 0) facingRight = true;
		else if (horizontalspeed < 0) facingRight = false;
		Debug.Log(movement.normalized * _movementSpeed * Time.deltaTime);



		_rigidbody.AddForce(movement.normalized * _movementSpeed * Time.deltaTime);

		lastTimeMovementPressed = Time.time;


        if (_rigidbody.velocity.x > _maxSpeed) // Only limit x velocity so we don't affect jump/gravity
        {
            _rigidbody.velocity = new Vector3(_maxSpeed, _rigidbody.velocity.y, _rigidbody.velocity.z);
        }
		else if (_rigidbody.velocity.x < - _maxSpeed) 
		{
			_rigidbody.velocity = new Vector3(-_maxSpeed, _rigidbody.velocity.y, _rigidbody.velocity.z);
		}

		Vector3 cameraRight = Camera.main.transform.TransformDirection(Vector3.right); // Determine what is currently right for the player

		if (horizontalspeed > 0f)
		{
			Camera.main.GetComponent<SmoothFollow>().TargetLead = 3f * cameraRight.x;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(cameraRight, transform.up), Time.deltaTime * 250f);
		}
		else  if (horizontalspeed < 0f)
		{
			Camera.main.GetComponent<SmoothFollow>().TargetLead = -3f * cameraRight.x;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-cameraRight, transform.up), Time.deltaTime * 250f);
		}

		if (jumping && isGrounded())
		{
			_rigidbody.velocity += transform.up*jumpHeight;
			jumping = false;
		}

		if (_rigidbody.velocity.y > Mathf.Abs(jumpHeight*transform.up.y)) // Limit y velocity separately
		{
			_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, Mathf.Abs(jumpHeight*transform.up.y), _rigidbody.velocity.z);
		}
		return horizontalspeed;
    }

    void Aim(float direction)
    {
		if (Input.GetButtonDown(fireString) && ready == true)
		{
			Vector3 cameraRight = Camera.main.transform.TransformDirection(Vector3.right);
			Vector3 cameraUp = Camera.main.transform.TransformDirection(Vector3.up);
			if (_owner.Shurikens > 0){
				_owner.Shurikens--;

				GameObject shuriken = Instantiate(Resources.Load("Shuriken", typeof(GameObject))) as GameObject;
				shuriken.transform.position = transform.position;

				if (facingRight){
					// Knockback the Player
					_rigidbody.AddForce(new Vector3(_movementSpeed * -cameraRight.x * Time.deltaTime * 50f,0, 0)); 

					//Throw the shuriken
					shuriken.transform.rotation = Quaternion.LookRotation(Camera.main.ScreenToWorldPoint(Input.mousePosition));

					shuriken.GetComponent<Rigidbody>().AddForce(new Vector3(2000 * cameraRight.x, 0, 0), ForceMode.Force);
				}
				else if (!facingRight){
					// Knockback the Player
					_rigidbody.AddForce(new Vector3(_movementSpeed * cameraRight.x * Time.deltaTime * 50f,0 ,0)); 

					//Throw the shuriken
					shuriken.transform.rotation = Quaternion.LookRotation(Camera.main.ScreenToWorldPoint(Input.mousePosition));
					shuriken.GetComponent<Rigidbody>().AddForce(new Vector3(-2000 * cameraRight.x, 0, 0), ForceMode.Force);
				}
				ready = false;
				utime = 0;
			}
		}


    }

    public int PlayerNum
    {
        get { return this._playerNum; }
        set { this._playerNum = value; }
    }
}