using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOverlay : MonoBehaviour
{
    private Material _lineMaterial;

    [SerializeField] private bool _showMain = true;
    [SerializeField] private bool _showSub = false;

    [SerializeField] private int _gridSizeX;
    [SerializeField] private int _gridSizeY;

    [SerializeField] private float _startX;
    [SerializeField] private float _startY;
    [SerializeField] private float _startZ;

    [SerializeField] private float _smallStep;
    [SerializeField] private float _largeStep;

    [SerializeField] private Color _mainColor = new Color(0f, 1f, 0f, 1f);
    [SerializeField] private Color _subColor = new Color(0f, 0.5f, 0f, 1f);

    private void CreateLineMaterial()
    {
        if (!_lineMaterial)
        {
            var shader = Shader.Find("Hidden/Internal-Colored");
            _lineMaterial = new Material(shader);

            // hide in garbage collector
            _lineMaterial.hideFlags = HideFlags.HideAndDontSave;

            // Turn on alpha blending
            _lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            _lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

            // Turn off depth writing
            _lineMaterial.SetInt("_ZWrite", 0);

            // Turn off backface culling
            _lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        }
    }

    private void OnDisable()
    {
        DestroyImmediate(_lineMaterial);
    }

    private void OnPostRender()
    {
        CreateLineMaterial();

        _lineMaterial.SetPass(0);

        GL.Begin(GL.LINES);

        if (_showSub)
            DrawGrid(_subColor, _smallStep);

        if (_showMain)
            DrawGrid(_mainColor, _largeStep);

        GL.End();
    }

    private void DrawGrid(Color color, float step)
    {
        GL.Color(color);

        for (float y = 0; y <= _gridSizeY; y += step)
        {
            GL.Vertex3(_startX, _startY + y, _startZ);
            GL.Vertex3(_startX + _gridSizeX, _startY + y, _startZ);
        }

        for (float x = 0; x <= _gridSizeX; x += step)
        {
            GL.Vertex3(_startX + x, _startY, _startZ);
            GL.Vertex3(_startX + x, _startY + _gridSizeY, _startZ);
        }
    }
}
