using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class TestCartesianMap {

    [Test]
    public void TestInsertAndGet()
    {
		CartesianMap<int> map = new CartesianMap<int>();
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
}
