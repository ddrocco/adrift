using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Linq;

public class CartesianMap<t> {

	public void Insert(int x, int y, t obj) {
	/* Inserts an object into the Cartesian Map at (x, y). */

		QuadrantCoordinates quadcoords = ConvertToQuadrants(x, y);
		List<List<t>> grid = GetQuadrant(quadcoords.quadrant);
		switch (quadcoords.quadrant) {
			case Quadrant.first:
				grid = _first;
				break;
			case Quadrant.second:
				grid = _second;
				break;
			case Quadrant.third:
				grid = _third;
				break;
			case Quadrant.fourth:
				grid = _fourth;
				break;
		}
		while (grid.Count <= quadcoords.x) {
			grid.Add(new List<t>());
		}
		while (grid[x].Count <= quadcoords.y) {
			grid[x].Add(default(t));
		}
		grid[x][y] = obj;
	}

	public t Get(int x, int y, t obj) {
	/* Fetches an object from the Cartesian Map at (x, y), or returns null if none exist. */

		QuadrantCoordinates quadcoords = ConvertToQuadrants(x, y);
		List<List<t>> grid = GetQuadrant(quadcoords.quadrant);
		if (grid.Count <= quadcoords.x || grid[quadcoords.x].Count <= quadcoords.y) {
			return default(t);
		}
		return grid[quadcoords.x][quadcoords.y];
	}

	/* Private Implementation Items */

	private enum Quadrant {
		first,
		second,
		third,
		fourth
	};

	// Items in the 1st cartesian quadrant (pos, pos)
	private List<List<t>> _first;
	// Items in the 2nd cartesian quadrant (neg, pos)
	private List<List<t>> _second;
	// Items in the 3rd cartesian quadrant (neg, neg)
	private List<List<t>> _third;
	// Items in the 4th cartesian quadrant (pos, neg)
	private List<List<t>> _fourth;

	struct QuadrantCoordinates {
		public Quadrant quadrant;
		public int x;
		public int y;
	}

	QuadrantCoordinates ConvertToQuadrants(int x, int y) {
		bool x_positive = true;
		bool y_positive = true;
		if (x < 0) {
			x_positive = false;
			x = -x - 1;
		}
		if (y < 0) {
			y_positive = false;
			y = -y - 1;
		}
		Quadrant quadrant;
		if (x_positive) {
			if (y_positive) {
				quadrant = Quadrant.first;
			} else {
				quadrant = Quadrant.fourth;
			}
		} else {
			if (y_positive) {
				quadrant = Quadrant.second;
			} else {
				quadrant = Quadrant.third;
			}
		}
		QuadrantCoordinates quadcoords;
		quadcoords.quadrant = quadrant;
		quadcoords.x = x;
		quadcoords.y = y;
		return quadcoords;
	}

	List<List<t>> GetQuadrant(Quadrant quadrant) {
		switch (quadrant) {
			case Quadrant.first:
				return _first;
			case Quadrant.second:
				return _second;
			case Quadrant.third:
				return _third;
			case Quadrant.fourth:
				return _fourth;
		}
		throw new NotImplementedException();
	}
}
