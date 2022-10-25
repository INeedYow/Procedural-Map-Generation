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
    public Dictionary<T, float> itemList = new Dictionary<T, float>();
    float totalWeight = 0;



    public void Add(T item, float weight)
    {
        itemList.Add(item, weight);

        totalWeight += weight;
    }


    public void Remove(T item)
    {
        float weight;

        if (itemList.TryGetValue(item, out weight))
        {
            totalWeight -= weight;
            itemList.Remove(item);
        }
    }


    public bool ChangeWeight(T item, float weight)
    {
        if (itemList.ContainsKey(item))
        {
            itemList[item] = weight;
            return true;
        }

        Debug.Log("존재하지 않는 값");
        return false;
    }


    public void Print()
    {
        foreach(KeyValuePair<T, float> pair in itemList)
        {
            Debug.Log(string.Format("item : {0}, weight : {1}", pair.Key, pair.Value));
        }
    }


    public T GetItem()
    {
        if (totalWeight <= 0)
        {
            Debug.Log("가중치 설정 오류");
            return default(T);
        }

        // (가중치 / 가중치 총합)으로 구한 해당 아이템의 확률을 weight변수로
        Dictionary<T, float> ratioList = new Dictionary<T, float>();


        foreach(KeyValuePair<T, float> it in itemList)
        {
            ratioList.Add(it.Key, it.Value / totalWeight);
        }


        float pivot = Random.value;
        float acc = 0f;


        foreach(KeyValuePair<T, float> it in ratioList)
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