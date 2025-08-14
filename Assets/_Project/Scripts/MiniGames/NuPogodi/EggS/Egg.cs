using UnityEngine;

public class Egg : MonoBehaviour
{
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wolf")
        {
            MG_NuPogodiGameManager.Instance.CollectEgg();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "EndTrigger")
        {
            MG_NuPogodiActionManager.OnDamageWolf?.Invoke(damage);
            Destroy(gameObject);
        }
    }
}
