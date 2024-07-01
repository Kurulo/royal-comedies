using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class KingLogic : MonoBehaviour
{
    [SerializeField] private ChoiceBehaviour _choiserBehaviour;
    [SerializeField] private MemberQueueManager _queue;
    [SerializeField] private CustomCursor _cursor;

    [Header("Sounds")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _laughter;
    [SerializeField] private AudioClip _nah;

    [Header("Helpers")]
    [SerializeField] private GameObject _helpText;
    [SerializeField] private GameObject _leftPoint;
    [SerializeField] private GameObject _rightPoint;

    [SerializeField] private LayerMask _rayLayer;

    [SerializeField] private List<GameObject> _lights = new List<GameObject> ();

    [SerializeField] private float _timeToHelp = 3f;

    private float _timeHelpLeft = 0f;
    private bool _helpMenuWasShown = false;

    private void Start()
    {
        _timeHelpLeft = _timeToHelp;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePaused) 
        {
            HideHelperUI();
            StopCoroutine(ShowHelperUI());
            return; 
        }

        Vector3 mousePosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        // Check cursor
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _rayLayer))
        {
            switch (hit.collider.tag)
            {
                case "LoserDoor":
                    _cursor.ChangeCursorToNotAproved();
                    break;
                case "WinerDoor":
                    _cursor.ChangeCursorToAproved();
                    break;
                default:
                    return;
            }
        }
        else
        {
            _cursor.ChangeCursorToStandart();
        }

        // If can not switch
        if (_choiserBehaviour.CurrentMember == null || !_choiserBehaviour.CurrentMember.IsReadyToByChosen)
        {
            return;
        }
        else
        {
            foreach (var light in _lights)
            {
                light.gameObject.SetActive(true);
            }
        }

        // Help UI
        _timeHelpLeft -= Time.deltaTime;
        if (_timeHelpLeft < 0f && !_helpMenuWasShown)
        {
            StartCoroutine(ShowHelperUI());
            _helpMenuWasShown = true;
        }

        // Check if hit dor
        if (Input.GetButtonDown("Fire1"))
        {
            if (hit.collider != null)
            {
                switch (hit.collider.tag)
                {
                    case "LoserDoor":
                        _audioSource.PlayOneShot(_nah);
                        _cursor.ChangeCursorToNotAproved();
                        _choiserBehaviour.SetLoserBihaviour();

                        _timeHelpLeft = _timeToHelp;
                        StopCoroutine(ShowHelperUI());
                        _helpMenuWasShown = false;
                        HideHelperUI();

                        break;
                    case "WinerDoor":
                        _audioSource.PlayOneShot(_laughter);
                        _cursor.ChangeCursorToAproved();
                        _choiserBehaviour.SetWinerBihaviour();

                        _timeHelpLeft = _timeToHelp;
                        StopCoroutine(ShowHelperUI());
                        _helpMenuWasShown = false;
                        HideHelperUI();

                        break;
                    default:
                        return;
                }

                _queue.InstantiateMemberWithTimeDelay();
            }

        }
    }

    public void OffLight()
    {
        foreach (var light in _lights)
        {
            light.gameObject.SetActive(false);
        }
    }

    private IEnumerator ShowHelperUI()
    {
        _helpText.SetActive(true);
        _leftPoint.SetActive(true);
        _rightPoint.SetActive(true);

        for (int i = 0; i <= 4; i++)
        {
            if (i % 2 == 0)
            {
                yield return new WaitForSeconds(0.5f);

                _leftPoint.SetActive(false);
                _rightPoint.SetActive(false);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);

                _leftPoint.SetActive(true);
                _rightPoint.SetActive(true);
            }
        }

        HideHelperUI();
    }

    private void HideHelperUI()
    {
        _helpText.SetActive(false);
        _leftPoint.SetActive(false);
        _rightPoint.SetActive(false);
    }
}
