using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour {

    public string Map1 = "";
    public string Map2 = "";
    public string Map3 = "";
    public string Map4 = "";
    public string Map5 = "";
    public string Map6 = "";
    public string Map7 = "";

    public static int StartButton_MapLevel = 1;
    public static int StartButton_MapNumber = 1;

    private LevelSelectControl Level_Select_Control;
    private SelectMap Select_Map;
    private int MapNumber = 1;
    private int SelectedMapLevel = 1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnGameStart() {
        this.Select_Map = GameObject.Find("SelectPanel").GetComponent<SelectMap>();
        this.Level_Select_Control = GameObject.Find("LevelToggles").GetComponent<LevelSelectControl>();

        // 各設定を変数に格納
        this.MapNumber = this.Select_Map.currentMapNum;
        StartButton_MapNumber = this.MapNumber;
        Debug.Log("MapNumber = " + this.MapNumber);
        this.SelectedMapLevel = this.Level_Select_Control.currentLevel;
        StartButton_MapLevel = this.SelectedMapLevel;
        Debug.Log("MapLevel = " + this.SelectedMapLevel);


        switch (MapNumber)
        {
            case 1:
                SceneManager.LoadScene("Scenes/" + this.Map1);
                break;

            case 2:
                SceneManager.LoadScene("Scenes/" + this.Map2);
                break;

            case 3:
                SceneManager.LoadScene("Scenes/" + this.Map3);
                break;

            case 4:
                SceneManager.LoadScene("Scenes/" + this.Map4);
                break;

            case 5:
                SceneManager.LoadScene("Scenes/" + this.Map5);
                break;

            case 6:
                SceneManager.LoadScene("Scenes/" + this.Map6);
                break;

            case 7:
                SceneManager.LoadScene("Scenes/" + this.Map7);
                break;

        }
    }

    public static int MapNum()
    {
        return StartButton_MapNumber;
    }

    public static int MapLevel()
    {
        return StartButton_MapLevel;
    }
}
