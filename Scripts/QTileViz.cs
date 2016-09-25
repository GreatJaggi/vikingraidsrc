using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QTileViz : MonoBehaviour {
	public Queuing queuing = new Queuing();
	public Image[] qTiles;
	public RectTransform[] rTiles;

	public Sprite[] tileSprite;

	ArrayList qList;
	ArrayList rList;
	// Use this for initialization
	void Start () {
		qList = queuing.queue.GetList ();
		rList = queuing.queue.GetYRotations ();
	}
	
	// Update is called once per frame
	public void UpdateViz () {
		rList = queuing.queue.GetYRotations ();

		//print ("List Size:" + rList.Count);

		//for (int i = 0; i < rList.Count; i++)
		//	print ("index " + i + ": " +rList [i]);


		Vector3 rot = transform.rotation.eulerAngles;
		for (int i = 0; i < 5; i++) {
			qTiles [i].sprite = tileSprite [(int)qList [i]];
			//print ("index " + i + ": " +rList [i]);

			rot.z = (int)rList [i];
			rTiles [i].rotation = Quaternion.Euler(rot);
		}//for
	}
}
