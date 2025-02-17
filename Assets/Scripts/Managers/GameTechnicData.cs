using UnityEngine;

public class GameTechnicData : MonoBehaviour
{
    private Camera _maincam;
    private Vector2 _screenBounds;


    public static GameTechnicData Instance { get; private set; }

    private void Awake()
    {
        #region SingletonPattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion



    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

}
