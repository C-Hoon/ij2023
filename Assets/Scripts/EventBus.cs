using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SubscribeEvent
{
    public int value;
}
public class EventBus
{

    static Dictionary<Type, List<Delegate>> subscribeListDic = new Dictionary<Type, List<Delegate>>();

    public static void Publish<T>(T eventObj) where T:class
    {
        List<Delegate> list;
        if (!subscribeListDic.TryGetValue(typeof(T), out list))
            return;
        foreach (Delegate dl in list)
        {
            var action= dl as Action<T>;
            if (action == null)
                continue;
            action(eventObj);
        }
    }

    public static void Subscribe<T>(Action<T> callback)
    {
        List<Delegate> list;
        if (!subscribeListDic.TryGetValue(typeof(T),out list))
        {
            list = new List<Delegate>();
            subscribeListDic.Add(typeof(T), list);
        }

        list.Add(callback);
    }

    public static void Unsubscribe<T> (Action<T> callback)
    {
        List<Delegate> list;
        if (!subscribeListDic.TryGetValue(typeof(T), out list))
            return;
        list.Remove(callback);
    }
}