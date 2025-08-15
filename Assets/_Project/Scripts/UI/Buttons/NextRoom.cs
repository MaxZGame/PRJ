using UnityEngine;

public class NextRoom : MonoBehaviour
{
    [SerializeField, Header("Вставьте объекты, которые нужно активировать")]
    private GameObject[] activatedGO;

    [SerializeField, Header("Вставьте объекты, которые нужно ДЕактивировать")]
    private GameObject[] deactivatedGO;

    public void Deactivated()
    {
        if (deactivatedGO != null & deactivatedGO.Length > 0)
        {
            foreach (GameObject go in deactivatedGO)
            {
                go?.SetActive(false);
            }
        }
        Inventory.Instance.OnDisableInventory();
    }

    public void Activated()
    {
        if (activatedGO != null && activatedGO.Length > 0)
        {
            foreach (GameObject go in activatedGO)
            {
                go?.SetActive(true);
            }
        }
    }
}
