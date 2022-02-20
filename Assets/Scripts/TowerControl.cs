using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float attackRadius;
    [SerializeField] Bullet Bulet;
    Enemy targetEnemy = null;
    float attackCounter;
    bool isAttacking = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackCounter -= Time.deltaTime;
        if (targetEnemy == null)
        {
            Enemy nearestEnemy = GetNearestEnemy();
            if (nearestEnemy != null && Vector2.Distance(transform.localPosition, nearestEnemy.transform.localPosition) <= attackRadius)
            {
                targetEnemy = nearestEnemy;
            }
           
        }
        else
        {
            if (attackCounter <= 0)
            {
                isAttacking = true;
                attackCounter = timeBetweenAttacks;
            }
            else
            {
                isAttacking = false;
            }
            if (Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition) > attackRadius)
            {
                targetEnemy = null;
            }
        }
       
    }

    public void FixedUpdate()
    {
        if (isAttacking==true)
        {
            Attack();
        }
    }
    public void Attack()
    {
        isAttacking = false;
        Bullet newBulet = Instantiate(Bulet)as Bullet;
        newBulet.transform.localPosition = transform.localPosition;

        if (targetEnemy == null)
        {
            Destroy(newBulet);
        }
        else
        {
            //move bullet to enemy
            StartCoroutine(MoveBullet(newBulet));
        }
    }

    IEnumerator MoveBullet(Bullet bullet)
    {
        while (getTargetDistace(targetEnemy) > 0.20f && bullet != null && targetEnemy != null)
        {
            var dir = targetEnemy.transform.localPosition - transform.localPosition;
            var angleDiraction = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angleDiraction, Vector3.forward);
            bullet.transform.localPosition = Vector2.MoveTowards(bullet.transform.localPosition, targetEnemy.transform.localPosition,5f*Time.deltaTime);
            yield return null;
        }
        if (bullet != null || targetEnemy == null)
        {
            Destroy(bullet);
        }
    }
    private float getTargetDistace(Enemy thisEnemy)
    {
        if(thisEnemy==null)
        {
            thisEnemy = GetNearestEnemy();
            if (thisEnemy == null)
            {
                return 0f;
            }
        }
        return Mathf.Abs(Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition));
    }
    private List<Enemy> GetEnemyesInRange()
    {
        List<Enemy> enemyesInRange = new List<Enemy>();
        foreach (Enemy enemy in Manager.Instance.EnemyList) 
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <=attackRadius)
            {
                enemyesInRange.Add(enemy);
            }
        }
        return enemyesInRange;
    }
    private Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float smalesDistance = float.PositiveInfinity;
        foreach (Enemy enemy in GetEnemyesInRange())
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < smalesDistance)
            {
                smalesDistance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}
