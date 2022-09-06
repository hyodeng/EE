using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//사용할고 했더니 안되네... 나중에 찾아서 고민
[Serializable]
public class PartsImage
{
    public Sprite[] partsSprites; //0
    //public string hair;
    //public string beard;
    //public string cloth3;
    //public string cloth4;
    //public string cloth5;
    //public string foot_r;
    //public string foot_l;
    //public string weaponR;
    //public string weaponL; //9

    public Color facecolor;
    public Color haircolor;
    public Color beardcolor;
}


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
    string pparts;

    //무식한 방법.... 색상 셋팅
    string[] name = new string[10];
    string facecolor;
    string haircolor;
    string beardcolor;
    
    Color[] temp = new Color[3];
    float[] R = new float[3];
    float[] G = new float[3];
    float[] B = new float[3];
    float[] A = new float[3];


    private void Start()
    {
        custom = FindObjectOfType<Customized>();

        pparts = File.ReadAllText(Application.dataPath + "/Resources/Json/" + "/PlayerParts.json");
        parts = JObject.Parse(pparts);

        LoadPlayerParts();
    }

    void LoadPlayerParts()
    {
        for (int i = 0; i < 10; i++)
        {
            name[i] = parts[$"{i}"].ToString();
            if (name[i] != "")
            {
                custom.SetParts(i, name[i]);              
            }
        }

        facecolor = parts["color0"].ToString();
        haircolor = parts["color1"].ToString();
        beardcolor = parts["color2"].ToString();

        float.TryParse(facecolor.Substring(5, 5), out R[0]);
        float.TryParse(facecolor.Substring(12, 5), out G[0]);
        float.TryParse(facecolor.Substring(19, 5), out B[0]);
        float.TryParse(facecolor.Substring(26, 5), out A[0]);
        Debug.Log(facecolor.Substring(26, 5));

        float.TryParse(haircolor.Substring(5,5), out R[1]);
        float.TryParse(haircolor.Substring(12,5), out G[1]);
        float.TryParse(haircolor.Substring(19,5), out B[1]);
        float.TryParse(haircolor.Substring(26,5), out A[1]);

        float.TryParse(beardcolor.Substring(5,5), out R[2]);
        float.TryParse(beardcolor.Substring(12,5), out G[2]);
        float.TryParse(beardcolor.Substring(19,5), out B[2]);
        float.TryParse(beardcolor.Substring(26,5), out A[2]);


        //face, hair, beard 색상
        for (int i = 0; i<3; i++)
        {
            temp[i] = new(R[i], G[i], B[i], A[i]);
            Debug.Log(temp[i].ToString());
            custom.parts[i].color = temp[i];
        }
    }

    //5, 12 , 19, 26
  //"color0": "RGBA(0.270, 0.972, 0.864, 1.000)",
  //"color1": "RGBA(0.176, 0.141, 0.751, 1.000)",
  //"color2": "RGBA(0.000, 0.000, 0.000, 0.000)"

}