using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public void buyItem(string itemName) {
        int currentQuantity = PlayerPrefs.GetInt(itemName);
        currentQuantity++;

        PlayerPrefs.SetInt(itemName, currentQuantity);
        PlayerPrefs.Save();
    }
}
