Shader "Campus/Architecture"
{
    Properties
    {
        _BaseMap("Base", 2D) = "white" {}
        _BaseColor("Wall", Color) = (0.83,0.79,0.69,1)
        _AccentColor("Wayfinding band", Color) = (0.08,0.32,0.34,1)
        _FloorColor("Stone floor", Color) = (0.40,0.45,0.46,1)
        _Cutoff("Cutoff", Float) = 0.5
        _Cull("Cull", Float) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        Pass
        {
            Tags { "LightMode"="UniversalForward" }
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #pragma multi_compile_fog
            #pragma multi_compile_instancing
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            CBUFFER_START(UnityPerMaterial)
            half4 _BaseColor, _AccentColor, _FloorColor;
            float4 _BaseMap_ST;
            float _Cutoff, _Cull;
            CBUFFER_END
            struct Attributes { float4 positionOS:POSITION; float3 normalOS:NORMAL; UNITY_VERTEX_INPUT_INSTANCE_ID };
            struct Varyings { float4 positionCS:SV_POSITION; float3 positionWS:TEXCOORD0; half3 normalWS:TEXCOORD1; half fog:TEXCOORD2; UNITY_VERTEX_OUTPUT_STEREO };
            Varyings Vert(Attributes input)
            {
                Varyings o;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.positionWS = TransformObjectToWorld(input.positionOS.xyz);
                o.positionCS = TransformWorldToHClip(o.positionWS);
                o.normalWS = TransformObjectToWorldNormal(input.normalOS);
                o.fog = ComputeFogFactor(o.positionCS.z);
                return o;
            }
            half4 Frag(Varyings i):SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
                half3 normal = normalize(i.normalWS);
                float height = frac((i.positionWS.y + 0.001) / 4.0) * 4.0;
                float band = step(0.72, height) * (1.0-step(1.08,height));
                half3 wall = lerp(_BaseColor.rgb, _AccentColor.rgb, band);
                float2 grid = abs(frac(i.positionWS.xz)-0.5);
                float seam = smoothstep(0.485,0.499,max(grid.x,grid.y));
                half3 floor = lerp(_FloorColor.rgb, _FloorColor.rgb*0.72,seam);
                half3 color = lerp(wall,floor,step(0.8,normal.y));
                Light light = GetMainLight();
                half3 illumination = max(SampleSH(normal),half3(0.35,0.35,0.35)) + light.color*saturate(dot(normal,light.direction))*0.6;
                return half4(MixFog(color*illumination,i.fog),1);
            }
            ENDHLSL
        }
        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
        UsePass "Universal Render Pipeline/Lit/DepthOnly"
    }
}
