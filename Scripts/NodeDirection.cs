using UnityEngine;
using System.Collections;

public class NodeDirection : MonoBehaviour {
	private int dir = 0;
	Int2D newNode = new Int2D();


	public const int UP = 0;
	public const int DOWN = 1;
	public const int LEFT = 2;
	public const int RIGHT = 3;


	public int getDir()	{
		return this.dir;
	}

	public void setDir(int value)	{
		this.dir = value;
	}

}
