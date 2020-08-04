using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{

    //public GameObject camera;
    BuildManager buildManager;
    public Animator animator;
    public Canvas NodeUICanvas;
    public Text UpgradeText;
    public Text SellText;
    private void Start()
    {
        buildManager = BuildManager.instance;
        //NodeUICanvas.enabled = false;
        NodeUICanvas.gameObject.SetActive(false);
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    public void MoveUI(Transform selectedTurret)
    {
        //NodeUICanvas.enabled = true;
        NodeUICanvas.gameObject.SetActive(true);
        //Debug.Log("Enabling the UI");
        transform.position = selectedTurret.transform.position;
    }

    public void ToggleUI(bool flag)
    {
        //NodeUICanvas.enabled = flag;
        //if (flag == false)
        //    animator.SetTrigger("OnDisable");
        NodeUICanvas.gameObject.SetActive(flag);
        //Debug.Log("Disabling the UI");
    }

    public void UpdateUpgradeText()
    {

    }

    public void UpdateSellText(float value)
    {
        SellText.text = "<b>Sell</b> \n$" + Mathf.FloorToInt(value);
    }

    public void OnSellTurret()
    {
        buildManager.SellTurret();
    }
    
}
