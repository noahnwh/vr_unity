using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class MunuStartUI : MonoBehaviour
{
    [SerializeField] GameObject xrRig;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject guideCanvas; // optional, drag your guide panel here

    void Awake()
    {
        if (menuPanel) menuPanel.SetActive(true);
        if (xrRig) xrRig.SetActive(false);
        if (guideCanvas) guideCanvas.SetActive(false);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartExperience()
    {
        Time.timeScale = 1f;

        if (menuPanel) menuPanel.SetActive(false);
        if (xrRig) xrRig.SetActive(true);
        if (guideCanvas) guideCanvas.SetActive(true);  // show guide

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
