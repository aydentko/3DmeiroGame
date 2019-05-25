using UnityEngine;
using UnityEngine.UI;

public class item : MonoBehaviour
{
    //public float speed; // 動く速さ
    public Text scoreText; // スコアの UI
    public Text winText; // リザルトの UI

    int map_level = 1;

    private CharacterController rb; // Rididbody=>CharacterController
    StaticScriptsCon ssc;
    private int score; // スコア

    void Start()
    {        
        // Rigidbody を取得
        rb = GetComponent<CharacterController>();

        // UI を初期化
        score = 0;
        SetCountText();
        map_level = StaticScriptsCon.MapLevel();
        switch (map_level)
        {
            case 1:
                winText.text = "たからばこ を５個以上あつめて 外へ出よう！";
                break;
            case 2:
                winText.text = "たからばこ を７個以上あつめて 外へ出よう！";
                break;
            case 3:
                winText.text = "たからばこ を１０個以上あつめて 外へ出よう！";
                break;
        }
    }

    //void Update()
    //{
      //  // カーソルキーの入力を取得
       // var moveHorizontal = Input.GetAxis("Horizontal");
        //var moveVertical = Input.GetAxis("Vertical");

        // カーソルキーの入力に合わせて移動方向を設定
//        var movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Ridigbody に力を与えて玉を動かす
  //      rb.AddForce(movement * speed);
   // }

    // 玉が他のオブジェクトにぶつかった時に呼び出される
    void OnTriggerEnter(Collider other)
    //void OnControllerColliderHit(ControllerColliderHit other)
    {
        // ぶつかったオブジェクトが収集アイテムだった場合
        if (other.gameObject.CompareTag("item"))
        {
            //Debug.Log(other.gameObject);
            // その収集アイテムを非表示にします
            other.gameObject.SetActive(false);

            // スコアを加算します
            score = score + 1;

            // UI の表示を更新します
            SetCountText();
        }
    }

    // UI の表示を更新する
    void SetCountText()
    {
        // スコアの表示を更新
        scoreText.text = "たからばこ: " + score.ToString();

        // すべての収集アイテムを獲得した場合
        switch (map_level)
        {
            case 1:
                if (score >= 5)
                {
                    // リザルトの表示を更新
                    winText.text = "でぐちのキーは 「 M 」だよ";
                }
                break;
            case 2:
                if (score >= 7)
                {
                    // リザルトの表示を更新
                    winText.text = "でぐちのキーは 「 M 」だよ";
                }
                break;
            case 3:
                if (score >= 10)
                {
                    // リザルトの表示を更新
                    winText.text = "でぐちのキーは 「 M 」だよ";
                }
                break;
        }
    }
}