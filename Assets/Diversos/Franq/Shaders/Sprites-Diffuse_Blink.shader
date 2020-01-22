Shader "Sprites/Blink in alpha"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_BlinkTex ("Sprite Texture to Blink", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_BlinkColor ("Blink Color", Color) = (1,0,0,1)
		_VelocityBlink ("Velocity Blink", Float) = 1
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _BlinkTex;
			fixed _VelocityBlink;
			fixed4 _BlinkColor;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed sin_time = sin(_Time.a * _VelocityBlink);
				if(sin_time < 0.8)
				{
					fixed4 original = tex2D(_MainTex, IN.texcoord) * IN.color;
					original.rgb *= original.a;
					
	//				fixed4 blink = _BlinkColor * tex2D(_BlinkTex, IN.texcoord).a;
	//				blink.rgb *= blink.a;
					
					fixed4 final = original;
					
	//				if(blink.a > 0.05 && sin_time > 0.8) {
	//					final = blink;
	//				}
					
					return final;
				}
				else
				{
					return fixed4(0,0,0,0);
				}
				
			}
		ENDCG
		}
	}
}
