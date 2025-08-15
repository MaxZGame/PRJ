using System.Collections.Generic;
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

    [SerializeField, Header("�������� ������, ������� ������ ������������ ��� ���������")]
    private GameObject newParentItems;


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
            itemScript.SetIsInInventory(true);

            inventoryItemsDIC[itemSO.NameItem] = objItem; // ��������� � ���������

            int index = Mathf.Min(inventoryItemsDIC.Count - 1, posSlots.Count - 1); //����������� ������

            //��������� ���� ��������
            posItemsDIC[itemSO.NameItem] = posSlots[index];
        }
        else
        {
            Debug.Log("������ �� �������!");
        }

        //���� ��������� � ���� ������ ������
        if (activatedInventoryButton.IsActivated)
        {
            OnEnableInventory();
        }
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
