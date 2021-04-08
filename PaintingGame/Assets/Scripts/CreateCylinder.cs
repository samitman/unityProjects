using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCylinder : MonoBehaviour
{
    public float distance = 10f;
    public float distanceChange = 1f;
    public float rotationAmount = 0f;
    public float rotationDelta = 0.0f;
    float posX = -1f;
    float posY = -1f;
    float posZ = -1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(1))
        {
            distance += distanceChange;
            posX = -1f;
            posY = -1f;
            posZ = -1f;
            Vector3 clickPosition = new Vector3(posX, posY, posZ);
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, distance));
            Debug.Log(clickPosition);
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.transform.position = clickPosition;
            rotationAmount += rotationDelta;
            cylinder.transform.Rotate(new Vector3(0f, 0f, rotationAmount));
        }
    }
}
