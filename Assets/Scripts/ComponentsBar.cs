using UnityEngine;

public class ComponentsBar : MonoBehaviour
{
    [Header("Компоненты для спавна")]
    [SerializeField] private GameObject resistorPrefab;

    [Header("Родитель для новых объектов")]
    [SerializeField] private Transform spawnParent;

    [Header("Координаты появления по умолчанию")]
    [SerializeField] private Vector3 defaultSpawnPosition = new Vector3(0, 0, 0);


    public void SpawnResistor()
    {
        Spawn(resistorPrefab);
    }

    private void Spawn(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("❌ Префаб не назначен в инспекторе!");
            return;
        }

        GameObject instance = Instantiate(prefab, defaultSpawnPosition, Quaternion.identity, spawnParent);
        //Debug.Log($"✅ Спавн: {prefab.name}");
    }
}

