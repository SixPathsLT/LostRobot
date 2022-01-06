Shader "Custom/Player - Cloakable"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.0
        _Metallic("Metallic", Range(0,1)) = 0.0
        _NoiseTex("Noise", 2D) = "white" {}

        [HDR] _EmissionColor("Color", Color) = (0,0,0)
        _Cutoff("Cut off", Range(0, 1)) = 0.25
        _EdgeWidth("Edge Width", Range(0, 1)) = 0.05
        [HDR] _EdgeColor("Edge Color", Color) = (1,1,1,1)

        [Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull", Float) = 2
    }
    SubShader
    {
        Tags { "RenderType" = "Geometry" "Queue" = "Transparent" }
        LOD 200
        Cull[_Cull]

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard addshadow fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NoiseTex;

        half _Glossiness;
        half _Metallic;
        half _Cutoff;
        half _EdgeWidth;

        fixed4 _Color;
        fixed4 _EmissionColor;
        fixed4 _EdgeColor;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NoiseTex;
        };

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        fixed4 noisePixel, pixel;
        half cutoff;
        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            pixel = tex2D(_MainTex, IN.uv_NoiseTex) * _Color;
            o.Albedo = pixel.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            noisePixel = tex2D(_NoiseTex, IN.uv_NoiseTex);

            clip(noisePixel.r >= _Cutoff ? 1 : -1);
            o.Emission = noisePixel.r >= (_Cutoff * (_EdgeWidth + 1.0)) ? (pixel.rgb * tex2D(_MainTex, IN.uv_MainTex).a * _EmissionColor) : _EdgeColor;
        }
        ENDCG
    }
    FallBack "Diffuse"
}