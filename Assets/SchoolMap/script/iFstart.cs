using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iFstart : MonoBehaviour {

    int ifspawn;
    private GameObject player;
    public static int deskdestroy;
    public GameObject originalitem = null;
    private int itemcounter = 0;
    public GameObject originalwall;

    GameObject[] desks_pos = new GameObject[3];
    GameObject[] desks = new GameObject[3];


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start () {
        // 机の壁を作る
        for (int i = 0; i < 3; i++)
        {
            // 机の壁を設置するポジションを取得するためのオブジェクトを取得
            int n = i + 1;
            desks_pos[i] = GameObject.Find("deskwall_pos" + n);
            // 机の壁を設置するポジションを取得
            Vector3 wallposi = new Vector3(0, 0, 0);
            wallposi = desks_pos[i].transform.position;
            // クローンを作成
            GameObject cloneObject = Instantiate(originalwall, wallposi, Quaternion.identity);
            int name_n = i + 1;
            cloneObject.name = "deskwall" + name_n;
            // 配列に保管
            desks[i] = cloneObject;

        }

        //この辺はシーン移動したときに主人公を適当な場所に移動させるためのやつ

        ifspawn = warptoptoi.getifspawn();

        if (ifspawn == 1){
            
            player.transform.position = GameObject.Find("StartPoint1").transform.position;

            ifspawn = 0;
        }

        else if(ifspawn == 2){

            player.transform.position = GameObject.Find("StartPoint2").transform.position;

            ifspawn = 0;
        }
        else
        {
            player.transform.position = GameObject.Find("StartPoint3").transform.position;

            ifspawn = 0;
        }

        // ここからはアイテムを設置する処理

        itemscript.IniItemCounter(); // 現在の獲得アイテム数を初期化
        //アイテムをランダムに配置するためのスクリプト
        int itemput = Random.Range(1, 7);
        print(itemput);
        switch (itemput)
        {
            case 1:
                //GameObject originalitem = GameObject.Find("originalitem");
                GameObject item = Instantiate(originalitem, new Vector3(3f, 4f, 56f), Quaternion.identity);
                item.name = "item";
                break;

            case 2:
                //GameObject originalitem = GameObject.Find("originalitem");
                item = Instantiate(originalitem, new Vector3(9f, 4f, 45f), Quaternion.identity);
                item.name = "item";
                break;

            case 3:
                //GameObject originalitem = GameObject.Find("originalitem");
                item = Instantiate(originalitem, new Vector3(3f, 4f, 36f), Quaternion.identity);
                item.name = "item";
                break;

            case 4:
                //GameObject originalitem = GameObject.Find("originalitem");
                item = Instantiate(originalitem, new Vector3(9f, 4f, 26f), Quaternion.identity);
                item.name = "item";
                break;

            case 5:
                //GameObject originalitem = GameObject.Find("originalitem");
                item = Instantiate(originalitem, new Vector3(6f, 4f, 12f), Quaternion.identity);
                item.name = "item";
                break;

            case 6:
                //GameObject originalitem = GameObject.Find("originalitem");
                item = Instantiate(originalitem, new Vector3(3f, 4f, 2f), Quaternion.identity);
                item.name = "item";
                break;
        }

    }

    // Update is called once per frame
    void Update () {

    }
}
