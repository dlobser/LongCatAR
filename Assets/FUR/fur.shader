// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/fur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _Data ("Data",vector) = (1,1,1,1)
        _Color("Color",color) = (1,1,1,1)
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
            #include "Noise.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 wPos : COLOR;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 wPos : COLOR;
            };


            float4 _Data;
            float4 _Color;
            
            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.wPos.xyz = mul (unity_ObjectToWorld, v.vertex).xyz;
                float w =normalize(mul (unity_ObjectToWorld, v.normal)).y;
                o.wPos.w = w;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float f = (snoise(i.wPos.xyz*_Data.x)+1)*.5;
                float f2 = (snoise(i.wPos.xyz*_Data.x*.02)+2.00)*.5;
                float y = pow((i.wPos.w+1)*.5,_Data.z)+_Data.w;
                return _Color*.8*f2*col*y;
            }
            ENDCG
        }
        
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
            #include "Noise.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float4 wPos : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 wPos : COLOR;
            };

            float4 _Data;
            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                
                o.vertex = UnityObjectToClipPos(v.vertex + v.normal*_Data.y*.05);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.wPos.xyz = mul (unity_ObjectToWorld, v.vertex).xyz;
                float w =normalize( mul (unity_ObjectToWorld, v.normal)).y;
                o.wPos.w = w;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                float f = (snoise(i.wPos.xyz*_Data.x)+1)*.5;
                float f2 = (snoise(i.wPos.xyz*_Data.x*.02)+2.00)*.5;
                fixed4 col = tex2D(_MainTex, i.uv);
                clip(f-.4);
                float y = pow((i.wPos.w+1)*.5,_Data.z)+_Data.w;

                return _Color*.9*f2*col*y;
            }
            ENDCG
        }
        
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
            #include "Noise.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float4 wPos : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 wPos : COLOR;
            };


            float4 _Data;
            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex + v.normal*_Data.y*.1);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.wPos.xyz = mul (unity_ObjectToWorld, v.vertex).xyz;
                float w = normalize(mul (unity_ObjectToWorld, v.normal)).y;
                o.wPos.w = w;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                float f = (snoise(i.wPos.xyz*_Data.x)+1)*.5;
                float f2 = (snoise(i.wPos.xyz*_Data.x*.02)+2.00)*.5;
                fixed4 col = tex2D(_MainTex, i.uv);
                clip(f-.5);
                float y = pow((i.wPos.w+1)*.5,_Data.z)+_Data.w;

                return _Color*.95*f2*col*y;
            }
            ENDCG
        }
        
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
            #include "Noise.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float4 wPos : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 wPos : COLOR;
            };

            float4 _Data;
            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex + v.normal*_Data.y*.15);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.wPos.xyz = mul (unity_ObjectToWorld, v.vertex).xyz;
                float w = normalize(mul (unity_ObjectToWorld, v.normal)).y;
                o.wPos.w = w;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                float f = (snoise(i.wPos.xyz*_Data.x)+1)*.5;
                float f2 = (snoise(i.wPos.xyz*_Data.x*.02)+2.00)*.5;
                fixed4 col = tex2D(_MainTex, i.uv);
                clip(f-.6);
                float y = pow((i.wPos.w+1)*.5,_Data.z)+_Data.w;

                return _Color*f2*col*y;
            }
            ENDCG
        }
        
    }
}
