Shader "Custom/Light"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
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

            };

            struct v2f
            {
                UNITY_FOG_COORDS(1)
            };

            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return _Color;
            }
            ENDCG
        }
    }
}
