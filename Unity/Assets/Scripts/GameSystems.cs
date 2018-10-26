using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To reduce Singletons this will be a quick lookup 
// table of all of our systems for the game
public class GameSystems{

    private Dictionary<string, object> systemTable = new Dictionary<string, object>();

    public void Register(object systemInstance, string systemName = null)
    {
        // Make sure we have a system name to use as the key
        if (string.IsNullOrEmpty(systemName))
        {
            systemName = systemInstance.GetType().Name;
        }

        systemTable[systemName] = systemInstance;
    }

    // Retrieve a system given the name
    public T Get<T>(string systemName = null)
    {
        if (string.IsNullOrEmpty(systemName))
        {
            systemName = typeof(T).Name;
        }

        object systemInstance; // Variable pointer

        // Fetch system instance using key from table
        if (systemTable.TryGetValue(systemName, out systemInstance))
        {
            // Return casted system object
            return (T)systemInstance;
        }

        // Return default of type T
        // Usually null
        return default(T);
    }
}
