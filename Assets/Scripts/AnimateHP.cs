using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateHP : MonoBehaviour
{
    private float maxScale = 3.52f;

    public void ScaleHP(float maxHP, float currentHP)
    {
        if (maxHP == currentHP)
        {
            gameObject.SetActive(false);
        }
        else if (currentHP <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            transform.GetChild(0).transform.localScale = new Vector3(
                (currentHP / maxHP) * maxScale,
                0.8f,
                1
            );
        }
    }
}
