using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Text txtCountItem;
    public string nameItem;

    public Image cooldownImage;
    bool isCooldown = false;
    public float timeCD;
    private float cooldownTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        txtCountItem.text = PlayerPrefs.GetInt(nameItem, 0).ToString();
    }
    private void Update()
    {
        updateCD();
    }
    public void updateCountItem()
    {
        txtCountItem.text = PlayerPrefs.GetInt(nameItem, 0).ToString();
        isCooldown = true;
        cooldownTimer = timeCD;
    }
    void updateCD()
    {
        if (isCooldown)
        {
            cooldownImage.gameObject.SetActive(true);
            cooldownTimer -= Time.deltaTime;
            cooldownImage.fillAmount = cooldownTimer / timeCD;
            if (cooldownTimer < 0f)
            {
                isCooldown = false;
                cooldownTimer = 0f;
                cooldownImage.fillAmount = 0f;
                cooldownImage.gameObject.SetActive(false);
            }
        }
    }
}
