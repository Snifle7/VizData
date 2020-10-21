﻿ Shader "Custom/dirt"
{



	 SubShader
	 {
		 Pass
		 {
			 CGPROGRAM

			 #pragma vertex Vert
			 #pragma fragment Frag


			 half hash12(half2 p)
			{
					 half3 p3 = frac(half3(p.xyx)* .1031);
					 p3 += dot(p3, p3.yzx + 33.33);
					 return frac((p3.x + p3.y) * p3.z);
			}				
		
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
				i.uv.x += floor(_Time.y * 10);
				half2 pos = floor(i.uv * 10);
				half result = hash12(pos);
				if (result > 0.1) discard;

				return half4(result.xxx, 1);

			}

			ENDCG
		}
	}
}