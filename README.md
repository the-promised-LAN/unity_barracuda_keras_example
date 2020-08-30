![](https://img.shields.io/badge/python-3.7-blue.svg)
![](https://img.shields.io/badge/tensorflow-2.1-orange.svg)
![](https://img.shields.io/badge/barracuda-1.0-black.svg)

# Unity Barracuda + Keras/TF2 Barebones Inference Example
Quick and dirty example of using Unity Barracuda v1.0 for inference using TensorFlow/Keras-created models
using a custom made and trained CNN in Keras (TensorFlow v2.1 backend) on [Dogs vs. Cats dataset](https://www.kaggle.com/c/dogs-vs-cats). 

[barracuda_inference.cs](https://github.com/the-promised-LAN/unity_barracuda_keras_example/blob/master/Assets/barracuda_inference.cs) explains how to perform inference step-by-step

[If you're looking on how to convert Keras' *.h5* models to *.ONNX*, check out example at this repo](https://github.com/the-promised-LAN/keras_to_onnx_example).

### Requirements
* Unity 2019.x (didn't test on older versions, but confirmed working on all 2019 versions)
* Unity Barracuda v1.0 (installation instructions: https://docs.unity3d.com/Packages/com.unity.barracuda@1.0/manual/index.html)

### Description
There are two provided images, *cat2.png* and *dog2.png*, both 150x150 RGB images (required by model) on which inference is run. The script is the most barebones needed for inference on images; can be a good starting point for further development.

[Don't forget to check out official docs for Barracuda here!!!](https://docs.unity3d.com/Packages/com.unity.barracuda@1.0/manual/index.html)

### How to use
* Create a new 2D project in Unity/Unity Hub
* Install Unity Barracuda v1.0 package (see link under Requirements)
* Import all files in Assets folder into the project's assets (drag and drop)
* Select *cat2* and *dog2* images in Project window inside Unity, and inside Inspector window, check *Read/Write Enabled* under Advanced [(where to find it)](https://github.com/the-promised-LAN/unity_barracuda_keras_example/blob/master/import_rw.PNG).
* Select Main Camera in the Hierarchy window, go to the Inspector window, click Add Component, and select *barracuda_inference* scritpt. 
* In the same Inspector window (with Main Camera still selected under Hierarchy), drag and drop *cats_dog_small_2.onnx*, *dog2.png* into Sprite1, and *cat2.png* from Assets into Sprite2 attribute [(how it's supposed to look)](https://github.com/the-promised-LAN/unity_barracuda_keras_example/blob/master/import_att.PNG).
* Press Run from within Unity, and observe output in the Console. Output is approx. 0 for cat photos, and
approx. 1 for Dog photos.

