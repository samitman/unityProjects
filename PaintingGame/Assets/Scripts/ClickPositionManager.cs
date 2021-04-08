using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickPositionManager : MonoBehaviour
{
    private int shape = 0; //number to choose shapes from dropdown
    private GameObject primitive; 
    private float red = 1f, green = 1f, blue = 1f; //numbers representing the chosen color values
    public Text mousePosition;
    private float size = 1f; //size of shape created

    [SerializeField]
    private float distance = 5f, distanceChange;

    private Vector3 clickPosition;
    private bool timedDestroyIsOn = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) || Input.GetMouseButton(1)) //right click and hold acts as an eraser
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)) //destroys shapes if you right click them
            {
                Destroy(hit.transform.gameObject);
            }
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) //left click and hold creates shapes
        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, distance));

            //chooses shape based on dropdown selection
            switch (shape)
            {
                case 0:
                    primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    break;
                case 1:
                    primitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    break;
                case 2:
                    primitive = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    break;
            }
            //randomizes the colors and scale of the shapes
            primitive.transform.localScale = new Vector3(Random.Range(0.1f, 1f)*size, Random.Range(0.1f, 1f)*size, Random.Range(0.1f, 1f)*size);
            primitive.transform.position = clickPosition;
            primitive.GetComponent<Renderer>().material.color = new Vector4(Random.Range(0f, red), Random.Range(0f, green), Random.Range(0f, blue),1f);
            primitive.transform.parent = this.transform;

            if (timedDestroyIsOn) //checks if time destroy is toggled on
            {
                Destroy(primitive, 3f);
            }
        }
        //displays mouse position
        mousePosition.text = "Mouse Position (x: " + Input.mousePosition.x.ToString("F0") + ", y: " + Input.mousePosition.y.ToString("F0")+")";

    }

    public void changeShape(int tempShape) //changes shape based on dropdown selection
    {
        shape = tempShape;
    }
    
    public void changeRed(float tempRed) //changes value of red based on slider
    {
        red = tempRed;
    }
    public void changeGreen(float tempGreen) //changes value of green based on slider
    {
        green = tempGreen;
    }
    public void changeBlue(float tempBlue) //changes calue of blue based on slider
    {
        blue = tempBlue;
    }

    public void changeSize(float tempSize) //changes size of shapes based on slider
    {
        size = tempSize;
    }

    public void destroyObjects() //destroys all shapes
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void ToggleTimedDestroy(bool timer) //lets us toggle time destroy on and off
    {
        timedDestroyIsOn = timer;
    }
 
}
