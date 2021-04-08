using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicks : MonoBehaviour { 

    public int n = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { Debug.Log("GetMouseButtonDown(0): Pressed LEFT CLICK" + n); }
        if (Input.GetMouseButtonDown(1)) { Debug.Log("GetMouseButtonDown(1): Pressed RIGHT CLICK" + n); }
        if (Input.GetMouseButtonDown(2)) { Debug.Log("GetMouseButtonDown(0): Pressed MIDDLE CLICK" + n); }
        n++;
    }
}
