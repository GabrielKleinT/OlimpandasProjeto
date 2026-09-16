using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerHud : MonoBehaviour
{
    [SerializeField]
    private PlayerPowerInventory inventory;

    [SerializeField]
    private Image powerIcon;

    [Header("Ícones")]
    [SerializeField] private Sprite stunIcon;
    [SerializeField] private Sprite slowIcon;

    private void Update()
    {
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (inventory == null || powerIcon == null)
            return;

        switch (inventory.CurrentPower)
        {
            case PlayerPowerInventory.PowerType.Stun:
                powerIcon.sprite = stunIcon;
                powerIcon.enabled = stunIcon != null;
                break;

            case PlayerPowerInventory.PowerType.Slow:
                powerIcon.sprite = slowIcon;
                powerIcon.enabled = slowIcon != null;
                break;

            default:
                powerIcon.sprite = null;
                powerIcon.enabled = false;
                break;
        }
    }
}