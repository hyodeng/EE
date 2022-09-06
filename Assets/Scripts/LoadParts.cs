using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LoadParts : MonoBehaviour
{
    public SpriteRenderer[] partsImage = null;

    public GameObject player;
    GameObject partner1;
    GameObject partner2;
    GameObject partner3;

    public JObject parts = new JObject();
    //public JObject part = new JObject();
    Customized custom;

    private void Awake()
    {

    }

    private void Start()
    {
        custom = FindObjectOfType<Customized>();

        string pparts = File.ReadAllText(Application.dataPath + "/Resources/Json/" + "/PlayerParts.json");
        parts = JObject.Parse(pparts);


        //LoadPlayer();
    }


    //void LoadPlayer()
    //{
    //    string temp = part["parts"].ToString();

    //    Debug.Log(temp);

    //    //for (int i = 0; i < 9; i++)
    //    //{
    //    //    custom.SetParts(i, parts["parts"][$"{i}"].Value<string>().ToString());
    //    //}
    //}


    //public void OtherSetParts(int i, string _name)
    //{
    //    //Debug.Log(_name);

    //    switch (i)
    //    {
    //        case 0:
    //            partsImage[0].sprite = MakeSprite(System.IO.Directory.GetFiles(Application.streamingAssetsPath + "/Character/Face", _name)[0]);
    //            partsImage[0].color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    //            break;
    //        case 1:

    //            partsImage[1].sprite = MakeSprite(System.IO.Directory.GetFiles(Application.streamingAssetsPath + "/Character/Hair", _name)[0]);
    //            partsImage[1].color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    //            break;
    //        case 2:

    //            partsImage[2].sprite = MakeSprite(System.IO.Directory.GetFiles(Application.streamingAssetsPath + "/Character/Beard", _name)[0]);
    //            partsImage[2].color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    //            break;
    //        case 3:
    //            for (int j = 0; j < Resources.LoadAll<Sprite>($"Character/Armor/{_name.Replace(".png", "")}").Length; j++)
    //            {
    //                partsImage[j + 3].sprite = Resources.LoadAll<Sprite>($"Character/Armor/{_name.Replace(".png", "")}")[j];

    //            }
    //            break;
    //        case 4:
    //            for (int j = 0; j < Resources.LoadAll<Sprite>($"Character/Pant/{_name.Replace(".png", "")}").Length; j++)
    //            {
    //                partsImage[j + 6].sprite = Resources.LoadAll<Sprite>($"Character/Pant/{_name.Replace(".png", "")}")[j];

    //            }
    //            break;
    //        case 5:
    //            partsImage[8].sprite = Resources.Load<Sprite>($"Character/Weapons/{_name.Replace(".png", "")}");

    //            break;
    //        case 6:
    //            partsImage[9].sprite = Resources.Load<Sprite>($"Character/Weapons/{_name.Replace(".png", "")}");

    //            break;
    //    }
    //}
    //public Sprite MakeSprite(string filePath)
    //{
    //    byte[] pngBytes = System.IO.File.ReadAllBytes(filePath);
    //    Texture2D tex = new(2, 2)
    //    {
    //        filterMode = FilterMode.Point
    //    };
    //    tex.LoadImage(pngBytes);

    //    Sprite fromTex = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0f), 100);
    //    return fromTex;
    //}




}