using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Linq;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Match;
using System.IO;

public class CartesianMap<t> {
/* A generic container using two-tuple integer indices on a Cartesian Plane.

Under-the-hood, is centered at the origin, and uses four List<List<t>>: one for each cartesian quadrant.

By default, records all individual entries in write-order.
The caching feature should not be used types which have non-null default values.
*/
	public CartesianMap(bool use_caching=true) {
	/* A constructor for the CartesianMap.

	If called with use_caching=false, disables the GetAll method.
	If called with use_caching=true, records all individual entries in a list and enables the GetAll method.
	*/
		_use_caching = use_caching;
	}

	public void Insert(int x, int y, t obj) {
	/* Inserts an object into the Cartesian Map at (x, y).

	If caching is enabled, also records that item in a List.
	If an item is overwritten or deleted, also overwrites or deletes that item from a list.
	*/
		QuadrantCoordinates quadcoords = ConvertToQuadrants(x, y);
		List<List<t>> grid = GetQuadrant(quadcoords.quadrant);
		while (grid.Count <= quadcoords.x) {
			grid.Add(new List<t>());
		}
		while (grid[quadcoords.x].Count <= quadcoords.y) {
			grid[quadcoords.x].Add(default(t));
		}
		if (_use_caching) {
		/* Caches entries to the list '_all_items'. */
			t old_item = grid[quadcoords.x][quadcoords.y];
			// Inserting a new entry.
			if (!obj.Equals(default(t)) && (old_item == null || old_item.Equals(default(t)))) {
				CartesianItem cartesian_item = new CartesianItem();
				cartesian_item.x = x;
				cartesian_item.y = y;
				cartesian_item.item = obj;
				_all_items.Add(cartesian_item);
			}
			// Replacing an old entry.
			else if (!obj.Equals(default(t)) && (old_item != null && !old_item.Equals(default(t)))) {
				CartesianItem old_cartesian_item = new CartesianItem() {
					x = x,
					y = y,
					item = old_item
				};
				int i = _all_items.FindIndex(entry=> entry.Equals(old_cartesian_item));
				CartesianItem new_cartesian_item = new CartesianItem() {
					x = x,
					y = y,
					item = obj
				};
				_all_items[i] = new_cartesian_item;
			}
			// Deleting an old entry.
			else if (obj.Equals(default(t)) && (old_item != null && !old_item.Equals(default(t)))) {
				CartesianItem old_cartesian_item = new CartesianItem() {
					x = x,
					y = y,
					item = old_item
				};
				_all_items.Remove(old_cartesian_item);
			}
			// Ignore deleting a non-existant entry.
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

	public List<t> GetAll() {
	/* Fetches all objects from the Cartesian Map. Does not use caching, so has a very high runtime. */
		List<t> list = new List<t>();
		List<List<t>> iterator_list = new List<List<t>>();
		iterator_list.AddRange(_first);
		iterator_list.AddRange(_second);
		iterator_list.AddRange(_third);
		iterator_list.AddRange(_fourth);
		foreach (List<t> column in iterator_list) {
			foreach (t item in column) {
				if (!item.Equals(default(t))) {
					list.Add(item);
				}
			}
		}
		return list;
	}

	public List<t> GetAllCached() {
	/* Fetches all non-null objects from the Cartesian Map.

	Requires that caching is enabled.
	*/
		if (!_use_caching) {
			throw new ArgumentException("GetAllCached no available for CartesianMap(caching=fale).");
		}
		List<t> items = new List<t>();
		foreach (CartesianItem cartesian_item in _all_items) {
			items.Add(cartesian_item.item);
		}
		return items;
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

	List<CartesianItem> _all_items = new List<CartesianItem>();

	bool _use_caching;

	private enum Quadrant {
		first,
		second,
		third,
		fourth
	};

	struct QuadrantCoordinates {
	/* Distinct from CartesianMapCoordinates.

	A struct of Quadrant-indexable values.  x and y must be positive.
	*/
		public Quadrant quadrant;
		public int x;
		public int y;
	}

	public struct CartesianItem {
	/* A single item and its location in the CartesianMap. */
		public int x;
		public int y;
		public t item;
	}

	QuadrantCoordinates ConvertToQuadrants(int x, int y) {
	/* Converts Cartesian coordinates to Quadrant coordinates, thereby implicitly allowing negative indexing. */
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
	/* Accesses a Quadrant from its associated enum.

	This effectively serves as a 4-entry dictionary.
	*/
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
