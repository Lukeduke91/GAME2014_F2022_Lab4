using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [Header("Bullet Properties")]
    [Range(10, 200)]
    public int bulletNumber = 50;
    public int bulletCount = 0;
    public int activeBullets = 0;
    [Range(10, 200)]
    public int enemyBulletNumber = 50;
    public int enemyBulletCount = 0;
    public int enemyActiveBullets = 0;

    private BulletFactory factory;
    private Queue<GameObject> bulletPool;
    private Queue<GameObject> enemyBulletPool;

    public Transform bulletParent;
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new Queue<GameObject>();
        enemyBulletPool = new Queue<GameObject>();
        factory = GameObject.FindObjectOfType<BulletFactory>();
        BuildBulletPool();
    }

    void BuildBulletPool()
    {
        for(int i = 0; i < bulletNumber; i++)
        {
            bulletPool.Enqueue(factory.createBullet(BulletType.PLAYER));
        }

        for (int i = 0; i < enemyBulletNumber; i++)
        {
            enemyBulletPool.Enqueue(factory.createBullet(BulletType.PLAYER));
        }

        bulletCount = bulletPool.Count;
        enemyBulletCount = enemyBulletPool.Count;
    }
    
    public GameObject GetBullet(Vector2 position, BulletType type)
    {
        

        GameObject bullet = null;

        switch (type)
        {
            case BulletType.PLAYER:
                {
                    if (bulletPool.Count < 1)
                    {
                        bulletPool.Enqueue(factory.createBullet(BulletType.PLAYER));
                    }
                    bullet = bulletPool.Dequeue();
                    bulletCount = bulletPool.Count;
                    activeBullets++;
                }
                break;
            case BulletType.ENEMY:
                {
                    if (enemyBulletPool.Count < 1)
                    {
                        enemyBulletPool.Enqueue(factory.createBullet(BulletType.ENEMY));
                    }
                    bullet = enemyBulletPool.Dequeue();
                    enemyBulletCount = enemyBulletPool.Count;
                    enemyActiveBullets++;
                }
                break;
                
        }

        //bullet.GetComponent<BulletBehaviour>().SetDirection(direction);

        bullet.SetActive(true);
        bullet.transform.position = position;

        return bullet;
    }

    public void ReturnBullet(GameObject bullet, BulletType type)
    {
        bullet.SetActive(false);
        
        switch(type)
        {
            case BulletType.PLAYER:
                bulletPool.Enqueue(bullet);
                bulletCount = bulletPool.Count;
                activeBullets--;
                break;
            case BulletType.ENEMY:
                enemyBulletPool.Enqueue(bullet);
                enemyBulletCount = enemyBulletPool.Count;
                enemyActiveBullets--;
                break;
        }
    }
}
