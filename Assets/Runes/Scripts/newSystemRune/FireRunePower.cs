using UnityEngine;

public class FireRunePower : MonoBehaviour
{
    [SerializeField] GameObject powerPrefab, firePoint;

    static public bool isRuneFreezed;
    public void Use()
    {
        Debug.Log("Lanzando proyectil!");
        AudioManager.instance.PlaySoundEffect("SFX1");
        GameObject launchedPower = Instantiate(powerPrefab, firePoint.transform.position, firePoint.transform.rotation);
        Destroy(launchedPower, 2.0f);
    }
}

