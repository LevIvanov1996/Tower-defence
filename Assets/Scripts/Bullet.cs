using UnityEngine;
public enum bulletType
{
    redFireball,bluFireball
};
public class Bullet : MonoBehaviour
{
    [SerializeField] int attacDamage;
    [SerializeField] bulletType bType;
    public int AttackDamage
    {
        get { return attacDamage; }
    }

    public bulletType BType
    {
        get { return bType; }
    }
}
