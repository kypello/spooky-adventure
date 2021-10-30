Shader "Unlit/PartySpotlight"
{
    Properties
    {
        _ColorA("ColorA", Color) = (1, 1, 1, 1)
        _ColorB("ColorB", Color) = (1, 1, 1, 1)
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
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
            float4 _ColorA;
            float4 _ColorB;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float dist = max(0.5 - distance(fixed2(0.5, 0.5), i.uv), 0) * 2;
                dist = dist * dist * dist * dist;

                dist = max(dist, 0.1);

                if (fmod(_Time.y * 0.8, 1) < 0.5) {
                    return _ColorA * dist;
                }
                return _ColorB * dist;
            }
            ENDCG
        }
    }
}
