using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour {

    public GameObject itemAPrefab;
    public GameObject itemBPrefab;
    public GameObject itemCPrefab;

    public void SpawnMonster()
    {
        int r = Random.Range(0, 3);
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
        Instantiate(itemAPrefab, screenToWorld, Quaternion.identity);
    }

    private void SpawnItemB()
    {
        Vector3 screenPointPos = new Vector3(0, 0, 0);

        int r = Random.Range(0, 3);
        float position1 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;
        float position2 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;

        screenPointPos = new Vector3(position1 * Screen.width, position2 * Screen.height, 10.0f);

        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(screenPointPos);
        Instantiate(itemBPrefab, screenToWorld, Quaternion.identity);
    }

    private void SpawnItemC()
    {
        Vector3 screenPointPos = new Vector3(0, 0, 0);

        int r = Random.Range(0, 3);
        float position1 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;
        float position2 = (float)(Random.Range(2, 6) + 1.5) / 10.0f;

        screenPointPos = new Vector3(position1 * Screen.width, position2 * Screen.height, 10.0f);

        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(screenPointPos);
        Instantiate(itemCPrefab, screenToWorld, Quaternion.identity);
    }

}
