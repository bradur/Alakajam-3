using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterController : MonoBehaviour
{
    [SerializeField]
    private Image meterGraph;

    [SerializeField]
    private ScriptableScale scales;

    private float minScale = 0f;
    private float maxScale = 2f;

    // Use this for initialization
    void Start()
    {
        //maxScale = scales.levelThresholds[scales.levelThresholds.Count - 1].value;
    }

    // Update is called once per frame
    void Update()
    {
        meterGraph.fillAmount = (scales.currentScale - minScale) / maxScale;
    }
}
