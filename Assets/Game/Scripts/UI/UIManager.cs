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
    [SerializeField] private GameObject DeathPanel;
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
            string randomElement = GetRandomElement(OOC);
            txtOOC.text= "OUT OF CONTROL!\r\nForbidden Direction: " + randomElement;
            PlayerController.Instace.SetDirectionLock(randomElement);
            yield return new WaitForSeconds(10f);
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
}
