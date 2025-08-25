using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    //�������� ��������� ���������
    private Dictionary<string, GameObject> inventoryItemsDIC = new Dictionary<string, GameObject>();

    //��������� ������� ��� ���������
    private Dictionary<string, Vector2> posItemsDIC = new Dictionary<string, Vector2>();


    [SerializeField, Header("�������� ������� ������� ������")]
    private Transform[] transformsSlots;
    private List<Vector2> posSlots = new List<Vector2>();

    [SerializeField, Header("������� ������������ ���������� ��������� � ���������"), Range(1, 9)]
    private int maxItemsInInventory = 5;

    [SerializeField, Header("�������� ��������� ������� �������� � ���������")]
    private Transform transformPointBegin;
    private Vector2 posPointBegin;

    [SerializeField, Header("�������� ������, ������� ������ ������������ ��� ��������� � ���������")]
    private GameObject newParentItems;

    //������, ������� ����� ������������ ��� �������� �� ���������
    private GameObject oldParentItems;


    [SerializeField, Header("�������� ������ ��������� ���������")]
    private ActivatedInventoryButton activatedInventoryButton;



    private void Awake()
    {
        Instance = this;
        Init();
    }

    public void AddItems(ItemSO itemSO, GameObject objItem)
    {
        if (posItemsDIC.Count < maxItemsInInventory)
        {

            objItem.transform.SetParent(newParentItems.transform); // ������������� ������ ��������
            objItem.SetActive(false); //������������
            objItem.transform.position = posPointBegin; //����������� ����� �������

            Item itemScript = objItem.GetComponent<Item>(); //�������������� ������ ��������

            inventoryItemsDIC[itemSO.NameItem] = objItem; // ��������� � ���������

            CompareNameItem(itemScript);    //��������� ���������� ����

            int index = GetFreeIndex(); //����������� ������

            itemScript.SetIsInInventory(true, index);

            //��������� ���� ��������
            posItemsDIC[itemSO.NameItem] = posSlots[itemScript.IndexItem];
            Debug.Log($"[Inventory,AddItems] ������ �������� � ��������� {itemScript.IndexItem}");

            if (activatedInventoryButton.IsActivated)
            {
                objItem.SetActive(true);
                objItem.transform.position = posItemsDIC[itemSO.NameItem];
            }
        }
        else
        {
            Debug.Log("������ �� �������!");
        }

    }

    public void RemoveItems(ItemSO itemSO, Item itemScript, GameObject objItem)
    {
        //�������� ��� ������� � ����� ITEMS
        GameObject[] oldParentItemsALL = GameObject.FindGameObjectsWithTag("ITEMS");

        //���������� ��� ������� � ����� ITEMS
        foreach (GameObject parents in oldParentItemsALL)
        {
            if (parents.activeSelf) // ���� ������ �������, �� ���� ���������� ���� ������
            {
                oldParentItems = parents;
            }
        }

        objItem.transform.SetParent(oldParentItems.transform); // ������������� ������� ��������
        oldParentItems = null; //���������� ������� ��������

        itemScript.SetIsInInventory(false, -1);

        inventoryItemsDIC.Remove(itemSO.NameItem);
        posItemsDIC.Remove(itemSO.NameItem);

        Debug.Log($"[Inventory,RemoveIitems] ������ �������� � ��������� {itemScript.IndexItem}");
    }

    public void OnEnableInventory()
    {
        foreach (var kvp in inventoryItemsDIC)
        {
            string key = kvp.Key;
            GameObject item = kvp.Value;
            if (!posItemsDIC.TryGetValue(key, out var pos))
            {
                Debug.LogWarning($"[Inventory] ��� ������� ��� �����: {key}");
                continue;
            }
            item.transform.position = pos;
            item.SetActive(true);
        }
    }

    public void OnDisableInventory()
    {
        foreach (var kvp in inventoryItemsDIC)
        {
            string key = kvp.Key;
            GameObject item = kvp.Value;
            item.SetActive(false);
            item.transform.position = posPointBegin;
            activatedInventoryButton.SetActivated(false);
        }
    }

    private int GetFreeIndex() //����� ������ ��������� ������
    {
        for (int i = 0; i < posSlots.Count; i++)
        {
            bool slotBusy = false;
            foreach (var kvp in inventoryItemsDIC)
            {
                Item item = kvp.Value.GetComponent<Item>();
                if (item.IndexItem == i)
                {
                    slotBusy = true;
                }
            }
            if (!slotBusy)
            {
                return i;
            }
        }
        return -1; //���� ��������� ������ �� �������
    }

    private void CompareNameItem(Item itemScript)
    {
        int countCompared = 0;
        foreach (var kvp in inventoryItemsDIC)
        {
            Item compareItemScripts = kvp.Value.GetComponent<Item>();
            if (itemScript.IndexItem == compareItemScripts.IndexItem)
            {
                countCompared++;
            }
            if (countCompared > 1)
            {
                Debug.LogError($"� {itemScript.gameObject.name} � {compareItemScripts.gameObject.name} ���������� ��� {itemScript.IndexItem}, �������� ��� � ItemSO, ����� �� ��������� ����������!");
            }
        }

    }

    private void Init()
    {
        for (int i = 0; i < transformsSlots.Length; i++)
        {
            posSlots.Add(transformsSlots[i].transform.position);
        }
        if (posSlots.Count < maxItemsInInventory)
        {
            Debug.LogError($"������ ������, ��� ��������� ������������ ���������� ���������!");
        }
        posPointBegin = transformPointBegin.transform.position;
    }

    private void OnValidate()
    {
        if (transformsSlots.Length < maxItemsInInventory)
        {
            Debug.LogError($"{transformsSlots} ������, ��� - ����������� ����������� ���������� ������! �������� ������ ������!");
        }
    }
}
