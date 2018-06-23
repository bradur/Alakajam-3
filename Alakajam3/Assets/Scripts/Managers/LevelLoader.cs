using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    [SerializeField]
    private string loadThisScene;

    // Use this for initialization
    void Start () {
        SceneManager.LoadScene(loadThisScene);
    }

}
