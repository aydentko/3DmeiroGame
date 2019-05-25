using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemscript: MonoBehaviour {

    public static int itemcounter = 0;

    GameObject[] desks = new GameObject[3];

    GameObject Item_text;
    
    //public GameObject item;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Item_text = GameObject.Find("ItemGetText");
        int dd;
        if (other.gameObject.tag == "Player")
        {
            //ここからiFからiFへ移動するときの机をランダムでどかすプログラム
            dd = Random.Range(1, 4); // どの机を破壊する（出口を作る）かランダムで決める
            GameObject desk = GameObject.Find("deskwall" + dd) as GameObject;
            Destroy(desk); // 机の破壊
            print(dd);

            //Invoke("destroy", 0.1f);
            Destroy(this.gameObject);
            itemcounter++;
            print(itemcounter);

            Item_text.GetComponent<Text>().text = "GET！";
        }
    }

    //void ItemDestroy()
    //{
        //GameObject item = GameObject.Find("originalitem(Clone)");

        //Destroy(this);
    //}

    public static int GetItemCounter()
    {
        return itemcounter;
    }

    public static void IniItemCounter()
    {
        itemcounter = 0;
    }

}
