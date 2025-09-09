using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class CardBase : MonoBehaviour
{
    //this is a base class for card effects
    private void Start() { StartEffect(); }

    private void OnCollisionEnter2D(Collision2D collision) { EnterEffect(collision); }
    
    private void OnCollisionExit2D(Collision2D collision) { ExitEffect(collision); }

    private void OnCollisionStay2D(Collision2D collision) { StayEffect(collision); }

    protected virtual void StartEffect() { }
    protected virtual void EnterEffect(Collision2D collision) { }
    protected virtual void ExitEffect(Collision2D collision) { }
    protected virtual void StayEffect(Collision2D collision) { }
}
