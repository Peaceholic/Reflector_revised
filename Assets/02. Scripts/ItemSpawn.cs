using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour {

    public GameObject immune;
    public GameObject gaugeMult;
    public GameObject healthRegen;

    public GameObject SpawnItem(int range)
    {
        GameObject item;
        int r = Random.Range(0, range);
        if (r == 0){
            item = SpawnItemA();
        } else if (r == 1){
            item = SpawnItemB();
        } else if (r == 2){
            item = SpawnItemC();
        } else {
            item = null;
        }

        return item;
    }

    private GameObject SpawnItemA()
    {
        Vector3 screenPointPos = new Vector3(0, 0, 0);

        int r = Random.Range(0, 3);
        float position1 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;
        float position2 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;

        screenPointPos = new Vector3(position1 * Screen.width, position2 * Screen.height, 10.0f);

        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(screenPointPos);
        GameObject item = Instantiate(immune, screenToWorld, Quaternion.identity);
        item.name = immune.name;
        return item;
    }

    private GameObject SpawnItemB()
    {
        Vector3 screenPointPos = new Vector3(0, 0, 0);

        int r = Random.Range(0, 3);
        float position1 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;
        float position2 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;

        screenPointPos = new Vector3(position1 * Screen.width, position2 * Screen.height, 10.0f);

        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(screenPointPos);
        GameObject item = Instantiate(gaugeMult, screenToWorld, Quaternion.identity);
        item.name = gaugeMult.name;
        return item;
    }

    private GameObject SpawnItemC()
    {
        Vector3 screenPointPos = new Vector3(0, 0, 0);

        int r = Random.Range(0, 3);
        float position1 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;
        float position2 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;

        screenPointPos = new Vector3(position1 * Screen.width, position2 * Screen.height, 10.0f);

        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(screenPointPos);
        GameObject item = Instantiate(healthRegen, screenToWorld, Quaternion.identity);
        item.name = healthRegen.name;
        return item;
    }

}
