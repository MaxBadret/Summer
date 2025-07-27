using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevels : MonoBehaviour
{
    public void LoadScene(Button button)
    {
        string buttonName = button.name;
        
        SceneManager.LoadScene(int.Parse(buttonName));
    }
}
