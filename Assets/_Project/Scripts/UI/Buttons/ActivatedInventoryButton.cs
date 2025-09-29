using System.Collections.Generic;
using UnityEngine;

public class ActivatedInventoryButton : MonoBehaviour
{

    public bool IsActivated { get; private set; }

    private List<NextRoom> deactiveNextRoom = new List<NextRoom>(); //������ ����������, ������� �� ���������
    private List<ItemVisual> deactiveitemVisuals = new List<ItemVisual>();//������ itemVisual, ������� �� ���������

    public void Activated()
    {
        IsActivated = !IsActivated;
        if (IsActivated)
        {
            Inventory.Instance.OnEnableInventory();
            ActDeactObj(false); //������������ �������, ������� �� ������ �������� ��� ������ ���������
        }
        else
        {
            Inventory.Instance.OnDisableInventory();
            ActDeactObj(true); //���������� ���������� ��������, ������� ���� ������� ��� ��������� ���������
        }
    }

    public void SetActivated(bool set)
    {
        IsActivated = set;
    }

    private void ActDeactObj(bool active) //����� ���������� ��������, ������� �� ������ �������� ��� ������ ���������
    {
        NextRoom[] nextRoomsDeactivated = FindObjectsByType<NextRoom>(FindObjectsSortMode.None); // ������� ������� ��������� � ������ �������

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
            foreach (NextRoom nextRoom in nextRoomsDeactivated)  // ��������� ������� ��������� � ������ �������
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
