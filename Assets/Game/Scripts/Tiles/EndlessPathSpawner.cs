﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPathSpawner : MonoBehaviour
{
    public List<GameObject> pathPrefabs;
    public float spawnDistance = 10f;
    public float speed = 5f;
    public int initialTiles = 3;

    private List<GameObject> spawnedPaths = new List<GameObject>();
    private float lastSpawnPositionX;

    void Start()
    {
        for (int i = 0; i < initialTiles; i++)
        {
            SpawnPath(i * spawnDistance);
        }
    }

    void Update()
    {
        for (int i = 0; i < spawnedPaths.Count; i++)
        {
            if (spawnedPaths[i] != null)
            {
                spawnedPaths[i].transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }

        if (spawnedPaths[spawnedPaths.Count - 1].transform.position.x < lastSpawnPositionX - spawnDistance)
        {
            SpawnPath(lastSpawnPositionX);
        }

        if (spawnedPaths[0] != null)
        {
            if (spawnedPaths.Count > 0 && spawnedPaths[0].transform.position.x < -25f)
            {
                Destroy(spawnedPaths[0]);
            }
        }
        else
        {
            spawnedPaths.RemoveAt(0);
        }
    }

    void SpawnPath(float spawnPositionX)
    {
        int randomIndex = Random.Range(0, pathPrefabs.Count);
        GameObject newPath = Instantiate(pathPrefabs[randomIndex], new Vector2(spawnPositionX, transform.position.y), Quaternion.identity);
        newPath.transform.SetParent(transform);

        spawnedPaths.Add(newPath);
        lastSpawnPositionX = spawnPositionX;
    }
}
