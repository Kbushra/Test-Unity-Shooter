using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoSpawner : MonoBehaviour
{
    public GameObject moundPrefab;
    public GameObject flowerPrefab;
    private GameObject mound;

    void Start()
    {
        int moundCount = Random.Range(5, 8);

        for (int i = 0; i < moundCount; i++)
        {
            int choice = Random.Range(1, 5);
            switch (choice)
            {
                case 1:
                    mound = Instantiate(moundPrefab, new Vector3(Random.Range(-35f, -25f), -5.5f, Random.Range(-30f, 30f)), Quaternion.identity);
                    break;
                case 2:
                    mound = Instantiate(moundPrefab, new Vector3(Random.Range(25f, 35f), -5.5f, Random.Range(-30f, 30f)), Quaternion.identity);
                    break;
                case 3:
                    mound = Instantiate(moundPrefab, new Vector3(Random.Range(-30f, 30f), -5.5f, Random.Range(-35f, -25f)), Quaternion.identity);
                    break;
                case 4:
                    mound = Instantiate(moundPrefab, new Vector3(Random.Range(-30f, 30f), -5.5f, Random.Range(25f, 35f)), Quaternion.identity);
                    break;
            }

            float scale = Random.Range(1f, 2f);
            mound.transform.localScale = new Vector3(scale, 1f, scale);
            mound.transform.GetChild(0).localScale = new Vector3(2f, Random.Range(3f, 10f), 2f);
        }

        int flowerCount = Random.Range(20, 50);

        for (int i = 0; i < flowerCount; i++)
        {
            Instantiate(flowerPrefab, new Vector3(Random.Range(-100, 100), 0.9f, Random.Range(-100, 100)), Quaternion.identity);
        }
    }
}
