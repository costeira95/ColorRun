using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncSore : MonoBehaviour {

    Text ScoreCount;
	// Use this for initialization
	void Start () {
        ScoreCount = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        ScoreCount.text = GameManager.instance.Score.ToString();
	}
}
