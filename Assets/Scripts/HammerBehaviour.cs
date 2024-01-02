using UnityEngine;
using UnityEngine.Events;

public class HammerBehaviour : MonoBehaviour
{
    public UnityEvent hammerSmashed;
    public ParticleSystem smashParticles;

    private Sound[] sounds;

    private void Start()
    {
        hammerSmashed.AddListener(() => SoundMaster.Instance.PlaySFX(GetRandomClip()));
        hammerSmashed.AddListener(() => smashParticles.Play());
    }

    private AudioClip GetRandomClip()
    {
        sounds = new Sound[3] {
            SoundMaster.Instance.GetSoundClip("Smash1"),
            SoundMaster.Instance.GetSoundClip("Smash2"),
            SoundMaster.Instance.GetSoundClip("Smash3")
        };

        int i = Random.Range(0, sounds.Length);

        return sounds[i].clip;
    }

    public void HammerSmash() => hammerSmashed.Invoke();
}
