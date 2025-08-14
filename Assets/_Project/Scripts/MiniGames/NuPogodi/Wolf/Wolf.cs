using UnityEngine;

public class Wolf : MonoBehaviour
{
    [field: SerializeField, Header("Максимальное здоровье"), Range(1, 3)]
    public int Health { get; private set; }

    private void OnEnable()
    {
        MG_NuPogodiActionManager.OnDamageWolf += DamageWolf;
    }

    public void DamageWolf(int damage)
    {
        Health -= damage;
    }

    public void NewWolf()
    {
        Health = 3;
    }

    private void OnDisable()
    {
        MG_NuPogodiActionManager.OnDamageWolf -= DamageWolf;
    }
}
