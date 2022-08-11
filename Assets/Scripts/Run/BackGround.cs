using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform[] bgSlots = null;
    public float slcollingSpeed = 2.5f;

    public Transform[] bgbubbles = null;
    public float BubbleSpeed = 5.0f;
    private SpriteRenderer[] bubbleRenderer = null;

    
    public float FishSpeed = 10.0f;

    const float BG_Width = 19.0f;


    private void Awake()
    {
        bubbleRenderer = new SpriteRenderer[bgbubbles.Length];
        for (int i = 0; i < bgbubbles.Length; i++)
        {
            bubbleRenderer[i] = bgbubbles[i].GetComponent<SpriteRenderer>();
        }
    }


    private void Update()
    {
        float minusX = transform.position.x - BG_Width; // 백그라운드의 x위치에서 왼쪽으로 BG_Width (그림 한장의 폭) 만큼 이동한 위치
        foreach (Transform bgSlot in bgSlots)
        {

            bgSlot.Translate(-transform.right * slcollingSpeed * Time.deltaTime);
            if (bgSlot.position.x < minusX)
            {
                // 충분히 왼쪽으로 이동한 위치
                Debug.Log("충분히 왼쪽이다.");
                bgSlot.Translate(transform.right * BG_Width * 1.5f); //오른쪽으로 BG_Width의 2배만큼 이동
            }
        }

  


      

    }
}
