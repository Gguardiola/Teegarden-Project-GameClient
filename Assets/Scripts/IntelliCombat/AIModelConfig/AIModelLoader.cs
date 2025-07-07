using UnityEngine;

public static class AIModelLoader
{
    private static LocalOnnxModel modelInstance;

    public static LocalOnnxModel GetIntellicombatModel()
    {
        if (modelInstance == null)
        {
            modelInstance = new LocalOnnxModel("AIModels/intellicombat_model_ready"); //TODO: que se intente bajar el ultimo modelo, si coincide, estara actualizado a la ultima, sino, cambiar.
        }

        return modelInstance;
    }

    public static bool IsModelReady()
    {
        return GetIntellicombatModel().IsReady;
    }
}