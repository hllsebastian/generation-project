using UnityEngine;
using System.Collections;

public class SpeedRunePower : MonoBehaviour
{
    public void Use()
    {
        StartCoroutine(BoostSpeed());
    }

    private IEnumerator BoostSpeed()
    {
        PlayerController movement = GetComponent<PlayerController>();
        float originalSpeed = movement.playerSpeed;
        movement.playerSpeed *= 3;

        yield return new WaitForSeconds(5);

        movement.playerSpeed = originalSpeed;
    }
}
