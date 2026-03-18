Shader "Custom/GhostShader"
{
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (0.2, 1.0, 0.6, 1.0)
        _Alpha("Alpha", Range(0, 1)) = 0.35

        _FresnelColor("Fresnel Color", Color) = (1, 1, 1, 1)
        _FresnelPower("Fresnel Power", Range(0.1, 8.0)) = 3.0
        _FresnelIntensity("Fresnel Intensity", Range(0, 5)) = 1.0

        _LineColor("Line Color", Color) = (1, 1, 1, 1)
        _LineDensity("Line Density", Range(1, 100)) = 25
        _LineThickness("Line Thickness", Range(0.01, 1.0)) = 0.15
        _LineSpeed("Line Speed", Float) = 1.0
        _LineIntensity("Line Intensity", Range(0, 5)) = 1.0
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        Pass
        {
            Name "ForwardUnlit"

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 positionWS  : TEXCOORD0;
                float3 normalWS    : TEXCOORD1;
                float3 positionOS  : TEXCOORD2;
            };

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                half _Alpha;

                half4 _FresnelColor;
                half _FresnelPower;
                half _FresnelIntensity;

                half4 _LineColor;
                half _LineDensity;
                half _LineThickness;
                half _LineSpeed;
                half _LineIntensity;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                VertexPositionInputs positionInputs = GetVertexPositionInputs(IN.positionOS.xyz);
                VertexNormalInputs normalInputs = GetVertexNormalInputs(IN.normalOS);

                OUT.positionHCS = positionInputs.positionCS;
                OUT.positionWS = positionInputs.positionWS;
                OUT.normalWS = normalInputs.normalWS;
                OUT.positionOS = IN.positionOS.xyz;

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float3 normalWS = normalize(IN.normalWS);
                float3 viewDirWS = normalize(GetCameraPositionWS() - IN.positionWS);

                float fresnel = 1.0 - saturate(dot(normalWS, viewDirWS));
                fresnel = pow(fresnel, _FresnelPower) * _FresnelIntensity;

                float lineCoord = IN.positionOS.y * _LineDensity + _Time.y * _LineSpeed;
                float linePattern = frac(lineCoord);

                float lineMask = step(linePattern, _LineThickness);
                float lineGlow = lineMask * _LineIntensity;

                float3 baseColor = _BaseColor.rgb;
                float3 fresnelColor = _FresnelColor.rgb * fresnel;
                float3 lineColor = _LineColor.rgb * lineGlow;

                float3 finalColor = baseColor + fresnelColor + lineColor;
                float finalAlpha = saturate((_BaseColor.a * _Alpha) + fresnel * 0.15 + lineGlow * 0.1);

                return half4(finalColor, finalAlpha);
            }
            ENDHLSL
        }
    }
}