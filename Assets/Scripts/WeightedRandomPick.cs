using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// public struct WeightedItem<T>
// {
//     public T        item;
//     public float    weight;

//     public WeightedItem(T item, float weight)
//     {
//         this.item = item;
//         this.weight = weight;
//     }

//     public bool HasItem(T it)
//     {
//         return EqualityComparer<T>.Default.Equals(item, it);
//     }
// }


public class WeightedRandomPick<T>
{
    public Dictionary<T, float> itemDic = new Dictionary<T, float>();
    float totalWeight = 0f;



    public void Add(T item, float weight)
    {
        itemDic.Add(item, weight);

        totalWeight += weight;
    }


    public void Remove(T item)
    {
        float weight;

        if (itemDic.TryGetValue(item, out weight))
        {
            totalWeight -= weight;
            itemDic.Remove(item);
        }
    }


    public void Clear()
    {
        itemDic.Clear();
        totalWeight = 0f;
    }


    public bool ChangeWeight(T item, float weight)
    {
        if (itemDic.ContainsKey(item))
        {
            itemDic[item] = weight;
            return true;
        }

        Debug.Log("존재하지 않는 값");
        return false;
    }



    public T GetItem()
    {
        if (totalWeight <= 0)
        {
            Debug.Log("가중치 설정 오류");
            return default(T);
        }

        // (가중치 / 가중치 총합)으로 구한 해당 아이템의 확률을 weight변수로
        Dictionary<T, float> ratioDic = new Dictionary<T, float>();


        foreach(KeyValuePair<T, float> it in itemDic)
        {
            ratioDic.Add(it.Key, it.Value / totalWeight);
        }


        float pivot = Random.value; 
        float acc = 0f;


        foreach(KeyValuePair<T, float> it in ratioDic)
        {
            acc += it.Value;
            
            if (pivot <= acc)
            {
                return it.Key;
            }
        }

        return default(T);

    }
}