Shader "Custom/PostEffects/Posterize"
{
    Properties
    {
        _Steps ("Steps", Float) = 4.0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        ZWrite Off ZTest Always Blend Off Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float _Steps;

            half4 Frag(Varyings input) : SV_Target
            {
                float2 uv = input.texcoord;
                half4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, uv);

                // 各チャンネルを Steps 段階に丸める
                color.rgb = floor(color.rgb * _Steps) / _Steps;

                return color;
            }
            ENDHLSL
        }
    }
}