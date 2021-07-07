using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Playables;

public class GameManager : Singleton<GameManager>
{
    public GameObject[] _singletonPrefabs;
    private List<GameObject> _instantiatedSingletonPrefabs;

    private static GameManager instance;
    private string _currentLevelName = string.Empty;
    private List<AsyncOperation> _loadOperations;

    [Header("Debug settings")]
    [SerializeField]bool sceneLoadDebug = false;
    [SerializeField]string sceneToStart = string.Empty;

    public event Action onLoad;
    public event Action onUnload;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _loadOperations = new List<AsyncOperation>();
        InstantiateSystemPrefabs();
        LoadLevel(sceneToStart);
        
    }
    #region Load Methods
    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if(ao == null)
        {
            Debug.LogError("[Game Manager] Unable to load level " + levelName);
        }
        ao.completed += OnLoadComplete;
        _loadOperations.Add(ao);
        _currentLevelName = levelName;
    }
    void OnLoadComplete(AsyncOperation ao)
    {
        if(_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);

            //Do things
            if (sceneLoadDebug) print("Load Complete");

        }

        if(onLoad != null)
        {
            onLoad();
        }
    }
    #endregion

    #region Unload Methods
    public void UnloadCurrentLevel()
    {
        UnloadLevel(_currentLevelName);
    }
    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[Game Manager] Unable to Unload level " + levelName);
        }
        ao.completed += OnUnloadComplete;
    }

    void OnUnloadComplete(AsyncOperation ao)
    {
        if (sceneLoadDebug) print("Unload Complete");
        if (onUnload != null)
        {
            onUnload();
        }
    }
    #endregion

    private void InstantiateSystemPrefabs()
    {
        _instantiatedSingletonPrefabs = new List<GameObject>();
        GameObject singleton;
        for(int i = 0; i < _singletonPrefabs.Length; i++ )
        {
            singleton = Instantiate(_singletonPrefabs[i]);
            _instantiatedSingletonPrefabs.Add(singleton);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        for(int i = 0; i < _instantiatedSingletonPrefabs.Count; i++)
        {
            Destroy(_instantiatedSingletonPrefabs[i]);
        }
        _instantiatedSingletonPrefabs.Clear();
    }

}
