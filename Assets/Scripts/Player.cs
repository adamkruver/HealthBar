using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    private int _health;

    public UnityAction<int, int> HealthChanged;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        ChangeHealth(-damage);
    }

    public void Heal(int healPoints)
    {
        ChangeHealth(healPoints);
    }

    private void ChangeHealth(int value)
    {
        _health += value;

        if (_health < 0)
            _health = 0;

        if (_health > _maxHealth)
            _health = _maxHealth;

        HealthChanged?.Invoke(_health, _maxHealth);
    }
}
