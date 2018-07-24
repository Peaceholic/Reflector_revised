using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEffect : MonoBehaviour {
    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.material.SetFloat("_ShineLocation", 0);
    }

    // Immune item take -> character changed to white with flicker
    // character position = item spawn position (if immune item)

    StartCoroutine("UnBeatTime");

    IEnumerator UnBeatTime()
    {
        float counttheTime = 0;
        float limit = 10;
        float dt = Time.deltaTime;
        float speed = 1;

        while (counttheTime < limit)
        {
            float minimum = 0;
            float maximum = 0.6;
            T += dt * speed;
            rend.material.SetFloat("_ShineLocation", Mathf.Lerp(minimum, maximum, T));
            if (T > maximum)
            {
                dt = -dt;
            }
            if (T < minimum)
            {
                dt = -dt;
            }
            counttheTime += Time.deltaTime;
            yield return null;
        }
    }
}
