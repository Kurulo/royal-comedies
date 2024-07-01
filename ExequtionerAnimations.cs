using UnityEngine;

public class ExequtionerAnimations : MonoBehaviour
{
    private Animator _animator;

    private float _currentTime = 0f;
    private bool _inOpenWayState = false;

    private void Start()
    {
        _currentTime = Random.Range(4f, 7f);
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_inOpenWayState)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime < 0f)
            {
                _animator.SetBool("PlayDefault", true);
                _currentTime = Random.Range(4f, 7f);
            }
        }
    }

    public void OffDefaultAnimation()
    {
        _animator.SetBool("PlayDefault", false);
    }

    public void PlayOpenWayAnimation()
    {

    }
}
