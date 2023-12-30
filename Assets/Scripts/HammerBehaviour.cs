using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HammerBehaviour : MonoBehaviour
{
    public Animator animator;
    public UnityEvent hammerSmashed;


    private void Awake()
    {
        animator.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(Smash());

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            List<BugBehaviour> bugs = new List<BugBehaviour>();
            Collider[] bugObjs = Physics.OverlapSphere(hitInfo.point, 5f);

            bugs = bugObjs.Select(x => x.GetComponent<BugBehaviour>()).ToList();

            foreach (BugBehaviour bug in bugs)
            {
                if (bug == null) return;

                bug.moveSpeed = 0;

                hammerSmashed.AddListener(() => bugs.Remove(bug));
                hammerSmashed.AddListener(() => bug.Squash());
            }
        }
    }

    private IEnumerator Smash()
    {
        yield return new WaitForSeconds(1f);

        animator.enabled = true;
    }

    public void HammerSmash() => hammerSmashed.Invoke();
}
