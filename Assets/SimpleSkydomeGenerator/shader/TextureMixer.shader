Shader "Generator/TextureMixer"
{
	Properties
	{
		_MainTex ("Main Tex", 2D) = "white" {}
		_SubTex ("Sub Tex", 2D) = "white" {}
		_mixPoint ("Mix Point", Range(0, 2.0)) = 1.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
            #include "Noise.cginc"
			
			sampler2D _MainTex;
			sampler2D _SubTex;
			float4 _MainTex_ST;
			float _mixPoint;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 main = tex2D(_MainTex, i.uv);
				fixed4 sub = tex2D(_SubTex, i.uv);
				float n = fBm(i.uv);
				
				fixed4 fix = lerp(main, sub, n * _mixPoint);
				
				return fixed4(fix.x, fix.y, fix.z, 1);
			}
			ENDCG
		}
	}
}
