using UnityEngine;

public class ActivatedInventoryButton : MonoBehaviour
{

    public bool IsActivated { get; private set; }

    public void Activated()
    {
        IsActivated = !IsActivated;
        if (IsActivated)
        {
            Inventory.Instance.OnEnableInventory();
        }
        else
        {
            Inventory.Instance.OnDisableInventory();
        }
    }

    public void SetActivated(bool set)
    {
        IsActivated = set;
    }
}
