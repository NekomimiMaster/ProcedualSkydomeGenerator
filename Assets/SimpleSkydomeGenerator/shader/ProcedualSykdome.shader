Shader "Unlit/ProcedualSykdome"
{
	Properties
	{
	    //RGBのブレンド
	    _RedValue("Red", Range(0, 1.0)) = 0.0
	    _GreenValue("Green", Range(0, 1.0)) = 0.6
	    _BlueValue("Blue", Range(0, 1.0)) = 1.0
	    
	    //明るさ
	    _Brightness("Brightness", Range(-1.0, 1.0)) = 0.0

        //雲のUV
        _uvX ("uv X", Range(1.0, 20.0)) = 10.0
        _uvY ("uv Y", Range(1.0, 20.0)) = 10.0
        //雲の高さ
        _CloudHeight ("Cloud Height", Range(1.0, 10.0)) = 5.0
        //雲の色
        _CloudColor ("Cloud Color", Color) = (1.0, 1.0, 1.0, 1.0)
	    
	    // 0:OFF(両面) 1:Front(裏面表示) 2:Back(表面表示)
	    _CullMode ("Cull Mode", Float) = 2.0
	}
	
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
         }

        Pass
        {
            Cull [_CullMode]
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            #include "Noise.cginc"
            
            //宣言
	        float _RedValue;
	        float _GreenValue;
	        float _BlueValue;
	        float _Brightness;
	        float _uvX;
	        float _uvY;
	        float _CloudHeight;
	        fixed4 _CloudColor;

            struct VertexInput {
                float4 pos:  POSITION;
                float2 uv:   TEXCOORD0;
            };

            struct VertexOutput {
                float4 v:    SV_POSITION;
                float2 uv:   TEXCOORD0;
            };
	        
            VertexOutput vert(VertexInput input)
            {
                VertexOutput output;
                output.v = UnityObjectToClipPos(input.pos);
                output.uv = input.uv;
                return output;
            }

            fixed4 frag(VertexOutput o) : SV_Target
            {
                //UVをタイリングする
                fixed2 tiling = fixed2(o.uv.x * _uvX, o.uv.y * _uvY);
                float fbm = fBm(tiling);
                
                //ノイズをマスクする
                float mask = clamp(0, 1, 10.0 - o.uv.x * 10.0) * step(0.1, o.uv.x);
                mask = mask + clamp(0, 1, o.uv.x * 10.0) * step(0.1, 1 - o.uv.x);
                fbm = fbm * clamp(0, 1, mask);
                fbm = fbm * clamp(0, 1, _CloudHeight - o.uv.y * _CloudHeight);
            
                //UV.y × _Brightness
                float y = o.uv.y * _Brightness;
                float r = y + _RedValue;
                float g = y + _GreenValue;
                float b = y + _BlueValue;
                fixed4 sky = fixed4(r, g, b, 1);
                
                //空と雲をノイズで合成
                fixed4 fix = lerp(sky, _CloudColor, fbm);
                
                return fix;       
            }
            
            ENDCG
        }
    }
}