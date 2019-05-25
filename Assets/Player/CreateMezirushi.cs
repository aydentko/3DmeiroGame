using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMezirushi : MonoBehaviour {

    public GameObject MezirushiPrefab = null;
    public int mark_Max_num = 1;
    private GameObject[] mezirushi_array = new GameObject[10];
    private int mezirushi_num = 0;
    private GameObject M_Point;
    private MezirushiControl Mezirushi_Triger;
    private GameObject[] M_Targets;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        // Playerオブジェクトの中の"mezirushiPoint"オブジェクトを読み込む
        this.M_Point = GameObject.FindGameObjectWithTag("M_point");
        //"mezirushiPoint"のコンポーネントにある"MezirushiControl"スクリプトを読み込む。
        this.Mezirushi_Triger = this.M_Point.GetComponent<MezirushiControl>();
        float m_x = M_Point.transform.position.x;
        float m_z = M_Point.transform.position.z;
        float m_y = M_Point.transform.position.y;
        float m_ry = this.transform.rotation.y;
        // QキーかゲームパッドのAが押されたら目の前に目印を置く
        if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("On_Marker"))
        {
            this.CreateObject(m_x, m_y, m_z, m_ry);
        }
        // EキーかゲームパッドのBが押されたら目の前の目印を消す
        if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("Remove_Marker"))
        {
            this.DeleteObject();

        }




    }

    void createMarkWindow() {
        // 目印の背景画像を表示する。
        for (int i = 0; i < mark_Max_num; i++)
        {

        }
    }

    void CreateObject(float x, float y, float z, float ry) {
        if (this.mezirushi_num < 10)
        {
            GameObject M_Block;
            M_Block = Instantiate(MezirushiPrefab) as GameObject;
            M_Block.transform.position = new Vector3(x, y, z);
            M_Block.transform.Rotate(0, ry, 0);
            this.mezirushi_array[this.mezirushi_num] = M_Block;
            this.mezirushi_num++;

        }
        else
        {
            // 最大個数を超えていた場合は（今は）作らない
            this.mezirushi_num = 10; //念のために最大値に戻しておく
        }
    }

    void DeleteObject() {
        //MezirushiControlのターゲットオブジェクト配列を格納
        this.M_Targets = Mezirushi_Triger.TargetMezirushi;

        // 範囲に目印が1個以上あるときのみ
        if (this.M_Targets.Length > 0) {

            List<GameObject> M_list = new List<GameObject>(this.mezirushi_array);  //Array型からList型を作成
            List<GameObject> T_list = new List<GameObject>(this.M_Targets);  //Array型からList型を
            foreach (GameObject item in T_list)
            {   
                // M_listのなかにT_listの要素が含まれている場合はその要素をM_listから削除。
                M_list.Remove(item);
            }
            this.mezirushi_array = M_list.ToArray();//配列の更新
            // 次に、オブジェクトを消去
            foreach(GameObject item in M_Targets)
            {
                Debug.Log("DeleteMezirushi");
                Destroy(item, 0f);//目印オブジェクトの削除
                // ターゲットの方の配列も更新しておく
                T_list.Remove(item);
                M_Targets = T_list.ToArray();
                this.mezirushi_num--;
            }

        }

    }


}
