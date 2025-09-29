using System.Collections.Generic;
using UnityEngine;

public class ActivatedInventoryButton : MonoBehaviour
{

    public bool IsActivated { get; private set; }

    private List<NextRoom> deactiveNextRoom = new List<NextRoom>(); //Массив некструмов, которые мы блокируем
    private List<ItemVisual> deactiveitemVisuals = new List<ItemVisual>();//Массив itemVisual, которые мы блокируем

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
            foreach (NextRoom nextRoom in deactiveNextRoom)
            {
                nextRoom.Locker(false);
            }
            foreach (ItemVisual itemVisual in deactiveitemVisuals)
            {
                itemVisual.Locker(false);
            }
            deactiveNextRoom.Clear();
            deactiveitemVisuals.Clear();
        }
        else
        {
            foreach (NextRoom nextRoom in nextRoomsDeactivated)  // Отключаем объекты переходов в другие комнаты
            {
                deactiveNextRoom.Add(nextRoom);
                ItemVisual itemVisual = nextRoom.gameObject.GetComponent<ItemVisual>();
                itemVisual.Locker(true);
                deactiveitemVisuals.Add(itemVisual);
                nextRoom.Locker(true);
            }
        }


    }
}
