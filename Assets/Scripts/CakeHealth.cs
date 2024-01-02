using UnityEngine;
using UnityEngine.UI;

public class CakeHealth : MonoBehaviour
{
    public Image healthBar, healthBarFill;
    [Range(0f, 1f)] public float health = 1;

    [Space(20)] public float cooldown = 1f;
    public LayerMask bugLayer;

    private float _cld;

    private void Start()
    {
        _cld = cooldown;
    }

    private void Update()
    {
        healthBar.fillAmount = health;

        healthBarFill.color = Color.Lerp(Color.red, Color.green, health);
    }

    public void DecreaseHealth()
    {
        health -= .02f;

        ScoreManager.instance.RemoveScore(10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bug")) return;

        Collider[] others = other.GetComponentsInChildren<Collider>();

        if (others.Length > 0)
        {
            _cld -= 1;

            if (_cld <= 0)
            {
                DecreaseHealth();

                _cld = cooldown;
            }
        }
    }
}
