using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    void Update () 
	{
		// Rotate the game object that this script is attached to by 15 in the X axis,
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);

    }
}	