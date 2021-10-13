Shader "Custom/Potion"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Background ("Background", Color) = (0, 0, 0, 1)
        _Swirl ("Swirl", Color) = (1, 1, 1, 1)
        _WarpAmount ("WarpAmount", Float) = 0.0
        _Speed ("Speed", Float) = 1.0
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _Swirl;
            float4 _Background;

            float4 _Points[45];

            float _WarpAmount;
            float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 noiseSample = i.uv;
                noiseSample += fixed2(fmod(_Time.x * _Speed, 1), fmod(_Time.x * _Speed, 1));

                /*
                if (noiseSample.x < 0.5) {
                    return fixed4(1, 0, 0, 1);
                }
                */

                float perlinValue = tex2D(_MainTex, noiseSample);
                fixed2 uv = fixed2(i.uv.x + perlinValue * _WarpAmount, i.uv.y);
                float closest = 10;
                int closestIndex = 0;

                for (int j = 0; j < 45; j++) {
                    float dist = distance(uv, _Points[j]);
                    if (dist < closest) {
                        closest = dist;
                        closestIndex = j;
                    }
                }

                closest *= 12;
                int closeInt = (int)closest;

                fixed4 color = lerp(_Background, _Swirl, abs(closest - closeInt - 0.5) * 2);

                UNITY_APPLY_FOG(i.fogCoord, color);

                return color;
            }
            ENDCG
        }
    }
}
