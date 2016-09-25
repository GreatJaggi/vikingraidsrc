using UnityEngine;
using System.Collections;

public class Pointer : MonoBehaviour {

	public Grid grid;
	public Path path;
	public Queuing queuing;
	public QTileViz viz;

	public int currentTile;
	public int x = 0, y = 0;
	// Use this for initialization
	void Start () {
		
		this.gameObject.transform.position = 
			new Vector3 (
				grid.grid [x, y].gameObject.transform.position.x,
				this.gameObject.transform.position.y,
				grid.grid [x, y].gameObject.transform.position.z
		);

		viz.UpdateViz ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow))
			if (y != grid.height - 1)
					y += 1;

		if (Input.GetKeyDown (KeyCode.DownArrow))
			if (y != 0)
				y -= 1;
		
		if (Input.GetKeyDown (KeyCode.LeftArrow))
			if (x != 0)
				x -= 1;
		
		if (Input.GetKeyDown (KeyCode.RightArrow))
			if (x != grid.width - 1)
				x += 1;

		// ROTATION OF CURRENT TILE
		if (Input.GetKeyDown (KeyCode.Z)) {
			//rotate counter-clockwise
			int yRotation = (int)queuing.queue.GetYRotations () [queuing.rotations.Count];

			if(yRotation == 0)
				queuing.queue.SetYRotation (queuing.rotations.Count, 270);
			else
				queuing.queue.SetYRotation (queuing.rotations.Count, yRotation - 90);
			
			viz.UpdateViz ();

		}

		if (Input.GetKeyDown (KeyCode.X)) {
			//rotate clockwise
			int yRotation = (int)queuing.queue.GetYRotations () [queuing.rotations.Count];

			if(yRotation == 270)
				queuing.queue.SetYRotation (queuing.rotations.Count, 0);
			else
				queuing.queue.SetYRotation (queuing.rotations.Count, yRotation + 90);
			
			viz.UpdateViz ();
		}

		// MAKE TILE
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(!grid.GetTile (x, y).GetComponent<Tile> ().visited)
				if (!(x == grid.startTile.x && y == grid.startTile.y) && !(x == grid.endTile.x && y == grid.endTile.y)) {
					//switch
					int yRotation = (int)queuing.queue.GetYRotations () [queuing.rotations.Count];
					//print ("Y ROTATION: " + yRotation);
					int deq = queuing.queue.Dequeue ();
					switch (deq) {
					case 0:
						grid.MakeCross (x, y, yRotation);
						queuing.queue.Enqueue (Random.Range (0, 3), (int)queuing.rotations [Random.Range (0, 4)]);
						break;
					case 1:
						grid.MakeStraight (x, y, yRotation);
						queuing.queue.Enqueue (Random.Range (0, 3), (int)queuing.rotations [Random.Range (0, 4)]);
						break;
					case 2:
						grid.MakeL (x, y, yRotation);
						queuing.queue.Enqueue (Random.Range (0, 3), (int)queuing.rotations [Random.Range (0, 4)]);
						break;
					default:
						print ("Cannot make a move");
						break;
					}//switch

					path.AnalyzePath (grid.GetTile (x, y).GetComponent<Tile> ());
					//path.AnalyzePath (x, y, yRotation);

					ArrayList qList = queuing.queue.GetYRotations ();
					//print ("List Size:" + qList.Count);
					//for (int i = 0; i < qList.Count; i++)
					//	print ("index " + i + ": " +qList [i]);

					viz.UpdateViz ();
				}
		}

		this.gameObject.transform.position = 
			new Vector3 (
				grid.grid [x, y].gameObject.transform.position.x,
				this.gameObject.transform.position.y,
				grid.grid [x, y].gameObject.transform.position.z
			);
		
	}

}
