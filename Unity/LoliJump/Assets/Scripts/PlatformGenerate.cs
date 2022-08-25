using UnityEngine;

public class PlatformGenerate : MonoBehaviour
{
    public GameObject platformPrefab;

    void Start()
    {
        Vector3 SpawnerPosition = new Vector3();

        for (int i=0; i < 5; i++ )
        {
            SpawnerPosition.x = Random.Range(-1.7f, 1.7f);
            SpawnerPosition.y += Random.Range(1f, 2.5f);
            // Тут необходимо изменить расстояние между спавнящимися объектами в зависимости от скорости
            Instantiate(platformPrefab, SpawnerPosition, Quaternion.identity);
        }

    }
}
