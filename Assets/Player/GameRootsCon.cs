using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRootsCon : MonoBehaviour {
    private Vector3Int StartTime = new Vector3Int(0, 0, 0);
    public static float NOW_GAMETIME = 0;
    int now_level = 1;
    int now_map = 1;
    StaticScriptsCon ssc;
    private int best_score = 0;
    public bool is_firstScene = false;
    public bool is_LastScene = false;

    private string time_text = "";
    public GameObject TimeText = null;
    public GameObject ScoreText = null;
    public GameObject ScorePanel = null;


    // Use this for initialization
    void Start () {
        ssc = GameObject.Find("StaticScriptsOb").GetComponent<StaticScriptsCon>();
        // 現在のマップ番号とレベルを保管
        this.now_level = StaticScriptsCon.MapLevel();
        this.now_map = StaticScriptsCon.MapNumber();
        // ベストスコアを保管
        this.best_score = StaticScriptsCon.BestTime(now_map, now_level);

        // スタートタイムを保管
        if(now_map == 2)
        {
            // 「がっこう」のマップの場合
            this.StartTime = new Vector3Int(420, 360, 300);

        }
        else if(now_map == 5)
        {
            // 「おいかけっこ」のマップの場合
            this.StartTime = new Vector3Int(420, 360, 300);

        }

        // 最初のシーンの場合はNOW_GAMETIMEを初期化する
        if (is_firstScene)
        {
            Debug.Log("TimeInitialize");
            switch (this.now_level)
            {
                case 1:
                    NOW_GAMETIME = this.StartTime.x;
                    break;
                case 2:
                    NOW_GAMETIME = this.StartTime.y;
                    break;
                case 3:
                    NOW_GAMETIME = this.StartTime.z;
                    break;
            }
            is_firstScene = false;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!is_LastScene)
        {
            // ゲームクリアシーンでないなら
            NOW_GAMETIME -= Time.deltaTime; // 時間を減らしていく

            float _time = NOW_GAMETIME; // 一時変数に残り時間を一旦保管
            int minute = Mathf.FloorToInt(_time) / 60;
            int second = Mathf.FloorToInt(_time) - minute * 60;
            // 残り時間を表示
            this.time_text = minute.ToString() + "分" + second.ToString() + "秒";
            this.TimeText.GetComponent<Text>().text = this.time_text;
        }
        else
        {
            // プレイヤーがゴールについていたら
            this.GameOver();  // ゲーム終了処理を行う

        }
    }

    private void GameOver()
    {
        //this.finish = true;  // タイマーを止める
        this.ScorePanel.SetActive(true);
        float st = 0;
        int bt = 0;
        switch (now_level)
        {
            case 1:
                st = this.StartTime.x;
                break;
            case 2:
                st = this.StartTime.y;
                break;
            case 3:
                st = this.StartTime.z;
                break;

        }
        bt = StaticScriptsCon.BestTime(now_map, now_level);

        int score = (int)st - (int)NOW_GAMETIME;  // かかった時間を計算
        int min = score / 60;
        int sec = score - min * 60;

        string score_text = min.ToString() + "分" + sec.ToString() + "秒";
        this.ScoreText.GetComponent<Text>().text = "あなたのタイム：" + score_text;
        if (score < this.best_score)
        {
            // 新記録なら更新する
            this.best_score = score;
        }
        min = best_score / 60;
        sec = best_score - min * 60;
        score_text = "";
        score_text = min.ToString() + "分" + sec.ToString() + "秒";
        this.ScoreText.GetComponent<Text>().text += "\nベストタイム：" + score_text;
        // static変数の値を更新
        StaticScriptsCon.NewRecordSet(now_map, now_level, best_score);

    }


}
