using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controls : MonoBehaviour {
	
	public float speed;
	public float RotateSpeed = 30f;
    public GameObject Gate;
    public GameObject BuildMenu;
    public GameObject BackButton;
    public GameObject IndicationUnits;
    public Text countText;
	private static int count;
	private int needed;
	public Text winText;
	public GameObject player;
	private static Vector3 Currentposition;
	
	private Rigidbody rb;
    private static bool pause;
    private Vector3 playerPos;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
        pause = false;
        BuildMenu.SetActive(false);
		needed = PSpawner.amount;
		Debug.Log(needed);
		count = 0;
		countText.text = "Count: " + count.ToString ();
		winText.text = "";
	}

	void Update()
	{

		Plane playerPlane = new Plane (Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float hitdist = 0.0f;
		if (playerPlane.Raycast (ray, out hitdist)) {
			Vector3 targetPoint = ray.GetPoint (hitdist);
			Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
		}

        if (Input.GetButtonDown("Jump") && Vector3.Distance(Gate.transform.position,transform.position) < 3)
        {
            pause = !pause;
            if (pause)
            {
                playerPos = transform.position;
                transform.position = new Vector3(0, -0.501f, -11.3f);
            } else if (!pause)
            {
                transform.position = playerPos;
                BuildMenu.SetActive(false);
                BackButton.SetActive(false);
                IndicationUnits.SetActive(false);
            }
        }

        if (pause)
        {
            Time.timeScale = 0;
        } else if (!pause)
        {
            Time.timeScale = 1;
        }
	}


	void FixedUpdate ()


	{


		float moveHorizontal = Input.GetAxis("Horizontal")*Time.deltaTime;
		float moveVertical = Input.GetAxis ("Vertical")*Time.deltaTime;
		
		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		transform.Translate(speed*moveHorizontal,0.0f,speed*moveVertical,Space.World);

		Vector3 playerPos = player.transform.position;
		//Debug.Log (playerPos);
		setposition (playerPos);
	}
	void OnTriggerEnter(Collider other) 
		{
				if (other.gameObject.CompareTag ("Pick-Up"))
				{
						other.gameObject.SetActive (false);
						count = count + 1;
						countText.text = "Count: " + count.ToString ();
						if (count >= needed)
			{
								winText.text = "You Win!";
							}
					}
			}

    static public bool getPause()
    {
        return pause;
    }

	void setposition(Vector3 here){
		Currentposition = here;
	}
	public static Vector3 getposition(){
		return Currentposition;
	}

    public static int getCount()
    {
        return count;
    }

    public static void setCount(int change)
    {
        count = count - change;
    }
}