﻿using UnityEngine;
using System.Collections;

<<<<<<< HEAD
namespace Leap.Unity{
  [ExecuteInEditMode]
  public class EnableDepthBuffer : MonoBehaviour {
    public const string DEPTH_TEXTURE_VARIANT_NAME = "USE_DEPTH_TEXTURE";
  
    [SerializeField]
    private DepthTextureMode _depthTextureMode = DepthTextureMode.Depth;
  
    void Awake() {
      GetComponent<Camera>().depthTextureMode = _depthTextureMode;
  
      if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth) &&
          _depthTextureMode != DepthTextureMode.None) {
        Shader.EnableKeyword(DEPTH_TEXTURE_VARIANT_NAME);
      } else {
        Shader.DisableKeyword(DEPTH_TEXTURE_VARIANT_NAME);
      }
=======
[ExecuteInEditMode]
public class EnableDepthBuffer : MonoBehaviour {
  public const string DEPTH_TEXTURE_VARIANT_NAME = "USE_DEPTH_TEXTURE";

  [SerializeField]
  private DepthTextureMode _depthTextureMode = DepthTextureMode.Depth;

  void Awake() {
    GetComponent<Camera>().depthTextureMode = _depthTextureMode;

    if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth) &&
        _depthTextureMode != DepthTextureMode.None) {
      Shader.EnableKeyword(DEPTH_TEXTURE_VARIANT_NAME);
    } else {
      Shader.DisableKeyword(DEPTH_TEXTURE_VARIANT_NAME);
>>>>>>> 4a3e1d6546375caea4e12609c9be6e8d42cb59fb
    }
  }
}
