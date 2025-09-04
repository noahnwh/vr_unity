using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class MenuStartUI : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;    // your main menu Panel (with Start/Quit)
    [SerializeField] GameObject guideCanvas;  // drag GuideCanvas here

    void Awake()
    {
        if (menuPanel) menuPanel.SetActive(true);
        if (guideCanvas) guideCanvas.SetActive(false);  // hidden at start

        Time.timeScale = 0f;                   // pause world while menu shows
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartExperience()
    {
        Time.timeScale = 1f;

        if (menuPanel) menuPanel.SetActive(false);      // hide main menu
        if (guideCanvas) guideCanvas.SetActive(true);   // show guide

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
