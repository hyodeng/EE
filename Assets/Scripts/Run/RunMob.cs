using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunMob : MonoBehaviour
{
    public Transform[] bgSlots = null;
    public float slcollingSpeed = 2.5f;

    const float BG_Width = 19.0f;





    private void Update()
    {
        float minusX = transform.position.x - BG_Width; // 백그라운드의 x위치에서 왼쪽으로 BG_Width (그림 한장의 폭) 만큼 이동한 위치
        foreach (Transform bgSlot in bgSlots)
        {

            bgSlot.Translate(-transform.right * slcollingSpeed * Time.deltaTime);
           
        }
    }
    }
