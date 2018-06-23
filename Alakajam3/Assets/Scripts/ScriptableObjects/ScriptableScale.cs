using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableScales", menuName = "ScriptableObjects/Scales")]
public class ScriptableScale : ScriptableObject
{
    [SerializeField]
    private string objectName = "ScriptableObstacle";
    public string Name { get { return objectName; } }

    [SerializeField]
    public List<StringFloat> growAmounts;

    [SerializeField]
    public List<StringFloat> levelThresholds;

    public float startingScale = 0.3f;
    public float currentScale = 0.3f;
}

[Serializable]
public class StringFloat
{
    public string key;
    public float value;

    public StringFloat(string k, float v)
    {
        key = k;
        value = v;
    }
}