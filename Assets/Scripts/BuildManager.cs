using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;
    // Start is called before the first frame update
    public TurretBlueprint turretToBuild;
    public GameObject buildTurretEffect;
    public GameObject turretSelected;
    public NodeUI nodeUI;

    public float gold;
    public Text goldText;

    public Vector3 buildOffset;
    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one instance of build manager found in scene");
            return;
        }
        turretToBuild = null;
        turretSelected = null;
        instance = this;
        
    }

    private void Start()
    {
        gold = PlayerStats.gold;
        UpdateGoldUI();
    }

    public void SetTurretToBuild(TurretBlueprint _turretToBuild)
    {
        turretToBuild = _turretToBuild;
        turretSelected = null;
    }

    public void SetTurretSelected(GameObject _turretSelected)
    {
        
        if (_turretSelected == turretSelected)
        {
            nodeUI.ToggleUI(!nodeUI.NodeUICanvas.gameObject.activeSelf);
        }
        else
        {
            Turret turret = _turretSelected.GetComponent<Turret>();
            nodeUI.MoveUI(_turretSelected.transform);
            nodeUI.UpdateSellText(turret.GetSelfValue());
            nodeUI.UpdateUpgradeText(turret.GetUpgradeCost());
            nodeUI.UpdateLevelText(turret.GetUpgradeCount()+1);
        }
        turretSelected = _turretSelected;
        turretToBuild = null;
    }

    public void AddGold(float amount)
    {
        gold += amount;
        UpdateGoldUI();
    }

    public void DeductGold(float amount)
    {
        gold -= amount;
        UpdateGoldUI();
    }
    
    public void UpdateGoldUI()
    {
        goldText.text = "$" + gold.ToString();
    }
    public bool canBuild { get { return turretToBuild != null; } }
    public bool hasMoney { get { return gold >= turretToBuild.cost; } }

    public void SellTurret()
    {
        if (turretSelected == null)
            return;
        Turret turret = turretSelected.GetComponent<Turret>();
        AddGold(Mathf.FloorToInt(turret.GetSelfValue()));
        Destroy(turretSelected);
        nodeUI.ToggleUI(false);
    }

    public void UpgradeTurret()
    {
        if (turretSelected == null)
            return;
        Turret turret = turretSelected.GetComponent<Turret>();
        if (gold < turret.GetUpgradeCost())
        {
            Debug.Log("Not enough gold to upgrade!");
            return;

        }
        DeductGold(turret.GetUpgradeCost());
        turret.Upgrade();
        GameObject effect = Instantiate(buildTurretEffect, turretSelected.transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        nodeUI.UpdateUpgradeText(turret.GetUpgradeCost());
        nodeUI.UpdateSellText(turret.GetSelfValue());
        nodeUI.UpdateLevelText(turret.GetUpgradeCount()+1);


    }


    public GameObject BuildTurret(Node targetNode)
    {

        nodeUI.ToggleUI(false);
        if (turretToBuild == null)
            return null;
        if (gold < turretToBuild.cost)
            return null;
        gold -= turretToBuild.cost;
        //Debug.Log("Remaining gold after building turret:" + gold);

        UpdateGoldUI();

        GameObject effect = Instantiate(buildTurretEffect, targetNode.transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        GameObject turret = (GameObject)Instantiate(turretToBuild.turretPrefab, targetNode.GetBuildPosition(), targetNode.transform.rotation);
        Turret temp = turret.GetComponent<Turret>();
        temp.UpdateSelfValue(turretToBuild.cost);
        temp.SetBaseUpgradeCost(turretToBuild.cost * 0.6f);
        return turret;
    }
    
}
