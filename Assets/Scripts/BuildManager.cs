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

    public int gold;
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
        instance = this;
        
    }

    private void Start()
    {
        gold = PlayerStats.gold;
    }

    public void SetTurretToBuild(TurretBlueprint _turretToBuild)
    {
        turretToBuild = _turretToBuild;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();
    }
    
    public void UpdateGoldUI()
    {
        goldText.text = "$" + gold.ToString();
    }
    public bool canBuild { get { return turretToBuild != null; } }
    public bool hasMoney { get { return gold >= turretToBuild.cost; } }



    public GameObject BuildTurret(Node targetNode)
    {
        
        if (turretToBuild == null)
            return null;
        if (gold < turretToBuild.cost)
            return null;
        gold -= turretToBuild.cost;
        //Debug.Log("Remaining gold after building turret:" + gold);

        UpdateGoldUI();

        GameObject effect = Instantiate(buildTurretEffect, targetNode.transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        return (GameObject)Instantiate(turretToBuild.turretPrefab,targetNode.GetBuildPosition() , targetNode.transform.rotation);
    }
    
}
