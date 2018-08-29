using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEffect : MonoBehaviour {

    // Immune item take -> character changed to white with flicker
    // character position = item spawn position (if immune item)

    public static float T = 0;

    public static IEnumerator UnBeatTime(SpriteRenderer rend, float speed)
    {
        float counttheTime = 0;
        float limit = 10;
        float dt = Time.deltaTime;

        while (counttheTime < limit)
        {
            float minimum = 0;
            float maximum = 0.8f;
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

    public static void SetToNormal(SpriteRenderer rend) {
        rend.material.SetFloat("_ShineLocation", 0);
        T = 0;
    }
}
