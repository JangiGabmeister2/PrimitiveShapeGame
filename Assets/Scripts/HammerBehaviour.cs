using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HammerBehaviour : MonoBehaviour
{
    public UnityEvent hammerSmashed;

    public void HammerSmash() => hammerSmashed.Invoke();
}
