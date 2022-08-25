using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavePlayerData
{
    public string _name;
    public int hp;
    public int mp;
    public int attack;
    public int magic;
    public int defence;
    public float speed;

    //플레이어 착장 이미지 (0.face ~ 9.weaponL)
    public string[] parts;


    //플레이어 위치
    public float playerPositionX;
    public float playerPositionY;


}
