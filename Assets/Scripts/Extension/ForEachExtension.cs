using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class ForEachExtension {
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, 
        Action<T> action)
    {
        foreach (T item in source)
            action(item);

        return source;
    }
	
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source,
        Action<T, int> action)
    {
        int index = 0;
        foreach (T item in source)
            action(item, index++);

        return source;
    }
}
