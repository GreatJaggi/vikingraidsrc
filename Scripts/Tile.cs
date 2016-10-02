using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public int direction = Direction.NULL;

	public Int2D tile = new Int2D();
	public TileType tileType = TileType.EMPTY;
	public int yRotation = 0; // 0, 90, 180, 270
	public bool visited = false;

	public void setTileInfo(Int2D tile, TileType tileType, int yRotation)	{
		this.tile = tile;
		this.tileType = tileType;
		this.yRotation = yRotation;
	}

	public bool HasNorth() {
		//printSelf ();
		return tileType.paths [(0 + (360 - yRotation) / 90) % 4];
	}

	public bool HasRight() {
		//printSelf ();
		return tileType.paths [(1 + (360 - yRotation) / 90) % 4];
	}

	public bool HasDown() {
		//printSelf ();
		return tileType.paths [(2 + (360 - yRotation) / 90) % 4];
	}

	public bool HasLeft() {
		//printSelf ();
		return tileType.paths [(3 + (360 - yRotation) / 90) % 4];
	}

	public void printSelf() {
		print ("yRotation " + yRotation);
		print ("North " + tileType.paths [(0 + (360 - yRotation) / 90) % 4]);
		print ("Right " + tileType.paths [(1 + (360 - yRotation) / 90) % 4]);
		print ("Down " + tileType.paths [(2 + (360 - yRotation) / 90) % 4]);
		print ("Left " + tileType.paths [(3 + (360 - yRotation) / 90) % 4]);
	}

}//class
