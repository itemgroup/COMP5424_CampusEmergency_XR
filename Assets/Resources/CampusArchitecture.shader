Shader "Campus/Architecture"
{
    Properties
    {
        _BaseMap("Base", 2D) = "white" {}
        _BaseColor("Upper wall", Color) = (0.96,0.97,0.98,1)
        _AccentColor("Lower wall and courtyard enclosure", Color) = (0.64,0.81,0.91,1)
        _FloorColor("Wood floor and stairs", Color) = (0.65,0.43,0.25,1)
        _CourtyardColor("Ground courtyard floor", Color) = (0.69,0.84,0.65,1)
        _StoreyHeight("Storey height", Float) = 4
        _WallSplitHeight("Lower wall height", Float) = 1.85
        _CourtyardHalfSize("Courtyard half size", Float) = 9
        [HideInInspector] _WoodSurface("Wood surface", Float) = 0
        [HideInInspector] _SolidAccent("Courtyard enclosure", Float) = 0
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
            half4 _BaseColor, _AccentColor, _FloorColor, _CourtyardColor;
            float4 _BaseMap_ST;
            float _Cutoff, _Cull;
            float _StoreyHeight, _WallSplitHeight, _CourtyardHalfSize, _WoodSurface, _SolidAccent;
            float4x4 _WorldToCampus;
            float4 _CampusUp;
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
                float3 campusPosition = mul(_WorldToCampus, float4(i.positionWS, 1.0)).xyz;
                float storeyHeight = max(_StoreyHeight, 0.01);
                float height = frac((campusPosition.y + 0.001) / storeyHeight) * storeyHeight;
                half3 wall = lerp(_AccentColor.rgb, _BaseColor.rgb, step(_WallSplitHeight, height));
                float grain = sin(campusPosition.x * 65.0 + sin(campusPosition.z * 3.0) * 0.6);
                float plankEdge = abs(frac(campusPosition.x / 0.25) - 0.5);
                float seam = smoothstep(0.48, 0.5, plankEdge);
                half3 wood = _FloorColor.rgb * (0.96 + grain * 0.04) * (1.0 - seam * 0.12);
                float upward = step(0.8, dot(normal, normalize(_CampusUp.xyz)));
                float courtyard = (1.0 - step(_CourtyardHalfSize, max(abs(campusPosition.x), abs(campusPosition.z)))) *
                    (1.0 - step(0.08, abs(campusPosition.y)));
                half3 floor = lerp(wood, _CourtyardColor.rgb, courtyard);
                half3 color = lerp(wall, floor, upward);
                color = lerp(color, wood, _WoodSurface);
                color = lerp(color, _AccentColor.rgb, _SolidAccent);
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
