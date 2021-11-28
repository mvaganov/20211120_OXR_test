using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLine : MonoBehaviour
{
    public void TestPosition(Vector3 position) {
        NonStandard.Lines.Make("test").Box(Vector3.one * .5f, position, color: Color.cyan, lineSize: 1f/64);
    }

    private void Start() {
        TestPosition(Vector3.forward);
    }
}
