using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject m_loadingPanel;
    [SerializeField] private CutSceneManager m_cutSceneManager;

    public void StartGame()
    {
        GameManager.LoadFromMenu();

        StartCoroutine(LoadPlayScene());
    }

    public void Retry()
    {
        StartCoroutine(LoadPlayScene());
    }

    private IEnumerator LoadPlayScene()
    {
        // カットアウト開始
        m_cutSceneManager.LoadCutScene_Out();

        // カットアウトが終わるまで待つ
        yield return new WaitForSeconds(1.0f);

        // カットインしてローディングへ
        m_cutSceneManager.LoadCutScene_In();

        // ローディングパネルを表示
        m_loadingPanel.SetActive(true);

        // ローディング画面を2秒表示
        yield return new WaitForSeconds(2.0f);

        // カットアウトしてゲームシーンへ
        m_cutSceneManager.LoadCutScene_Out();

        // カットアウト1秒表示
        yield return new WaitForSeconds(1.0f);

        // シーン遷移
        SceneManager.LoadScene("Stage_Real_Work");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void GotoResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}