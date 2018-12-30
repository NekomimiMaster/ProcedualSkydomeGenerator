Shader "Unlit/ProcedualSykdome"
{
	Properties
	{
	    //RGBのブレンド
	    _RedValue("Red", Range(0, 1.0)) = 0.0
	    _GreenValue("Green", Range(0, 1.0)) = 0.0
	    _BlueValue("Blue", Range(0, 1.0)) = 0.0
	    
	    //明るさ
	    _Brightness("Brightness", Range(-1.0, 1.0)) = 0.0

	    [Space(10)]
	    
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
            #pragma enable_d3d11_debug_symbols

            struct VertexInput {
                float4 pos:  POSITION;
                float2 uv:   TEXCOORD0;
            };

            struct VertexOutput {
                float4 v:    SV_POSITION;
                float2 uv:   TEXCOORD0;
            };
            
            //宣言
	        float _RedValue;
	        float _GreenValue;
	        float _BlueValue;
	        float _Brightness;
	        
            //Vertex
            VertexOutput vert(VertexInput input)
            {
                VertexOutput output;
                output.v = UnityObjectToClipPos(input.pos);
                output.uv = input.uv;

                return output;
            }

            //pixel
            fixed4 frag( VertexOutput output) : SV_Target
            {
                //UV.y × _Brightness
                float y = output.uv.y * _Brightness;
                
                float Red = y + _RedValue;
                float Green = y + _GreenValue;
                float Blue = y + _BlueValue;
                
                return fixed4(Red, Green, Blue, 1.0);       
            }
            
            ENDCG
        }
    }
}
