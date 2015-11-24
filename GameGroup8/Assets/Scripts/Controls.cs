using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controls : MonoBehaviour {
	
	public float walkingSpeed;
	public float RotateSpeed = 30f;
    public GameObject Gate;
    public GameObject BuildMenu;
    public GameObject BackButton;
    public GameObject IndicationUnits;
    public Text countText;
	private static int count;
	public GameObject player;
	private static Vector3 currentPosition;

    private static bool pause;
    private Vector3 playerPos;

	void Start () {
        pause = false;
        BuildMenu.SetActive(false);
		count = 0;
		countText.text = "Amount of units " + count;
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


	void FixedUpdate (){
		float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
		float moveVertical = Input.GetAxis ("Vertical") * Time.deltaTime;
		
		transform.Translate(walkingSpeed * moveHorizontal, 0.0f, walkingSpeed * moveVertical, Space.World);

		Vector3 playerPos = player.transform.position;
		setPosition (playerPos);
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.CompareTag ("Pick-Up")){
			Destroy (collider);
			count ++;
			countText.text = "Count: " + count;
		}
	}

    static public bool getPause(){
        return pause;
    }

	void setPosition(Vector3 here){
		currentPosition = here;
	}

	public static Vector3 getPosition(){
		return currentPosition;
	}

    public static int getCount(){
        return count;
    }

    public static void setCount(int change){
        count = count - change;
    }
}