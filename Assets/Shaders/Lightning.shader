Shader "Unlit/wobbleEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Amplitude("Amplitude", Float) = 0.0
		_Freq("Frequency", Float) = 0.0
		_Color("Color", Color) = (1,1,1,1)
		_LighterColor("LighterColor", Color) = (1,1,1,1)
		_GlowColor("GlowColor", Color) = (1,1,1,1)
		_GlowStrength("GlowStrength", Float) = 0.0
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

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _Amplitude;
			float _Freq;
			fixed4 _Color;
			fixed4 _LighterColor;
			fixed4 _GlowColor;
			float _GlowStrength;

            v2f vert (appdata v)
            {
                v2f o;

				const float PI = 3.14159265;

				//v.vertex.x = -(2.0f * _Amplitude / PI) * atan(tan((PI / 2 - v.vertex.z * PI / _Freq) + _Time.y));
				o.vertex = UnityObjectToClipPos(v.vertex);
				
				o.vertex.x = o.vertex.x +( -(2.0f * _Amplitude / PI) * atan(tan((PI / 2 - v.vertex.z * PI / _Freq) + _Time.y)));

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

			fixed4 frag(v2f i) : SV_Target
			{
				//fixed4 col = tex2D(_MainTex, i.uv);
				float4 col = _Color;
				if (i.uv.y >= 0.7f)
				{
					col = _LighterColor;
				}
				return col += _GlowColor * _GlowStrength;
            }
            ENDCG
        }
    }
}

