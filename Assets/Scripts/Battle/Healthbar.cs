using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject health;

    public void SetHP(float hpNormalized)
    {
        health.transform.localScale = new Vector3(hpNormalized, 1f);
    }

    public IEnumerator SetHPSmooth(float newHP)
    {
        float curHP = health.transform.localScale.x;
        float changeAmt = curHP - newHP; // Corrected variable name

        while (curHP - newHP > Mathf.Epsilon) // Corrected syntax and variable name
        {
            curHP -= changeAmt * Time.deltaTime; // Corrected variable name
            health.transform.localScale = new Vector3(curHP, 1f);
            yield return null;
        }
        health.transform.localScale = new Vector3(newHP, 1f);
    }
}

