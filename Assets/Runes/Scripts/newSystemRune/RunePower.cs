using UnityEngine;

[CreateAssetMenu(fileName = "New Power", menuName = "RunePower")]
public class RunePower : ScriptableObject
{
    [SerializeField] public Sprite icon;
    [SerializeField] public string runeName;
    [SerializeField] public float cooldown;
    public enum PowerType { Projectile, Invisibility, Speed }
    [SerializeField] public PowerType powerType;
}