using UnityEngine;

public class StoneGolemController : MonoBehaviour
{
    private static readonly int _sad = Animator.StringToHash("Sad");
    private static readonly int _cheer = Animator.StringToHash("Cheer");

    private static readonly int _fLoat = Animator.StringToHash("FLoat");

//[SerializeField] GameObject Golem;
    private Animator _animator;
    private bool  _golemFly;


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }


    public void GolemFloat()
    {
        _golemFly = !_golemFly;
        _animator.SetBool(_fLoat, _golemFly);
    }

    public void GolemCheer()
    {
        _animator.SetTrigger(_cheer);
    }

    public void GolemSad()
    {
        _animator.SetTrigger(_sad);
    }
}