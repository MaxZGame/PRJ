using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory_UI))]
[RequireComponent(typeof(Inventory_Sound))]
public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    //�������� ��������� ���������
    public Dictionary<string, GameObject> inventoryItemsDIC = new Dictionary<string, GameObject>();

    //��������� ������� ��� ���������
    private Dictionary<string, Vector2> posItemsDIC = new Dictionary<string, Vector2>();

    [SerializeField, Header("�������� ������� ������� ������")]
    private Transform[] transformsSlots;
    private List<Vector2> posSlots = new List<Vector2>();

    [SerializeField, Header("������� ������������ ���������� ��������� � ���������"), Range(1, 9)]
    private int maxItemsInInventory = 5;
    [SerializeField, Header("������� �� ����� ����� �������� �������� �� ���������"), Range(0f, 2f)]
    private float timeSpeedItem = 0.5f;


    [SerializeField, Header("������� ��������� ������������� ��������� ��� ��������"), Range(1, 4)]
    private float scaleInventoryOpen = 2;
    private Vector3 beginScale; //��������� ������ ������ ���������

    [SerializeField, Header("�������� ��������� ������� �������� � ���������")]
    private Transform transformPointBegin;
    private Vector2 posPointBegin;

    [SerializeField, Header("�������� ������, ������� ������ ������������ ��� ��������� � ���������")]
    private GameObject newParentItems;

    //������, ������� ����� ������������ ��� �������� �� ���������
    private GameObject oldParentItems;

    [SerializeField, Header("�������� ������ ������� ���������")]
    private GameObject objInventory;

    [SerializeField, Header("�������� ������� ���������")]
    private Transform transformInventory;

    [SerializeField, Header("�������� ������ ��������� ���������")]
    private ActivatedInventoryButton activatedInventoryButton;

    private Inventory_Sound inventory_Sound; //�������� ������ ���������


    //����, ���� ��������� � ������ �������
    [HideInInspector]
    public bool isNextRoomStep = false;

    //������, ������� �������� �� ���������� ����������� ���������
    private Inventory_UI inventory_UI;

    private void Awake()
    {
        Instance = this;
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

            if (activatedInventoryButton.IsActivated)
            {
                objItem.SetActive(true);
                objItem.transform.position = posItemsDIC[itemSO.NameItem];
            }

            inventory_Sound.PlayAddItem(); //���� ���������� ��������
        }
        else
        {
            inventory_Sound.PlayAudio("FullInventory");
        }

        inventory_UI.UpdateCount(inventoryItemsDIC.Count, maxItemsInInventory); //��������� UI ����������� ���������� ���������
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

        inventory_UI.UpdateCount(inventoryItemsDIC.Count, maxItemsInInventory); //��������� UI ����������� ���������� ���������
    }

    public void OnEnableInventory()
    {
        
        objInventory.transform.position = posPointBegin; //������ �� �������
        RectTransform rectTransform = objInventory.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(scaleInventoryOpen, scaleInventoryOpen, scaleInventoryOpen);//������ ������ ���������
        //���������

        foreach (var kvp in inventoryItemsDIC)
        {
            string key = kvp.Key;
            GameObject item = kvp.Value;
            if (!posItemsDIC.TryGetValue(key, out var pos))
            {
                Debug.LogWarning($"[Inventory] ��� ������� ��� �����: {key}");
                continue;
            }
            StartCoroutine(MoveWithLerp.Instance.Move(item.transform, pos, timeSpeedItem)); //������ ����������� �������� �� ������
            item.SetActive(true);
        }
        if (!isNextRoomStep)
        {
            inventory_Sound.PlayAudio("OpenClose"); // ���� ��������,�������� ���������
        }
    }

    public void OnDisableInventory()
    {
        //������ ������ ���������
        objInventory.transform.position = transformInventory.position;
        RectTransform rectTransform = objInventory.GetComponent<RectTransform>();
        rectTransform.localScale = beginScale;
        //���������

        foreach (var kvp in inventoryItemsDIC)
        {
            string key = kvp.Key;
            GameObject item = kvp.Value;
            item.SetActive(false);
            item.transform.position = posPointBegin;
            activatedInventoryButton.SetActivated(false);
        }
        if (!isNextRoomStep)
        {
            inventory_Sound.PlayAudio("OpenClose"); // ���� ��������,�������� ���������
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

    public void Init() //����� �������������
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
        beginScale = objInventory.transform.localScale;
        inventory_Sound = GetComponent<Inventory_Sound>();
        inventory_UI = GetComponent<Inventory_UI>();
        inventory_UI.UpdateCount(inventoryItemsDIC.Count, maxItemsInInventory); //��������� UI ����������� ���������� ���������
        ActivatedInventory();
    }

    public void ActivatedInventory()
    {
        if (!GameManager.Instance.inventory_Start.isStartInventory) //���� ������ ������ ��������� �� ��������, �� ������ ��������� ���������
        {
            objInventory.SetActive(false);
        }
        else
        {
            objInventory.SetActive(true);
        }
    }

#if UNITY_EDITOR
    private void OnValidate() //������ �� ������ (� ��� ����� � �� ����)
    {
        if (transformsSlots.Length < maxItemsInInventory)
        {
            Debug.LogError($"{transformsSlots} ������, ��� - ����������� ����������� ���������� ������! �������� ������ ������!");
        }
    }
#endif
}
