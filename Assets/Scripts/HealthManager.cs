using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour 
{
    private List<Image> images = new List<Image>();
    private int hp;

	void Start () 
    {
        foreach(Image image in GetComponentsInChildren<Image>())
        {
            if(image.name.Equals("Hp"))images.Add(image);
        }

        hp = images.Count;
	}

    public int GetHp() { return hp; }

    public void Damage()
    {
        --hp;

        images[hp].enabled = false;
    }
}
