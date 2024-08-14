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
    private int hp = 100;
    [SerializeField] private Text txtWater;
    int countWater = 0;
    [SerializeField] private GameObject DeathPanel;

    private bool isPaused = false;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(RandomSelectCoroutine());
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
                yield return new WaitForSeconds(10f);
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
