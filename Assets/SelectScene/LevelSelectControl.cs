using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSelectControl : MonoBehaviour
{
    public int currentLevel = 1;
    public Sprite LevelStars1;
    public Sprite LevelStars2;
    public Sprite LevelStars3;
    public Sprite LevelStars4;
    public Sprite LevelStars5;

    private Sprite[] LevelStars;

    private GameObject LevelStarsPanel;
    private GameObject SelectMapPanel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectLevels(int Level)
    {
        this.LevelStars = new Sprite[] { this.LevelStars1, this.LevelStars2, this.LevelStars3, this.LevelStars4, this.LevelStars5 };
        //this.LevelStarsPanel = GameObject.Find("LevelStar");
        this.SelectMapPanel = GameObject.Find("SelectPanel");
        //Image m_levelstar = this.LevelStarsPanel.GetComponent<Image>();
        SelectMap select_map = this.SelectMapPanel.GetComponent<SelectMap>();
        select_map.SetMaps();
        int map_num = 1; // 一度初期設定しておく
        int levelstars_num = 1;
        map_num = StaticScriptsCon.MapNumber();
        StaticScriptsCon.SetMapLevel(Level);
        this.currentLevel = Level;
        //Debug.Log(currentLevel);

        levelstars_num = select_map.StarsNum(map_num, this.currentLevel);
        select_map.UpdateLevelStars(levelstars_num, this.LevelStars);
        BestTimeTextCon besttime_con = GameObject.Find("BestTimeText").GetComponent<BestTimeTextCon>();
        besttime_con.TextUpdate();

    }

    public void LevelSetActive(int level, bool isActive)
    {
        GameObject.Find("Level" + level).GetComponent<Toggle>().interactable = isActive;

    }

    public void LevelToggleSetOn(int level, bool ison)
    {
        GameObject.Find("Level" + level).GetComponent<Toggle>().isOn = ison;

    }
}