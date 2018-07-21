using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour {

    public GameObject immune;
    public GameObject gaugeMult;
    public GameObject healthRegen;

    public void SpawnItem(int range)
    {
        int r = Random.Range(0, range);
        if (r == 0){
            SpawnItemA();
        } else if (r == 1){
            SpawnItemB();
        } else if (r == 2){
            SpawnItemC();
        }
    }

    private void SpawnItemA()
    {
        Vector3 screenPointPos = new Vector3(0, 0, 0);

        int r = Random.Range(0, 3);
        float position1 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;
        float position2 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;

        screenPointPos = new Vector3(position1 * Screen.width, position2 * Screen.height, 10.0f);

        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(screenPointPos);
        Instantiate(immune, screenToWorld, Quaternion.identity);
    }

    private void SpawnItemB()
    {
        Vector3 screenPointPos = new Vector3(0, 0, 0);

        int r = Random.Range(0, 3);
        float position1 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;
        float position2 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;

        screenPointPos = new Vector3(position1 * Screen.width, position2 * Screen.height, 10.0f);

        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(screenPointPos);
        Instantiate(gaugeMult, screenToWorld, Quaternion.identity);
    }

    private void SpawnItemC()
    {
        Vector3 screenPointPos = new Vector3(0, 0, 0);

        int r = Random.Range(0, 3);
        float position1 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;
        float position2 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;

        screenPointPos = new Vector3(position1 * Screen.width, position2 * Screen.height, 10.0f);

        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(screenPointPos);
        Instantiate(healthRegen, screenToWorld, Quaternion.identity);
    }

}
