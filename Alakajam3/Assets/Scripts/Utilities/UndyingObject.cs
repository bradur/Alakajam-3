using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndyingObject : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
