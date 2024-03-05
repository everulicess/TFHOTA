using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    [SerializeField] Animator doorAnim;
    [SerializeField] Animator elevatorAnim;

    public void OnGroundFloorButtonPressed()
    {
        PlayAnimation(doorAnim,"DoorOpen");
        PlayAnimation(elevatorAnim, "ElevatorDown");
        PlayAnimation(doorAnim, "DoorClosed");
    }
    public void OnFirstFloorButtonPressed()
    {
        PlayAnimation(doorAnim, "DoorOpen");
        PlayAnimation(elevatorAnim, "ElevatorUp");
        PlayAnimation(doorAnim, "DoorClosed");
    }
    public void GoToFirstFloor()
    {
        PlayAnimation(doorAnim, "DoorClosed");
        PlayAnimation(elevatorAnim, "ElevatorDown");
        PlayAnimation(doorAnim, "DoorOpen");
    }
    public void GoToGroundFloor()
    {
        PlayAnimation(doorAnim, "DoorClosed");
        PlayAnimation(elevatorAnim, "ElevatorUp");
        PlayAnimation(doorAnim, "DoorOpen");
    }
    private void PlayAnimation(Animator anim, string animationName)
    {
        anim.Play(animationName);
    }
}
