using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestTimeTextCon : MonoBehaviour {
    private int best_time = 300;
    private int map_num;
    private int map_level;

    private StaticScriptsCon ssc;

	// Use this for initialization
	void Start () {
        ssc = GameObject.Find("StaticScriptsOb").GetComponent<StaticScriptsCon>();
        this.TextUpdate();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void TextUpdate()
    {
        map_num = GameObject.Find("SelectPanel").GetComponent<SelectMap>().currentMapNum;
        map_level = GameObject.Find("LevelToggles").GetComponent<LevelSelectControl>().currentLevel;

        //Debug.Log(StaticScriptsCon.BestTime(map_num, map_level));
        this.best_time = StaticScriptsCon.BestTime(map_num, map_level);

        int min = best_time / 60;
        int sec = best_time - min * 60;
        this.GetComponent<Text>().text = "ベストタイム：" + min.ToString() + "分" + sec.ToString() + "秒";

    }

}
