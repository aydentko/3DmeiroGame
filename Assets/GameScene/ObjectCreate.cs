using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreate : MonoBehaviour {
    public struct iPosition
    {  // グリッドでの座標を表す構造体
        public int x;
        public int y;

    };

    public struct oPosition
    {  // オブジェクト間での座標を表す構造体
        public int x;
        public int y;

    };

    public static int GRID_NUM_X; // グリッド座標のX方向の最大数
    public static int GRID_NUM_Y; // グリッド座標のY方向の最大数

    public iPosition i_pos;  // グリッド座標
    public oPosition o_pos;  // オブジェクト間座標
    private static MeiroCreate meiro_create;    // MeiroCreateクラスを定義

    public static int OBJECT_NUM_X; // オブジェクト間座標のX方向の最大数
    public static int OBJECT_NUM_Y; // オブジェクト間座標のY方向の最大数

    public Vector2 EVEN_SIZE = new Vector2(1.0f, 1.0f); // 偶数行/列のオブジェクトのサイズ
    public Vector2 ODD_SIZE = new Vector2(8.0f, 8.0f); // 奇数行/列のオブジェクトのサイズ


    // 迷路の部品となるGameObjectのPrefabを定義
    public GameObject BigFloorPrefab = null;
    public GameObject SmartFloorPrefab_x = null;
    public GameObject SmartFloorPrefab_y = null;
    public GameObject StartWallPrefab = null;
    public GameObject GrowWallPrefab_x = null;
    public GameObject GrowWallPrefab_y = null;
    public GameObject StartFloorPrefab = null;
    public GameObject FinishFloorPrefab = null;
    public GameObject StatuePrefab = null;

    // 壁のテクスチャを複数用意
    public Material Texture1 = null;
    public Material Texture2 = null;
    public Material Texture3 = null;
    public Material Texture4 = null;


    // Use this for initialization
    void Start () {
        meiro_create = this.GetComponent<MeiroCreate>();
        meiro_create.MakeMeiro();  // まずは全ての値を算出

        // オブジェクト間座標の最大数
        OBJECT_NUM_X = meiro_create.all.x;
        OBJECT_NUM_Y = meiro_create.all.y;

        // グリッド座標の最大数を算出
        GRID_NUM_X = 1 * meiro_create.fs_wall_num.x + 3 * (meiro_create.all.x - meiro_create.fs_wall_num.x);
        GRID_NUM_Y = 1 * meiro_create.fs_wall_num.y + 3 * (meiro_create.all.y - meiro_create.fs_wall_num.y);


        // 床の作成
        this.FloorMake();

        // 枠壁の作成の作成
        this.BaseWallMake();

        GameObject start_floor = GameObject.FindGameObjectWithTag("Start");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = GameObject.FindGameObjectWithTag("Start").transform.position;
        player.transform.position += new Vector3(0.0f, 3.0f, 0.0f);  // 高さだけ調整しなおす

        switch (meiro_create.enter_wall[0])
        {
            case 0:// 左側に入口があるなら、プレイヤーは右を向く
                player.transform.Rotate(0.0f, 90.0f, 0.0f);
                break;
            case 1:// 下側に入口があるなら、プレイヤーは上を向く
                player.transform.Rotate(0.0f, 0.0f, 0.0f);
                break;
            case 2:// 上側に入口があるなら、プレイヤーは下を向く
                player.transform.Rotate(0.0f, 180.0f, 0.0f);
                break;
            case 3:// 右側に入口があるなら、プレイヤーは左を向く
                player.transform.Rotate(0.0f, -90.0f, 0.0f);
                break;


        }

        // 迷路本体を作る
        this.GrowWallMake();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public Vector3 CalcObjectPosition(Vector2Int grid)
    {
        Vector3 position = new Vector3(-(GRID_NUM_X / 2.0f), 0.0f, -(GRID_NUM_Y / 2.0f));

        int i = grid.x % 2;
        int g = grid.y & 2;
        if(i == 0)
        {
            // iが偶数なら
            // 初期値＋グリッド座標×配置するオブジェクトのサイズ
            position.x += ((float)grid.x / 2) * this.EVEN_SIZE.x + ((float)grid.x / 2) * this.ODD_SIZE.x + 
                this.EVEN_SIZE.x / 2;
        }
        else
        {
            // iが奇数なら
            position.x += (((float)grid.x - 1) / 2) * this.ODD_SIZE.x + (((float)grid.x + 1) / 2) * this.EVEN_SIZE.x + 
                this.ODD_SIZE.x / 2;
        }
        if (g == 0)
        {
            // gが偶数なら
            position.z += ((float)grid.y / 2) * this.EVEN_SIZE.y + ((float)grid.y / 2) * this.ODD_SIZE.y + 
                this.EVEN_SIZE.y / 2;
        }
        else
        {
            // gが奇数なら
            position.z += (((float)grid.y - 1) / 2) * this.ODD_SIZE.y + (((float)grid.y + 1) / 2) * this.EVEN_SIZE.y +
                this.ODD_SIZE.y / 2;
        }

        return (position);
    }


    void FloorMake()  // 床と始点壁を作るメソッド
    {   // BigFloorはオブジェクト間座標上でのXYがともに偶数の位置にある。
        // 細長い床はMeiroCreateのbool配列によってBigFloorの周りに並べられる

        // prefabを定義
        GameObject smart_floor_x;
        GameObject smart_floor_y;
        GameObject big_floor;
        GameObject start_wall;

        int c_x = 0;
        int c_y = 0;
        int s_x = 0; // StartWall用の座標
        int s_y = 0;

        // 作成したオブジェクトを保管する配列を用意
        GameObject[,] BigFloors = new GameObject[(OBJECT_NUM_X - 1) / 2, (OBJECT_NUM_Y - 1) / 2];
        GameObject[,] SmartFloors_x = new GameObject[meiro_create.s_floor_x_num.x, meiro_create.s_floor_x_num.y];
        GameObject[,] SmartFloors_y = new GameObject[meiro_create.s_floor_y_num.x, meiro_create.s_floor_y_num.y];
        GameObject[,] StartWalls = new GameObject[meiro_create.s_wall_num.x, meiro_create.s_wall_num.x];

        Vector3 ObPosition;

        for (int i = 0; i < OBJECT_NUM_X - 2; i++)
        {
            for (int g = 0; g < OBJECT_NUM_Y - 2; g++)
            {
                c_x = i % 2;
                c_y = g % 2;
                if (c_x == 0)
                {
                    // xが偶数の時
                    if (c_y == 0)
                    {
                        // yが偶数の時
                        // BigFloorを作る
                        // BigFloorオブジェクトを作成
                        big_floor = Instantiate(BigFloorPrefab) as GameObject;
                        // (i, g)の位置のゲーム座標を算出
                        ObPosition = CalcObjectPosition(new Vector2Int(i + 1, g + 1));

                        // 算出した位置にBigFloorを移動
                        big_floor.transform.position = ObPosition;

                        // 作成したBigFloorの名前を設定
                        big_floor.name = "BigFloor(" + i.ToString() + "," + g.ToString() + ")";
                        //Debug.Log(big_floor.name);

                        // 作成したBigFloorを配列に保管
                        BigFloors[i / 2, g / 2] = big_floor;
                    }
                    else
                    {
                        // yが奇数の時
                        // SmartFloor_xを作る
                        // SmartFloor_xオブジェクトを作成
                        smart_floor_x = Instantiate(SmartFloorPrefab_x) as GameObject;
                        // (i, g)の位置のゲーム座標を算出
                        ObPosition = CalcObjectPosition(new Vector2Int(i + 1, g + 1));

                        // 算出した位置にSmartFloor_xを移動
                        smart_floor_x.transform.position = ObPosition;

                        // 作成したSmartFloor_xの名前を設定
                        smart_floor_x.name = "SmartFloor_x(" + i.ToString() + "," + g.ToString() + ")";
                        //Debug.Log(smart_floor_x.name);

                        // 作成したSmartFloor_xを配列に保管
                        SmartFloors_x[i / 2, (g - 1)/2] = smart_floor_x;

                    }
                }
                else
                {
                    // xが奇数の時
                    if (c_y == 0)
                    {
                        // yが偶数の時
                        // SmartFloor_yを作る
                        // SmartFloor_yオブジェクトを作成
                        smart_floor_y = Instantiate(SmartFloorPrefab_y) as GameObject;
                        // (i, g)の位置のゲーム座標を算出
                        ObPosition = CalcObjectPosition(new Vector2Int(i + 1, g + 1));

                        // 算出した位置にSmartFloor_yを移動
                        smart_floor_y.transform.position = ObPosition;

                        // 作成したSmartFloor_yの名前を設定
                        smart_floor_y.name = "SmartFloor_y(" + i.ToString() + "," + g.ToString() + ")";
                        //Debug.Log(smart_floor_y.name);

                        // 作成したSmartFloor_yを配列に保管
                        SmartFloors_y[(i - 1)/ 2, g / 2] = smart_floor_y;

                    }
                    else
                    {
                        // yが奇数の時
                        // StartWallを作る
                        // 今作ろうとしているStartWallの位置を確認
                        if(meiro_create.statues_posi.Contains(new Vector2Int(s_x, s_y)))
                        {
                            // もし今作ろうとしているStartWallが石像になるやつなら
                            // Statueオブジェクトを作成
                            start_wall = Instantiate(StatuePrefab) as GameObject;
                        }
                        else
                        {
                            // ふつうに始点壁なら
                            // StartWallオブジェクトを作成
                            start_wall = Instantiate(StartWallPrefab) as GameObject;
                        }
                        // (i, g)の位置のゲーム座標を算出
                        ObPosition = CalcObjectPosition(new Vector2Int(i + 1, g + 1));

                        // 算出した位置にStartWallを移動
                        start_wall.transform.position = ObPosition;

                        // 作成したStartWallの名前を設定
                        start_wall.name = "StartWall(" + i.ToString() + "," + g.ToString() + ")";
                        //Debug.Log(start_wall.name);

                        // 作成したStartWallを配列に保管
                        StartWalls[(i - 1)/2, (g - 1)/2] = start_wall;

                        // s_x,s_yの位置も移動しておく
                        if (s_y < meiro_create.s_wall_num.y - 1)
                        {
                            s_y++;
                        }
                        else
                        {
                            s_y = 0;
                            if (s_x < meiro_create.s_wall_num.x - 1)
                            {
                                s_x++;
                            }
                            else
                            {
                                s_x = 0;
                            }
                        }


                    }
                }
            }
        }

    }


    void BaseWallMake()
    {
        // prefabを定義
        GameObject small_wall; //= Instantiate(StartWallPrefab) as GameObject;
        GameObject long_wall_x; //= Instantiate(GrowWallPrefab_x) as GameObject;
        GameObject long_wall_y; //= Instantiate(GrowWallPrefab_y) as GameObject;

        // 作成したオブジェクトを保管する配列を用意
        GameObject[] DownWalls = new GameObject[OBJECT_NUM_X];  // 下の枠壁
        GameObject[] LeftWalls = new GameObject[OBJECT_NUM_Y];  // 左の枠壁
        GameObject[] UpWalls = new GameObject[OBJECT_NUM_X];  // 上の枠壁
        GameObject[] RightWalls = new GameObject[OBJECT_NUM_Y];  // 右の枠壁

        Vector3 ObPosition;

        // 下の枠壁を作成
        for (int i = 0; i < OBJECT_NUM_X; i++)
        {
            int g = i % 2;  // iが偶数か奇数か（g = 0なら偶数）
            if(g == 0)
            {
                // 偶数の場合はSmallWallを作る
                small_wall = Instantiate(StartWallPrefab) as GameObject;

                // (i, 0)の位置のゲーム座標を算出
                ObPosition = CalcObjectPosition(new Vector2Int(i, 0));

                // 算出した位置にSmallWallを移動
                small_wall.transform.position = ObPosition;

                // 作成したSmallWallの名前を設定
                small_wall.name = "DownSmallWall(" + i.ToString() + ")";
                //Debug.Log(small_wall.name);

                // 作成したSmallWallを配列に保管
                DownWalls[i] = small_wall;


            } else if(g == 1)
            {
                // 奇数の場合はLongWallを作る
                long_wall_x = Instantiate(GrowWallPrefab_x) as GameObject;
                if(i >= Mathf.FloorToInt(OBJECT_NUM_X / 2))
                {
                    // 作った壁の数が2/4に差し掛かったなら壁のテクスチャをTexture3か4に変更する
                    float r = Random.Range(0f, 2f);
                    if (r <= 1f)
                    {
                        long_wall_x.GetComponent<Renderer>().material = Texture3;

                    }
                    else
                    {
                        long_wall_x.GetComponent<Renderer>().material = Texture4;

                    }

                }
                else if(i >= Mathf.FloorToInt(OBJECT_NUM_X / 4))
                {
                    // 作った壁の数が1/4に差し掛かったなら壁のテクスチャをTexture2に変更する
                    long_wall_x.GetComponent<Renderer>().material = Texture2;

                }

                // (i, 0)の位置のゲーム座標を算出
                ObPosition = CalcObjectPosition(new Vector2Int(i, 0));

                // 算出した位置にLongWall_xを移動
                long_wall_x.transform.position = ObPosition;

                // 作成したLongWall_xの名前を設定
                long_wall_x.name = "DownLongWall_x(" + i.ToString() + ")";
                //Debug.Log(long_wall_x.name);

                // 作成したLongWall_xを配列に保管
                DownWalls[i] = long_wall_x;


            }
        }

        // 左の枠壁を作成
        for (int i = 0; i < OBJECT_NUM_Y; i++)
        {
            int g = i % 2;  // iが偶数か奇数か（g = 0なら偶数）
            if (g == 0)
            {
                // 偶数の場合はSmallWallを作る
                small_wall = Instantiate(StartWallPrefab) as GameObject;

                // (0, i)の位置のゲーム座標を算出
                ObPosition = CalcObjectPosition(new Vector2Int(0, i));

                // 算出した位置にSmartFloor_xを移動
                small_wall.transform.position = ObPosition;

                // 作成したSmallWallの名前を設定
                small_wall.name = "LeftSmallWall(" + i.ToString() + ")";
                //Debug.Log(small_wall.name);

                // 作成したBigFloorを配列に保管
                LeftWalls[i] = small_wall;


            }
            else if (g == 1)
            {
                // 奇数の場合はLongWallを作る
                long_wall_y = Instantiate(GrowWallPrefab_y) as GameObject;
                if (i >= Mathf.FloorToInt(OBJECT_NUM_Y / 2))
                {
                    // 作った壁の数が2/4に差し掛かったなら壁のテクスチャをTexture3か4に変更する
                    float r = Random.Range(0f, 2f);
                    if (r <= 1f)
                    {
                        long_wall_y.GetComponent<Renderer>().material = Texture3;

                    }
                    else
                    {
                        long_wall_y.GetComponent<Renderer>().material = Texture4;

                    }

                }
                else if (i >= Mathf.FloorToInt(OBJECT_NUM_Y / 4))
                {
                    // 作った壁の数が1/4に差し掛かったなら壁のテクスチャをTexture2に変更する
                    long_wall_y.GetComponent<Renderer>().material = Texture2;

                }

                // (0, i)の位置のゲーム座標を算出
                ObPosition = CalcObjectPosition(new Vector2Int(0, i));

                // 算出した位置にLong_Floor_yを移動
                long_wall_y.transform.position = ObPosition;

                // 作成したLongWall_yの名前を設定
                long_wall_y.name = "LeftLongWall_y(" + i.ToString() + ")";
                //Debug.Log(long_wall_y.name);

                // 作成したLongWall_yを配列に保管
                LeftWalls[i] = long_wall_y;


            }
        }

        // 上の枠壁を作成
        for (int i = 0; i < OBJECT_NUM_X; i++)
        {
            int g = i % 2;  // iが偶数か奇数か（g = 0なら偶数）
            if (g == 0)
            {
                // 偶数の場合はSmallWallを作る
                small_wall = Instantiate(StartWallPrefab) as GameObject;

                // (i, 最大y - 1)の位置のゲーム座標を算出
                ObPosition = CalcObjectPosition(new Vector2Int(i, OBJECT_NUM_Y - 1));

                // 算出した位置にSmallWallを移動
                small_wall.transform.position = ObPosition;

                // 作成したSmallWallの名前を設定
                small_wall.name = "UpSmallWall(" + i.ToString() + ")";
                //Debug.Log(small_wall.name);

                // 作成したSmallWallを配列に保管
                UpWalls[i] = small_wall;


            }
            else if (g == 1)
            {
                // 奇数の場合はLongWallを作る
                long_wall_x = Instantiate(GrowWallPrefab_x) as GameObject;
                if (i >= Mathf.FloorToInt(OBJECT_NUM_X / 2))
                {
                    // 作った壁の数が2/4に差し掛かったなら壁のテクスチャをTexture3か4に変更する
                    float r = Random.Range(0f, 2f);
                    if (r <= 1f)
                    {
                        long_wall_x.GetComponent<Renderer>().material = Texture3;

                    }
                    else
                    {
                        long_wall_x.GetComponent<Renderer>().material = Texture4;

                    }

                }
                else if (i >= Mathf.FloorToInt(OBJECT_NUM_X / 4))
                {
                    // 作った壁の数が1/4に差し掛かったなら壁のテクスチャをTexture2に変更する
                    long_wall_x.GetComponent<Renderer>().material = Texture2;

                }

                // (i, 最大y)の位置のゲーム座標を算出
                ObPosition = CalcObjectPosition(new Vector2Int(i, OBJECT_NUM_Y - 1));

                // 算出した位置にLongWall_xを移動
                long_wall_x.transform.position = ObPosition;

                // 作成したLongWall_xの名前を設定
                long_wall_x.name = "UpLongWall_x(" + i.ToString() + ")";
                //Debug.Log(long_wall_x.name);

                // 作成したLongWall_xを配列に保管
                UpWalls[i] = long_wall_x;


            }
        }

        // 右の枠壁を作成
        for (int i = 0; i < OBJECT_NUM_Y; i++)
        {
            int g = i % 2;  // iが偶数か奇数か（g = 0なら偶数）
            if (g == 0)
            {
                // 偶数の場合はSmallWallを作る
                small_wall = Instantiate(StartWallPrefab) as GameObject;

                // ((最大x - 1, i)の位置のゲーム座標を算出
                ObPosition = CalcObjectPosition(new Vector2Int(OBJECT_NUM_X - 1, i));

                // 算出した位置にSmartFloor_xを移動
                small_wall.transform.position = ObPosition;

                // 作成したSmallWallの名前を設定
                small_wall.name = "RightSmallWall(" + i.ToString() + ")";
                //Debug.Log(small_wall.name);

                // 作成したBigFloorを配列に保管
                RightWalls[i] = small_wall;


            }
            else if (g == 1)
            {
                // 奇数の場合はLongWallを作る
                long_wall_y = Instantiate(GrowWallPrefab_y) as GameObject;
                if (i >= Mathf.FloorToInt(OBJECT_NUM_Y / 2))
                {
                    // 作った壁の数が2/4に差し掛かったなら壁のテクスチャをTexture3か4に変更する
                    float r = Random.Range(0f, 2f);
                    if (r <= 1f)
                    {
                        long_wall_y.GetComponent<Renderer>().material = Texture3;

                    }
                    else
                    {
                        long_wall_y.GetComponent<Renderer>().material = Texture4;

                    }

                }
                else if (i >= Mathf.FloorToInt(OBJECT_NUM_Y / 4))
                {
                    // 作った壁の数が1/4に差し掛かったなら壁のテクスチャをTexture2に変更する
                    long_wall_y.GetComponent<Renderer>().material = Texture2;

                }

                // (GRID_NUM_X * 2, i)の位置のゲーム座標を算出
                ObPosition = CalcObjectPosition(new Vector2Int(OBJECT_NUM_X - 1, i));

                // 算出した位置にLong_Floor_yを移動
                long_wall_y.transform.position = ObPosition;

                // 作成したLongWall_yの名前を設定
                long_wall_y.name = "RightLongWall_y(" + i.ToString() + ")";
                //Debug.Log(long_wall_y.name);

                // 作成したLongWall_yを配列に保管
                RightWalls[i] = long_wall_y;


            }
        }

        // MeiroCreateのbool配列から入口と出口のところを空ける
        int enter_d = meiro_create.enter_wall[0];
        int enter_i = meiro_create.enter_wall[1];
        enter_i = enter_i * 2 + 1;  // 穴をあける壁は奇数の位置にある

        int exit_d = meiro_create.exit_wall[0];
        int exit_i = meiro_create.exit_wall[1];
        exit_i = exit_i * 2 + 1;  // 

        // まずは入り口から

        switch(enter_d)
        {
            case 0: // 左方向に入口があるなら
                GameObject destroied = GameObject.Find("LeftLongWall_y(" + enter_i + ")") as GameObject;
                Vector3 enter_posi = destroied.transform.position;
                Object.Destroy(destroied);

                // 代わりにスタートとなる床をStartFloorで作る
                GameObject StartFloor = Instantiate(StartFloorPrefab) as GameObject;
                enter_posi.x -= StartFloor.transform.localScale.x / 2 - 0.5f;
                StartFloor.transform.position = enter_posi;
                // 向きを右向きに変える
                StartFloor.transform.Rotate(0.0f, 90.0f, 0.0f);
                break;

            case 1: // 下方向に入口があるなら
                destroied = GameObject.Find("DownLongWall_x(" + enter_i + ")") as GameObject;
                enter_posi = destroied.transform.position;
                Object.Destroy(destroied);

                // 代わりにスタートとなる床をStartFloorで作る
                StartFloor = Instantiate(StartFloorPrefab) as GameObject;
                enter_posi.z -= StartFloor.transform.localScale.z / 2 - 0.5f;
                StartFloor.transform.position = enter_posi;
                // 向きを上向きのまま
                StartFloor.transform.Rotate(0.0f, 0.0f, 0.0f);
                break;

            case 2: // 上方向に入口があるなら
                destroied = GameObject.Find("UpLongWall_x(" + enter_i + ")") as GameObject;
                enter_posi = destroied.transform.position;
                Object.Destroy(destroied);

                // 代わりにスタートとなる床をStartFloorで作る
                StartFloor = Instantiate(StartFloorPrefab) as GameObject;
                enter_posi.z += StartFloor.transform.localScale.z / 2 - 0.5f;
                StartFloor.transform.position = enter_posi;
                // 向きを下向きに変える
                StartFloor.transform.Rotate(0.0f, 180.0f, 0.0f);
                break;

            case 3: // 右方向に入口があるなら
                destroied = GameObject.Find("RightLongWall_y(" + enter_i + ")") as GameObject;
                enter_posi = destroied.transform.position;
                Object.Destroy(destroied);

                // 代わりにスタートとなる床をStartFloorで作る
                StartFloor = Instantiate(StartFloorPrefab) as GameObject;
                enter_posi.x += StartFloor.transform.localScale.x / 2 - 0.5f;
                StartFloor.transform.position = enter_posi;
                // 向きを左向きに変える
                StartFloor.transform.Rotate(0.0f, -90.0f, 0.0f);
                break;

        }

        // 次に出口

        switch (exit_d)
        {
            case 0:// 左方向に出口があるなら
                GameObject destroied = GameObject.Find("LeftLongWall_y(" + exit_i + ")") as GameObject;
                Vector3 exit_posi = destroied.transform.position;
                Object.Destroy(destroied);

                // 代わりにスタートとなる床をFinishFloorで作る
                GameObject FinishFloor = Instantiate(FinishFloorPrefab) as GameObject;
                exit_posi.x -= FinishFloor.transform.localScale.x / 2 - 0.5f;
                FinishFloor.transform.position = exit_posi;
                // 向きを右向きに変える
                FinishFloor.transform.Rotate(0.0f, 90.0f, 0.0f);
                break;

            case 1: // 下方向に出口があるなら
                destroied = GameObject.Find("DownLongWall_x(" + exit_i + ")") as GameObject;
                exit_posi = destroied.transform.position;
                Object.Destroy(destroied);

                // 代わりにスタートとなる床をFinishFloorで作る
                FinishFloor = Instantiate(FinishFloorPrefab) as GameObject;
                exit_posi.z -= FinishFloor.transform.localScale.z / 2 - 0.5f;
                FinishFloor.transform.position = exit_posi;
                // 向きは上向きのまま
                FinishFloor.transform.Rotate(0.0f, 0.0f, 0.0f);
                break;

            case 2: // 上方向に出口があるなら
                destroied = GameObject.Find("UpLongWall_x(" + exit_i + ")") as GameObject;
                exit_posi = destroied.transform.position;
                Object.Destroy(destroied);

                // 代わりにスタートとなる床をFinishFloorで作る
                FinishFloor = Instantiate(FinishFloorPrefab) as GameObject;
                exit_posi.z += FinishFloor.transform.localScale.z / 2 - 0.5f;
                FinishFloor.transform.position = exit_posi;
                // 向きを下向きに変える
                FinishFloor.transform.Rotate(0.0f, 180.0f, 0.0f);
                break;

            case 3: // 右方向に出口があるなら
                destroied = GameObject.Find("RightLongWall_y(" + exit_i + ")") as GameObject;
                exit_posi = destroied.transform.position;
                Object.Destroy(destroied);

                // 代わりにスタートとなる床をFinishFloorで作る
                FinishFloor = Instantiate(FinishFloorPrefab) as GameObject;
                exit_posi.x += FinishFloor.transform.localScale.x / 2 - 0.5f;
                FinishFloor.transform.position = exit_posi;
                // 向きを左向きに変える
                FinishFloor.transform.Rotate(0.0f, -90.0f, 0.0f);
                break;
        }




    }

    void GrowWallMake()
    {
        // 伸びる壁を立てていく
        // 伸びる壁をつくる
        GameObject grow_wall_x; //= Instantiate(GrowWallPrefab_x) as GameObject;
        GameObject grow_wall_y; //= Instantiate(GrowWallPrefab_y) as GameObject;


        // 作成したオブジェクトを保管する配列を用意
        GameObject[,] GrowWalls_x = new GameObject[meiro_create.g_wall_x_num.x, meiro_create.g_wall_x_num.y];
        GameObject[,] GrowWalls_y = new GameObject[meiro_create.g_wall_y_num.x, meiro_create.g_wall_y_num.y];

        // 調整後の位置を保管する変数
        Vector3 ObPosition;

        bool can_make = false; // 壁を作れるかどうかを保存する用の変数

        // GrowWall_xを作っていく
        for (int i = 0; i < meiro_create.g_wall_x_num.x; i++)
        {
            for(int g = 0; g < meiro_create.g_wall_x_num.y; g++)
            {
                can_make = meiro_create.g_wall_x[i, g];
                if (can_make)//テスト用にtrueにしておく
                {
                    // 壁が作れるなら
                    // grow_wall_xを作る
                    grow_wall_x = Instantiate(GrowWallPrefab_x) as GameObject;
                    float r = Random.Range(0f, 4f);
                    if (r >= 1f && r < 2f)
                    {
                        // 乱数が1～2ならテクスチャ2に変更する
                        grow_wall_x.GetComponent<Renderer>().material = Texture2;

                    }
                    else if (r >= 2f && r < 3f)
                    {
                        // 乱数が2～3ならテクスチャ3に変更する
                        grow_wall_x.GetComponent<Renderer>().material = Texture3;

                    }
                    else if (r >= 3f)
                    {
                        // 乱数が3～4ならテクスチャ4に変更する
                        grow_wall_x.GetComponent<Renderer>().material = Texture4;

                    }

                    // (i * 2 + 1, g * 2 + 2)の位置のゲーム座標を算出
                    ObPosition = CalcObjectPosition(new Vector2Int(i * 2 + 1, g * 2 + 2));

                    // 算出した位置にSmallWallを移動
                    grow_wall_x.transform.position = ObPosition;

                    // 作成したSmallWallの名前を設定
                    grow_wall_x.name = "GrowWall_x(" + i.ToString() + ")";
                    //Debug.Log(grow_wall_x.name);

                    // 作成したSmallWallを配列に保管
                    GrowWalls_x[i, g] = grow_wall_x;

                }

            }
            
        }

        // GrowWall_yを作っていく
        for (int i = 0; i < meiro_create.g_wall_y_num.x; i++)
        {
            for (int g = 0; g < meiro_create.g_wall_y_num.y; g++)
            {
                can_make = meiro_create.g_wall_y[i, g];
                if (can_make)//テスト用にtrueにしておく
                {
                    // 壁が作れるなら
                    // grow_wall_yを作る
                    grow_wall_y = Instantiate(GrowWallPrefab_y) as GameObject;
                    float r = Random.Range(0f, 4f);
                    if (r >= 1f && r < 2f)
                    {
                        // 乱数が1～2ならテクスチャ2に変更する
                        grow_wall_y.GetComponent<Renderer>().material = Texture2;

                    }
                    else if (r >= 2f && r < 3f)
                    {
                        // 乱数が2～3ならテクスチャ3に変更する
                        grow_wall_y.GetComponent<Renderer>().material = Texture3;

                    }
                    else if (r >= 3f)
                    {
                        // 乱数が3～4ならテクスチャ4に変更する
                        grow_wall_y.GetComponent<Renderer>().material = Texture4;

                    }

                    // (i * 2 + 2, g * 2 + 1)の位置のゲーム座標を算出
                    ObPosition = CalcObjectPosition(new Vector2Int(i * 2 + 2, g * 2 + 1));

                    // 算出した位置にSmallWallを移動
                    grow_wall_y.transform.position = ObPosition;

                    // 作成したSmallWallの名前を設定
                    grow_wall_y.name = "GrowWall_y(" + i.ToString() + ")";
                    //Debug.Log(grow_wall_y.name);

                    // 作成したSmallWallを配列に保管
                    GrowWalls_y[i, g] = grow_wall_y;

                }

            }

        }

    }
}