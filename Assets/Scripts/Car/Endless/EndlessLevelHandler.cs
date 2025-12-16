using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessLevelHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] SectionsPrefabs;

    [Header("Player")]
    [SerializeField]
    Transform playerCarTransform;

    [Header("Generation")]
    [SerializeField]
    int poolSize = 20;

    [SerializeField]
    int activeSectionsCount = 10;

    [SerializeField]
    float sectionLength = 26f;

    GameObject[] sectionsPool;

    GameObject[] sections;

    WaitForSeconds waitFor100ms;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerCarTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerCarTransform = player.transform;
        }

        if (playerCarTransform == null)
        {
            CarHandler car = FindFirstObjectByType<CarHandler>();
            if (car != null)
                playerCarTransform = car.transform;
        }

        if (playerCarTransform == null)
        {
            Debug.LogError("EndlessLevelHandler: No player transform found. Assign Player Car Transform or tag your car as 'Player'.");
            enabled = false;
            return;
        }

        if (SectionsPrefabs == null || SectionsPrefabs.Length == 0)
        {
            Debug.LogError("EndlessLevelHandler: No section prefabs assigned.");
            enabled = false;
            return;
        }

        poolSize = Mathf.Max(1, poolSize);
        activeSectionsCount = Mathf.Clamp(activeSectionsCount, 1, poolSize);
        sectionLength = Mathf.Max(0.1f, sectionLength);

        sectionsPool = new GameObject[poolSize];
        sections = new GameObject[activeSectionsCount];
        waitFor100ms = new WaitForSeconds(0.1f);

        int prefabIndex = 0;
        for(int i = 0; i < sectionsPool.Length; i++)
        {
            sectionsPool[i] = Instantiate(SectionsPrefabs[prefabIndex]);
            sectionsPool[i].SetActive(false);

            prefabIndex ++;

            if(prefabIndex > SectionsPrefabs.Length - 1)
            {
                prefabIndex = 0;
            }
        }

        for(int i = 0; i < sections.Length ; i++)
        {
            GameObject randomSection = GetRandomSectionFromPool();
            randomSection.transform.position = new Vector3(0f, 0f, i * sectionLength);
            randomSection.SetActive(true);

            sections[i] = randomSection;
        }
        StartCoroutine(UpdateLessOftenCO());
    }
    
    IEnumerator UpdateLessOftenCO()
    {
        while (true)
        {
            yield return waitFor100ms;
            UpdateSectionPosition();
        }
    }

    void UpdateSectionPosition()
    {
        for(int i = 0; i < sections.Length; i++)
        {
            if(sections[i].transform.position.z - playerCarTransform.position.z < -sectionLength)
            {
                Vector3 lastSectionPositions = sections[i].transform.position;
                sections[i].SetActive(false);

                sections[i] = GetRandomSectionFromPool();

                sections[i].transform.position = new Vector3(0f, 0f, lastSectionPositions.z + sectionLength*sections.Length);
                sections[i].SetActive(true);
            }
        }
    }

    GameObject GetRandomSectionFromPool()
    {
        int randomIndex = Random.Range(0, sectionsPool.Length);
        bool isNewSectionFound = false;
        while (!isNewSectionFound)
        {
            if (!sectionsPool[randomIndex].activeInHierarchy)
            {
                isNewSectionFound = true;
            }
            else
            {
                randomIndex ++;
                if(randomIndex > sectionsPool.Length - 1)
                {
                    randomIndex = 0;
                }
            }
        }
        return sectionsPool[randomIndex];
    }
}
