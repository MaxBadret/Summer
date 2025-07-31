using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Buttnoscontroller : MonoBehaviour
{
    [Header("Названия уровней в порядке")]
    public List<string> levelNames = new();

    [SerializeField] private GameObject Levels;
    [SerializeField] private GameObject Button;
    private CanvasGroup group;
    private RectTransform scale;
    [SerializeField] private Button[] level;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite disabledSprite;

    void Awake()
    {
        LevelProgressManager.CreateOrUpdateProgressFile(levelNames);
    }

    void Start()
    {
        group = Levels.GetComponent<CanvasGroup>();
        scale = Levels.GetComponent<RectTransform>();
        Levels.SetActive(false);
    }

    public void MainButton()
    {
        StartCoroutine(EnterLevels(group, scale, 1));
        Button.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public IEnumerator EnterLevels(CanvasGroup group, RectTransform scale, float x)
    {
        group.alpha = 0;
        Levels.SetActive(true);

        Levelcontroller.LevelUpdate(level, activeSprite, disabledSprite);

        float duration = 0.5f;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            group.alpha = Mathf.Lerp(0, 1, t / duration);
            scale.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t / duration);
            yield return null;
        }

        group.alpha = 1;
        scale.localScale = Vector3.one;
    }
}
