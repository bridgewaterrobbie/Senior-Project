﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

<<<<<<< HEAD
namespace Leap.Unity{
  [CustomEditor(typeof(LeapImageRetriever))]
  public class LeapImageRetrieverEditor : CustomEditorBase {

    private GUIContent _brightTextureGUIContent;
    private GUIContent _rawTextureGUIContent;
    private GUIContent _distortionTextureGUIContent;

    protected override void OnEnable() {
      base.OnEnable();

      _brightTextureGUIContent = new GUIContent("Bright Texture");
      _rawTextureGUIContent = new GUIContent("Raw Texture");
      _distortionTextureGUIContent = new GUIContent("Distortion Texture");
    }

    public override void OnInspectorGUI() {
      base.OnInspectorGUI();

      if (Application.isPlaying) {
        LeapImageRetriever retriever = target as LeapImageRetriever;
        var data = retriever.TextureData;
        var dataType = typeof(Object);

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField(_brightTextureGUIContent, data.BrightTexture.CombinedTexture, dataType, true);
        EditorGUILayout.ObjectField(_rawTextureGUIContent, data.RawTexture.CombinedTexture, dataType, true);
        EditorGUILayout.ObjectField(_distortionTextureGUIContent, data.Distortion.CombinedTexture, dataType, true);
        EditorGUI.EndDisabledGroup();
      }
    }
  }
=======
[CustomEditor(typeof(LeapImageRetriever))]
public class LeapImageRetrieverEditor : Editor {

  private List<string> BasicModePropertyNames = new List<string>() {
      "m_Script",
      "handController",
    };

  private GUIContent _brightTextureGUIContent;
  private GUIContent _rawTextureGUIContent;
  private GUIContent _distortionTextureGUIContent;

  void OnEnable() {
    _brightTextureGUIContent = new GUIContent("Bright Texture");
    _rawTextureGUIContent = new GUIContent("Raw Texture");
    _distortionTextureGUIContent = new GUIContent("Distortion Texture");
  }

  public override void OnInspectorGUI() {
    serializedObject.Update();
    SerializedProperty properties = serializedObject.GetIterator();

    bool useEnterChildren = true;
    while (properties.NextVisible(useEnterChildren) == true) {
      useEnterChildren = false;
      if (AdvancedMode._advancedModeEnabled || BasicModePropertyNames.Contains(properties.name)) {
        EditorGUILayout.PropertyField(properties, true);
      }
    }

    if (Application.isPlaying) {
      LeapImageRetriever retriever = target as LeapImageRetriever;
      var data = retriever.TextureData;
      var dataType = typeof(Object);

      EditorGUI.BeginDisabledGroup(true);
      EditorGUILayout.ObjectField(_brightTextureGUIContent, data.BrightTexture.CombinedTexture, dataType, true);
      EditorGUILayout.ObjectField(_rawTextureGUIContent, data.RawTexture.CombinedTexture, dataType, true);
      EditorGUILayout.ObjectField(_distortionTextureGUIContent, data.Distortion.CombinedTexture, dataType, true);
      EditorGUI.EndDisabledGroup();
    }

    serializedObject.ApplyModifiedProperties();
  }

>>>>>>> 4a3e1d6546375caea4e12609c9be6e8d42cb59fb
}
