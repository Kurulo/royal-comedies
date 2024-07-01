using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberQueueManager : MonoBehaviour
{
    [Header("Members")]
    [SerializeField] int _memberPartCount = 10;
    [SerializeField] private List<Member> _allMembers = new List<Member>();

    [Header("Points")]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _destination;

    [Header("Components")]
    [SerializeField] private ChoiceBehaviour _choiceBehaviour;
    [SerializeField] private KingLogic _king;
    [SerializeField] private Animator _doorsAnimator;

    [Header("Audio")]
    [SerializeField] private LocalAudioManager _localAudioManager;
    [SerializeField] private AudioClip _clip;

    [Header("Settings")]
    [SerializeField] private float _timeDelay = 1.0f;

    private List<Member> _membersPart = new List<Member>();

    private int _currentMemberID = 0;
    public int CurrentMemberID { get { return _currentMemberID; } }

    public delegate void QueueChanged();
    public static event QueueChanged OnQueueChanged;

    private void Start()
    {
        CreateMembersPartList();
    }

    private void CreateMembersPartList()
    {
        var allMembers = new List<Member>();
        allMembers = SetNewListValues(_allMembers, allMembers);

        for (int i = 0; i < _memberPartCount; i++)
        {
            var randomMemberID = Random.Range(0, allMembers.Count - 1);

            _membersPart.Add(allMembers[randomMemberID]);
            allMembers.RemoveAt(randomMemberID);
            _allMembers.RemoveAt(randomMemberID);
        }

        InstantiateMemberWithTimeDelay();
    }

    private List<Member> SetNewListValues(List<Member> a, List<Member> b)
    {
        foreach (var m in a)
        {
            b.Add(m);
        }

        return b;
    }

    public void InstantiateMemberWithTimeDelay()
    {
        StartCoroutine(InstantiateMember());
    }

    public IEnumerator InstantiateMember()
    {
        yield return new WaitForSeconds(_timeDelay);
        _doorsAnimator.SetBool("Open", true);
        if (_currentMemberID < _memberPartCount && _choiceBehaviour.WinersNumber != 3)
        {
            Member member = Instantiate<Member>(_membersPart[_currentMemberID], _spawnPoint.position, Quaternion.identity);

            member.InitializedComponents();
            member.ClearList();
            member.SetDestinationWay(_destination);
            member.SetReady();

            _localAudioManager.PlaySound(_clip);

            _choiceBehaviour.CurrentMember = member;

            _currentMemberID++;
            OnQueueChanged();
        }
        yield return new WaitForSeconds(0.25f);
        _doorsAnimator.SetBool("Open", false);
        yield return new WaitForSeconds(1.5f);
        _king.OffLight();
    }
}
