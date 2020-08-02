using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{

    //public GameObject camera;
    BuildManager buildManager;

    public Canvas NodeUICanvas;
    public Text UpgradeText;
    public Text SellText;
    private void Start()
    {
        buildManager = BuildManager.instance;
        NodeUICanvas.enabled = false;
    }

    public void MoveUI(Transform selectedTurret)
    {
        NodeUICanvas.enabled = true;
        //Debug.Log("Enabling the UI");
        transform.position = selectedTurret.transform.position;
    }

    public void DisableUI()
    {
        NodeUICanvas.enabled = false;
        //Debug.Log("Disabling the UI");
    }

    public void UpdateUpgradeText()
    {

    }

    public void UpdateSellText(float value)
    {
        SellText.text = "<b>Sell</b> \n" + Mathf.FloorToInt(value);
    }

    public void OnSellTurret()
    {
        buildManager.SellTurret();
    }
    
}
