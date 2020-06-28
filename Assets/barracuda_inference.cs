using UnityEngine;
using Unity.Barracuda;

public class barracuda_inference : MonoBehaviour
{
    // Barracuda variables
    public NNModel modelSource;     // ONNX model (asset)
    public Model model;             // Runtime model wrapper (binary)
    private IWorker worker;         // Barracuda worker for inference
    public Sprite sprite1, sprite2; // Images to be classified (assets)

    // Start is called before the first frame update
    void Start()
    { 
        model = ModelLoader.Load(modelSource);      // Load ONNX model as runtime binary model 
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, model);  // Create Worker 

        Classify();     // Calling it once
    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * Runs inference on two images
     */
    private void Classify()
    {
        // convert WebCamTexture to Tensor
        var channels = 3;   // color; use 1 for grayscale, 4 for alpha

        // Get pixels from dog sprite
        var dog_px = sprite1.texture.GetPixels(
            (int)sprite1.textureRect.x,
            (int)sprite1.textureRect.y,
            (int)sprite1.textureRect.width,
            (int)sprite1.textureRect.height);

        // Get pixels from cat sprite
        var cat_px = sprite2.texture.GetPixels(
            (int)sprite2.textureRect.x,
            (int)sprite2.textureRect.y,
            (int)sprite2.textureRect.width,
            (int)sprite2.textureRect.height);

        Texture2D input = new Texture2D(150, 150);      // Creating Texture2D and setting pixels to dog image
        input.SetPixels(dog_px);
        input.Apply();

        // ==== Inference on a dog picture (should return 1) ====

        var in_tensor = new Tensor(input, channels);    // Create tensor from Texture2D (3 channel)
        UnityEngine.Debug.Log("Got preprocessed tensor...");

        worker.Execute(in_tensor);                      // run inference on tensor
        UnityEngine.Debug.Log("Model executed on dog photo...");

        // Explore inference detections
        var out_tensor = worker.PeekOutput("dense_5");  // name of output layers; 'predictions' for Xception NN
        var max_val = Mathf.Max(out_tensor.ToReadOnlyArray());
        var arr = out_tensor.ToReadOnlyArray();
        var index = System.Array.IndexOf(arr, max_val);

        UnityEngine.Debug.Log("Output = " + out_tensor[0]);     // should be approx. 1 for dogs
        UnityEngine.Debug.Log("Max prob = " + max_val);
        UnityEngine.Debug.Log("Index of max = " + index);

        // ==== Inference on a cat picture (should return 0) ====

        input.SetPixels(cat_px);                        // Set Texture2D pixels to cat photo
        input.Apply();
        in_tensor = new Tensor(input, channels);        // Convert to tensor

        worker.Execute(in_tensor);                      // run inference on tensor
        UnityEngine.Debug.Log("Model executed on cat photo...");

        // Explore inference detections
        out_tensor = worker.PeekOutput("dense_5");      // name of output layers; 'predictions' for Xception NN
        max_val = Mathf.Max(out_tensor.ToReadOnlyArray());
        arr = out_tensor.ToReadOnlyArray();
        index = System.Array.IndexOf(arr, max_val);

        UnityEngine.Debug.Log("Output = " + out_tensor[0]);   // Should be approx. 0 for cats
        UnityEngine.Debug.Log("Max prob = " + max_val);
        UnityEngine.Debug.Log("Index of max = " + index);

        // Clean up (don't forget!)
        worker.Dispose();
        in_tensor.Dispose();
        out_tensor.Dispose();     // not necessary if gathered by worker.PeekOutput()
    }
}
