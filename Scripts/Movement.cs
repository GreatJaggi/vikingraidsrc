using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	Grid grid;
	Path path;
	public float speed;
	public float speedMultiplierInterval;
	public float speedMultiplier;

	public float timer;
	public float timeLapsed;

	public PointSystem pointSystem;

	int currentDirection;
	int count;
	Transform nodeTransform;
	private void Start()	{
		timeLapsed = 0;

		pointSystem = GameObject.FindGameObjectWithTag ("ScoreBoard").gameObject.GetComponent<PointSystem> ();
		GameObject gridGO = GameObject.FindGameObjectWithTag ("Grid");
		grid = gridGO.GetComponent<Grid> ();
		path = gridGO.GetComponent<Path> ();

		count = 0;
		//nodeTransform = GetNodeTransform(grid.GetTile(5, 5));
		nodeTransform = GetNodeTransform(grid.GetTile(path.pathList[count].x, path.pathList[count].y));
		//nodeTransform = GetNodeTransform (grid.GetTile(currentNode.x, currentNode.y -1)); // always going below start at first

	}

	public void Move()	{
		Vector3 dir = nodeTransform.position - this.transform.localPosition;
		float distThisFrame = speed * Time.deltaTime;

		//reaching the next node which is now the currentNode
		//if Reached
		if (dir.magnitude < distThisFrame) {
			count += 1;
			nodeTransform = GetNodeTransform(grid.GetTile(path.pathList[count].x, path.pathList[count].y));
			grid.GetTile (path.pathList [count].x, path.pathList [count].y).GetComponent<Tile> ().visited = true;

			//Int2D possibleEndTile = new Int2D (path.pathList [count].x, path.pathList [count].y - 1);

			if (grid.endTile == path.pathList [count])
				print ("End tile Reached!");
			pointSystem.AddScore (1);
		} 

		else {
			this.transform.Translate (dir.normalized * distThisFrame, Space.World);
			Quaternion targetRotation = Quaternion.LookRotation (dir);
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, targetRotation, Time.deltaTime * 5);	
		}
	}

	public Transform GetNodeTransform(GameObject targetGO)	{
		return targetGO.GetComponentInChildren<Transform> ();
	}//getNode

	public int GetDirection(Int2D node)	{
		return 0;
	}


	int currentTime = 0;
	private void Update()	{
		if (timer < 0) {
			try {
				Move ();	
			} catch (System.Exception ex) {
				//print ("Can't move anymore");
			}

			timeLapsed += 1 * Time.deltaTime;
			if(currentTime != (int)timeLapsed)	{
				if ((int)timeLapsed % speedMultiplierInterval == 0)
					speed *= speedMultiplier;
				currentTime = (int)timeLapsed;
			}
		}
		else timer -=  1 * Time.deltaTime;





	}
}