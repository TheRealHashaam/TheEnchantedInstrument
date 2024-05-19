using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public SnakeCharmer Player;
    public List<GameObject> Snakes;
    public Image FadePanel;
    public GameObject GamePlayUI;
    public GameObject MainMenuPanel;
    public GameObject MainmenuCamera;
    public GameObject PlayerCamera;
    public AudioSource ClickSound;
    public GameObject FailedPanel;
    public GameObject CompletePanel;
    public GameObject CompleteCamera;
    private void Start()
    {
        if (PlayerPrefs.GetInt("Again") == 0)
        {
            OpenMainMenu();
        }
        else
        {
            FadeInOut(0);
            StartGame();
        }
    }

    public void StartGame()
    {
        Player.playerInput.enabled = true;
        Player.starterInputs.SetCursorState(true);
        GamePlayUI.SetActive(true);
        PlayerCamera.SetActive(true);
        MainMenuPanel.SetActive(false);
        MainmenuCamera.SetActive(false);
        ClickSound.Play();
    }

    public void LevelFailed()
    {
        FailedPanel.SetActive(true);
        GamePlayUI.SetActive(false);
        Player.starterInputs.SetCursorState(false);
    }
    public void LevelComplete()
    {
        Player.playerInput.enabled = false;
        CompletePanel.SetActive(true);
        PlayerCamera.SetActive(false);
        CompleteCamera.SetActive(true);
        GamePlayUI.SetActive(false);
        Player.starterInputs.SetCursorState(false);
        Player.PlayFlute();
        foreach (GameObject go in Snakes)
        {
            go.SetActive(false);
        }
    }

    public void OpenMainMenu()
    {
        FadeInOut(0);
        MainMenuPanel.SetActive(true);
        MainmenuCamera.SetActive(true);
    }

    public void FadeInOut(int val)
    {
        FadePanel.DOFade(val, 0.5f).SetUpdate(true);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Again", 0);
    }
    public void PlayAgain()
    {
        PlayerPrefs.SetInt("Again", 1);
        FadeInOut(1);
        StartCoroutine(Restart_Delay());
    }

    IEnumerator Restart_Delay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        PlayerPrefs.SetInt("Again", 0);
        FadeInOut(1);
        StartCoroutine(Quit_Delay());
        ClickSound.Play();
    }
    IEnumerator Quit_Delay()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}
