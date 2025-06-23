using Unity.Barracuda;
using UnityEngine;

public class LocalOnnxModel
{
    private NNModel modelAsset;
    private IWorker worker;

    public bool IsReady => worker != null;

    public LocalOnnxModel(string modelPath)
    {
        LoadModel(modelPath);
    }

    private void LoadModel(string modelPath)
    {
        modelAsset = Resources.Load<NNModel>(modelPath);

        if (modelAsset == null)
        {
            Debug.LogError("ONNX model not found at Resources/" + modelPath);
            return;
        }

        var model = ModelLoader.Load(modelAsset);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, model);
    }
    
    public float[] PredictRawQValues(float[] input)
    {
        // Asumiendo que ya tienes tu tensor y worker configurado
        Tensor inputTensor = new Tensor(1, input.Length, input);
        worker.Execute(inputTensor);
        Tensor output = worker.PeekOutput();  // Asume solo un output

        float[] qValues = output.ToReadOnlyArray();
        inputTensor.Dispose();
        output.Dispose();

        return qValues;
    }

}
