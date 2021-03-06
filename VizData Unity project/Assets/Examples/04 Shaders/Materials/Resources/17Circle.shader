﻿ Shader "Custom/17Circle"
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
				float2 uv : TEXCOORD0; //Receive UV set 0 
			};
			
			struct ToFrag
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0; 
			};


			ToFrag Vert( ToVert v )
			{
				ToFrag o;
				o.vertex = UnityObjectToClipPos( v.vertex );
				o.uv = v.uv; //Copy uv to output that will be forwarded to Frag function
				return o;
			}
			
			half4 Frag( ToFrag i ) : SV_Target
			{

		
				half2 pos = (i.uv * 2) - 1;
				half d = length(pos);
				//if (d > 1) discard;

				float result = floor(1 - d +1);

				return half4(result.xxx, 1);

			}

			ENDCG
		}
	}
}