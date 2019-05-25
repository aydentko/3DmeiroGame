using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRoot : MonoBehaviour {
    private Vector3[] Start_Time = new Vector3[8];
    //public Cache# Game_start_button = null;
    //public BestTimeTextCon Best_time_text_con = null;

    float game_time = 300f;
    bool finish = false;
    private string time_text = "";
    public GameObject TimeText = null;
    public GameObject ScoreText = null;
    public GameObject ScorePanel = null;
    GameObject goal_floor;
    GoalTriger goal_triger;
    int score = 0;
    string score_text = "";
    private int now_level;
    // 現在のマップを取得
    private int now_map;
    private int best_score;
    private StaticScriptsCon ssc;

    // Use this for initialization
    void Start () {
        ssc = GameObject.Find("StaticScriptsOb").GetComponent<StaticScriptsCon>();
        // 現在のマップ番号とレベルを保管
        this.now_level = StaticScriptsCon.MapLevel();
        this.now_map = StaticScriptsCon.MapNumber();
         //Debug.Log("Map" + now_map + "Level" + now_level);
        // ベストスコアを保管
        this.best_score = StaticScriptsCon.BestTime(now_map, now_level);

        // プレイヤーを初期位置に配置
        GameObject StartFloor = GameObject.FindGameObjectWithTag("Start") as GameObject;
        GameObject Player = GameObject.FindGameObjectWithTag("Player") as GameObject;
        Player.transform.position = StartFloor.transform.position;
        Player.transform.position += new Vector3(0, 3.0f, 0);

        // ゴール判定処理の準備
        this.goal_floor = GameObject.FindGameObjectWithTag("Goal");
        this.goal_triger = this.goal_floor.GetComponent<GoalTriger>();

        this.Start_Time[0] = new Vector3(0, 0, 0);//適当に作っとく
        // Map1
        this.Start_Time[1] = new Vector3(300, 360, 480);

        // Map2
        this.Start_Time[2] = new Vector3(420, 360, 300);

        // Map3
        this.Start_Time[3] = new Vector3(420, 360, 300);

        // Map4
        this.Start_Time[4] = new Vector3(420, 360, 300);

        // Map5
        this.Start_Time[5] = new Vector3(420, 360, 300);

        // Map6
        this.Start_Time[6] = new Vector3(420, 360, 300);

        // Map7
        this.Start_Time[7] = new Vector3(420, 360, 300);

        switch (now_level)
        {
            case 1:
                this.game_time = this.Start_Time[now_map].x;
                break;
            case 2:
                this.game_time = this.Start_Time[now_map].y;
                break;
            case 3:
                this.game_time = this.Start_Time[now_map].z;
                break;

        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!finish)
        {
            this.game_time -= Time.deltaTime;

            float _time = this.game_time;
            int minute = Mathf.FloorToInt(_time) / 60;
            int second = Mathf.FloorToInt(_time) - minute * 60;
            
            this.time_text = minute.ToString() + "分" + second.ToString() + "秒";
            this.TimeText.GetComponent<Text>().text = this.time_text;
        }

        if (this.goal_triger.game_over)
        {
            // プレイヤーがゴールについていたら
            this.GameOver();  // ゲーム終了処理を行う

        }
    }
    
    private void GameOver()
    {
        this.finish = true;  // タイマーを止める
        this.ScorePanel.SetActive(true);
        float st = 0;
        int bt = 0;
        switch (now_level)
        {
            case 1:
                st = this.Start_Time[now_map].x;
                break;
            case 2:
                st = this.Start_Time[now_map].y;
                break;
            case 3:
                st = this.Start_Time[now_map].z;
                break;

        }
        bt = StaticScriptsCon.BestTime(now_map, now_level);

        this.score = (int)st - (int)game_time;  // 残り時間を加算
        int min = this.score / 60;
        int sec = this.score - min * 60;

        this.score_text = min.ToString() + "分" + sec.ToString() + "秒";
        this.ScoreText.GetComponent<Text>().text = "あなたのタイム：" + this.score_text;
        if(this.score < this.best_score)
        {
            // 新記録なら更新する
            this.best_score = this.score;
        }
        min = this.best_score / 60;
        sec = this.best_score - min * 60;
        this.score_text = "";
        this.score_text = min.ToString() + "分" + sec.ToString() + "秒";
        this.ScoreText.GetComponent<Text>().text += "\nベストタイム：" + this.score_text;
        // static変数の値を更新
        StaticScriptsCon.NewRecordSet(now_map, now_level, this.best_score);

    }

}
