using UnityEngine;
using UnityEngine.UI;

public class ComponentsBar : MonoBehaviour
{
    [Header("Mouse Manager")]
    [SerializeField] private MouseManager mouseMan;

    [Header("Компоненты для спавна")]
    [SerializeField] private GameObject resistorPrefab;

    [SerializeField] private GameObject switchPrefab;

    [SerializeField] private GameObject amperPrefab;

    [SerializeField] private GameObject voltPrefab;

    [SerializeField] private GameObject diodPrefab;

    [SerializeField] private GameObject lightPrefab;

    [Header("Родитель для новых объектов")]
    [SerializeField] private Transform spawnParent;

    [Header("Координаты появления по умолчанию")]
    [SerializeField] private Vector3 defaultSpawnPosition = new Vector3(0, 0, 0);

    [Header("Картинки кнопок компонентов")]
    [SerializeField] private Image confModeIm;

    [SerializeField] private Image setModeIm;

    [SerializeField] private Image wireModeIm;

    public enum MouseMode { Config, Set, Wire}

    private void Start()
    {
        SetModeSwitch();
    }

    public void SpawnResistor()
    {
        Spawn(resistorPrefab);
    }

    public void SpawmSwitch()
    {
        Spawn(switchPrefab);
    }

    public void SpawmAmper()
    {
        Spawn(amperPrefab);
    }

    public void SpawmVolt()
    {
        Spawn(voltPrefab);
    }

    public void SpawmDiod()
    {
        Spawn(diodPrefab);
    }

    public void SpawmLight()
    {
        Spawn(lightPrefab);
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

    private void SetImageAlpha(Image image, float alpha)
    {
        if (image == null) return;

        Color color = image.color;
        color.a = Mathf.Clamp01(alpha); // Гарантируем значение между 0 и 1
        image.color = color;
    }

    public void WireModeSwitch()
    {
        SetImageAlpha(wireModeIm, 0.8f);
        SetImageAlpha(setModeIm, 0.5f);
        SetImageAlpha(confModeIm, 0.5f);
        mouseMan.mode = MouseManager.MouseMode.Wire;
    }

    public void SetModeSwitch()
    {  
        SetImageAlpha(wireModeIm, 0.5f);
        SetImageAlpha(setModeIm, 0.8f);
        SetImageAlpha(confModeIm, 0.5f);
        mouseMan.mode = MouseManager.MouseMode.Set;
    }

    public void ConfigModeSwitch()
    {
        SetImageAlpha(wireModeIm, 0.5f);
        SetImageAlpha(setModeIm, 0.5f);
        SetImageAlpha(confModeIm, 0.8f);
        mouseMan.mode = MouseManager.MouseMode.Config;
    }
}

