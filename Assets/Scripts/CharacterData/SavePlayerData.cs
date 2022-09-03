using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SavePlayerData
{

    public string _name, desc;
    public int maxhp, hp, maxmp, mp, maxattack, attack, maxmagic, magic, maxdefence, defence, maxspeed, speed;

    public string armor;
    public string weapon;

    //�÷��̾� ���� �̹��� (0.face ~ 9.weaponL)
    public string[] parts;

    //��ų
    public string skillname;
    public string skilldesc;

    //����NPC �ο���
    public int partnerNum;



}

[Serializable]
public class PlayerList
{

}