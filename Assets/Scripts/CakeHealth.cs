using UnityEngine;
using UnityEngine.UI;

public class CakeHealth : MonoBehaviour
{
    public Image healthBar, healthBarFill;
    [Range(0f, 1f)] public float health = 1;
    private float _currentHealth;

    [Space(20)] public float cooldown = 1f;
    public LayerMask bugLayer;

    private float _cld;

    public float GetCurrentHealth => _currentHealth;

    private void Start()
    {
        _currentHealth = health;
        _cld = cooldown;
    }

    private void Update()
    {
        if (GameHandler.instance.CurrentState == GameStates.Game
            || GameHandler.instance.CurrentState == GameStates.Intermission)
        {
            healthBar.fillAmount = _currentHealth;

            healthBarFill.color = Color.Lerp(Color.red, Color.green, _currentHealth);

            if (_currentHealth <= 0)
            {
                GameHandler.instance.CurrentState = GameStates.End;
                _currentHealth = health;
            }
        }
    }

    public void DecreaseHealth()
    {
        _currentHealth -= .02f;

        ScoreManager.instance.RemoveScore(10);
    }

    public void RegainHealth()
    {
        _currentHealth += health / 5;
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
