using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour 
{
    [SerializeField]
    private int id = 0;

	void Start () 
    {
		
	}

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal" + id.ToString()) * 0.1f;
        float y = Input.GetAxis("Vertical" + id.ToString()) * 0.1f;

        transform.position += new Vector3(x, y, 0);
    }
}
