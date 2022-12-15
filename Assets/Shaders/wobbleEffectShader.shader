Shader "Unlit/wobbleEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            v2f vert (appdata v)
            {
                v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.vertex.y = o.vertex.y + sin(v.vertex.x + _Time.y);
				o.vertex.x = o.vertex.x + sin(5 * v.vertex.y + _Time.y * 2);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				//fixed4 col = tex2D(_MainTex, i.uv) * 10;
				fixed4 col;
				
				col.r = 3 + sin(_Time.z * i.uv.x / 100) * 1;
				col.y = 7 +sin(_Time.x * 3) * 10;
				col.z = 7 + sin(_Time.y*6) * 10;
				//col.w = 0.0f;

				//col.r = sin(_Time.x) + 0.5;
				//col.y = sin(_Time.x) + 0.5;

				//col.r = i.uv.x + _Time.x * 2;
				//col.y = i.uv.y * 2 + _Time.x;
				return col;
            }
            ENDCG
        }
    }
}
