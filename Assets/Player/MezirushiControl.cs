using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MezirushiControl : MonoBehaviour {
    public GameObject[] TargetMezirushi;
    public Material PointedMaterial;
    public Material NotPointedMaterial;

    private List<GameObject> List_Target = new List<GameObject>();
    private MeshRenderer MezirushiColor;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Mezirushi") && !this.List_Target.Contains(other.gameObject))
        {
            // ポイントに入った目印の色を変える
            this.MezirushiColor = other.gameObject.GetComponent<MeshRenderer>();
            this.MezirushiColor.material = this.PointedMaterial;

            // 目印ポイントの中にある目印オブジェクトを格納
            //Debug.Log("mezirushi");
            // リストの更新
            this.List_Target = new List<GameObject>(this.TargetMezirushi);
            this.List_Target.RemoveAll(item => item == null); // 空要素の削除
            // リストへ追加
            this.List_Target.Add(other.gameObject);
            this.TargetMezirushi = this.List_Target.ToArray(); // 配列の更新
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Mezirushi") && this.List_Target.Contains(other.gameObject))
        {
            // ポイントから外れた目印オブジェクトの色を変える
            this.MezirushiColor = other.gameObject.GetComponent<MeshRenderer>();
            this.MezirushiColor.material = this.NotPointedMaterial;

            // 目印ポイントから外れたオブジェクトは配列から削除する
            // リストを更新
            this.List_Target = new List<GameObject>(this.TargetMezirushi);
            // リストから削除
            this.List_Target.Remove(other.gameObject);
            this.List_Target.RemoveAll(item => item == null); // 空要素の削除
            this.TargetMezirushi = this.List_Target.ToArray(); // 配列の更新

        }
    }

}
