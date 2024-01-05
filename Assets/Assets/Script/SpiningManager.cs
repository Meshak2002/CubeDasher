using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiningManager : MonoBehaviour {

	int randVal;
	private float timeInterval;
	private bool isCoroutine;
	private int finalAngle;
	public Text cube_text;
    public GameObject spinPanel;
    public GameObject LeaderboardButton;

	public Text winText;
	public int section;
	float totalAngle;
	public string[] PrizeName;

	// Use this for initialization
	private void Start () {
		isCoroutine = true;
		totalAngle = 360 / section;
	}

	// Update is called once per frame
	private void Update ()
    {
  
    }
	public void start_spin()
    {
		StartCoroutine(Spin());
	}

	private IEnumerator Spin(){

		isCoroutine = false;
		randVal = Random.Range (200, 300);

		timeInterval = 0.0001f*Time.deltaTime*2;

		for (int i = 0; i < randVal; i++) {

			transform.Rotate (0, 0, (totalAngle/2)); //Start Rotate 


			//To slow Down Wheel
			if (i > Mathf.RoundToInt (randVal * 0.2f))
				timeInterval = 0.5f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.5f))
				timeInterval = 1f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.7f))
				timeInterval = 1.5f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.8f))
				timeInterval = 2f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.9f))
				timeInterval = 2.5f*Time.deltaTime;

			yield return new WaitForSeconds (timeInterval);

		}

		if (Mathf.RoundToInt (transform.eulerAngles.z) % totalAngle != 0) //when the indicator stop between 2 numbers,it will add aditional step 
			transform.Rotate (0, 0, totalAngle/2);
		
		finalAngle = Mathf.RoundToInt (transform.eulerAngles.z);//round off euler angle of wheel value

		

		//Prize check
		for (int i = 0; i < section; i++) {

			if (finalAngle == i * totalAngle)

			{
				winText.text = "You win " + PrizeName[i] + " CUBES";
                PlayerPrefs.GetInt("PlayerCube");
                cube_text.text = PlayerPrefs.GetInt("PlayerCube").ToString();
			}
		}
		isCoroutine = true;
	}
}
