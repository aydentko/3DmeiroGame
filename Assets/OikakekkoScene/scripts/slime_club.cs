using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime_club : MonoBehaviour {

    public GameObject Human;
    public GameObject Slime;
    int i, j;
    float hu_x, hu_z;
    float sli_x, sli_z;
    int hux, huz, slix, sliz;
    int[] res = new int[4];
    int[] res_xz = new int[8];
    const int iiy = 114514;
    int[,] walls = new int[9,8];
    int[,] cost = new int[9, 8];
    bool move = false;
    bool alt = false;
    int move_count = 0;
    // Use this for initialization
    void Start () {
        //walls[][]に壁を記入
        for (i = 0; i < 9; i++)
        {
            for (j = 0; j < 8; j++)
            {
                walls[i, j] = 1;
                switch (i)
                {
                    case 0:
                        switch (j)
                        {
                            case 0:
                                walls[i, j] *= 2*3;
                                break;
                            case 1:
                                walls[i, j] *= 3*7;
                                break;
                            case 2:
                                walls[i, j] *= 3*7;
                                break;
                            case 3:
                                walls[i, j] *= 3;
                                break;
                            case 4:
                                walls[i, j] *= 3;
                                break;
                            case 5:
                                walls[i, j]*= 3*7;
                                break;
                            case 6:
                                walls[i, j]*= 3*7;
                                break;
                            case 7:
                                walls[i, j]*= 3*5;
                                break;
                        }
                        break;
                    case 1:
                        switch (j)
                        {
                            case 0:
                                walls[i, j]*= 2*5;
                                break;
                            case 1:
                                walls[i, j]*= 2*3;
                                break;
                            case 2:
                                walls[i, j] *= 3;
                                break;
                            case 3:
                                walls[i, j] *= 7;
                                break;
                            case 4:
                                break;
                            case 5:
                                walls[i, j] *= 3;
                                break;
                            case 6:
                                walls[i, j]*= 3*5;
                                break;
                            case 7:
                                walls[i, j]*= 2*5;
                                break;
                        }
                        break;
                    case 2:
                        switch (j)
                        {
                            case 0:
                                walls[i, j]*= 2*5;
                                break;
                            case 1:
                                walls[i, j] *= 2;
                                break;
                            case 2:
                                break;
                            case 3:
                                walls[i, j] *= 3;
                                break;
                            case 4:
                                walls[i, j] *= 7;
                                break;
                            case 5:
                                break;
                            case 6:
                                walls[i, j] *= 5;
                                break;
                            case 7:
                                walls[i, j]*= 2*5;
                                break;
                        }
                        break;
                    case 3:
                        switch (j)
                        {
                            case 0:
                                walls[i, j] *= 2;
                                break;
                            case 1:
                                break;
                            case 2:
                                walls[i, j] *= 5;
                                break;
                            case 3:
                                walls[i, j] *= 2;
                                break;
                            case 4:
                                walls[i, j] *= 3;
                                break;
                            case 5:
                                walls[i, j] *= 5;
                                break;
                            case 6:
                                walls[i, j] *= 2;
                                break;
                            case 7:
                                walls[i, j] *= 5;
                                break;
                        }
                        break;
                    case 4:
                        switch (j)
                        {
                            case 0:
                                walls[i, j] *= 2;
                                break;
                            case 1:
                                walls[i, j] *= 5;
                                break;
                            case 2:
                                walls[i, j] *= 2;
                                break;
                            case 3:
                                walls[i, j] *= 7;
                                break;
                            case 4:
                                walls[i, j] *= 5;
                                break;
                            case 5:
                                walls[i, j] *= 2;
                                break;
                            case 6:
                                break;
                            case 7:
                                walls[i, j] *= 5;
                                break;
                        }
                        break;
                    case 5:
                        switch (j)
                        {
                            case 0:
                                walls[i, j]*= 2*5;
                                break;
                            case 1:
                                walls[i, j] *= 2;
                                break;
                            case 2:
                                break;
                            case 3:
                                walls[i, j] *= 3;
                                break;
                            case 4:
                                walls[i, j] *= 7;
                                break;
                            case 5:
                                break;
                            case 6:
                                walls[i, j] *= 5;

                                break;
                            case 7:
                                walls[i, j] *= 2*5;
                                break;
                        }
                        break;
                    case 6:
                        switch (j)
                        {
                            case 0:
                                walls[i, j] *= 2*5;
                                break;
                            case 1:
                                walls[i, j]*= 2*7;
                                break;
                            case 2:
                                walls[i, j] *= 7;
                                break;
                            case 3:
                                break;
                            case 4:
                                walls[i, j] *= 3;
                                break;
                            case 5:
                                walls[i, j] *= 7;
                                break;
                            case 6:
                                walls[i, j]*= 5*7;
                                break;
                            case 7:
                                walls[i, j]*= 2*5;
                                break;
                        }
                        break;
                    case 7:
                        switch (j)
                        {
                            case 0:
                                walls[i, j]*= 2*7;
                                break;
                            case 1:
                                walls[i, j]*= 3*7;
                                break;
                            case 2:
                                walls[i, j]*= 3*7;
                                break;
                            case 3:
                                walls[i, j] *= 7;
                                break;
                            case 4:
                                walls[i, j] *= 7;
                                break;
                            case 5:
                                walls[i, j]*= 3*7;
                                break;
                            case 6:
                                walls[i, j]*= 3*7;
                                break;
                            case 7:
                                walls[i, j] *= 5;
                                break;
                        }
                        break;
                    case 8:
                        switch (j)
                        {
                            case 0:
                                walls[i, j] *= 2 * 3 * 7;
                                break;
                            case 1:
                                walls[i, j] *= 3*7;
                                break;
                            case 2:
                                walls[i, j] *= 3*7;
                                break;
                            case 3:
                                walls[i, j] *= 3*7;
                                break;
                            case 4:
                                walls[i, j] *= 3*7;
                                break;
                            case 5:
                                walls[i, j] *= 3*7;
                                break;
                            case 6:
                                walls[i, j] *= 3*7;
                                break;
                            case 7:
                                walls[i, j] *= 5*7;
                                break;
                        }
                        break;
                }
            }
        }
    }

// Update is called once per frame
    void Update() {
        int count = 0;
        int res_min = 114514, res_max = 0;

        //人間とスライムの位置を取得してhu,sliに代入
        Vector3 hu = Human.transform.position;
        Vector3 sli = Slime.transform.position;

        //hu座標を今いる床の真ん中にする
        hu_x = hu.x - ((int)hu.x % 8) +4.0f;
        hu_z = hu.z - ((int)hu.z % 8) + 4.0f;

        //sliも
        sli_x = sli.x - ((int)sli.x % 8) + 4.0f;
        sli_z = sli.z - ((int)sli.z % 8) + 4.0f;


        //座標を配列用の整数値に変換
        hux = ((int)hu_x - 4) / 8;
        huz = ((int)hu_z - 4) / 8;
        slix = ((int)sli_x - 4) / 8;
        sliz = ((int)sli_z - 4) / 8;
        if ((hux != slix || huz != sliz) && (slix != 8 || sliz != 0)&& move==false) {
            
                for (i = 0; i < 9; i++)
                {
                    for (j = 0; j < 8; j++)
                    {
                        cost[i, j] = iiy;
                    }
                }

                cost[hux, huz] = 0;
            do
            {
                alt = false;
                for (i = 0; i < 9; i++)
                {
                    for (j = 0; j < 8; j++)
                    {

                        if (walls[i, j] % 7 != 0)
                        {
                            if (cost[i, j] > cost[i + 1, j] + 1)
                            {
                                cost[i, j] = cost[i + 1, j] + 1;
                                if (alt == false) alt = true;
                            }
                            if (cost[i, j] + 1 < cost[i + 1, j])
                            {
                                cost[i + 1, j] = cost[i, j] + 1;
                                if (alt == false) alt = true;
                            }
                        }
                        if (walls[i, j] % 5 != 0)
                        {
                            if (cost[i, j] > cost[i, j + 1] + 1)
                            {
                                cost[i, j] = cost[i, j + 1] + 1;
                                if (alt == false) alt = true;
                            }
                            if (cost[i, j] + 1 < cost[i, j + 1])
                            {
                                cost[i, j + 1] = cost[i, j] + 1;
                                if (alt == false) alt = true;
                            }
                        }
                    }
                }
            } while (alt == true);
            if (walls[slix, sliz] % 2 != 0)
            {
                res[count] = cost[slix, sliz - 1];
                res_xz[count * 2] = slix;
                res_xz[count * 2 + 1] = sliz - 1;
                count += 1;
            }
            if (walls[slix, sliz] % 3 != 0)
            {
                res[count] = cost[slix - 1, sliz];
                res_xz[count * 2] = slix - 1;
                res_xz[count * 2 + 1] = sliz;
                count += 1;
            }
            if (walls[slix, sliz] % 5 != 0)
            {
                res[count] = cost[slix, sliz + 1];
                res_xz[count * 2] = slix;
                res_xz[count * 2 + 1] = sliz + 1;
                count += 1;
            }
            if (walls[slix, sliz] % 7 != 0)
            {
                res[count] = cost[slix + 1, sliz];
                res_xz[count * 2] = slix + 1;
                res_xz[count * 2 + 1] = sliz;
                count += 1;
            }

            for (i = 0; i < count; i++)
            {
                if (res[i] < res_min) res_min = res[i];
                if (res[i] > res_max) res_max = res[i];
            }

            int randy = Random.Range(0, 20);

            for (i = randy % count; ; i++)
            {
                if (i == count) i = 0;
                if (res[i] == res_max) break;
            }

            //i*2,i+2+1に移動
            if (res_min < 3)
            {
                if (slix == res_xz[i * 2] && sliz > res_xz[i * 2 + 1]) Slime.transform.rotation = Quaternion.Euler(0, 180.0f, 0);
                else if (slix > res_xz[i * 2] && sliz == res_xz[i * 2 + 1]) Slime.transform.rotation = Quaternion.Euler(0, 270.0f, 0);
                else if (slix == res_xz[i * 2] && sliz < res_xz[i * 2 + 1]) Slime.transform.rotation = Quaternion.Euler(0, 0, 0);
                else if (slix < res_xz[i * 2] && sliz == res_xz[i * 2 + 1]) Slime.transform.rotation = Quaternion.Euler(0, 90.0f, 0);
                move = true;
                move_count = 0;
            }
        }

        if (move == true)
        {
            Slime.transform.Translate(0, 0, 1.0f);
            move_count+=1;
            if (move_count >= 8) move = false;
        }
    }
}
