using System.Collections;
using System.Collections.Generic;
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

    private void OnTriggerStay(Collider other)
    {
        List<BugBehaviour> bugs = new List<BugBehaviour>();

        if (other.CompareTag("Bug"))
        {
            bugs.Add(other.GetComponent<BugBehaviour>());
        }

        foreach (BugBehaviour bug in bugs)
        {
            bug.moveSpeed = 0;

            hammerSmashed.AddListener(() => bug.Squash());
        }
    }
}
