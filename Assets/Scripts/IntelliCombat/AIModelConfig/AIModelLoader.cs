using UnityEngine;

public static class AIModelLoader
{
    private static LocalOnnxModel modelInstance;

    public static LocalOnnxModel GetIntellicombatModel()
    {
        if (modelInstance == null)
        {
            modelInstance = new LocalOnnxModel("AIModels/intellicombat_model_ready_latest");
        }

        return modelInstance;
    }

    public static bool IsModelReady()
    {
        return GetIntellicombatModel().IsReady;
    }
}
