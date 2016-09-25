using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour {

	Grid grid;
	public List<Int2D> pathList = new List<Int2D>();
	Tile currentTile = new Tile ();

	private void Awake()	{
		grid = this.gameObject.GetComponent<Grid> ();
		pathList.Add (grid.startTile); //Int2D startNode
		currentTile = grid.startTileObject;
	}

	public void AnalyzePath (Tile inputTile)	{
		if(IsConnected(inputTile))	{
			pathList.Add(new Int2D(inputTile.tile.x, inputTile.tile.y));
			currentTile = inputTile;
		}

		print ("END TILE: " + grid.endTileObject.tile.y);
		print ("INPUT TILE: " + inputTile.tile.y);
		print ("X END TILE: " + grid.endTileObject.tile.x);
		print ("X INPUT TILE: " + inputTile.tile.x);
		if (grid.endTileObject.tile.y == inputTile.tile.y + 1 && grid.endTileObject.tile.x == inputTile.tile.x) {
			print ("FOUND END TILE");
			if (IsConnected (grid.endTileObject)) {
				print ("CONNECTED");
				pathList.Add (new Int2D (grid.endTileObject.tile.x, grid.endTileObject.tile.y));
				currentTile = grid.endTileObject;
			}	
		}

	}//AnalyzePath

	bool IsConnected(Tile inputTile)	{
		int x = currentTile.tile.x;
		if (currentTile.tile.x == inputTile.tile.x &&
			currentTile.tile.y + 1 == inputTile.tile.y &&
			currentTile.HasNorth() && inputTile.HasDown()) // check adjacent up
			return true;
		
		if (currentTile.tile.x - 1 == inputTile.tile.x &&
			currentTile.tile.y == inputTile.tile.y &&
			currentTile.HasLeft() && inputTile.HasRight()) // check adjacent left
			return true;

		if (currentTile.tile.x == inputTile.tile.x &&
			currentTile.tile.y - 1 == inputTile.tile.y &&
			currentTile.HasDown() && inputTile.HasNorth()) // check adjacent down
			return true;

		if (currentTile.tile.x + 1 == inputTile.tile.x &&
			currentTile.tile.y == inputTile.tile.y &&
			currentTile.HasRight() && inputTile.HasLeft()) // check adjacent right
			return true;
		
		return false;
	}//IsConnected

	bool CheckTypeConnection(Tile inputTile)	{
		return false;
	}
}
