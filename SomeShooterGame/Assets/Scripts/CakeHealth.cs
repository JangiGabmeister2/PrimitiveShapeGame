using UnityEngine;

public class CakeHealth : MonoBehaviour
{
    [Range(0f, 1f)] public float health = 1;

    public void DecreaseHealth()
    {
        health -= .01f;
    }
}
