using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Movement Behaviour")]
    public Boundary Horizontalboundary;
    public Boundary Verticalboundary;
    public Boundary ScreenBounds;
    public float HorizontalSpeed;
    public float VerticalSpeed;

    [Header("Bullet Projectiles")]
    public Transform bulletSpawnPoint;
    [Range(0.1f, 2.0f)]
    public float fireRate = 0.2f;
    public BulletManager bulletManager;

    public Color randomColor;

    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        ResetBounds();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletManager = FindObjectOfType<BulletManager>();

        InvokeRepeating("FireBullets", 0.0f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    void Move()
    {
        var boundaryLength = Horizontalboundary.max - Horizontalboundary.min;
        transform.position = new Vector3(Mathf.PingPong(Time.time * HorizontalSpeed, boundaryLength) - Horizontalboundary.max,
            transform.position.y - VerticalSpeed * Time.deltaTime, 0.0f);
    }
    public void CheckBounds()
    {
        if(transform.position.y < ScreenBounds.min)
        {
            ResetBounds();
        }
    }

    public void ResetBounds()
    {
        var startingXPosition = Random.Range(Horizontalboundary.min, Horizontalboundary.max);
        var startingYPosition = Random.Range(Verticalboundary.min, Verticalboundary.max);
        HorizontalSpeed = Random.Range(1.0f, 6.0f);
        VerticalSpeed = Random.Range(1.0f, 3.0f);
        transform.position = new Vector3(startingXPosition, startingYPosition, 0.0f);

        var colorArray = new List<Color>();
        colorArray.Add(Color.red);
        colorArray.Add(Color.yellow);
        colorArray.Add(Color.magenta);
        colorArray.Add(Color.cyan);
        colorArray.Add(Color.white);
        //randomColor = colorArray;
        //spriteRenderer.material.SetColor("_Color", randomColor);
    }
    void FireBullets()
    {
        var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.ENEMY);
    }
}