using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SavePlayerData
{

    public string _name, desc;
    public int maxhp, hp, maxmp, mp, attack, attackup, magic, magicup, defence, defenceup, speed, speedup;

    //플레이어 착장 이미지 (0.face ~ 9.weaponL)
    public string[] parts;

    //스킬
    public string skillname;
    public string skilldesc;

    //동료NPC 인원수
    public int partnerNum;

    //플레이어 위치
    public float positionX;
    public float positionY;


}

[Serializable]
public class PlayerList
{
    public SavePlayerData[] playerList;
}