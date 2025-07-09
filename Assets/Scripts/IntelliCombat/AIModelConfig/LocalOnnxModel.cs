using System.Collections.Generic;
using System.IO;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using UnityEngine;

public class LocalOnnxModel
{
    private InferenceSession session;

    public bool IsReady => session != null;

    public LocalOnnxModel(string modelRelativePath)
    {
        LoadModel(modelRelativePath);
    }

    private void LoadModel(string modelRelativePath)
    {
        string modelPath = Path.Combine(Application.persistentDataPath, modelRelativePath + ".onnx");

        if (!File.Exists(modelPath))
        {
            Debug.LogError($"ONNX model not found at: {modelPath}");
            return;
        }

        session = new InferenceSession(modelPath);
        Debug.Log($"ONNX model loaded from: {modelPath}");

        foreach (var kvp in session.InputMetadata)
        {
            Debug.Log($"[INPUT META] Name: {kvp.Key}  Shape: [{string.Join(",", kvp.Value.Dimensions)}]  Type: {kvp.Value.ElementType}");
        }

        foreach (var kvp in session.OutputMetadata)
        {
            Debug.Log($"[OUTPUT META] Name: {kvp.Key}  Shape: [{string.Join(",", kvp.Value.Dimensions)}]  Type: {kvp.Value.ElementType}");
        }
    }
    
    public float[] PredictRawQValues(float[] input)
    {
        if (session == null)
        {
            Debug.LogError("ONNX Session is not ready!");
            return new float[0];
        }

        var inputTensor = new DenseTensor<float>(new[] { 1, input.Length });
        for (int i = 0; i < input.Length; i++)
        {
            inputTensor[0, i] = input[i];
        }

        var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("input_1", inputTensor)
        };

        using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(inputs);

        foreach (var r in results)
        {
            if (r.Name == "output")
            {
                var outputTensor = r.AsTensor<float>();

                float[] qValues = new float[outputTensor.Length];
                int idx = 0;
                foreach (var v in outputTensor)
                {
                    qValues[idx++] = v;
                }

                return qValues;
            }
        }

        Debug.LogError("ONNX output not found!");
        return new float[0];
    }
}