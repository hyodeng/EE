using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Puzzle : MonoBehaviour
{
    const int width = 3;
    const int height = 3;
    public SpriteRenderer[] Tiles = new SpriteRenderer[width * height - 1];
    public Vector2[] puzzleVector = new Vector2[9];

    public GameObject gameClear = null;

    public void Awake()
    {
        PuzzleSetup();
    }
    public void PuzzleSetup()
    {
        List<float> x = new List<float>(); 
        x.Add(-1);
        x.Add(0);
        x.Add(1);

        List<float> y = new List<float>();
        y.Add(-1);
        y.Add(0);
        y.Add(1);
        for (int i = 0; i < x.Count; i++)
        {
            var rand1 = Random.Range(0, x.Count);
            var rand2 = Random.Range(0, x.Count);

            var temp = x[rand1];
            x[rand1] = x[rand2];
            x[rand2] = temp;
        }
        for (int i = 0; i < y.Count; i++)
        {
            var rand1 = Random.Range(0, x.Count);
            var rand2 = Random.Range(0, x.Count);
            (x[rand2], x[rand1]) = (x[rand1], x[rand2]);
        }
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                puzzleVector[i * 3 + j] = new Vector2(x[j], y[i]);
                if (i * j == 4)
                {
                    return;
                }
                Tiles[i * 3 + j] = transform.GetChild(i * 3 + j).GetComponent<SpriteRenderer>();
                Tiles[i * 3 + j].transform.position = new Vector2(x[j], y[i]);
            }
        }
    }
    public void Update()
    {
        if (Keyboard.current.anyKey.ReadValue() == 1)
        {
            for (int i = 0; i < puzzleVector.Length; i++)
            {
                puzzleVector[i] = new Vector2(-1 + i % 3, 1 - i / 3);
                if (i == 8)
                {
                    return;
                }
                Tiles[i].transform.position = puzzleVector[i];
            }
        }
        if (Mouse.current.leftButton.ReadValue() == 1)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (hit.collider != null && hit.collider.name == "Tile")
            {
                if (Mathf.Abs(puzzleVector[8].x - hit.collider.transform.position.x) + Mathf.Abs(puzzleVector[8].y - hit.collider.transform.position.y) == 1)
                {
                    var temp = hit.collider.transform.position;
                    hit.collider.transform.position = puzzleVector[8];
                    puzzleVector[8] = temp;
                    PuzzleCheck();
                }
            }
        }
    }
    public void PuzzleCheck()
    {
        bool[] check = new bool[puzzleVector.Length];
        for (int i = 0; i < puzzleVector.Length; i++)
        {
            if (puzzleVector[i] == new Vector2(-1 + i % 3, 1 - i / 3))
            {
                check[i] = true;
            }
        }
        for (int i = 0; i < check.Length; i++)
        {
            if (!check[i])
            {
                return;
            }
        }
        Debug.Log("우승");

        Destroy(this.gameObject);

        gameClear.SetActive(true);
        //아마 여기서 보상을 주면 되지 않을까, 끝나면 명진씨랑 합쳐
    }
}
