using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public int weight;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, GameManager.instance.Speed * Time.deltaTime, 0));
    }
}
