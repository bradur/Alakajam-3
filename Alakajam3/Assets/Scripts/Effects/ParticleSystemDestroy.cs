using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDestroy : MonoBehaviour
{
    private ParticleSystem particles;
    [SerializeField]
    private float particleSize;

    // Use this for initialization
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        if (particles != null)
        {
            var main = particles.main;
            main.startSizeMultiplier = particleSize;
            Destroy(gameObject, particles.main.duration);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetParticleSize(float size)
    {
        particleSize = size;
    }
}
