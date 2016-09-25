using UnityEngine;
using System.Collections;

public class Int2D {

	public int x;
	public int y;

	public string XYOut()	{
		return ("[X, Y] ==> [" + x + ", " + y  + "]");
	}


	public Int2D()	{
	}

	public Int2D(int x, int y)	{
		this.x = x;
		this.y = y;
	}
}
