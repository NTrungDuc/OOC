using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Text txtCountItem;
    public string nameItem;
    // Start is called before the first frame update
    void Start()
    {
        updateCountItem();
    }
    public void updateCountItem()
    {
        txtCountItem.text = PlayerPrefs.GetInt(nameItem, 0).ToString();
    }
}
