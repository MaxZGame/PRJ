using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    [SerializeField, Header("��� ������������� ������?")]
    private bool isInteractive = false;

    [SerializeField, Header("===���� �������������, �� ��������� �� ����===")]
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
        Debug.Log("������� �� �������");
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
                Debug.LogError($"[Item,Validate]��� ������������� ������ ��� �������������� �������!{gameObject.name}");
            }
        }
        else
        {
            objInteractive = null;
        }
    }
}
