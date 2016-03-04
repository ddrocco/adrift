using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Xml.Linq;

public class TestCartesianMap {

    [Test]
    public void TestInsertAndGet()
    {
		CartesianMap<int> map = new CartesianMap<int>(false);
		map.Insert(0, 0, 10);
		map.Insert(0, 1, 11);
		map.Insert(1, 0, 12);
		map.Insert(-1, 0, 13);
		map.Insert(0, -1, 14);
		map.Insert(-1, -1, 15);
		Assert.AreEqual(map.Get(0, 0), 10);
		Assert.AreEqual(map.Get(0, 1), 11);
		Assert.AreEqual(map.Get(1, 0), 12);
		Assert.AreEqual(map.Get(-1, 0), 13);
		Assert.AreEqual(map.Get(0, -1), 14);
		Assert.AreEqual(map.Get(-1, -1), 15);
		Assert.AreEqual(map.Get(-1, 1), default(int));
		map.Insert(-1, 0, 20);
		Assert.AreEqual(map.Get(-1, 0), 20);
    }

	[Test]
    public void TestGetAllCached()
    {
		CartesianMap<int> map = new CartesianMap<int>(true);

		// Test default
		List<int> mapvalues = map.GetAllCached();
		Assert.AreEqual(mapvalues.Count, 0);

		// Test Insert
		map.Insert(0, 0, -10);
		map.Insert(0, 1, 15);
		mapvalues = map.GetAllCached();
		Assert.AreEqual(mapvalues.Count, 2);
		Assert.AreEqual(mapvalues[0]+mapvalues[1], 5);

		// Test Replace
		map.Insert(0, 1, 5);
		mapvalues = map.GetAllCached();
		Assert.AreEqual(mapvalues.Count, 2);
		Assert.AreEqual(mapvalues[0]+mapvalues[1], -5);

		// Test Remove
		map.Insert(0, 1, 0);
		mapvalues = map.GetAllCached();
		Assert.AreEqual(mapvalues.Count, 1);
		Assert.AreEqual(mapvalues[0], -10);
    }
}
