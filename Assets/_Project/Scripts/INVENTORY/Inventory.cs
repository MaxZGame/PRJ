using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    //Основное хранилище предметов
    private Dictionary<string, GameObject> inventoryItemsDIC = new Dictionary<string, GameObject>();

    //Хранилище позиций для предметов
    private Dictionary<string, Vector2> posItemsDIC = new Dictionary<string, Vector2>();


    [SerializeField, Header("Вставьте объекты позиций слотов")]
    private Transform[] transformsSlots;
    private List<Vector2> posSlots = new List<Vector2>();

    [SerializeField, Header("Укажите максимальное количество предметов в инвентаре"), Range(1, 9)]
    private int maxItemsInInventory = 5;

    [SerializeField, Header("Вставьте начальную позицию предмета в инвентаре")]
    private Transform transformPointBegin;
    private Vector2 posPointBegin;

    [SerializeField, Header("Вставьте объект, который станет родительским для предметов")]
    private GameObject newParentItems;


    [SerializeField, Header("Вставьте кнопку активации инвентаря")]
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
            objItem.transform.SetParent(newParentItems.transform); // Устанавливаем нового родителя
            objItem.SetActive(false); //Деактивируем
            objItem.transform.position = posPointBegin; //Присваиваем новую позицию

            Item itemScript = objItem.GetComponent<Item>(); //Инициализируем скрипт предмета
            itemScript.SetIsInInventory(true);

            inventoryItemsDIC[itemSO.NameItem] = objItem; // Добавляем в хранилище

            int index = Mathf.Min(inventoryItemsDIC.Count - 1, posSlots.Count - 1); //Присваиваем индекс

            //Добавляем слот предмету
            posItemsDIC[itemSO.NameItem] = posSlots[index];
        }
        else
        {
            Debug.Log("Больше не влезает!");
        }

        //Если инвентарь в этот момент открыт
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
                Debug.LogWarning($"[Inventory] Нет позиции для ключа: {key}");
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
            Debug.LogError($"Слотов меньше, чем указанное максимальное количество предметов!");
        }
        posPointBegin = transformPointBegin.transform.position;
    }

    private void OnValidate()
    {
        if (transformsSlots.Length < maxItemsInInventory)
        {
            Debug.LogError($"{transformsSlots} меньше, чем - максимально назначенное количество слотов! Создайте больше слотов!");
        }
    }
}
