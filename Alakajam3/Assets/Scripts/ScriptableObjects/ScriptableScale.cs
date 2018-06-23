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
    public List<StringFloat> growAmounts; /*= new List<StringFloat>()
    {
        new StringFloat("ScaleLevel0", 0.3f ),
        new StringFloat( "ScaleLevel1", 0.6f ),
        new StringFloat( "ScaleLevel2", 1.2f ),
        new StringFloat( "ScaleLevel3", 3.6f )
    };*/

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
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