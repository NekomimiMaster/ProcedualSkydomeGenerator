using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSkydomeGenerator
{
    public class CameraMover : MonoBehaviour
    {
        //カメラ移動のスピード(ローカル)
        [SerializeField,Range(0.1f, 10.0f)]
        private float _camSpeedLocal = 6.0f;

        //カメラ操作の有効無効
        [SerializeField]
        private bool _cameraControlActive = true;

        //cameraのtransform
        private Transform _camTransform;

        //マウス操作の始点
        private Vector3 _startMousePos;

        //カメラ回転の始点情報
        private Vector3 _presentCamRotation;
        private Vector3 _presentCamPos;

        //初期位置
        private Vector3 _initialCamPos;
        private Quaternion _initialCamRotation;

        void Start()
        {
            //cameraのtransform
            _camTransform = this.gameObject.transform;

            //初期位置の保存
            _initialCamPos = this.gameObject.transform.position;
            _initialCamRotation = this.gameObject.transform.rotation;
        }

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CamControlIsActive();   //カメラ操作の有効無効
            }

            if (_cameraControlActive)
            {
                ResetCamTransform();            //初期位置に戻す            
                ResetCamRotation();             //回転角度のみリセット
                CameraRotationMouseControl();   //カメラの回転 マウス
                CameraSlideMouseControl();      //カメラのローカル移動 マウス
                CameraLocalposKeyControl();     //カメラのローカル移動 キー
            }

        }

        void OnGUI()
        {
            if (_cameraControlActive == false)
            {
                GUI.color = Color.black;
                //表示位置X,表示位置Y,幅,高さ
                GUI.Label(new Rect((Screen.width / 2) - 100, 20, 200, 20), "Camera Stop (Space)");
            }
        }

        //カメラ操作の有効無効
        public void CamControlIsActive()
        {
            _cameraControlActive = !_cameraControlActive;
            Debug.Log("Cam Control : " + _cameraControlActive);
        }

        //位置/回転を初期位置にする
        private void ResetCamTransform()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _camTransform.position = _initialCamPos;
                _camTransform.rotation = _initialCamRotation;
                Debug.Log("Cam Position : " + _initialCamPos.ToString() + "Cam Rotate : " + _initialCamRotation.ToString());
            }
        }

        //回転を初期状態にする
        private void ResetCamRotation()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                this.gameObject.transform.rotation = _initialCamRotation;
                Debug.Log("Cam Rotate : " + _initialCamRotation.ToString());
            }
        }

        //カメラの回転 マウス
        private void CameraRotationMouseControl()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _startMousePos = Input.mousePosition;
                _presentCamRotation.x = _camTransform.transform.eulerAngles.x;
                _presentCamRotation.y = _camTransform.transform.eulerAngles.y;
            }

            if (Input.GetMouseButton(1))
            {
                //(移動開始座標 - マウスの現在座標) / 解像度 で正規化
                float x = (_startMousePos.x - Input.mousePosition.x) / Screen.width;
                float y = (_startMousePos.y - Input.mousePosition.y) / Screen.height;

                //回転開始角度 ＋ マウスの変化量 * 90
                float eulerX = _presentCamRotation.x + y * 90.0f;
                float eulerY = _presentCamRotation.y + x * 90.0f;

                _camTransform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
            }
        }

        //カメラのローカル移動 マウス
        private void CameraSlideMouseControl()
        {
            if (Input.GetMouseButtonDown(2))
            {
                _startMousePos = Input.mousePosition;
                _presentCamPos = _camTransform.position;
            }

            if (Input.GetMouseButton(2))
            {
                //(移動開始座標 - マウスの現在座標) / 解像度
                float size = 5.0f;
                float x = ((_startMousePos.x - Input.mousePosition.x) / Screen.width) * size;
                float y = ((_startMousePos.y - Input.mousePosition.y) / Screen.height) * size;

                //マウスの変化量から３次元ベクトルを生成
                Vector3 mousePointX = new Vector3(x, x, x);
                Vector3 mousePointY = new Vector3(y, y, y);

                //マウスの変化量 × Gameobjectの持つベクトル で移動量のベクトルを計算
                Vector3 movePoint = _presentCamPos
                                    + Vector3.Scale(mousePointY, _camTransform.up.normalized)
                                    + Vector3.Scale(mousePointX, _camTransform.right.normalized);

                _camTransform.position = movePoint;
            }
        }

        //カメラのローカル移動 キー
        private void CameraLocalposKeyControl()
        {
            //現在地の取得
            Vector3 campos = _camTransform.position;
            float speed = Time.deltaTime * _camSpeedLocal;

            if (Input.GetKey(KeyCode.E)) { campos += _camTransform.up.normalized * speed; }
            if (Input.GetKey(KeyCode.Q)) { campos -= _camTransform.up.normalized * speed; }
            if (Input.GetKey(KeyCode.D)) { campos += _camTransform.right.normalized * speed; }
            if (Input.GetKey(KeyCode.A)) { campos -= _camTransform.right.normalized * speed; }
            if (Input.GetKey(KeyCode.W)) { campos += _camTransform.forward.normalized * speed; }
            if (Input.GetKey(KeyCode.S)) { campos -= _camTransform.forward.normalized * speed; }

            //現在地の更新
            _camTransform.position = campos;
        }
    }
}

