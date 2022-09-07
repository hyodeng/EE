using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Customized : MonoBehaviour
{
    public SpriteRenderer[] parts = null;

    private void Start()
    {
        Init();
    }
    void Init()
    {
        if (name.IndexOf("Character") > -1)
        {
            GameManager.Inst.userPartyCheck[System.Convert.ToInt32(gameObject.name.Replace("Character", ""))] = true;
            AsyncParts(System.Convert.ToInt32(gameObject.name.Replace("Character", "")));
        }
    }
    public void SetParts(int i, string _name)
    {
        int index = System.Convert.ToInt32(gameObject.name.Replace("Character", ""));
        switch (i)
        {
            case 0:
                parts[0].sprite = MakeSprite(Directory.GetFiles(Application.streamingAssetsPath + "/Character/Face", _name)[0]);
                GameManager.Inst.partsColor[i, index] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                parts[0].color = GameManager.Inst.partsColor[i, index];
                GameManager.Inst.temp[i, index] = parts[i].sprite;
                GameManager.Inst.partsName[0] = _name;
                break;
            case 1:
                parts[1].sprite = MakeSprite(Directory.GetFiles(Application.streamingAssetsPath + "/Character/Hair", _name)[0]);
                GameManager.Inst.partsColor[i, index] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                parts[1].color = GameManager.Inst.partsColor[i, index];
                GameManager.Inst.temp[i, index] = parts[i].sprite;
                GameManager.Inst.partsName[1] = _name;
                break;
            case 2:
                parts[2].sprite = MakeSprite(Directory.GetFiles(Application.streamingAssetsPath + "/Character/Beard", _name)[0]);
                GameManager.Inst.partsColor[i, index] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                parts[2].color = GameManager.Inst.partsColor[i, index];
                GameManager.Inst.temp[i, index] = parts[i].sprite;
                GameManager.Inst.partsName[2] = _name;
                break;
            case 3:
                for (int j = 0; j < Resources.LoadAll<Sprite>($"Character/Armor/{_name.Replace(".png", "")}").Length; j++)
                {
                    parts[j + 3].sprite = Resources.LoadAll<Sprite>($"Character/Armor/{_name.Replace(".png", "")}")[j];
                    GameManager.Inst.temp[j + 3, index] = parts[j + 3].sprite;
                    GameManager.Inst.temp[j+3,  index] = parts[j+3].sprite;
                    GameManager.Inst.partsName[j + 3] = _name;
                }
                break;
            case 4:
                for (int j = 0; j < Resources.LoadAll<Sprite>($"Character/Pant/{_name.Replace(".png", "")}").Length; j++)
                {
                    parts[j + 6].sprite = Resources.LoadAll<Sprite>($"Character/Pant/{_name.Replace(".png", "")}")[j];
                    GameManager.Inst.temp[j + 6, index] = parts[j + 6].sprite;
                    GameManager.Inst.temp[j + 6, index] = parts[j + 6].sprite;
                    GameManager.Inst.partsName[j+6] = _name;

                }
                break;
            case 5:
                parts[8].sprite = Resources.Load<Sprite>($"Character/Weapons/{_name.Replace(".png", "")}");
                GameManager.Inst.temp[i, index] = parts[i].sprite;
                GameManager.Inst.partsName[8] = _name;
                break;
            case 6:
                parts[9].sprite = Resources.Load<Sprite>($"Character/Weapons/{_name.Replace(".png", "")}");
                GameManager.Inst.temp[i, index] = parts[i].sprite;
                GameManager.Inst.partsName[9] = _name;
                break;
        }
        GameManager.Inst.temp[i, index] = parts[i].sprite;
    }

    public Sprite MakeSprite(string filePath)
    {
        byte[] pngBytes = File.ReadAllBytes(filePath);
        Texture2D tex = new(2, 2)
        {
            filterMode = FilterMode.Point
        };
        tex.LoadImage(pngBytes);

        Sprite fromTex = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0f), 100);
        fromTex.name = filePath.Split(@"\")[^1].Replace(".png", "");
        return fromTex;
    }
    // 0: 얼굴, 1: 헤어, 2: 수염, 3: 아머, 4: 바지, 5:오른쪽 무기, 6 : 왼손 무기
    public void RandomParts(int i)
    {
        DirectoryInfo directoryInfo;
        switch (i)
        {
            case 0:
                directoryInfo = new(Application.streamingAssetsPath + "/Character/Face");
                SetParts(i, directoryInfo.GetFiles("*.png", SearchOption.AllDirectories)[Random.Range(0, directoryInfo.GetFiles("*.png", SearchOption.AllDirectories).Length)].Name);
                break;
            case 1:
                directoryInfo = new(Application.streamingAssetsPath + "/Character/Hair");
                SetParts(i, directoryInfo.GetFiles("*.png", SearchOption.AllDirectories)[Random.Range(0, directoryInfo.GetFiles("*.png", SearchOption.AllDirectories).Length)].Name);
                break;
            case 2:
                directoryInfo = new(Application.streamingAssetsPath + "/Character/Beard");
                SetParts(i, directoryInfo.GetFiles("*.png", SearchOption.AllDirectories)[Random.Range(0, directoryInfo.GetFiles("*.png", SearchOption.AllDirectories).Length)].Name);
                break;
            case 3:
                directoryInfo = new(Application.dataPath + "/Resources/Character/Armor");
                SetParts(i, directoryInfo.GetFiles("*.png", SearchOption.AllDirectories)[Random.Range(0, directoryInfo.GetFiles("*.png", SearchOption.AllDirectories).Length)].Name);
                break;
            case 4:
                directoryInfo = new(Application.dataPath + "/Resources/Character/Pant");
                SetParts(i, directoryInfo.GetFiles("*.png", SearchOption.AllDirectories)[Random.Range(0, directoryInfo.GetFiles("*.png", SearchOption.AllDirectories).Length)].Name);
                break;
            case 5:
                directoryInfo = new(Application.dataPath + "/Resources/Character/Weapons");
                SetParts(i, directoryInfo.GetFiles("*.png", SearchOption.AllDirectories)[Random.Range(0, directoryInfo.GetFiles("*.png", SearchOption.AllDirectories).Length)].Name);
                break;
            case 6:
                directoryInfo = new(Application.dataPath + "/Resources/Character/Weapons");
                SetParts(i, directoryInfo.GetFiles("*.png", SearchOption.AllDirectories)[Random.Range(0, directoryInfo.GetFiles("*.png", SearchOption.AllDirectories).Length)].Name);
                break;
        }
    }
    public void AsyncParts(int index)
    {
        for (int i = 0; i < parts.Length; i++)
        {
            if (GameManager.Inst.temp[i, index] != null)
            {
                parts[i].sprite = GameManager.Inst.temp[i, index];
            }
        }
        parts[0].color = GameManager.Inst.partsColor[0, index];
        parts[1].color = GameManager.Inst.partsColor[1, index];
        parts[2].color = GameManager.Inst.partsColor[2, index];
    }
}