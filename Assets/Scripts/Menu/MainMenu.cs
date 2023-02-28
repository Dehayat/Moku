using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool isTryingToPlay = false;
    private NetworkRunner _runner;

    private void Awake()
    {
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;
    }

    public async void Play()
    {
        SceneManager.LoadScene("Game");
        //if (isTryingToPlay)
        //{
        //    return;
        //}
        //isTryingToPlay = true;
        //await _runner.StartGame(new StartGameArgs()
        //{
        //    GameMode = GameMode.AutoHostOrClient,
        //    Scene = SceneManager.GetActiveScene().buildIndex,
        //    SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        //});
        //if (!_runner.IsPlayer)
        //{
        //    await _runner.Shutdown();
        //    isTryingToPlay = false;
        //    Debug.Log("Fuck");
        //}
        //else
        //{
        //    Debug.Log("Hek yeah");
        //    Debug.Log("I'm a server? " + _runner.IsServer);
        //}
    }
}
