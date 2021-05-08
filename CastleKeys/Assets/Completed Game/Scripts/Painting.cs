using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Painting : MonoBehaviour
{
    
    private GameObject primitive;
    private float red = 1f, green = 1f, blue = 1f; //numbers representing the chosen color values

    private Vector3 clickPosition;
    private bool timedDestroyIsOn = true;

    [SerializeField]
    private float distance;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetMouseButtonDown(1)) //right click creates shape
        {
            //instantiates an object where you are looking
            Vector3 playerPos = Camera.main.transform.position;
            Vector3 playerDirection = Camera.main.transform.forward;
            Quaternion playerRotation = Camera.main.transform.rotation;

            //get's distance to clicked object using raycast
            RaycastHit hit;
            if (Physics.Raycast(playerPos, Vector3.forward, out hit))
            {
                distance = hit.distance;
            }

                Vector3 spawnPos = playerPos + playerDirection * 4;

            //randomizes the colors and scale of the shapes
            primitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
            primitive.transform.localScale = new Vector3(1.75f,1.75f,1.75f);
            primitive.GetComponent<Renderer>().material.color = new Vector4(Random.Range(0f, red), Random.Range(0f, green), Random.Range(0f, blue), 1f);
            GameObject clone = (GameObject)Instantiate(primitive, spawnPos, playerRotation);
            //Instantiate(primitive, spawnPos, playerRotation);
            //primitive.transform.position = spawnPos;
            //primitive.transform.parent = this.transform;

            if (timedDestroyIsOn) //checks if time destroy is toggled on
            {
                Destroy(clone, 20f);
            }
        }

    }

    public void ToggleTimedDestroy(bool timer) //lets us toggle time destroy on and off
    {
        timedDestroyIsOn = timer;
    }

}