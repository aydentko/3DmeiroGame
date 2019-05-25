using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTriger : MonoBehaviour {
    public bool game_over = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // タイマーを止め、ゲーム終了処理を行う
            Debug.Log("GameOver!");
            this.game_over = true;
        }
    }

}
