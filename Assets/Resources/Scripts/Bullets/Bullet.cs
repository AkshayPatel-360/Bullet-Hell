using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletTypes
{
    Red,Egg,Lazer
}
public class Bullet : MonoBehaviour, IPoolable
{
    public BulletTypes types;
    public  int bulletSpeed;
    Vector2 bulletMoveDirection;
    public Rigidbody2D rb;
    public float timer = 0f;
    public float destroyAfter;
    public GameObject GetGameObject { get { return gameObject; } }

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
  /*  public virtual void FireTheBullet()
    {
        bulletSpeed = 100;
    }*/
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>destroyAfter)
        {
            DestroyMyBullet();
        }

    }
    public void DestroyMyBullet()
    {
        BulletManager.Instance.RemoveBullet(this);
        timer = 0f;
    }


   

    public void Pooled()
    {
        //throw new System.NotImplementedException();
        rb.velocity = new Vector2(0,0);
    }

    public void DePooled()
    {
        transform.position = RandomDirection();
        
    }

    public Vector2 RandomDirection()
    {

        return Random.insideUnitCircle.normalized * 5;
    }



}
