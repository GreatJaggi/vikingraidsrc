using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour {

	int totalPoints = 0;
	Text scoreText;
	string defTExt;
	// Use this for initialization
	void Start () {
		scoreText = this.gameObject.GetComponent<Text> ();
		totalPoints = 0;
		defTExt = "Score: ";
		UpdatePoints ();
	}

	void UpdatePoints () {
		scoreText.text = defTExt + totalPoints.ToString ();
	}

	public void AddScore(int points)	{
		totalPoints += points;
		UpdatePoints ();
	}
}
