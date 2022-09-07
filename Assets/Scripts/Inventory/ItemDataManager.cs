using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemDataManager : MonoBehaviour
{

    Player player;
    ItemDataManager itemData; 
    InventoryUI inventoryUI;

    public ItemDataManager ItemData
    {
        get => itemData;
    }
    public InventoryUI InvenUI => inventoryUI;

    static ItemDataManager instance = null; 
    public static ItemDataManager Inst
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Initialize();
    }

    private void Initialize()
    {
        itemData = GetComponent<ItemDataManager>();

        inventoryUI = FindObjectOfType<InventoryUI>();
        player = FindObjectOfType<Player>();
    }
    public ItemData[] itemDatas;

    public ItemData this[uint i]     // 인덱서. 
    {
        get => itemDatas[i];
    }

    public ItemData this[ItemCode code]   // 인덱서를 통해 편리하게 아이템 종류별 데이터에 접근(enum으로 배열접근하게 변경)
    {
        get => itemDatas[(int)code];
    }

    /// <summary>
    /// 아이템 종류 개수
    /// </summary>
    public int Length
    {
        get => itemDatas.Length;
    }
}
