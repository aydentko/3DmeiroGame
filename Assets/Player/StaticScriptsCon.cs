using UnityEngine;

public class StaticScriptsCon : MonoBehaviour {
    public static bool isinitialize = false; // 開発者用初期化コード

    public static int MAP_NUMBER = 1;
    public static int MAP_LEVEL = 1;
    public static int[,] BEST_TIMES = new int[8,4];
    static int is_ini = 0;
    //********** 開始 **********// 
    // 保存キーの配列を作成
    string[,] keys = new string[8, 4];
    string key = "SavedData";
    //********** 終了 **********// 

    // Use this for initialization
    void Start () {
        // 開発者用初期化処理
        if (isinitialize)
        {
            PlayerPrefs.SetInt("ini", 0);
            isinitialize = false;
        }
        
        //********** 開始 **********// 
        // 各キーの作成と配列への保管
        for (int i = 1; i < 8; i++)
        {
            for (int n = 1; n < 4; n++)
            {
                keys[i, n] = key + i + "_" + n; // ”SaveData1_1”のように名前を付ける
            }
        }

        // 初期化したかどうかのスイッチを取得
        is_ini = PlayerPrefs.GetInt("ini");

        // BEST_TIMEの初期化
        if (is_ini == 0)
        {
            this.IniBestTime();
            is_ini = 1;
            PlayerPrefs.SetInt("ini", is_ini);
        }

        // データのロード
        //保存キー「SavedDatai_n」で保存されたstring型のデータがあればそれを取得
        for (int i = 1; i < 8; i++)
        {
            for (int n = 1; n < 4; n++)
            {
                BEST_TIMES[i, n] = PlayerPrefs.GetInt(keys[i, n]);
            }
        }
        //********** 終了 **********// 

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SaveData(int map_num, int map_level, int best_time)
    {
        // データの保存
        //********** 開始 **********//
        //保存キー「SavedData」で入力文字を保存
        PlayerPrefs.SetInt("SavedData" + map_num + "_" + map_level, best_time);
        PlayerPrefs.Save();
        //********** 終了 **********// 
    }


    public void OnInitialize()
    {
        isinitialize = true;
    }

    void IniBestTime()
    {
        for (int i = 1; i < 8; i++)
        {
            if (i == 1)
            {
                // Map1の時のみ
                BEST_TIMES[i, 1] = 300;
                BEST_TIMES[i, 2] = 360;
                BEST_TIMES[i, 3] = 480;
            }
            else if(i >=2)
            {
                // Map2以降
                BEST_TIMES[i, 1] = 420;
                BEST_TIMES[i, 2] = 360;
                BEST_TIMES[i, 3] = 300;
            }
            // データの保存
            for (int n = 1; n < 4; n++)
            {
                this.SaveData(i, n, BEST_TIMES[i, n]);
            }

        }

    }

    // 以下はstatic変数を取得するためのGET関数
    public static int MapNumber()
    {
        return MAP_NUMBER;
    }

    public static int MapLevel()
    {
        return MAP_LEVEL;
    }

    public static int BestTime(int MapNum, int MapLevel)
    {
        return BEST_TIMES[MapNum, MapLevel];
    }

    public static void NewRecordSet(int MapNum, int Level, int NewRecord)
    {
        BEST_TIMES[MapNum, Level] = NewRecord;
        // セーブデータへの書き込み
        PlayerPrefs.SetInt("SavedData" + MapNum + "_" + Level, BEST_TIMES[MapNum, Level]);
        PlayerPrefs.Save();
    }

    public static void SetMapLevel(int Level)
    {
        MAP_LEVEL = Level;
    }

    public static void SetMapNumber(int MapNumber)
    {
        MAP_NUMBER = MapNumber;
    }

}


