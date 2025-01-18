using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instace { get { return instance; } }
    [SerializeField] private Text txtOOC;
    [SerializeField] private List<string> OOC;
    [SerializeField] private Text txtHP;
    public int hp = 100;
    [SerializeField] private Text txtWater;
    int countWater = 0;
    [SerializeField] private GameObject DeathPanel;
    public float timeLockDir;
    public Text countdownText;
    public int countdownTime = 3;

    private bool isPaused = false;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(Countdown());
        StartCoroutine(RandomSelectCoroutine());
    }

    IEnumerator Countdown()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }

        countdownText.text = "Start!";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    IEnumerator RandomSelectCoroutine()
    {
        while (true)
        {
            if (isPaused)
            {
                yield return null;
            }
            else
            {
                string randomElement = GetRandomElement(OOC);
                txtOOC.text = "OUT OF CONTROL!\r\nForbidden Direction: " + randomElement;
                PlayerController.Instace.SetDirectionLock(randomElement);
                yield return new WaitForSeconds(timeLockDir);
            }
        }
    }

    string GetRandomElement(List<string> list)
    {
        if (list.Count == 0)
        {
            Debug.LogWarning("List is empty!");
            return null;
        }

        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
    public void updateHP(int value)
    {
        if (PlayerController.Instace.playerState == PlayerState.Death)
        {
            return;
        }
        hp -= value;
        txtHP.text = "HP: " + hp.ToString();
        if (hp <= 0)
        {
            PlayerController.Instace.playerState = PlayerState.Death;
            DeathPanel.SetActive(true);
        }
    }
    public void updateScore(int value)
    {
        if(PlayerController.Instace.playerState == PlayerState.Death){
            return;
        }
        countWater += value;
        txtWater.text = ": " + countWater.ToString();
    }
    public void freeDiretionSkill()
    {
        if (PlayerPrefs.GetInt("freeDirection") > 0)
        {
            int countItem = PlayerPrefs.GetInt("freeDirection");
            countItem--;
            PlayerPrefs.SetInt("freeDirection",countItem);
            PlayerPrefs.Save();
            isPaused = true;
            PlayerController.Instace.freeDirection();
            txtOOC.text = "OUT OF CONTROL!\r\nForbidden Direction: Free Direction";
            StartCoroutine(PauseRandomDirection());
        }
    }
    IEnumerator PauseRandomDirection()
    {
        yield return new WaitForSeconds(10f);
        isPaused = false;

    }
    public void freezeTimeSkill()
    {
        if (PlayerPrefs.GetInt("freezeTime") > 0)
        {
            int countItem = PlayerPrefs.GetInt("freezeTime");
            countItem--;
            PlayerPrefs.SetInt("freezeTime", countItem);
            PlayerPrefs.Save();
            StartCoroutine(EndlessPathSpawner.Instace.freezeTime());
        }
    }
    public void shieldSkill()
    {
        if (PlayerPrefs.GetInt("shield") > 0)
        {
            int countItem = PlayerPrefs.GetInt("shield");
            countItem--;
            PlayerPrefs.SetInt("shield", countItem);
            PlayerPrefs.Save();
            PlayerController.Instace.activeShield(true);
        }
    }
}
