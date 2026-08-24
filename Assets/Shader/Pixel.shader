
Shader "Custom/PostEffects/Pixelate"
{
    Properties
    {
        _BlockSize ("Block Size", Float) = 8.0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        ZWrite Off ZTest Always Blend Off Cull Off

        Pass
        {
            HLSLPROGRAM

            // Unityから使用するプロントを指定し宣言
            #pragma vertex Vert
            #pragma fragment Frag

            // おまじない
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float _BlockSize;

            half4 Frag(Varyings input) : SV_Target
            {
                float2 uv = input.texcoord;

                // 画面解像度を取得
                float2 resolution = _BlitTexture_TexelSize.zw;

                // UV をブロック単位に丸める
                float2 blockUV = floor(uv * resolution / _BlockSize) * _BlockSize / resolution;

                return SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, blockUV);
            }
            ENDHLSL
        }
    }
}
