 Shader "Custom/20SimplexNoiseDeformation"
{

	 properties{
		 _NoiseFrequency("Noise Frequency", Float) = 5.0
	 }

	 SubShader
	 {
		 Pass
		 {
			 CGPROGRAM

			 #include "SimplexNoise.cginc"

			 #pragma vertex Vert
			 #pragma fragment Frag

			
		
			struct ToVert
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};
			
			struct ToFrag
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0; 
			};

			half _NoiseFrequency;

			ToFrag Vert( ToVert v )
			{
				v.vertex.xyz += v.normal* SimplexNoise(half4(v.vertex.xyz * _NoiseFrequency, _Time.y));

				ToFrag o;
				o.vertex = UnityObjectToClipPos( v.vertex );
				o.uv = v.uv;
				return o;
			}
			
			half4 Frag( ToFrag i ) : SV_Target
			{

				half2 pos = i.uv * _NoiseFrequency;
				half result = SimplexNoise(half3(pos, _Time.y));

				return half4(result.xxx, 1);

			}

			ENDCG
		}
	}
}