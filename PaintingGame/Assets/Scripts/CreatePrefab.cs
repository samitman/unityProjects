using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePrefab : MonoBehaviour
{
    [Range(1f, 30f)] [SerializeField] private float distance = 10f;
    [Range(-3f, 3f)] [SerializeField] private float distanceChange = 1f;
    public float rotation = 0f;
    public float rotationChange = 5f;
    public GameObject fancy;
    float posX = -1f, posY = -1f, posZ = -1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(1))
        {
            Vector3 clickPosition = new Vector3(posX, posY, posZ);
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, distance));
            Debug.Log(clickPosition);
            distance += distanceChange;
            rotation += rotationChange;
            fancy.transform.position = clickPosition;
            fancy.transform.Rotate(new Vector3(0f, rotation, 0f));
            Instantiate(fancy);
        }
    }
}
