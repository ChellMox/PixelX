using UnityEngine;

public class SwithComponent : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _state;
    [SerializeField] private string _anitationKey;
    // для двери компонент
    public void Swith()
    {
        _state = !_state;
        _animator.SetBool(_anitationKey, _state);
    }

    [ContextMenu("Swith")]
    public void SwithIr()
    {
        Swith();
    }
}
