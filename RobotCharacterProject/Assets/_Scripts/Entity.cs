using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Anything that can take damage
/// </summary>
public class Entity : MonoBehaviour
{
    [Header("Base Entity Properties")]
    [SerializeField]
    protected int _health;
    [SerializeField]
    private UIHealth _uiHealthComponent;

    private int _maxHealth = 1;

    public delegate void OnDeathEvent();
    public OnDeathEvent OnDeath;

    public virtual void Start() {
        _maxHealth = _health;
    }

    /// <summary>
    /// Take damage
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(int damage) {
        _health -= damage;
        _uiHealthComponent.UpdateHealthUI(CurrentHealthPercent());
        if(_health <= 0) {
            _health = 0;
            HandleDeath();
        }
    }

    /// <summary>
    /// Handle any special cases on death, if any
    /// </summary>
    protected virtual void HandleDeath() {
        if(OnDeath != null) OnDeath();
    }

    /// <summary>
    /// Get current health as a percent
    /// </summary>
    /// <returns></returns>
    public float CurrentHealthPercent() { 
        return Mathf.Clamp((float)_health / _maxHealth, 0f, 1f);
    }
}
