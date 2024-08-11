using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    public GameObject[] tilesPrefab;
    public Transform spawnPoint;
    public float timeSpawn;
    void Start()
    {
        StartCoroutine(SpawnObjectRoutine());
    }

    IEnumerator SpawnObjectRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeSpawn);

            int randomIndex = Random.Range(0, tilesPrefab.Length);
            GameObject selectedObject = tilesPrefab[randomIndex];

            GameObject spawnedObject = Instantiate(selectedObject, spawnPoint.position, spawnPoint.rotation);

            spawnedObject.transform.SetParent(transform);
        }
    }
}
