 Shader "Custom/14SquareGradient"
{



	SubShader
	{
		Pass
		{
			CGPROGRAM

			
			#pragma vertex Vert
			#pragma fragment Frag
		
			struct ToVert
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			struct ToFrag
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0; 
			};

			int _TileCount;


			ToFrag Vert( ToVert v )
			{
				ToFrag o;
				o.vertex = UnityObjectToClipPos( v.vertex );
				o.uv = v.uv; 
				return o;
			}
			
			half4 Frag( ToFrag i ) : SV_Target
			{
				half brightness = abs((i.uv.x - 0.5)*2); //mirror pattern on x axis

				return half4 (brightness.xxx, 1);

			}

			ENDCG
		}
	}
}