using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSystem : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float maxHunger;
    [SerializeField] private float maxStamina;
    [SerializeField] private float StaminaAmount;
    [SerializeField] private float HungerAmount;
    [SerializeField] private float healAmount;

    [SerializeField] private float healDelay;

    private float currentHp;
    private float currentHunger;
    private float currentStamina;

    private bool canHeal;

    private Scenes scene;
    void Start()
    {
        scene = FindObjectOfType<Scenes>();
        currentHp = maxHp;
        currentHunger = maxHunger;
        currentStamina = maxStamina;
    }
    private void Update()
    {
        if (currentHp < maxHp && canHeal) Heal();
    }

    public void RemoveStamina(float amount)
    {
        if (currentStamina - amount < 0) currentStamina = 0;
        else currentStamina -= amount;
    }
    public void AddStamina()
    {
        if (currentStamina + StaminaAmount > maxStamina) currentStamina = maxStamina;
        else currentStamina += StaminaAmount;
    }

    public void RemoveHunger(float amount)
    {
        if (currentHunger - amount < 0) currentHunger = 0;
        else currentHunger -= amount;
    }
    public void AddHunger()
    {
        if (currentHunger + HungerAmount > maxHunger) currentHunger = maxHunger;
        else currentHunger += HungerAmount;
    }

    public void TakeDamage(float dmg)
    {
        if (currentHp - dmg <= 0)
        {
            currentHp = 0;
            Die();
        }
        else currentHp -= dmg;
        canHeal = false;
        StopCoroutine(DelayHealing(healDelay));
        StartCoroutine(DelayHealing(healDelay));
    }
    public void Heal()
    {
        if (currentHp + healAmount > maxHp) currentHp = maxHp;
        else currentHp += healAmount * Time.deltaTime;
    }

    private void Die()
    {
        scene.LoadDeathScene();
        Destroy(gameObject);
    }

    IEnumerator DelayHealing(float time)
    {
        yield return new WaitForSeconds(time);
        canHeal = true;
    }

    // Getters

    public float GetHp()
    {
        return currentHp;
    }
    public float GetHunger()
    {
        return currentHunger;
    }
    public float GetStamina()
    {
        return currentStamina;
    }
}
