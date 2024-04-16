using UnityEngine;

// [CreateAssetMenu(menuName = "Rune/LauncherPower")]public RuneEffects 
public class LauncherPower : MonoBehaviour
{

    [SerializeField] GameObject powerPrefab;
    [SerializeField] GameObject firePoint;
    [SerializeField] float firingRate;
    public InputsUI uiInput;
    private float lastBulletTime = 0;
    static public bool canLaunch = false;
    private InputActions controls;


    private void Awake()
    {
        uiInput = new InputsUI();
    }

    private void Update()
    {
        if (CanLaunch() && canLaunch)
        {
            uiInput.Buttons.LeftClick.performed += ctx => LaunchPower();
        }

    }

    private void LaunchPower()
    {
        Debug.Log("FIREEE");
        lastBulletTime = Time.time;
        GameObject launchedPower = Instantiate(powerPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Destroy(launchedPower, 2.0f);
    }

    private bool CanLaunch()
    {
        if (Time.time < firingRate + lastBulletTime)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnEnable()
    {
        uiInput.Enable();
    }

    private void OnDisable()
    {
        uiInput.Disable();
    }
}
