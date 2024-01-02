using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HammerBehaviour : MonoBehaviour
{
    public UnityEvent hammerSmashed;
    public ParticleSystem smashParticles;

    private void Start()
    {
        hammerSmashed.AddListener(() => smashParticles.Play());
    }

    public void HammerSmash() => hammerSmashed.Invoke();
}
