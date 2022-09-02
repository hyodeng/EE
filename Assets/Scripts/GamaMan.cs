using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaMan : MonoBehaviour
{
    static GamaMan instance = null;
    public static GamaMan Inst => instance;
    public CharacterData[] userPartyData = new CharacterData[4];
    public string[,] partsName = new string[10, 4];
    public Sprite[,] temp = new Sprite[10, 4];
    public Color[,] partsColor = new Color[3, 4];
    public bool[] userPartyCheck = new bool[4];
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
    }

}
