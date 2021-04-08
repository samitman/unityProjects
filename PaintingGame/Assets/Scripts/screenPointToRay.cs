using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenPointToRay : MonoBehaviour
{

    public LayerMask clickMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(1))
        {
            //vector stores mouse position, sets to 1 automatically
            Vector3 clickPosition = -Vector3.one;

            //raycast using colliders, casts a ray from th eposition of the camera out to infinity
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit; //don't have to assign this as the raycast will assign it

            /*
            if(Physics.Raycast(ray, out hit)) //export out the info to hit
            {
                clickPosition = hit.point;
                GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                primitive.transform.position = clickPosition;
            }
            */

            if (Physics.Raycast(ray, out hit, 100f, clickMask)) //need to add max distance, and layermask
            {
                //hit holds a lot of information about the raycast collision on both sides
                clickPosition = hit.point;
                //problem is that it hits everything including the spheres that populate
                //so we add physics layers and update the raycast method call
                GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                primitive.transform.position = clickPosition;
            }

            Debug.Log(clickPosition);
        }
    }
}
