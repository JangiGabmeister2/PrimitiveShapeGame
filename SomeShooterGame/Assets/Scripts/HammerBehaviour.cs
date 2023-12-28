using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HammerBehaviour : MonoBehaviour
{
    public Animator animator;
    public UnityEvent hammerSmashed;

    private void Awake() => animator.enabled = false;

    private void Start() => StartCoroutine(nameof(Smash));

    private IEnumerator Smash()
    {
        yield return new WaitForSeconds(1f);

        animator.enabled = true;
    }

    public void HammerSmash()
    {
        hammerSmashed.Invoke();
    }
}
