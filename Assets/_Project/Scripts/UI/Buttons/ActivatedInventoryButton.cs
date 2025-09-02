using System.Collections.Generic;
using UnityEngine;

public class ActivatedInventoryButton : MonoBehaviour
{

    public bool IsActivated { get; private set; }

    private List<GameObject> deactiveObj = new List<GameObject>(); //������ ��������, ������� �� �������������� 

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
            foreach (GameObject obj in deactiveObj)
            {
                obj.SetActive(true);
            }
            deactiveObj.Clear();
        }
        else
        {
            foreach (NextRoom nextRoom in nextRoomsDeactivated)  // ��������� ������� ��������� � ������ �������
            {
                GameObject obj = nextRoom.gameObject;
                deactiveObj.Add(obj);
                obj.SetActive(false);
            }
        }


    }
}
