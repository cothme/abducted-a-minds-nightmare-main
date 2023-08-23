Shader "Unlit/NewUnlitShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Strength ("Strength", Range(0, 1)) = 0.1
        _Speed ("Speed", Range(0, 1)) = 0.5
    }
    
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            float _Strength;
            float _Speed;
            sampler2D _MainTex; // Declare the texture variable
            
            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            half4 frag (v2f i) : SV_Target {
                float2 uv = i.uv + _Strength * sin(_Time.y * _Speed);
                half4 col = tex2D(_MainTex, uv);
                return col;
            }
            ENDCG
        }
    }
}
