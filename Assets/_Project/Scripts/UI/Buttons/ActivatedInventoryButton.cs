using System.Collections.Generic;
using UnityEngine;

public class ActivatedInventoryButton : MonoBehaviour
{

    public bool IsActivated { get; private set; }

    private List<GameObject> deactiveObj = new List<GameObject>(); //Массив объектов, которые мы деактивировали 

    public void Activated()
    {
        IsActivated = !IsActivated;
        if (IsActivated)
        {
            Inventory.Instance.OnEnableInventory();
            ActDeactObj(false); //Деактивируем объекты, которые не должны работать при работе инвентаря
        }
        else
        {
            Inventory.Instance.OnDisableInventory();
            ActDeactObj(true); //Возвращаем активность объектам, которые были закрыты при активации инвентаря
        }
    }

    public void SetActivated(bool set)
    {
        IsActivated = set;
    }

    private void ActDeactObj(bool active) //Метод отключения объектов, которые не должны работать при работе инвентаря
    {
        NextRoom[] nextRoomsDeactivated = FindObjectsByType<NextRoom>(FindObjectsSortMode.None); // Находим объекты переходов в другие комнаты

        if (active)
        {
            foreach (GameObject obj in deactiveObj)
            {
                obj.SetActive(true);
            }
            deactiveObj.Clear();
        }
        else
        {
            foreach (NextRoom nextRoom in nextRoomsDeactivated)  // Отключаем объекты переходов в другие комнаты
            {
                GameObject obj = nextRoom.gameObject;
                deactiveObj.Add(obj);
                obj.SetActive(false);
            }
        }


    }
}
