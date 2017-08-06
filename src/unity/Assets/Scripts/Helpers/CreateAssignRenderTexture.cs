﻿// This file is subject to the MIT License as seen in the root of this folder structure (LICENSE)

using UnityEngine;

/// <summary>
/// This script creates a render texture and assigns it to the camera. We found the connection to the render textures kept breaking
/// when we saved the scene, so we create and assign it at runtime as a workaround.
/// </summary>
[RequireComponent(typeof(Camera))]
public class CreateAssignRenderTexture : MonoBehaviour
{
    public string _targetName = string.Empty;
    public int _width = 32;
    public int _height = 32;
    public int _depthBits = 0;
    public RenderTextureFormat _format = RenderTextureFormat.ARGBFloat;
    public TextureWrapMode _wrapMode = TextureWrapMode.Clamp;
    public int _antiAliasing = 1;
    public FilterMode _filterMode = FilterMode.Bilinear;
    public int _anisoLevel = 0;
    public bool _useMipMap = false;

    public bool _createPingPongTargets = false;

	void Start()
    {

        if( !_createPingPongTargets )
        {
            RenderTexture rt = CreateRT( _targetName );
            GetComponent<Camera>().targetTexture = rt;
        }
        else
        {
            CreatePingPongRts();
        }

	}

    RenderTexture CreateRT( string name )
    {
        RenderTexture tex = new RenderTexture( _width, _height, _depthBits, _format );

        if( !string.IsNullOrEmpty( name ) )
        {
            tex.name = name;
        }

        tex.wrapMode = _wrapMode;
        tex.antiAliasing = _antiAliasing;
        tex.filterMode = _filterMode;
        tex.anisoLevel = _anisoLevel;
        tex.useMipMap = _useMipMap;

        return tex;
    }

    void CreatePingPongRts()
    {
        PingPongRts ppr = GetComponent<PingPongRts>();
        if( ppr == null )
        {
            Debug.LogError( "To create ping pong render targets, a PingPongRts components needs to be added to this GO.", this );
            return;
        }

        ppr._rtA = CreateRT( _targetName + "_A" );
        ppr._rtB = CreateRT( _targetName + "_B" );
        ppr._source_1 = CreateRT( _targetName + "_1" );
    }
}
