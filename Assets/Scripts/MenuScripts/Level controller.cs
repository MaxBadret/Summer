using UnityEngine;
using UnityEngine.UI;

public class Levelcontroller
{
    private static int lvl = 0;

    public static void LevelUpdate(Button[] arr, Sprite activeSprite, Sprite disabledSprite)
    {
        lvl = LevelProgressManager.GetCompletedLevelCount();
        for (int i = 0; i < arr.Length; i++)
        {
            Debug.Log("Current lvl: " + lvl);
            if (i <= lvl)
            {
                arr[i].interactable = true;
                arr[i].image.sprite = activeSprite;
                Debug.Log("True but" + i);
            }
            else
            {
                arr[i].interactable = false;
                arr[i].image.sprite = disabledSprite;
                Debug.Log("False but" + i);
            }
        }
    }
}
