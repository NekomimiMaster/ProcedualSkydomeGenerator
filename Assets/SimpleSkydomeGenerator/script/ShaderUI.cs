using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSkydomeGenerator
{
    public class ShaderUI : MonoBehaviour
    {
        [SerializeField]
        private Material _targetShader;

        //RGB
        private float _redValue = 0.0f;
        private float _greenValue = 0.6f;
        private float _blueValue = 1.0f;

        //明るさ
        private float _Brightness = 0.0f;

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
            _targetShader.SetFloat("_Brightness", _Brightness);
        }

        void OnGUI()
        {
            //フォントcolor
            GUI.color = Color.black;

            //RGB
            _redValue = GUI.HorizontalSlider(new Rect(60, 20, 80, 20), _redValue, 0.0f, 1.0f);
            _greenValue = GUI.HorizontalSlider(new Rect(60, 50, 80, 20), _greenValue, 0.0f, 1.0f);
            _blueValue = GUI.HorizontalSlider(new Rect(60, 80, 80, 20), _blueValue, 0.0f, 1.0f);
            GUI.Label(new Rect(10, 15, 100, 20), "Red");
            GUI.Label(new Rect(10, 45, 100, 20), "Green");
            GUI.Label(new Rect(10, 75, 100, 20), "Blue");

            //明るさ
            _Brightness = GUI.HorizontalSlider(new Rect(60, 110, 80, 20), _Brightness, -1.0f, 1.0f);
            GUI.Label(new Rect(10, 105, 100, 20), "明るさ");
            
        }
    }

}