using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletFactory : MonoBehaviour
{
    //bullet prefab
    private GameObject bulletPrefab;

    //Sprite Textures
    private Sprite playerBulletSprite;
    private Sprite enemyBulletSprite;

    //bullet Parent
    private Transform bulletParent;

    // Start is called before the first frame update
    void Start()
    {
        Initialized();

        
    }

    private void Initialized()
    {
        playerBulletSprite = Resources.Load<Sprite>("Sprites/Bullet");
        enemyBulletSprite = Resources.Load<Sprite>("Sprites/EnemySmallBullet");
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        bulletParent = GameObject.Find("Bullets").transform;
    }

    public GameObject createBullet(BulletType type)
    {
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);
        bullet.GetComponent<BulletBehaviour>().bulletType = type;
        switch (type)
        {
            case BulletType.PLAYER:
                bullet.GetComponent<SpriteRenderer>().sprite = enemyBulletSprite;
                bullet.GetComponent<BulletBehaviour>().SetDirection(BulletDirection.UP);
                bullet.name = "PlayerBullet";
                break;
            case BulletType.ENEMY:
                bullet.GetComponent<SpriteRenderer>().sprite = enemyBulletSprite;
                bullet.GetComponent<BulletBehaviour>().SetDirection(BulletDirection.DOWN);
                bullet.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                bullet.name = "EnemyBullet";
                break;
        }
        
        if (bullet != null)
        {
            bullet.SetActive(false);
        }

        return bullet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
