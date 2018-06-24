using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PauseDolly : MonoBehaviour
{
    [SerializeField]
    private float timePaused; //in seconds
    [SerializeField]
    private PlayableDirector director;

    // Use this for initialization
    void Start()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        StartCoroutine(StartPlay());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator StartPlay()
    {
        yield return new WaitForSeconds(timePaused);
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
