using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeiroCreate : MonoBehaviour {
    public int[,] grid_mass;                         // グリッド座標のマス目。今はint型にしているが後で
                                                     //ここに壁や床などのGameObjectが登録される。
    public Vector2Int all;  // 壁と床の総数。x,y共に奇数でなければならない
    public Vector2Int s_wall_num;     // 始点となる壁の総数。列数(x)と行数(y)でそれぞれ管理
    private bool[,] s_wall;            // 始点となる壁が使用済みかどうかの配列。trueなら使用済み
    private List<Vector2Int> new_s_walls = new List<Vector2Int>(); // 未使用の始点壁が保管される。フリーズ防止用
    public bool[,] g_wall_x;          // 左右に伸びる壁が作られたかどうかの配列。trueなら生成済み
    public bool[,] g_wall_y;          // 上下に伸びる壁が作られたかどうかの配列。trueなら生成済み
    public int[] enter_wall = new int[2];        // 入口がどこにあるか。[0]に枠壁の方向（0～3）が、[1]に座標が入る
    public int[] exit_wall  = new int[2];         // 出口がどこにあるか。[0]に枠壁の方向（0～3）が、[1]に座標が入る
    private Vector2Int f_wall_num;  // 枠の大きい壁の総数。xには上下の、yには左右のそれぞれの枠壁の数が入る。
    public Vector2Int g_wall_x_num; // 左右に伸びる壁の一行/一列当たりの総数。xには横方向の、yには縦方向の総数が入る。
    public Vector2Int g_wall_y_num; // 上下に伸びる壁の一行/一列当たりの総数。xには横方向の、yには縦方向の総数が入る。
    public Vector2Int s_floor_x_num; // 左右に伸びる床の一行/一列当たりの総数。xには横方向の、yには縦方向の総数が入る。
    public Vector2Int s_floor_y_num; // 左右に伸びる床の一行/一列当たりの総数。xには横方向の、yには縦方向の総数が入る。
    public Vector2Int fs_wall_num;   // 枠の小さい壁の一行/一列当たりの総数。xには横方向の、yには縦方向の総数が入る。

    public List<Vector2Int> statues_posi = new List<Vector2Int>(); // ぽつんと立った始点壁を石像(仮)に変える用。

    private GameStartButton Game_Start_Button;


    // Use this for initialization
    void Start()
    {
    }
	// Update is called once per frame
	void Update () {
		
	}

    void CalcWallNum() {

        //this.Game_Start_Button = GameObject.Find("StartButton").GetComponent<GameStartButton>();
        int Map_Level = GameStartButton.MapLevel();
        switch (Map_Level)
        {
            case 1:
                this.all = new Vector2Int(15, 15);
                break;
            case 2:
                this.all = new Vector2Int(19, 19);
                break;
            case 3:
                this.all = new Vector2Int(25, 25);
                break;

        }

        // 始点となる壁の総数を算出
        // Int型では割り算ができなかったのでいったん使い捨ての変数を作ってそれをintに変換して使う
        Vector2 itizi = new Vector2(all.x + 1, all.y + 1);
        itizi /= 2;
        itizi -= new Vector2(2, 2);  // 枠壁の分を引く
        this.s_wall_num = new Vector2Int((int)itizi.x, (int)itizi.y);

        // 枠壁の始点壁の個数を算出
        this.fs_wall_num = new Vector2Int(s_wall_num.x + 2, s_wall_num.y + 2);

        // 枠壁の数を算出
        this.f_wall_num = new Vector2Int(all.x - fs_wall_num.x, all.y - fs_wall_num.y);

        // 伸びる壁の総数を算出
        this.g_wall_x_num = new Vector2Int((all.x - 1) / 2 , (all.y - 3) / 2);
        this.g_wall_y_num = new Vector2Int((all.x - 3) / 2 , (all.y - 1) / 2);

        // 細長い床の総数を算出
        this.s_floor_x_num = new Vector2Int(g_wall_x_num.x, g_wall_x_num.y);
        this.s_floor_y_num = new Vector2Int(g_wall_y_num.x, g_wall_y_num.y);

    }


    void InitializeWallNum()
    {
        // 各bool型配列をインスタンス化
        s_wall = new bool[s_wall_num.x, s_wall_num.y];
        g_wall_x = new bool[g_wall_x_num.x, g_wall_x_num.y];
        g_wall_y = new bool[g_wall_y_num.x, g_wall_y_num.y];

        // 始点となる壁をいったんすべてfalseにしておく
        for (int i = 0; i < s_wall_num.x; i++)
        {
            for (int g = 0; g < s_wall_num.y; g++)
            {
                this.s_wall[i, g] = false;
            }
        }

        // 出入口を決める
        int e_d;
        int e_i;
        do
        {
            // 入口を決める
            e_d = Random.Range(0, 3);  // 枠壁の方向を決める
            if (e_d == 0 || e_d == 3)
            {
                // 左か右に決まったら
                e_i = Random.Range(0, f_wall_num.x);

            } else
            {
                // 下か上に決まったら
                e_i = Random.Range(0, f_wall_num.y);

            }
            //enter_wall配列に保管
            enter_wall[0] = e_d;
            enter_wall[1] = e_i;

            // 出口を決める
            e_d = Random.Range(0, 3);  // 枠壁の方向を決める
            if (e_d == 0 || e_d == 3)
            {
                // 左か右に決まったら
                e_i = Random.Range(0, f_wall_num.x);

            }
            else
            {
                // 下か上に決まったら
                e_i = Random.Range(0, f_wall_num.y);

            }
            //exit_wall配列に保管
            exit_wall[0] = e_d;
            exit_wall[1] = e_i;

        } while (exit_wall[0] == enter_wall[0] && exit_wall[1] == enter_wall[1]); // 出口が入口とちがうところになるまで処理を繰り返す


        // 伸びる壁をいったんすべてfalseにしておく
        for (int i = 0; i > g_wall_x_num.x; i++)
        {
            for (int g = 0; g > g_wall_x_num.y; g++)
            {
                this.g_wall_x[i, g] = false;
            }
        }
        for (int i = 0; i > g_wall_y_num.x; i++)
        {
            for (int g = 0; g > g_wall_y_num.y; g++)
            {
                this.g_wall_y[i, g] = false;
            }
        }

        // 未使用の始点壁を一式配列に保管
        int x = 0;
        int y = 0;
        for(int i = 0; i < s_wall_num.x * s_wall_num.y; i++)
        {
            new_s_walls.Add(new Vector2Int(x, y));
            if(y < s_wall_num.y - 1)
            {
                y++;
            }
            else
            {
                y = 0;
                if (x < s_wall_num.x - 1)
                {
                    x++;
                }
                else
                {
                    x = 0;
                }

            }

        }
    }

    // ランダムな迷路を作成する処理（どこに壁を立てるか決める）
    void RamdomMeiroCreate()
    {
        int arry_num = 0; // 未使用のs_wallの座標が保管されている配列の番号

        int current_sx = 0;  // 現在いる始点x
        int current_sy = 0;  // 現在いる始点y

        int next_sx = 0;  // 次の行き先始点x
        int next_sy = 0;  // 次の行き先始点y

        int dir = 0;
        int[] dir_rng = new int[4];  // 進行可能な方向の配列
        int dir_rng_num = 4;  // 上記の配列の要素数

        bool can_make_wall = true; // 次の行き先に壁を伸ばせるかどうか
        bool al_swall = false; // 次の行き先にある始点壁がすでに使用されているかどうか
        bool new_making = true; // 新しく始点を作成したばかりか（伸びてきたものでないか）

        bool can_next = true;
        bool is_leftdir = true;
        bool is_rightdir = true;
        bool is_updir = true;
        bool is_downdir = true;
        Vector2Int left_wall = new Vector2Int(0, 0);
        Vector2Int right_wall = new Vector2Int(0, 0);
        Vector2Int up_wall = new Vector2Int(0, 0);
        Vector2Int down_wall = new Vector2Int(0, 0);

        // ①始点を決める
        do
        {
            //Debug.Log("Start!");
            // まだ使える壁が残っている場合は始点を決める

            // 0～配列の最大番号までのランダムな数字を算出
            arry_num = Random.Range(0, new_s_walls.Count);
            // 未使用な始点壁の座標が保管されている配列の中から使用する壁の座標を取得
            current_sx = new_s_walls[arry_num].x; // arry_num番の配列の要素のx値を保管
            current_sy = new_s_walls[arry_num].y; // arry_num番の配列の要素のy値を保管

            // 取得した座標にある始点壁を使用済みに変えておく
            this.s_wall[current_sx, current_sy] = true;
            // 取得した座標を配列から削除する
            this.new_s_walls.RemoveAt(arry_num);
            // 新しく始点を作成したばかりなのでフラグを立てておく
            new_making = true;
            //Debug.Log("listの要素数：" + this.new_s_walls.Count);
            //Debug.Log("壁伸ばし開始");


            // ②周囲の壁をチェックし、進める方向を調べる
            do
            {
                // いったんすべての方向をtrueにする
                is_leftdir = true;
                is_rightdir = true;
                is_updir = true;
                is_downdir = true;

                //Debug.Log("current_xy = (" + current_sx + ", " + current_sy + ")");

                // 周囲にすでに作られている壁がないかチェック
                left_wall = new Vector2Int(current_sx, current_sy);　// 今の始点から見て左にあるgrow_wallの座標を代入
                if (g_wall_x[left_wall.x, left_wall.y])
                {
                    // 左方向に壁ができていた場合はそっちに行けないようにする
                    is_leftdir = false;
                }
                right_wall = new Vector2Int(current_sx + 1, current_sy);
                if (g_wall_x[right_wall.x, right_wall.y])
                {
                    // 右方向に壁ができていた場合はそっちに行けないようにする
                    is_rightdir = false;
                }

                up_wall = new Vector2Int(current_sx , current_sy + 1);
                if (g_wall_y[up_wall.x, up_wall.y])
                {
                    // 上方向に壁ができていた場合はそっちに行けないようにする
                    is_updir = false;
                }

                down_wall = new Vector2Int(current_sx, current_sy);
                if (g_wall_y[down_wall.x, down_wall.y])
                {
                    // 下方向に壁ができていた場合はそっちに行けないようにする
                    is_downdir = false;
                }

                // 次に、周囲の始点が使われているかどうかを確認する
                for (int i = 0; i < 4; i++)
                {
                    // 次の始点先を今と同じ位置にしておく
                    next_sx = current_sx;
                    next_sy = current_sy;

                    switch (i)
                    {
                        case 0:    // 左方向に一つ進む
                            next_sx -= 1;
                            if (next_sx >= 0)
                            {
                                // 左端の時は左に始点がないので処理しない
                                al_swall = this.s_wall[next_sx, next_sy];
                                if (al_swall)
                                {
                                    // すでに壁が作られている場合はfalseにする
                                    is_leftdir = false;
                                }

                            }
                            break;

                        case 1:    // 下方向に一つ進む
                            next_sy -= 1;
                            if (next_sy >= 0)
                            {
                                //下端の時は下に始点がないので処理しない
                                al_swall = this.s_wall[next_sx, next_sy];
                                if (al_swall)
                                {
                                    // すでに壁が作られている場合はfalseにする
                                    is_downdir = false;
                                }

                            }
                            break;

                        case 2:    // 上方向に一つ進む
                            next_sy += 1;
                            if (next_sy <= s_wall_num.y - 1)
                            {
                                //上端の時は上に始点がないので処理しない
                                al_swall = this.s_wall[next_sx, next_sy];
                                if (al_swall)
                                {
                                    // すでに壁が作られている場合はfalseにする
                                    is_updir = false;
                                }

                            }
                            break;

                        case 3:    // 右方向に一つ進む
                            next_sx += 1;
                            if (next_sx <= s_wall_num.x - 1)
                            {
                                //右端の時は右に始点がないので処理しない
                                al_swall = this.s_wall[next_sx, next_sy];
                                if (al_swall)
                                {
                                    // すでに壁が作られている場合はfalseにする
                                    is_rightdir = false;
                                }
                            }
                            break;
                    }

                }// チェック終了

                // もしここまでの処理によってすべての方向に進めなくなった場合は処理を中断
                do
                {
                    if (is_leftdir)
                    {
                        can_make_wall = true;
                        break;  // 1つでも進める方向があるならループを脱出
                    }
                    if (is_rightdir)
                    {
                        can_make_wall = true;
                        break;  // 1つでも進める方向があるならループを脱出
                    }
                    if (is_updir)
                    {
                        can_make_wall = true;
                        break;  // 1つでも進める方向があるならループを脱出
                    }
                    if (is_downdir)
                    {
                        can_make_wall = true;
                        break;  // 1つでも進める方向があるならループを脱出
                    }
                    // もしすべての方向がfalseなら、これ以上この壁を伸ばすのを止める
                    can_make_wall = false;

                } while (false);

                if (!can_make_wall)
                {
                    // これ以上壁を作れない場合は壁伸ばしのループを終了する
                    can_next = false;
                    if (new_making)
                    {
                        // もし始点壁を生成したばかりなら、この壁は石像にする
                        statues_posi.Add(new Vector2Int(current_sx, current_sy));
                    }
                    //Debug.Log("壁伸ばし終了1");
                    goto label; // ラベルに飛ぶ

                }
                else
                {
                    // そうでないなら壁伸ばしを始める
                    can_next = true;
                }

                // ③進める方向があるならその中で進む方向を決める
                // 進行可能な方向を算出
                dir_rng_num = -1;
                if (is_leftdir)
                {
                    // 左ok
                    dir_rng_num++;
                    dir_rng[dir_rng_num] = 0;
                }
                if (is_updir)
                {
                    // 上ok
                    dir_rng_num++;
                    dir_rng[dir_rng_num] = 1;

                }
                if (is_downdir)
                {
                    // 下ok
                    dir_rng_num++;
                    dir_rng[dir_rng_num] = 2;

                }
                if (is_rightdir)
                {
                    // 右ok
                    dir_rng_num++;
                    dir_rng[dir_rng_num] = 3;

                }

                // 進む方向を保管
                dir = dir_rng[Random.Range(0, dir_rng_num + 1)];
                // デバッグ用switch
                switch (dir)
                {
                    case 0:
                        //Debug.Log("dir = 左に");

                        break;
                    case 1:
                        //Debug.Log("dir = 上に");

                        break;
                    case 2:
                        //Debug.Log("dir = 下に");

                        break;
                    case 3:
                        //Debug.Log("dir = 右に");

                        break;
                }

                // ④伸びる方向(dir)が決まったらその位置に壁を作る(g_wallとs_wallをtrueにする)
                switch (dir)
                {
                    case 0:    // 進む方向が左の場合
                        this.g_wall_x[current_sx, current_sy] = true;
                        // 次の始点に移動する
                        next_sx = current_sx - 1;
                        next_sy = current_sy;
                        if (next_sx < 0)
                        {
                            //左端で始点がない場合は移動せず、currentWall.can_nextをfalseにしておく
                            next_sx = current_sx;
                            can_next = false;
                        }
                        break;

                    case 1:    // 進む方向が上の場合
                        this.g_wall_y[current_sx, current_sy + 1] = true;
                        // 次の始点に移動する
                        next_sy = current_sy + 1;
                        next_sx = current_sx;
                        if (next_sy > this.s_wall_num.y - 1)
                        {
                            //上端で始点がない場合は移動せず、currentWall.can_nextをfalseにしておく
                            next_sy = current_sy;
                            can_next = false;
                        }

                        break;

                    case 2:    // 進む方向が下の場合
                        this.g_wall_y[current_sx, current_sy] = true;
                        // 次の始点に移動する
                        next_sy = current_sy - 1;
                        next_sx = current_sx;
                        if (next_sy < 0)
                        {
                            //下端で始点がない場合は移動せず、currentWall.can_nextをfalseにしておく
                            next_sy = current_sy;
                            can_next = false;
                        }

                        break;

                    case 3:    // 進む方向が右の場合
                        this.g_wall_x[current_sx + 1, current_sy] = true;
                        // 次の始点に移動する
                        next_sx = current_sx + 1;
                        next_sy = current_sy;
                        if (next_sx > this.s_wall_num.x - 1)
                        {
                            //右端で始点がない場合は移動せず、currentWall.can_nextをfalseにしておく
                            next_sx = current_sx;
                            can_next = false;
                        }
                        break;

                }
                // 移動先の始点をtrueにしておく
                //Debug.Log("next_xy = (" + next_sx + ", " + next_sy + ")");
                if (can_next)
                {
                    // 上手く次の始点に移動できた時だけ、その始点のboolをtrueにする
                    this.s_wall[next_sx, next_sy] = true;
                    // 取得した座標を配列から削除する
                    this.new_s_walls.Remove(new Vector2Int(next_sx, next_sy));
                    // new_makingのフラグを消しておく
                    new_making = false;
                    //Debug.Log("listの要素数：" + this.new_s_walls.Count);
                    current_sx = next_sx;
                    current_sy = next_sy;
                    //Debug.Log("壁伸ばし続行！");
                }
                else
                {
                    // 端の方に来てこれ以上壁を伸ばせない場合
                    //Debug.Log("壁伸ばし終了2");
                }
                // これで壁を伸ばす一通りの処理が終了、ここから②にもどり、さらに壁を伸ばせるならそのまま③～を処理する。

                // これ以上壁が伸ばせない場合は②の処理からここにジャンプし、そのままループを終了する
                label:; // ラベル設置
            } while (can_next); // can_next②～④の壁伸ばし処理ループ終了地点

            // s_wall_num = this.s_wall_num.x * this.s_wall_num.y = 121
        } while (this.new_s_walls.Count > 0);// 迷路生成処理全体のループ
        // 全ての始点壁が使われてしまっている場合は迷路生成のループ処理を終了する

        // 全ての迷路生成処理が終わったらこのメソッドも終了する
        Debug.Log("Finish!");
    }

    // MeiroCreateの全体処理
    public void MakeMeiro()
    {
        this.CalcWallNum();
        this.InitializeWallNum();

        // ランダムな迷路を自動生成する。
        this.RamdomMeiroCreate();

    }

}
