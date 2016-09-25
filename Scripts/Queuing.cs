using UnityEngine;
using System.Collections;

public class Queuing : MonoBehaviour {
	public Queue queue = new Queue();
	public int queueSize;
	public ArrayList rotations = new ArrayList();
	void Awake()	{

		rotations.Add (0);
		rotations.Add (90);
		rotations.Add (180);
		rotations.Add (270);

		for( int i = 0; i < queueSize; i++)
			queue.Enqueue(Random.Range(0, 3), (int)rotations[Random.Range(0, 4)]);
		
		ArrayList qList = queue.GetList ();
	}//awake
}

public class Queue 	{
	
	private ArrayList list = new ArrayList();
	private ArrayList yRotations = new ArrayList ();
	int size = 0;


	public ArrayList GetList()	{
		return list;
	}//getList

	public ArrayList GetYRotations()	{
		return yRotations;
	}//getYRotations

	public void SetYRotation(int idx, int yRotation)	{
		yRotations [idx] = yRotation;
	}//setYRotations

	public void Enqueue(int value, int yRotation)	{
		if (list.Count == 0) {
			list.Add (value);
			yRotations.Add (yRotation);
			size++;
		}
		else {
			list.Add (list [list.Count - 1]);
			yRotations.Add (yRotations [yRotations.Count - 1]);
			for(int i = list.Count - 1; i > 0; i--)	{
				list [i] = list [i - 1];
				yRotations [i] = yRotations [i - 1];
			}

			list [0] = (value);
			yRotations [0] = (yRotation);
		}
			
				
	}//Enqueue

	public int Dequeue()	{
		int frontNumber = (int)list [list.Count - 1];
		list.RemoveAt (list.Count - 1);
		yRotations.RemoveAt (yRotations.Count - 1);
		return frontNumber;
	}//Dequeue
}//class Queue
