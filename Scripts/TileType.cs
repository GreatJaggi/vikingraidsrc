using UnityEngine;
using System.Collections;

public class TileType {

	/**
	 * UP, RIGHT, DOWN, LEFT
	 * CLOCKWISE
	 **/
	public bool[] paths;

	public TileType(bool[] paths) {
		this.paths = paths;
	}

	// UP DOWN LEFT RIGHT?
	public static TileType CROSS = new TileType(new bool[] {true, true, true, true});
	public static TileType STRAIGHT = new TileType(new bool[] {false, true, false, true});
	public static TileType L = new TileType(new bool[] {true, false, false, true});
	public static TileType EMPTY = new TileType(new bool[] {false, false, false, false});
	public static TileType START = new TileType(new bool[] {false, false, true, false});
	public static TileType END = new TileType(new bool[] {true, false, true, false});


}
