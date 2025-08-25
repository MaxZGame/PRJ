using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField, Header("Вставьте объект, который станет родительским для предметов в инвентаре")]
    private GameObject newParentItems;

    //Объект, который будет родительским при удалении из инвентаря
    private GameObject oldParentItems;


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

            inventoryItemsDIC[itemSO.NameItem] = objItem; // Добавляем в хранилище

            CompareNameItem(itemScript);    //Проверяем совпадение имен

            int index = GetFreeIndex(); //Присваиваем индекс

            itemScript.SetIsInInventory(true, index);

            //Добавляем слот предмету
            posItemsDIC[itemSO.NameItem] = posSlots[itemScript.IndexItem];
            Debug.Log($"[Inventory,AddItems] Индекс предмета в инвентаре {itemScript.IndexItem}");

            if (activatedInventoryButton.IsActivated)
            {
                objItem.SetActive(true);
                objItem.transform.position = posItemsDIC[itemSO.NameItem];
            }
        }
        else
        {
            Debug.Log("Больше не влезает!");
        }

    }

    public void RemoveItems(ItemSO itemSO, Item itemScript, GameObject objItem)
    {
        //Получаем все объекты с тегом ITEMS
        GameObject[] oldParentItemsALL = GameObject.FindGameObjectsWithTag("ITEMS");

        //Перебираем все объекты с тегом ITEMS
        foreach (GameObject parents in oldParentItemsALL)
        {
            if (parents.activeSelf) // Если объект активен, то даем переменной этот объект
            {
                oldParentItems = parents;
            }
        }

        objItem.transform.SetParent(oldParentItems.transform); // Устанавливаем старого родителя
        oldParentItems = null; //Сбрасываем старого родителя

        itemScript.SetIsInInventory(false, -1);

        inventoryItemsDIC.Remove(itemSO.NameItem);
        posItemsDIC.Remove(itemSO.NameItem);

        Debug.Log($"[Inventory,RemoveIitems] Индекс предмета в инвентаре {itemScript.IndexItem}");
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

    private int GetFreeIndex() //Метод поиска свободных слотов
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
        return -1; //Если свободных слотов не нашлось
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
                Debug.LogError($"У {itemScript.gameObject.name} и {compareItemScripts.gameObject.name} одинаковое имя {itemScript.IndexItem}, измените имя у ItemSO, чтобы не возникало конфликтов!");
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
