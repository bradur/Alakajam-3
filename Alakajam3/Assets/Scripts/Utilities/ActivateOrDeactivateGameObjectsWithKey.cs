using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOrDeactivateGameObjectsWithKey : MonoBehaviour {

    [SerializeField]
    private KeyCode triggerKey;

    [SerializeField]
    private GameObject[] deactivateTheseGameObjects;

    [SerializeField]
    private GameObject[] activateTheseGameObjects;

    [SerializeField]
    private bool destroySelfAfterwards = true;

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(triggerKey))
        {
            foreach(GameObject deactivatableObject in deactivateTheseGameObjects)
            {
                deactivatableObject.SetActive(false);
            }
            foreach(GameObject activatableObject in activateTheseGameObjects)
            {
                activatableObject.SetActive(true);
            }
            if (destroySelfAfterwards)
            {
                Destroy(gameObject);
            }
        }
    }
}
