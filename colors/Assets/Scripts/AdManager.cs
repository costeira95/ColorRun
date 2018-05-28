using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour {

    public static int count = 0;

    // Use this for initialization
    void Start () {
        if ((count % 3) == 0)
        {
            Advertisement.Initialize("1773228", true);
            if (Advertisement.IsReady())
                Advertisement.Show("video");
            count = 0;
        }
    }
	
}
