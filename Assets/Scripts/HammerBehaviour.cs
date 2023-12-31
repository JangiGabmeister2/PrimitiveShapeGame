using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HammerBehaviour : MonoBehaviour
{
    public Animator animator;
    public UnityEvent hammerSmashed;

    [SerializeField] List<BugBehaviour> bugs = new List<BugBehaviour>();

    private void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            Collider[] bugObjs = Physics.OverlapSphere(hitInfo.point, 5f);

            bugs = bugObjs.Select(x => x.GetComponent<BugBehaviour>()).ToList();

            foreach (BugBehaviour bug in bugs)  
            {
                if (bugs.Count > 0 && bug == null)
                {
                    bugs.Remove(bug);
                    break;
                }

                bug.moveSpeed = 0;

                hammerSmashed.AddListener(() => bugs.Remove(bug));
                hammerSmashed.AddListener(() => bug.Squash());
            }
        }
    }

    public void HammerSmash() => hammerSmashed.Invoke();
}
