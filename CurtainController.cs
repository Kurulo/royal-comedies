using System.Collections;
using UnityEngine;

public class CurtainController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string Open_AnimName = "OpenCurtaine";
    private const string Close_AnimName = "CloseCurtaine";
    private const string Hold_AnimName = "HoldCurtaine";

    private const string Bool_OpenName = "OpenCurtaine";
    private const string Bool_CloseName = "CloseCurtaine";
    private const string Bool_HoldName = "HoldCurtaine";

    private bool _isClosed = false;
    private bool _isOpened = false;
    private bool _isHolden = false;

    private string _currentAnimationName = "";
    private ScenesLoader _scenesLoader = new ScenesLoader();

    private void OnEnable()
    {
        GameManager.GamePausedEvent += PlayCloseAnimation;
        GameManager.GameResumeEvent += PlayOpenAnimation;
    }

    private void Start()
    {
        PlayOpenAnimation();

    }

    private void Update()
    {
        if (_isOpened)
            PlayOpenAnimation();
        else if (_isClosed) 
            PlayCloseAnimation();
        else if (_isHolden) 
            PlayHoldAnimation();
    }

    // In start of the game or when player close menu
    public void PlayOpenAnimation()
    {
        _isOpened = true;
        _animator.SetBool(Bool_OpenName, _isOpened);

        _isClosed = false;
        _animator.SetBool(Bool_CloseName, _isClosed);

        _isHolden = false;
        _animator.SetBool(Bool_HoldName, _isHolden);

        _currentAnimationName = Open_AnimName;
    }

    // Button (Play)
    public void PlayCloseAnimation()
    {
        _isOpened = false;
        _animator.SetBool(Bool_OpenName, _isOpened);

        _isHolden = false;
        _animator.SetBool(Bool_HoldName, _isHolden);

        _isClosed = true;
        _animator.SetBool(Bool_CloseName, _isClosed);

        _currentAnimationName = Close_AnimName;
    }

    // if player open menu (pause game)
    public void PlayHoldAnimation()
    {
        _isHolden = true;
        _animator.SetBool(Bool_HoldName, _isHolden);

        _isClosed = false;
        _animator.SetBool(Bool_CloseName, _isClosed);

        _isOpened = false;
        _animator.SetBool(Bool_OpenName, _isOpened);

        _currentAnimationName = Hold_AnimName;
    }

    public void CloseAnimationEndEvent()
    {
        _scenesLoader.LoadGameScene();
    }

    private void OnDisable()
    {
        GameManager.GamePausedEvent -= PlayCloseAnimation;
        GameManager.GameResumeEvent -= PlayOpenAnimation;
    }
}
