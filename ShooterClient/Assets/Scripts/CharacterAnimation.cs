using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private const string Grouned = "Grounded";
    private const string Speed = "Speed";
    [SerializeField] private Animator animator;
    [SerializeField] private CheckFly checkFly;
    [SerializeField] private Character character;

    private void Update()
    {
        Vector3 localVelocity = character.transform.InverseTransformVector(character.Velocity);
        float speed = localVelocity.magnitude / character.Speed;
        float sign = Mathf.Sign(localVelocity.z);

        animator.SetFloat(Speed, speed * sign);
        animator.SetBool(Grouned, checkFly.IsFly == false);
    }
}
