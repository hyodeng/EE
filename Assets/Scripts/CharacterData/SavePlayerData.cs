using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SavePlayerData
{

    public string _name, desc;
    public int maxhp, hp, maxmp, mp, maxattack, attack, maxmagic, magic, maxdefence, defence, maxspeed, speed;

    //플레이어 착장 이미지 (0.face ~ 9.weaponL)
    //방어구 : 3개까지
    //무기 : 3개까지
    public string[] armor;
    public string[] weapon;
    public string[] parts;

    //스킬
    public string skillname;
    public string skilldesc;

}



