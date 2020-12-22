using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager
{
    #region Singleton
    private static BulletManager instance;

    public static BulletManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BulletManager();
            }
            return instance;
        }
    }
    #endregion


    Dictionary<BulletTypes, List<Bullet>> bulletDict;
    Stack<Bullet> BulletsToRemoveStack;
    Stack<Bullet> BulletsToAddStack;

    private BulletManager()
    {
        bulletDict = new Dictionary<BulletTypes, List<Bullet>>();
        BulletsToRemoveStack = new Stack<Bullet>();
        BulletsToAddStack = new Stack<Bullet>();


    }

    public void UpdateBulletManager()
    {
        while (BulletsToRemoveStack.Count >0)
        {
            Bullet toRemove = BulletsToRemoveStack.Pop();
            BulletTypes mBulletTypes = toRemove.types;
            if (!bulletDict.ContainsKey(mBulletTypes) || !bulletDict[mBulletTypes].Contains(toRemove))
            {
                Debug.LogError("Stack tried to remove element of type: " + mBulletTypes.ToString() + " but was not found in dictionary ? ");
            }

            else
            {
                bulletDict[mBulletTypes].Remove(toRemove);
                ObjectPool.Instance.AddToPool(toRemove.types, toRemove);//----------------------------------------
                if (bulletDict[mBulletTypes].Count == 0)
                    bulletDict.Remove(mBulletTypes);

            }

        }



        while (BulletsToAddStack.Count >0)
        {

            Bullet toAdd = BulletsToAddStack.Pop();
            BulletTypes aBulletTypes = toAdd.types;

            if (!bulletDict.ContainsKey(aBulletTypes))
            { 
                bulletDict.Add(aBulletTypes, new List<Bullet>() { toAdd });

            }

            else if (!bulletDict[aBulletTypes].Contains(toAdd))
            {
                bulletDict[aBulletTypes].Add(toAdd);
            }

            else
            {
                Debug.LogError("The bullet you are trying to add is already in the bullet dict");

            }



        }

        


    }


    public void Addbullet(Bullet toadd)
    {
        BulletsToAddStack.Push(toadd);
    }


    public void RemoveBullet(Bullet toRemove)
    {
        BulletsToRemoveStack.Push(toRemove);
    }

}


