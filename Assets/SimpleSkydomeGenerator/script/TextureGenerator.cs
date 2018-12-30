using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System;

namespace SimpleSkydomeGenerator
{
    public class TextureGenerator : MonoBehaviour
    {

        //ファイル名
        [SerializeField, ContextMenuItem("Create Texture", "MenuCreateTexture")]
        private string _fileName = "SkydomeTex";

        //書き出し元のレンダーテクスチャ
        [SerializeField]
        private RenderTexture _renderingTarget;

        [SerializeField, Tooltip("Trueならタイムスタンプが付きます")]
        private bool _useTimeStamp = false;

        //保存先
        private string _filePath = "Assets/SimpleSkydomeGenerator/texture/";

        private bool _uiMessageActiv = false;

        [ContextMenu("Create Texture")]
        public void MenuCreateTexture()
        {
            CreateTexture(_filePath, _fileName);
        }

        private void CreateTexture(string path, string name)
        {
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fullpath;

            //Pathが無効の場合作成する
            AccessTexturePath(path);

            //テクスチャ作成
            RenderTexture.active = _renderingTarget;
            Texture2D texture = new Texture2D(_renderingTarget.width, _renderingTarget.height);
            texture.ReadPixels(new Rect(0, 0, _renderingTarget.width, _renderingTarget.height), 0, 0);
            texture.Apply();

            //PNGのbyteへ変換
            var bytes = texture.EncodeToPNG();

            //タイムスタンプの判定
            if (_useTimeStamp)
            {
                fullpath = path + name + time + ".png";
            }
            else
            {
                fullpath = path + name + ".png";
            }

            //書き出し処理
            System.IO.File.WriteAllBytes(fullpath, bytes);
            Debug.Log("Create : " + fullpath);
            AssetDatabase.Refresh();
            StartCoroutine(DisplayUiMessage());
        }

        //タイムスタンプの有効/無効
        public void TimaStampToggle()
        {
            _useTimeStamp = !_useTimeStamp;
            Debug.Log("TimeStamp : " + _useTimeStamp);
        }

        private void AccessTexturePath(string path)
        {
            if (Directory.Exists(path)) { return; }

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
                Debug.Log("CreatePath : " + path);
            }
        }

        private IEnumerator DisplayUiMessage()
        {
            _uiMessageActiv = true;
            float time = 0;
            while (time < 3)
            {
                time = time + Time.deltaTime;
                yield return null;
            }
            _uiMessageActiv = false;
        }

        void OnGUI()
        {
            GUI.color = Color.black;
            GUI.Label(new Rect(Screen.width - 130, 50, 100, 20), "TimeStamp有効");
            if (_uiMessageActiv)
            {
                GUI.Label(new Rect(Screen.width - 150, 70, 100, 20), "テクスチャ生成完了");
            }
            
            GUI.color = Color.white;
            _useTimeStamp = GUI.Toggle(new Rect(Screen.width - 150, 50, 100, 20), _useTimeStamp, "");

            if (GUI.Button(new Rect(Screen.width - 150, 10, 140, 30), "テクスチャ生成"))
            {
                MenuCreateTexture();
            }
        }
    }
}