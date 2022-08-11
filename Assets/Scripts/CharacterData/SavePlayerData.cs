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

    public SpriteRenderer[] parts;

    //�÷��̾� ��ġ
    public float playerPositionX;
    public float playerPositionY;


}
