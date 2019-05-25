using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMap : MonoBehaviour {
    public int currentMapNum = 1;
    public struct MapSets
    {  // グリッドでの座標を表す構造体
        public int Map_Number;
        public string Map_Name;
        public Sprite Map_Image;
        public int[] Map_Level_Stars;

    };

    public Sprite LevelStars1;
    public Sprite LevelStars2;
    public Sprite LevelStars3;
    public Sprite LevelStars4;
    public Sprite LevelStars5;

    private Sprite[] LevelStars;

    public Sprite Map1Image;
    public Sprite Map2Image;
    public Sprite Map3Image;
    public Sprite Map4Image;
    public Sprite Map5Image;
    public Sprite Map6Image;
    public Sprite Map7Image;

    private MapSets Map1;
    private MapSets Map2;
    private MapSets Map3;
    private MapSets Map4;
    private MapSets Map5;
    private MapSets Map6;
    private MapSets Map7;

    private MapSets[] Maps;

    private GameObject MapImagePanel;
    private GameObject MapTitleText;
    private GameObject MapLevelStarsPanel;

    private int currentLevel;
    private int star_num = 1;


    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void SetMaps()
    {
        // ＜Map1＞
        this.Map1 = new MapSets
        {
            Map_Number = 1,
            Map_Name = "1: おかしのくに",
            Map_Image = this.Map1Image,
            Map_Level_Stars = new int[] { 2, 3, 5 }

        };
        // ＜Map2＞
        this.Map2 = new MapSets
        {
            Map_Number = 2,
            Map_Name = "2: がっこう",
            Map_Image = this.Map2Image,
            Map_Level_Stars = new int[] { 1, 2, 3 }
        };
        // ＜Map3＞
        this.Map3 = new MapSets
        {
            Map_Number = 3,
            Map_Name = "3: ガーデン",
            Map_Image = this.Map3Image,
            Map_Level_Stars = new int[] { 3, 4, 5 }
        };
        // ＜Map4＞
        this.Map4 = new MapSets
        {
            Map_Number = 4,
            Map_Name = "4: おやしき",
            Map_Image = this.Map4Image,
            Map_Level_Stars = new int[] { 3, 4, 5 }
        };
        // ＜Map5＞
        this.Map5 = new MapSets
        {
            Map_Number = 5,
            Map_Name = "5: おいかけっこ",
            Map_Image = this.Map5Image,
            Map_Level_Stars = new int[] { 2, 3, 4 }
        };
        // ＜Map6＞
        this.Map6 = new MapSets
        {
            Map_Number = 6,
            Map_Name = "6: Map6",
            Map_Image = this.Map6Image,
            Map_Level_Stars = new int[] { 2, 3, 4 }
        };
        // ＜Map7＞
        this.Map7 = new MapSets
        {
            Map_Number = 7,
            Map_Name = "7: Map7",
            Map_Image = this.Map7Image,
            Map_Level_Stars = new int[] { 2, 3, 4 }

        };
        this.Maps = new MapSets[]{ Map1, Map2, Map3, Map4, Map5, Map6, Map7};
    }

    public int StarsNum(int MapNum, int MapLevel) {
        this.currentLevel = GameObject.Find("LevelToggles").GetComponent<LevelSelectControl>().currentLevel;
        MapNum--;
        MapLevel--;
        if(MapNum < 0)
        {
            MapNum = 0;
        }
        if(MapLevel < 0)
        {
            MapLevel = 0;
        }
        int Stars_num = this.Maps[MapNum].Map_Level_Stars[MapLevel];
        return Stars_num;
    }

    public void UpdateLevelStars(int StarNum, Sprite[] LevelStars) {
        // 難易度☆の表示
        Image m_LevelStar = GameObject.Find("LevelStar").GetComponent<Image>();
        switch (StarNum)
        {
            case 1:
                m_LevelStar.sprite = LevelStars[0];
                break;
            case 2:
                m_LevelStar.sprite = LevelStars[1];
                break;
            case 3:
                m_LevelStar.sprite = LevelStars[2];
                break;
            case 4:
                m_LevelStar.sprite = LevelStars[3];
                break;
            case 5:
                m_LevelStar.sprite = LevelStars[4];
                break;

        }

    }

    public void SelectMaps(int MapNumber)
    {
        Debug.Log("ClickButton");
        this.LevelStars = new Sprite[] { this.LevelStars1, this.LevelStars2, this.LevelStars3, this.LevelStars4, this.LevelStars5 };
        this.MapImagePanel = GameObject.Find("MapImage");
        this.MapTitleText = GameObject.Find("MapTitle");
        this.MapLevelStarsPanel = GameObject.Find("LevelStar");
        this.SetMaps();
        LevelSelectControl level_select = GameObject.Find("LevelToggles").GetComponent<LevelSelectControl>();
        this.currentMapNum = MapNumber;
        StaticScriptsCon.SetMapNumber(MapNumber);

        switch (MapNumber)
        {
            case 1:
                Debug.Log("Map1");
                Image m_Image = this.MapImagePanel.GetComponent<Image>();
                m_Image.sprite = this.Map1.Map_Image;
                Text m_Title = this.MapTitleText.GetComponentInChildren<Text>();
                m_Title.text = this.Map1.Map_Name;
                level_select.LevelSetActive(2, true);
                level_select.LevelSetActive(3, true);

                this.star_num = this.StarsNum(MapNumber, this.currentLevel);

                break;

            case 2:
                Debug.Log("Map2");
                m_Image = this.MapImagePanel.GetComponent<Image>();
                m_Image.sprite = this.Map2.Map_Image;
                m_Title = this.MapTitleText.GetComponentInChildren<Text>();
                m_Title.text = this.Map2.Map_Name;
                level_select.LevelToggleSetOn(1, true); // 1に設定しなおす
                level_select.LevelSetActive(2, true);
                level_select.LevelSetActive(3, true);

                this.star_num = this.StarsNum(MapNumber, this.currentLevel);

                break;

            case 3:
                Debug.Log("Map3");
                m_Image = this.MapImagePanel.GetComponent<Image>();
                m_Image.sprite = this.Map3.Map_Image;
                m_Title = this.MapTitleText.GetComponentInChildren<Text>();
                m_Title.text = this.Map3.Map_Name;
                level_select.LevelToggleSetOn(1, true);
                level_select.LevelSetActive(2, true);
                level_select.LevelSetActive(3, true);

                this.star_num = this.StarsNum(MapNumber, this.currentLevel);

                break;

            case 4:
                Debug.Log("Map4");
                m_Image = this.MapImagePanel.GetComponent<Image>();
                m_Image.sprite = this.Map4.Map_Image;
                m_Title = this.MapTitleText.GetComponentInChildren<Text>();
                m_Title.text = this.Map4.Map_Name;
                level_select.LevelToggleSetOn(1, true);
                level_select.LevelSetActive(2, true);
                level_select.LevelSetActive(3, true);

                this.star_num = this.StarsNum(MapNumber, this.currentLevel);

                break;

            case 5:
                Debug.Log("Map5");
                m_Image = this.MapImagePanel.GetComponent<Image>();
                m_Image.sprite = this.Map5.Map_Image;
                m_Title = this.MapTitleText.GetComponentInChildren<Text>();
                m_Title.text = this.Map5.Map_Name;
                level_select.LevelToggleSetOn(1, true); // 1で固定
                level_select.LevelSetActive(2, true);
                level_select.LevelSetActive(3, true);

                this.star_num = this.StarsNum(MapNumber, this.currentLevel);

                break;

            case 6:
                Debug.Log("Map6");
                m_Image = this.MapImagePanel.GetComponent<Image>();
                m_Image.sprite = this.Map6.Map_Image;
                m_Title = this.MapTitleText.GetComponentInChildren<Text>();
                m_Title.text = this.Map6.Map_Name;
                level_select.LevelToggleSetOn(1, true); // 1で固定
                level_select.LevelSetActive(2, true);
                level_select.LevelSetActive(3, true);

                this.star_num = this.StarsNum(MapNumber, this.currentLevel);

                break;

            case 7:
                Debug.Log("Map7");
                m_Image = this.MapImagePanel.GetComponent<Image>();
                m_Image.sprite = this.Map7.Map_Image;
                m_Title = this.MapTitleText.GetComponentInChildren<Text>();
                m_Title.text = this.Map7.Map_Name;
                level_select.LevelToggleSetOn(1, true); // 1で固定
                level_select.LevelSetActive(2, true);
                level_select.LevelSetActive(3, true);

                this.star_num = this.StarsNum(MapNumber, this.currentLevel);

                break;


        }

        this.UpdateLevelStars(this.star_num, this.LevelStars);
        BestTimeTextCon besttime_con = GameObject.Find("BestTimeText").GetComponent<BestTimeTextCon>();
        besttime_con.TextUpdate();


    }


}
