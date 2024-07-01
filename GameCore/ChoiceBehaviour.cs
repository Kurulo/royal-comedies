using System.Collections.Generic;
using UnityEngine;

public class ChoiceBehaviour : MonoBehaviour
{
    [Header("Ways")]
    [SerializeField] private List<Transform> _winerWay = new List<Transform>();
    [SerializeField] private List<Transform> _loserWay = new List<Transform>();

    private Member _currentMember;
    public Member CurrentMember { get { return _currentMember; } set { _currentMember = value; } }

    private List<Member> _winers = new List<Member>();
    public List<Member> Winers { get { return _winers;  } }
    public int WinersNumber { get { return _winers.Count; } }

    public delegate void WinersChanged();
    public static event WinersChanged OnWinersChanged;

    private int _allMembersNum = 0;
    public int AllMembersNum { get { return _allMembersNum; } }

    public void SetWinerBihaviour()
    {
        if (_currentMember != null && _currentMember.IsReachedDestination)
        {
            _currentMember.ClearList();

            foreach (var way in _winerWay)
            {
                _currentMember.SetDestinationWay(way);
            }

            _currentMember.WinerBehaviour();
            _currentMember.SetReady();
            _winers.Add(_currentMember);
            
            OnWinersChanged();
        }

        _allMembersNum++;
        _currentMember = null;
    }

    public void SetLoserBihaviour()
    {
        if (_currentMember != null && _currentMember.IsReachedDestination)
        {
            {
                _currentMember.ClearList();

                foreach (var way in _loserWay)
                {
                    _currentMember.SetDestinationWay(way);
                }

                _currentMember.LoserBehaviour();
                _currentMember.SetReady();
            }
        }
        _allMembersNum++;
        _currentMember = null;
    }
}
