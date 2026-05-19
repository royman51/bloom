using UnityEngine;

public class BloxyColaItem : InventoryItem
{
    public float speedBonus = 4f;
    public float duration = 5f;

    public float targetFOV = 80f;
    public float fovChangeSpeed = 8f;

    public float orthographicSizeMultiplier = 1.25f;

    private void Awake()
    {
        itemName = "Bloxy Cola";
    }

    public override void Use(GameObject player)
    {
        BloxyColaEffect effect = player.GetComponent<BloxyColaEffect>();

        if (effect == null)
        {
            effect = player.AddComponent<BloxyColaEffect>();
        }

        effect.StartColaEffect(
            speedBonus,
            duration,
            targetFOV,
            fovChangeSpeed,
            orthographicSizeMultiplier
        );
    }
}