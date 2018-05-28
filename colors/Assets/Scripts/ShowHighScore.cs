using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowHighScore : MonoBehaviour {

    private void Awake()
    {
        var hs = transform.parent.GetComponent<MainMenuCode>().RetriveHighScore();
        string text = "<b>High Score</b>\n\n";
            text += string.Format("You caught {0} in {1} minutes\n", hs.points, hs.gameTime);
        transform.Find("Text").GetComponent<Text>().text = text;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
            SceneManager.LoadScene("Menu");
    }
}
