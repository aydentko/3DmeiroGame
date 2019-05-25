using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class PlayerControl : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animCon; //  アニメーションするための変数。たぶん使わない
    private Vector3 moveDirection = Vector3.zero; //  移動する方向とベクトル（動く力、速度）の変数（最初は初期化しておく）

    public float idoSpeed = 100.0f;         // 移動速度
    public float kaitenSpeed = 1200.0f;   // プレイヤーの回転速度
    public float gravity = 20.0F;   //重力の強さ
    public float jumpPower = 6.0F; //ジャンプのスピード

    private GameObject p_head;
    private int VerticalButton = 0; // 前後移動ボタン
    private int HorizontalButton = 0; // 左右移動ボタン

    Vector3 vector = Vector3.zero;
    float head_angle = 0.0f;
    float add_angle = 0.0f;
    GameObject player_cam;
    GameObject third_cam;
    GameObject bird_cam;
    Vector3Int player_dir;

    private bool rt_move = false;

    private bool isBackRotated = false;
    private Vector3 BackRotation = Vector3.zero;

    public Vector2 rotationSpeed; // カメラの回転速度
    public Vector2 limitAngle; // カメラの移動限度(50なら-50～50の範囲)
                               //private Vector2 lastMousePosition; // マウス座標を格納する変数（左下が原点）
                               //private Vector2 nowMousePosition; // 現在のマウス座標
                               //private Vector2 mouseMoveDis; // カーソルの移動量
                               // private Vector2 MaxScreenSize;
                               //private Vector2 newAngle = new Vector2(0, 0); // カメラの角度を格納する変数
                               //private bool angleUpdate = false;

    // マウスカーソルを制限するための関数
    //  [DllImport("user32.dll")]
    //   public static extern bool SetCursorPos(int x, int y);

    // Use this for initialization
    void Start()
    {
        this.player_cam = GameObject.Find("PlayerCamera");
        this.third_cam = GameObject.Find("ThirdEyesCamera");
        this.bird_cam = GameObject.Find("BirdEyesCamera");

        this.third_cam.SetActive(false);
        this.bird_cam.SetActive(false);
        this.player_cam.SetActive(true);

        characterController = GetComponent<CharacterController>();
        animCon = GetComponent<Animator>(); // アニメーターのコンポーネントを参照する
        p_head = GameObject.FindGameObjectWithTag("P_Head") as GameObject ;

    }

    // Update is called once per frame
    void Update()
    {
        // ▼▼▼移動処理▼▼▼
        if(Input.GetAxis("Vertical") > 0 || Input.GetAxis("L_Stick_V") > 0 || Input.GetAxis("D_Pad_V") > 0) // この時点では歩く場合と走る場合どちらも入れる
        {
            this.VerticalButton = 1;
            //animCon.SetInteger("LR", 0); // LRパラメータに代入にする

        }
        else if(Input.GetAxis("Vertical") < 0 || Input.GetAxis("L_Stick_V") < 0 || Input.GetAxis("D_Pad_V") < 0)
        {
            this.VerticalButton = -1;
            //animCon.SetInteger("LR", 0); // LRパラメータに代入にする

        }
        else
        {
            this.VerticalButton = 0;
            //animCon.SetInteger("LR", 0); // LRパラメータに代入にする

        }
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("L_Stick_H") > 0 || Input.GetAxis("D_Pad_H") > 0)
        {
            this.HorizontalButton = 1;
            //animCon.SetInteger("LR", 1); // LRパラメータに代入にする

        }
        else if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("L_Stick_H") < 0 || Input.GetAxis("D_Pad_H") < 0)
        {
            this.HorizontalButton = -1;
            //animCon.SetInteger("LR", -1); // LRパラメータに代入にする

        }
        else
        {
            this.HorizontalButton = 0;
            //animCon.SetInteger("LR", 0); // LRパラメータに代入にする

        }

        if (this.VerticalButton == 0 && this.HorizontalButton == 0)  //  テンキーや3Dスティックの入力（GetAxis）がゼロの時の動作
        {
            // 水平移動方向を0に
            this.player_dir.x = 0;
            this.player_dir.z = 0;
            animCon.SetFloat("Speed", 0); // Speedパラメータを0にする
            //animCon.SetInteger("LR", 0); // LRパラメータに代入にする

        }
        else //  テンキーや3Dスティックの入力（GetAxis）がゼロではない時の動作
        {
            // 移動方向を代入
            this.player_dir.z = this.VerticalButton;
            this.player_dir.x = this.HorizontalButton;
            if(this.player_dir.z !=0)
            {
                animCon.SetFloat("Speed", this.player_dir.z); // Speedパラメータに前後の移動距離を代入する

            }
            else
            {
                animCon.SetFloat("Speed", this.player_dir.x); // Speedパラメータに前後の移動距離を代入する
            }
            //animCon.SetInteger("LR", this.player_dir.x); // LRパラメータに代入にする
            //if()
        }

        // ▼▼▼方向転換処理▼▼▼
        this.ChangeAngle();  //  向きを変える動作の処理を実行する（後述）


        // ▼▼▼落下処理▼▼▼
        if (characterController.isGrounded)    //CharacterControllerの付いているこのオブジェクトが接地している場合の処理
        {
            //animCon.SetBool("Jump", Input.GetKeyDown("space") || Input.GetButtonDown("Jump"));  //  キーorボタンを押したらジャンプアニメを実行
            moveDirection.y = 0f;  //Y方向への速度をゼロにする
            moveDirection = this.player_dir;
            moveDirection *= idoSpeed;  //移動スピードを向いている方向に与える

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) //Spaceキーorジャンプボタンが押されている場合
            {
                //Debug.Log("jump!");
                moveDirection.y = jumpPower; //Y方向への速度に「ジャンプパワー」の変数を代入する
            }
            else //Spaceキーorジャンプボタンが押されていない場合
            {
                moveDirection.y -= gravity * Time.deltaTime; //マイナスのY方向（下向き）に重力を与える（これを入れるとなぜかジャンプが安定する…）
            }

        }
        else  //CharacterControllerの付いているこのオブジェクトが接地していない場合の処理
        {
            moveDirection.y -= gravity * Time.deltaTime;  //マイナスのY方向（下向き）に重力を与える
        }

        // 最終的な移動処理
        this.PlayerVectors();
        this.vector.y = this.moveDirection.y;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetAxis("L_Stick_H") != 0 || Input.GetAxis("L_Stick_V") != 0)
        {
            // Shiftキーが押されているか、ゲームパッドの左スティックが使われているときは走る
            characterController.Move(this.vector * 2 * Time.deltaTime);  //CharacterControllerの付いているこのオブジェクトを移動させる処理
            animCon.SetBool("Run", true); // Runパラメータをtrueにする

        }
        else
        {
            characterController.Move(this.vector * Time.deltaTime);  //CharacterControllerの付いているこのオブジェクトを移動させる処理
            animCon.SetBool("Run", false); // Runパラメータをfalseにする

        }

        // ▼▼▼アクション処理▼▼▼
        //animCon.SetBool("Action", Input.GetKeyDown("x") || Input.GetButtonDown("Action1"));  //  キーorボタンを押したらアクションアニメを実行
        //animCon.SetBool("Action2", Input.GetKeyDown("z") || Input.GetButtonDown("Action2"));  //  キーorボタンを押したらアクション2アニメを実行
        //animCon.SetBool("Action3", Input.GetKeyDown("c") || Input.GetButtonDown("Action3"));  //  キーorボタンを押したらアクション3アニメを実行

        // ▼▼▼視点切り替え処理▼▼▼
        if (Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("Joy_Button_0"))
        {
            this.ChangeCamera();
        }
    }


    // ■向きを変える動作の処理
    void MukiWoKaeru(Vector3 mukitaiHoukou)
    {
        Quaternion q = Quaternion.LookRotation(mukitaiHoukou);          // 向きたい方角をQuaternion型に直す
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, kaitenSpeed * Time.deltaTime);   // 向きを q に向けてじわ～っと変化させる.
    }

    void ChangeAngle()
    {
        // 首の回転処理
        if (!isBackRotated)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("R_Stick_H") > 0)  //右回転
            {
                this.transform.Rotate(0.0f, this.rotationSpeed.x * Time.deltaTime, 0.0f);

            }else  if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("R_Stick_H") < 0)  //左回転
            {
                this.transform.Rotate(0.0f, -this.rotationSpeed.x * Time.deltaTime, 0.0f);

            }
            else
            {
                this.transform.Rotate(0.0f, 0, 0.0f);
            }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("R_Stick_V") > 0)  //上を向く
            {
                this.add_angle = -this.rotationSpeed.y * Time.deltaTime;
                if (head_angle < -40.0f)
                {  // 首の角度がおかしくない範囲で
                    this.add_angle = 0.0f;
                }
                rt_move = true;

            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("R_Stick_V") < 0)  //下を向く
            {
                this.add_angle = this.rotationSpeed.y* Time.deltaTime;
                if (head_angle > 40.0f)
                {  // 首の角度がおかしくない範囲で
                    this.add_angle = 0.0f;
                }
                rt_move = true;

            }
            else
            {
                this.add_angle = 0.0f;
                rt_move = false;
            }

            if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("Back"))
            {
                this.BackRotation = new Vector3(0f, 180f, 0f) + this.transform.eulerAngles;
                this.isBackRotated = true;
            }

            if (rt_move)
            {
                this.head_angle += this.add_angle;
                //p_head.transform.rotation = Quaternion.Euler(head_angle, 0.0f, 0.0f);
                p_head.transform.Rotate(add_angle, 0, 0);
                //Debug.Log("顔の角度：" + p_head.transform.rotation.x);
            }

        }
        else if (this.isBackRotated)
        {
            // 後ろを振り向く処理
            if (Mathf.DeltaAngle(this.transform.eulerAngles.y, this.BackRotation.y) < -0.1f)
            {
                this.transform.Rotate(new Vector3(0f, -5f, 0f));
            }
            else if (Mathf.DeltaAngle(this.transform.eulerAngles.y, this.BackRotation.y) > 0.1f)
            {
                this.transform.Rotate(new Vector3(0f, 5f, 0f));

            }
            else
            {
                this.isBackRotated = false;
            }
        }


    }

    void PlayerVectors()
    {
        // 移動方向の計算処理
        float euler_y = this.transform.eulerAngles.y;// 現在のプレイヤーのeulerAngleのyを保管
        Vector3 dir = new Vector3(Mathf.Sin(euler_y * Mathf.Deg2Rad), 0.0f, Mathf.Cos(euler_y * Mathf.Deg2Rad));
        switch (player_dir.z)
        {
            case 0:
                // 前後移動しない場合
                this.vector.x = 0;
                this.vector.z = 0;
                break;
            case 1:
                // 前進する場合
                this.vector.x = dir.x;
                this.vector.z = dir.z;
                break;
            case -1:
                // 後退する場合
                this.vector.x = -dir.x;
                this.vector.z = -dir.z;
                break;
        }
        switch (player_dir.x)
        {
            case 0:
                // 左右移動しない場合
                this.vector.x += 0;
                this.vector.z += 0;
                break;
            case 1:
                // 右に移動する場合
                this.vector.x += dir.z;
                this.vector.z += -dir.x;
                break;
            case -1:
                // 左に移動する場合
                this.vector.x += -dir.z;
                this.vector.z += dir.x;
                break;
        }
        //移動速度を加える
        this.vector.x *= idoSpeed;
        this.vector.z *= idoSpeed;

    }

    void ChangeCamera()
    {
        // カメラの切り替え
        if (player_cam.activeSelf)
        {
            this.third_cam.SetActive(true);
            this.player_cam.SetActive(false);
            this.bird_cam.SetActive(false);
            this.p_head = this.third_cam;
        }
        else if (third_cam.activeSelf)
        {
            this.third_cam.SetActive(false);
            this.player_cam.SetActive(false);
            this.bird_cam.SetActive(true);
            this.p_head = this.bird_cam;
        }
        else
        {
            this.third_cam.SetActive(false);
            this.player_cam.SetActive(true);
            this.bird_cam.SetActive(false);
            this.p_head = this.player_cam;

        }

    }

}
