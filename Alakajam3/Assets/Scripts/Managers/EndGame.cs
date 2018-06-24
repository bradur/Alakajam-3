using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {
    [SerializeField]
    private KeyCode endGameKey;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(endGameKey))
        {
            Debug.Log("QUIT");
            Application.Quit();
        }
    }
}
