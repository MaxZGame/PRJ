using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    [SerializeField, Header("Это интерактивный объект?")]
    private bool isInteractive = false;

    [SerializeField, Header("===Если интерактивный, то вставляем всё ниже===")]
    private GameObject objInteractive;

    private void OnEnable()
    {
        if (objInteractive != null && objInteractive.activeSelf == true)
        {
            objInteractive.SetActive(false);
        }
    }

    private void Update()
    {
        CloseInteractiveObj();
    }

    void OnMouseDown()
    {
        Debug.Log("Кликнул по объекту");
        if (isInteractive)
        {
            objInteractive.SetActive(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CloseInteractiveObj()
    {
        if (objInteractive != null && objInteractive.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                objInteractive.SetActive(false);
            }
        }
    }

    private void OnValidate()
    {
        if (isInteractive)
        {
            if (objInteractive == null)
            {
                Debug.LogError($"[Item,Validate]Нет интерактивной панели для интерактивного объекта!{gameObject.name}");
            }
        }
        else
        {
            objInteractive = null;
        }
    }
}
