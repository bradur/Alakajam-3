using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{

    [SerializeField]
    private KeyCode toggleKey;

    [SerializeField]
    private KeyCode exitKey;
    [SerializeField]
    private KeyCode restartKey;

    public static ExitManager main;

    [SerializeField]
    private GameObject ui;

    private bool isWaiting = false;

    void Awake()
    {
        main = this;
    }
    public void StartWaiting()
    {
        ui.SetActive(true);
        isWaiting = true;
    }
    public void StopWaiting()
    {
        ui.SetActive(false);
        isWaiting = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(toggleKey))
        {
            if (isWaiting)
            {
                StopWaiting();
            }
            else
            {
                StartWaiting();
            }
        }
        if (isWaiting)
        {
            if (Input.GetKeyUp(exitKey))
            {
                ExitGame();
            }
            if (Input.GetKeyUp(restartKey))
            {
                RestartLevel();
            }
        }
    }

    public void RestartLevel()
    {
        StopWaiting();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        StopWaiting();
        Application.Quit();
    }
}
