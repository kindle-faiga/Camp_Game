using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour 
{
	private AudioSource[] sources;

	void Start()
	{
		sources = gameObject.GetComponents<AudioSource>();
	}

	void Update () 
    {
        if(Input.GetButtonDown("Start"))
        {
            StartCoroutine(WaitForStart());
        }
	}

    private IEnumerator WaitForStart()
    {
        sources[1].Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Main");
    }
}
