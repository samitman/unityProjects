using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log(name + " was clicked at " + Time.time + " position = " + transform.position);
    }
}
