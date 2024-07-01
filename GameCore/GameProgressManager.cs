using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    [Header("Managbe UI Elements")]
    [SerializeField] private TMP_Text _memberLeft;
    [SerializeField] private TMP_Text _winers;

    [Header("Particles")]
    [SerializeField] private List<ParticleSystem> _particles = new List<ParticleSystem>();

    [Header("Dependencis")]
    [SerializeField] private ChoiceBehaviour _choiser;
    [SerializeField] private MemberQueueManager _queue;

    [Header("Winers Positions")]
    [SerializeField] List<Transform> _positions = new List<Transform>();

    private bool _isWinersEventStart = false;

    private void OnEnable()
    {
        ChoiceBehaviour.OnWinersChanged += ChangeWinersText;
        MemberQueueManager.OnQueueChanged += ChangeMembersText;
    }

    private void Update()
    {
        if (!_isWinersEventStart)
        {
            if (_choiser.WinersNumber == 3)
            {
                Debug.Log("all winers");
                StartCoroutine(WinersIsChosen());
                _isWinersEventStart = true;
            }
            else if (_choiser.AllMembersNum == 10 && _choiser.WinersNumber != 0)
            {
                Debug.Log("Some winers");
                StartCoroutine(WinersIsChosen());
                _isWinersEventStart = true;
            }
            else if (_choiser.AllMembersNum == 10 && _choiser.WinersNumber == 0)
            {
                _isWinersEventStart = true;
                Debug.Log("No winers");
                StartCoroutine(BadEnindCaricature());
            }
        }     
    }

    private IEnumerator BadEnindCaricature()
    {
        yield return new WaitForSeconds(3f);
        GameManager.Instance.PauseGame();
        yield return new WaitForSeconds(1f);
        GameManager.SceneLoader.LoadSceneWithID(3);
    }

    private IEnumerator WinersIsChosen()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.PauseGame();
        StartCoroutine(StartWinersEvent());
    }

    private IEnumerator StartWinersEvent()
    {
        List<Member> winers = _choiser.Winers;

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < winers.Count; i++)
        {
            winers[i].gameObject.SetActive(true);
            winers[i].IsWinersEventStart = true;
            winers[i].transform.position = _positions[i].position;
            winers[i].transform.rotation = Quaternion.identity;
            winers[i].WinerEventBehaviour();
        }
        GameManager.Instance.ResumeGame();
        GlobalAudioManager.Instance.OnPlayWinerTheme();
        foreach(var par in _particles)
        {
            par.Play();
        }
        StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(10);
        GameManager.Instance.PauseGame();
        yield return new WaitForSeconds(1f);
        GameManager.SceneLoader.LoadStartScene();
        GlobalAudioManager.Instance.OnPlayMainTheme();
    }


    private void ChangeMembersText()
    {
        _memberLeft.text = $"{_queue.CurrentMemberID}/10";
    }

    private void ChangeWinersText()
    {
        _winers.text = $"{_choiser.WinersNumber}/3";
    }

    private void OnDisable()
    {
        MemberQueueManager.OnQueueChanged -= ChangeMembersText;
        ChoiceBehaviour.OnWinersChanged -= ChangeWinersText;
    }
}
