using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class warppoint3 : MonoBehaviour {

    public GameObject player;
    public static int dd;
    public GameObject originalwall;
    //public GameObject originaldesk;
    int itemcounter;
    static int pre_itemcounter = 0;
    int itemput;
    public GameObject originalitem;
    //public static int firstfspawn3 = 0;
    public static int firstfspawn = 0;
    public int StairsNum = 1;
    public GameObject Item_text = null;

    private int map_level = 1;
    private int MaxItems = 3;

    GameObject[] desks_pos = new GameObject[3];
    GameObject[] desks = new GameObject[3];

    private void Awake()
    {
        //firstfspawn = 1;
        player = GameObject.FindGameObjectWithTag("Player");


    }


    void OnTriggerEnter(Collider other)
    {
        firstfspawn = this.StairsNum; // プレイヤーが降りた階段の番号を保管
        //GameObject desk1 = GameObject.Find("deskwall1");
        //GameObject desk2 = GameObject.Find("deskwall2");
        //GameObject desk3 = GameObject.Find("deskwall3");
        //GameObject originalwall = GameObject.Find("originalwall");
        map_level = StaticScriptsCon.MapLevel();
        switch (map_level)
        {
            case 1:
                MaxItems = 1;
                break;
            case 2:
                MaxItems = 2;
                break;
            case 3 :
                MaxItems = 3;
                break;
        }

        // 現在のアイテムの獲得数を取得
        itemcounter = itemscript.GetItemCounter();
        if (itemcounter >= MaxItems)
        {

            //1階に行くときのスクリプト

            firstfspawn = 1;

            SceneManager.LoadScene("1F");
            // 初期化しておく
            itemscript.IniItemCounter();
            pre_itemcounter = 0;
        }
        else
        {
            // 2階以上にいる間

            //アイテムをランダムに配置するためのスクリプト
            if (itemcounter != pre_itemcounter)
            {
                // ちゃんとアイテムを拾ってから階段を下りた場合は、新しいアイテムを設置する
                itemput = Random.Range(1, 7);
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
            // 獲得したアイテムの個数を別の変数に保管しておく
            pre_itemcounter = itemcounter;

            // プレイヤーの移動
            player.transform.position = GameObject.Find("StartPoint" + firstfspawn).transform.position;
            player.transform.Rotate(0, -180, 0);

            // アイテムをゲットしたかのテキストを消す
            Item_text.GetComponent<Text>().text = "";

            // ここからは机の壁を設置する処理

            // 設置する壁の配置を取得
            for (int i = 0; i < 3; i++)
            {
                int n = i + 1;
                desks_pos[i] = GameObject.Find("deskwall_pos" + n);
            }
            //現在設置されている机のオブジェクトを取得
            for(int i = 0; i < 3; i++)
            {
                int  n = i + 1;
                desks[i] = GameObject.Find("deskwall" + n);
            }

            // 一度すべての机を撤去
            foreach (var item in desks)
            {
                Destroy(item);
            }

            for (int i = 0; i<3; i++)
            {
                //失った机のかべをもとに戻すスクリプト
                //GameObject cloneObject = Instantiate(originalwall, new Vector3(5, 4, 64), Quaternion.identity);
                Vector3 wallposi = new Vector3(0,0,0);
                wallposi = desks_pos[i].transform.position; // 机の座標を取得
                GameObject cloneObject = Instantiate(originalwall, wallposi, Quaternion.identity); // 机のオブジェクトを生成
                int name_n = i + 1;
                cloneObject.name = "deskwall" + name_n; // オブジェクトに名前を付ける
                desks[i] = cloneObject; //配列に保管

            }
        }
    }

    public static int getfirstfspawn()
    {
        return firstfspawn;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
