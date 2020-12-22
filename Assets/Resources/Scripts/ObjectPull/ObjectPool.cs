using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool 
{
    // Start is called before the first frame update

    #region Singleton
    private static ObjectPool instance;

    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }
    #endregion

    //Dictionary<>

    Transform objectPoolParent;
    Dictionary<BulletTypes, Stack<IPoolable>> pulledBullet = new Dictionary<BulletTypes, Stack<IPoolable>>();

    private ObjectPool()
    {
        objectPoolParent = new GameObject().transform;
        objectPoolParent.name = ToString();
    }


    public void AddToPool(BulletTypes bulletTypes, IPoolable iPoolableBulletStack)
    {
        if (!pulledBullet.ContainsKey(bulletTypes))
            pulledBullet.Add(bulletTypes, new Stack<IPoolable>());
            pulledBullet[bulletTypes].Push(iPoolableBulletStack);

        iPoolableBulletStack.GetGameObject.transform.SetParent(objectPoolParent);
        iPoolableBulletStack.GetGameObject.SetActive(false);
        iPoolableBulletStack.Pooled();     //Just in case the object wants to be alerted


    }

    public IPoolable RetrieveFromPool(BulletTypes bulletTypes)
    {
        if (pulledBullet.ContainsKey(bulletTypes) && pulledBullet[bulletTypes].Count > 0)
        {
            IPoolable toRet = pulledBullet[bulletTypes].Pop();
            toRet.GetGameObject.transform.SetParent(null);
            toRet.GetGameObject.SetActive(true);
            toRet.DePooled();  //Just in case the object wants to be alerted
            return toRet;
        }
        return null;
    }











}
