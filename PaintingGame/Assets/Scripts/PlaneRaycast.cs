using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneRaycast : MonoBehaviour
{
    private GameObject primitive; //don't need layermask
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(1))
        {
            Vector3 clickPosition = -Vector3.one;

            //raycast using a plane
            Plane plane = new Plane(Vector3.forward, 0f); //forward lets us paint on a plane like a wall in front of you, up let's you draw across the world

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distanceToPlane;

            if(plane.Raycast(ray, out distanceToPlane))
            {
                clickPosition = ray.GetPoint(distanceToPlane);
            }

            Debug.Log(clickPosition);
            primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            primitive.transform.position = clickPosition;
        }
    }
}
