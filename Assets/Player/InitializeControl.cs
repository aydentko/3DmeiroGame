using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeControl : MonoBehaviour {
    StaticScriptsCon ssc;
	// Use this for initialization
	void Start () {
        ssc = GameObject.Find("StaticScriptsOb").GetComponent<StaticScriptsCon>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        ssc.OnInitialize();
    }
}
