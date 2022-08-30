using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraMove : MonoBehaviour
{
    public bool isShake = false;

    public float elapsedTime = 0.0f;    //경과시간
    public float movingTime = 0.5f;     //무빙시간
    public AnimationCurve curve;

    //------------------------------
    public float strength = 100f;
    [SerializeField]
    Vector3 offset = Vector3.zero;
    Quaternion originRot;
    WaitForSeconds delayTime = new WaitForSeconds(0.2f);

    private void Start()
    {
        originRot = transform.rotation;
    }

    private void Update()
    {
        //테스트 : 인스펙터 창에서 true 체크하면 실행
        if (isShake)
        {
            StartCoroutine(CameraShaking());

        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ResetRotation());
        }

    }


    IEnumerator CameraShaking()
    {
        Vector3 euler = transform.eulerAngles;

        while (true)
        {
            float rotX = Random.Range(-offset.x, offset.x);
            float rotY = Random.Range(-offset.y, offset.y);
            float rotZ = Random.Range(-offset.z, offset.z);

            Vector3 randomRot = euler + new Vector3(rotX, rotY, rotZ);
            Quaternion rot = Quaternion.Euler(randomRot);

            if (Quaternion.Angle(transform.rotation, rot) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, strength * Time.deltaTime);
                yield return delayTime;
            }
            yield return delayTime;
        }
    }

    IEnumerator ResetRotation()
    {
        while (Quaternion.Angle(transform.rotation, originRot) > 0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, originRot, strength * Time.deltaTime);
            yield return delayTime;
        }
        yield return delayTime;
    }


    //IEnumerator CameraShaking()
    //{
    //    Vector3 startPosition = transform.position;

    //    if (elapsedTime < movingTime)
    //    {
    //        elapsedTime += Time.deltaTime;

    //        //흔들림 강도
    //        float strength = curve.Evaluate(elapsedTime / movingTime);

    //        //Vector3 dis = new Vector3(Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f), 0);

    //        transform.position = startPosition + Random.insideUnitSphere * strength;

    //        Debug.Log(transform.position);
    //        //Debug.Log(Random.insideUnitSphere);

    //        yield return null;
    //    }

    //    transform.position = startPosition;
    //}

}
