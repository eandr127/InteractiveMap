using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDetector : MonoBehaviour {
	
	public GameObject[] videos;
	public GameObject[] buttons;

	int currentSelection = 0;


	void Update () {
		if (Input.GetKeyDown("left")) {

			currentSelection--;

			videos[currentSelection].GetComponent<videoplay>().isSelected = true;

			buttons[currentSelection].transform.FindChild("arrow").gameObject.SetActive(true);

			videos[currentSelection + 1].GetComponent<videoplay>().isSelected = false;

			buttons[currentSelection + 1].transform.FindChild("arrow").gameObject.SetActive(false);

		} else if (Input.GetKeyDown("right")) {

			currentSelection++;

			videos[currentSelection].GetComponent<videoplay>().isSelected = true;

			buttons[currentSelection].transform.FindChild("arrow").gameObject.SetActive(true);

			videos[currentSelection - 1].GetComponent<videoplay>().isSelected = false;

			buttons[currentSelection - 1].transform.FindChild("arrow").gameObject.SetActive(false);

		}
		if (Input.GetKeyDown("x")) {

			//lazy way cause i did not want to do a for each loop
			videos[0].GetComponent<videoplay>().CloseVideo();
			videos[1].GetComponent<videoplay>().CloseVideo();
			videos[2].GetComponent<videoplay>().CloseVideo();
			videos[3].GetComponent<videoplay>().CloseVideo();
			videos[4].GetComponent<videoplay>().CloseVideo();
			
		}
	}
}
