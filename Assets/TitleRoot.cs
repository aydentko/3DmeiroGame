using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleRoot : MonoBehaviour {
    public string NextScene = "";

    // Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnClick() {
        SceneManager.LoadScene(NextScene);

    }

}
