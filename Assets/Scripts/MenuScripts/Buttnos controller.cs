using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Buttnoscontroller : MonoBehaviour
{
    [SerializeField] private GameObject Levels;
    [SerializeField] private GameObject Button;
    [SerializeField] private CanvasGroup Group;
    [SerializeField] private RectTransform scale;
    [SerializeField] private Button[] level;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite disabledSprite;

    void Start()
    {
        Levels.SetActive(false);
        Levelcontroller.lvl = PlayerPrefs.GetInt("level");
        Levelcontroller.lvl = Levelcontroller.lvl < 1 ? 1 : Levelcontroller.lvl;
    }

    public void MainButton()
    {
        StartCoroutine(EnterLevels(Group, scale, 1));
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