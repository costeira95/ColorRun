using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] balls;
    public float SpawnSpeed = 0.5f;
    // Use this for initialization
    void Start () {
        StartCoroutine(BallSpawner());
    }
	
	// Update is called once per frame
	void Update () {

    }    

    IEnumerator BallSpawner()
    {
        for (; ; )
        {
            Vector3 StartPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), 0, 1));
            GameObject.Instantiate(balls[ChooseNextBall()], StartPosition, Quaternion.identity);
            yield return new WaitForSeconds(SpawnSpeed);
        }
    }

    int ChooseNextBall()
    {
        int range = 0;
        int i;
        for (i = 0; i < balls.Length; i++)
            range += balls[i].GetComponent<Movement>().weight;

        var rand = Random.Range(0, range);
        var top = 0;

        for (i = 0; i < balls.Length; i++)
        {
            top += balls[i].GetComponent<Movement>().weight;
            if (rand < top)
                return i;
        }
        return -1;
    }
}
