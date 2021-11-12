using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                    instance = new GameObject().AddComponent<T>();
            }

            return instance;
        }
    }

    protected void SingletonInit(T ins, bool option = false)
    {
        if (instance == null)
            instance = ins;
        else if (instance != ins)
        {
            Destroy(ins.gameObject);
            return;
        }
        if (option)
            DontDestroyOnLoad(instance.gameObject);
    }
}
