using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualButtonControl : MonoBehaviour {
    public GameObject Manual = null;
    public GameObject ManualText = null;
    public GameObject ChangeButtonText = null;
    string Manual_Text_1;
    string Manual_Text_2;
    bool OnGamePadManual = false;

    // Use this for initialization
    void Start () {
        Manual_Text_1 =
            "☆いどう：W（前） A（左） S（後ろ） D（右）\n" +
            "☆はやくはしる：Shiftをおしながらいどう \n" +
            "☆かおを上下にうごかす：↑(PgUp) ↓(PgDn) \n" +
            "☆むきをかえる：←(Home) →(End) \n" +
            "☆うしろをふりむく：B \n" +
            "☆めじるしをおく：Q（10コまでおけるよ） \n" +
            "☆めじるしをけす：（めじるしのちかくで）E \n" +
            "☆カメラのきりかえ：C \n" +
            "☆ジャンプ：Spaceキー";


        Manual_Text_2 =
            "☆あるく：Dパッド（十字ボタン）\n" +
            "☆はやくはしる：ひだりスティック \n" +
            "☆むきをかえる：みぎスティック \n" +
            "☆うしろをふりむく：LBまたはRBボタン \n" +
            "☆めじるしをおく：Aボタン \n" +
            "☆めじるしをけす：（めじるしのちかくで）Bボタン \n" +
            "☆カメラのきりかえ：Xボタン \n" + 
            "☆ジャンプ：Yボタン\n";

        if (!OnGamePadManual)
        {
            ManualText.GetComponent<Text>().text = Manual_Text_1;
        }
        else
        {
            ManualText.GetComponent<Text>().text = Manual_Text_2;
        }

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnManualButton()
    {
        this.Manual.SetActive(true);

    }

    public void OnXButton()
    {
        GameObject.Find("Manual Panel").SetActive(false);

    }

    public void OnChangeButton()
    {
        if (!OnGamePadManual)
        {
            // キーボード操作のマニュアルからゲームパッド操作のマニュアルに変更する場合
            OnGamePadManual = true;
            ManualText.GetComponent<Text>().text = Manual_Text_2;
            ChangeButtonText.GetComponent<Text>().text = "キーボードでのそうさほうほう";
        }
        else
        {
            // ゲームパッド操作のマニュアルからキーボード操作のマニュアルに変更する場合
            OnGamePadManual = false;
            ManualText.GetComponent<Text>().text = Manual_Text_1;
            ChangeButtonText.GetComponent<Text>().text = "ゲームパッドでのそうさほうほう";
        }
    }
}
