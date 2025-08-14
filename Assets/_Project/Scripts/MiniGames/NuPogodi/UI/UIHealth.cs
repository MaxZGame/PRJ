using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    public static UIHealth Instance;

    [SerializeField]
    private List<Image> health;

    [SerializeField, Header("Вставьте префаб ''сердечка''")]
    private Image prefabHearth;


    //Счетчик дамага
    private int damageSum = 0;

    private void OnEnable()
    {
        MG_NuPogodiActionManager.OnDamageWolf += DelHealth;
    }

    private void Awake()
    {
        Instance = this;
    }


    public void DelHealth(int damage)
    {
        damageSum += damage;
        int healthWolf = MG_NuPogodiGameManager.Instance.Wolf.Health + 1;
        if (healthWolf != 0)
        {
            Destroy(health[healthWolf - 1]);
            health.RemoveAt(healthWolf - 1);
        }
    }

    public void NewHealth()
    {
        if (health.Count > 0 && health != null)
        {
            foreach (Image image in health)
            {
                if (image.gameObject.tag != "UIPanel")
                {
                    Destroy(image);
                }
                for (int i = 0; i < health.Count; i++)
                {
                    health.RemoveAt(i);
                }
            }
        }
        for (int i = 0; i < MG_NuPogodiGameManager.Instance.Wolf.Health; i++)
        {
            Image image = Instantiate(prefabHearth, gameObject.transform);
            health.Add(image);
        }
        damageSum = 0;
    }

    private void OnDisable()
    {
        MG_NuPogodiActionManager.OnDamageWolf -= DelHealth;
    }
}
