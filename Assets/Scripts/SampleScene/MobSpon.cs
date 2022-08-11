using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpon : MonoBehaviour
{
    public GameObject Mob = null;
    public float spawnInternal = 1.0f;
    public float randomRange = 8.0f;
    protected WaitForSeconds waitSecond = null;

    private void Start()
    {
        waitSecond = new WaitForSeconds(spawnInternal);
        StartCoroutine(Spawn());
    }
    public virtual IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInternal);
            GameObject obj = Instantiate(Mob);
            obj.transform.position = this.transform.position;
            obj.transform.Translate(Vector3.right * Random.Range(0.0f, randomRange));
        }
    }
}
