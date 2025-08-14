using System.Collections;
using UnityEngine;

public class MG_ButerGameManager : MonoBehaviour
{
    public static MG_ButerGameManager Instance;

    //Это менеджер переключения панелей внутри мини-игры
    private MG_ButterPanelManager butterPanelManager;

    //Это счетчик плохих выборов
    private int badChoices = 0;

    [SerializeField, Header("Сколько негативных выборов нужно для поражения?"), Range(1, 10)]
    private int badChoicesForBadFinal = 2;

    [SerializeField, Header("Сколько cекунд проигрывается анимация?"), Range(1, 10)]
    private float timeAnimation = 5f;

    [SerializeField, Header("Вставьте объект с хорошим финалом")]
    private GameObject goGoodFinal;
    [SerializeField, Header("Вставьте объект с плохим финалом")]
    private GameObject goBadFinal;
    [SerializeField, Header("Вставьте объект комнаты куда игрок перейдет при плохой концовке")]
    private GameObject goGoToRoom;
    [SerializeField, Header("Вставить объекты, которые нужно запустить при плохой концовке")]
    private GameObject[] activatedGo;
    [SerializeField, Header("Вставить объекты, которые нужно ДЕактивировать при плохой концовке")]
    private GameObject[] deActivatedGo;

    [SerializeField, Header("Вставьте кнопку =назад=")]
    private MGNuPogodiButtonBack buttonBack;




    //Флаги на концовки
    private bool isGoodFinal = false;
    private bool isBadFinal = false;

    private void Awake()
    {
        Instance = this;
        if (gameObject.activeSelf)
        {
            InitPanelManager();
            InitFinals();
            gameObject.SetActive(false);
        }
    }

    public void AddChoice(bool isGood)
    {
        if (!isGood)
        {
            badChoices++;
            Debug.Log($"Всего негативных выборов - {badChoices}");
        }
        else
        {
            Debug.Log("Сделан хороший выбор");
        }
    }

    private void InitPanelManager()
    {
        butterPanelManager = GetComponent<MG_ButterPanelManager>();
        butterPanelManager.Init();
    }

    public void IsFinal()
    {
        //Плохая концовка
        if (badChoices >= badChoicesForBadFinal)
        {
            isBadFinal = true;
            badChoices = 0;
            StartCoroutine(FinalAnimation(goBadFinal));
        }
        //Хорошая концовка
        else
        {
            isGoodFinal = true;
            badChoices = 0;
            StartCoroutine(FinalAnimation(goGoodFinal));
        }
    }

    private IEnumerator FinalAnimation(GameObject final)
    {
        MG_ButterPanelManager.Instance.goAllFinals.SetActive(true);
        final.SetActive(true);
        yield return new WaitForSeconds(timeAnimation);
        final.SetActive(false);
        if (isBadFinal)
        {
            goGoToRoom.SetActive(true);
            foreach (GameObject go in activatedGo)
            {
                go.SetActive(true);
            }
            foreach (GameObject go in deActivatedGo)
            {
                go.SetActive(false);
            }
            isBadFinal = false;
        }
        if (isGoodFinal)
        {
            buttonBack.Back();
            isGoodFinal = false;
        }
    }

    private void InitFinals()
    {
        if (goGoodFinal != null)
        {
            if (goGoodFinal.activeSelf)
            {
                goGoodFinal.SetActive(false);
            }
        }
        if (goBadFinal != null)
        {
            if (goBadFinal.activeSelf)
            {
                goBadFinal.SetActive(false);
            }
        }
    }
}
