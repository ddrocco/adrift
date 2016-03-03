using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Linq;
using UnityEngine.Networking.NetworkSystem;

public class CartesianMap<t> {

	public void Insert(int x, int y, t obj) {
	/* Inserts an object into the Cartesian Map at (x, y). */

		QuadrantCoordinates quadcoords = ConvertToQuadrants(x, y);
		List<List<t>> grid = GetQuadrant(quadcoords.quadrant);
		while (grid.Count <= quadcoords.x) {
			grid.Add(new List<t>());
		}
		while (grid[quadcoords.x].Count <= quadcoords.y) {
			grid[quadcoords.x].Add(default(t));
		}
		grid[quadcoords.x][quadcoords.y] = obj;
	}

	public t Get(int x, int y) {
	/* Fetches an object from the Cartesian Map at (x, y), or returns null if none exist. */

		QuadrantCoordinates quadcoords = ConvertToQuadrants(x, y);
		List<List<t>> grid = GetQuadrant(quadcoords.quadrant);
		if (grid.Count <= quadcoords.x || grid[quadcoords.x].Count <= quadcoords.y) {
			return default(t);
		}
		return grid[quadcoords.x][quadcoords.y];
	}

	/* Private Implementation Items */

	// Items in the 1st cartesian quadrant (pos, pos)
	List<List<t>> _first = new List<List<t>>();
	// Items in the 2nd cartesian quadrant (neg, pos)
	List<List<t>> _second = new List<List<t>>();
	// Items in the 3rd cartesian quadrant (neg, neg)
	List<List<t>> _third = new List<List<t>>();
	// Items in the 4th cartesian quadrant (pos, neg)
	List<List<t>> _fourth = new List<List<t>>();

	private enum Quadrant {
		first,
		second,
		third,
		fourth
	};

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
