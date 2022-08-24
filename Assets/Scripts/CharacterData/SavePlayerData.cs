using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SavePlayerData
{

    public string _name, desc;
    public int maxhp, hp, maxmp, mp, attack, attackup, magic, magicup, defence, defenceup, speed, speedup;

    //�÷��̾� ���� �̹��� (0.face ~ 9.weaponL)
    public string[] parts;

    //��ų
    public string skillname;
    public string skilldesc;

    //����NPC �ο���
    public int partnerNum;

    //�÷��̾� ��ġ
    public float positionX;
    public float positionY;


}

[Serializable]
public class PlayerList
{
    public SavePlayerData[] playerList;
}