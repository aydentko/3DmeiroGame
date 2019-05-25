using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour {

    public float speed = 1f;
    float alfa;
    float red, green, blue;
    public GameObject treasure_box;
    bool tre_clear;
   
	// Use this for initialization
	void Start () {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;

    }
	
	// Update is called once per frame
	void Update () {
        tre_clear = treasure_box.GetComponent<treasure_box>().clear;

        if (tre_clear==true)
        {
            if(alfa < 1)
            {
                GetComponent<Image>().color = new Color(red, green, blue, alfa);
                alfa += speed * Time.deltaTime;
            }
        }
	}
}
