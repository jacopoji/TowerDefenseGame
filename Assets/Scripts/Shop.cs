using UnityEngine;
public class Shop : MonoBehaviour
{
    private BuildManager buildManager;
    public TurretBlueprint standardTurret;
    public TurretBlueprint missleTurret;
    public TurretBlueprint laserTurret;


    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandardTurret()
    {
        buildManager.SetTurretToBuild(standardTurret);
    }

    public void PurchaseMissleTurret()
    {
        buildManager.SetTurretToBuild(missleTurret);
    }

    public void PurchaseLaserTurret()
    {
        buildManager.SetTurretToBuild(laserTurret);
    }
}
