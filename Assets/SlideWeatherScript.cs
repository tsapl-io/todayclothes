using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideWeatherScript : MonoBehaviour {
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    public int NowSlider;
    public GameObject WeatherSlider;
    public GameObject TodayObject;
    public GameObject TomorrowObject;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            GetDirection();
        }
    }
    void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        string direction = "none";

        if (100f < directionX) {
            //右向きにフリック
            direction = "right";
        } else if (-100f > directionX) {
            //左向きにフリック
            direction = "left";
        }

        if (direction == "right") {
            TodayObject.SetActive(true);
            TomorrowObject.SetActive(false);
        }
        if (direction == "left") {
            TodayObject.SetActive(false);
            TomorrowObject.SetActive(true);
        }
    }
}