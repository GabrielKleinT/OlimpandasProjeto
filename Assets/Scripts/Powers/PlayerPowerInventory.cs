using UnityEngine;

public class PlayerPowerInventory : MonoBehaviour
{
    public enum PowerType
    {
        None,
        Stun,
        Slow
    }

    [SerializeField]
    private PowerType currentPower = PowerType.None;

    public PowerType CurrentPower => currentPower;

    public bool HasPower(PowerType power)
    {
        return currentPower == power;
    }

    public void GrantPower(PowerType power)
    {
        currentPower = power;

        Debug.Log(
            $"{name} pegou o poder {power}!"
        );
    }

    public void ConsumePower()
    {
        currentPower = PowerType.None;
    }
}