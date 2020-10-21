 Shader "Custom/10Checkers"
{
	 Properties{
		 _TileCount ("Tile Count", Range(0,100)) = 8
	 }

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

			int _TileCount;


			ToFrag Vert( ToVert v )
			{
				ToFrag o;
				o.vertex = UnityObjectToClipPos( v.vertex );
				o.uv = v.uv; //Copy uv to output that will be forwarded to Frag function
				return o;
			}
			
			half4 Frag( ToFrag i ) : SV_Target
			{
		
				float offset = floor(fmod(i.uv.y*_TileCount, 2.0));

				half gradientValue = fmod(i.uv.x * _TileCount + offset,2.0);
				half brightness = floor(gradientValue);

				return half4(brightness.xxx, 1);

			}

			ENDCG
		}
	}
}