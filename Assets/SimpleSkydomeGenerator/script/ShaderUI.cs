using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSkydomeGenerator
{
    public class ShaderUI : MonoBehaviour
    {
        [SerializeField, Tooltip("変更先のマテリアル")]
        private Material _targetShader;

        [SerializeField]
        private Color _fontColor = Color.black;
        
        [SerializeField, Range(50.0f, 200.0f)]
        private float _sliderWidth = 80.0f;

        [SerializeField, Range(50.0f, 200.0f)]
        private float _sliderOffset = 80.0f;

        //RGB
        private float _redValue = 0.0f;
        private float _greenValue = 0.6f;
        private float _blueValue = 1.0f;

        //明るさ
        private float _brightness = 0.0f;
        
        //UV タイリング
        private float _uvX = 5.0f;
        private float _uvY = 5.0f;
        //雲の高さ
        private float _cloudHeight = 5.0f;
        //雲の濃度
        private float _cloudBrightness = 1.0f;
        //雲のSeed
        private float _cloudSeed = 1.0f;
        
        void Update()
        {
            SetShaderProperty();
        }

        private void SetShaderProperty()
        {
            if (_targetShader == null)
            {
                Debug.Log("PrimitiveGradationシェーダーが適用されたマテリアルをアタッチしてください");
                return;
            }

            //RGB
            _targetShader.SetFloat("_RedValue", _redValue);
            _targetShader.SetFloat("_GreenValue", _greenValue);
            _targetShader.SetFloat("_BlueValue", _blueValue);

            //明るさ
            _targetShader.SetFloat("_Brightness", _brightness);
            
            //雲のUV タイリング
            _targetShader.SetFloat("_uvX", _uvX);
            _targetShader.SetFloat("_uvY", _uvY);
            //雲の高さ
            _targetShader.SetFloat("_CloudHeight", _cloudHeight);
            //雲の濃度
            _targetShader.SetFloat("_CloudBrightness", _cloudBrightness);
            //雲の濃度
            _targetShader.SetFloat("_Seed", _cloudSeed);
 
        }

        void OnGUI()
        {
            //フォントcolor
            GUI.color = _fontColor;

            //RGB
            _redValue = GUI.HorizontalSlider(new Rect(_sliderOffset, 20, _sliderWidth, 20), _redValue, 0, 1.0f);
            _greenValue = GUI.HorizontalSlider(new Rect(_sliderOffset, 50, _sliderWidth, 20), _greenValue, 0, 1.0f);
            _blueValue = GUI.HorizontalSlider(new Rect(_sliderOffset, 80, _sliderWidth, 20), _blueValue, 0, 1.0f);
            GUI.Label(new Rect(10, 15, 100, 20), "赤");
            GUI.Label(new Rect(10, 45, 100, 20), "緑");
            GUI.Label(new Rect(10, 75, 100, 20), "青");

            //明るさ
            _brightness = GUI.HorizontalSlider(new Rect(_sliderOffset, 110, _sliderWidth, 20), _brightness, -1.0f, 1.0f);
            GUI.Label(new Rect(10, 105, 100, 20), "明るさ");
            
            //雲のUV タイリング
            _uvX = GUI.HorizontalSlider(new Rect(_sliderOffset, 140, _sliderWidth, 20), _uvX, 1.0f, 20.0f);
            GUI.Label(new Rect(10, 135, 100, 20), "横 密度");
            _uvY = GUI.HorizontalSlider(new Rect(_sliderOffset, 170, _sliderWidth, 20), _uvY, 1.0f, 20.0f);
            GUI.Label(new Rect(10, 165, 100, 20), "縦 密度");
            //雲の高さ
            _cloudHeight = GUI.HorizontalSlider(new Rect(_sliderOffset, 200, _sliderWidth, 20), _cloudHeight, 1.0f, 10.0f);
            GUI.Label(new Rect(10, 195, 100, 20), "雲 高さ");
            //雲の濃度
            _cloudBrightness = GUI.HorizontalSlider(new Rect(_sliderOffset, 230, _sliderWidth, 20), _cloudBrightness, 0, 1.5f);
            GUI.Label(new Rect(10, 225, 100, 20), "雲 濃度");
            //乱数調整
            _cloudSeed = GUI.HorizontalSlider(new Rect(_sliderOffset, 260, _sliderWidth, 20), _cloudSeed, 1.0f, 100.0f);
            GUI.Label(new Rect(10, 255, 100, 20), "乱数調整");
            
        }
    }

}