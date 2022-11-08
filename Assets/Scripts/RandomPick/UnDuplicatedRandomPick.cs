using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// struct로 구현하니 에러 발생
// error CS0573 : cannot have instance property or field initializers in structs
public class UnDuplicatedRandomPick<T>
{
    List<T> itemList = new List<T>();

    public void SetItem(List<T> items)
    {
        itemList.Clear();
        AddItem(items);
    }

    public void AddItem(T item)
    {
        itemList.Add(item);
    }


    public void AddItem(List<T> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            itemList.Add(items[i]);
        }
    }
    

    public T GetItem()
    {   
        if (itemList.Count == 0)
        {
            return default(T);
        }

        int random = Random.Range(0, itemList.Count);

        T item = itemList[random];
        
        itemList.RemoveAt(random);

        return item;
    }

    public bool IsEmpty()
    {
        return itemList.Count == 0;
    }
}
