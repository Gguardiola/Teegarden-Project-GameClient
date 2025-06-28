using UnityEngine;

public static class StateEncoder
{
    public static float[] Encode(CombatManager cm)
    {
        return new float[]
        {
            cm.enemyAvatar.GetHealth(),
            cm.enemyAvatar.GetEnergy(),
            cm.enemyAvatar.isShielded ? 1f : 0f,
            cm.playerAvatar.GetHealth(),
            cm.playerAvatar.GetEnergy(),
            cm.playerAvatar.isShielded ? 1f : 0f
        };
    }
}