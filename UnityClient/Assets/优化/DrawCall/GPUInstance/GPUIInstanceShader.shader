Shader "Custom/GPUIInstanceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex1 ("Albedo (RGB)", 2D) = "white" {}
        _MainTex2 ("Albedo (RGB)", 2D) = "white" {}
        _MainTex3 ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="TransparentCutout" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex1;
        sampler2D _MainTex2;
        sampler2D _MainTex3;

        struct Input
        {
            float2 uv_MainTex1;
            float2 uv_MainTex2;
            float2 uv_MainTex3;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_DEFINE_INSTANCED_PROP(fixed4, Color2)
            UNITY_DEFINE_INSTANCED_PROP(int, Type)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D(_MainTex1, IN.uv_MainTex1) * UNITY_ACCESS_INSTANCED_PROP(Props, Color2);
            if (UNITY_ACCESS_INSTANCED_PROP(Props, Type) == 1)
            {
                c = tex2D(_MainTex1, IN.uv_MainTex1) * UNITY_ACCESS_INSTANCED_PROP(Props, Color2);
            }
            else if (UNITY_ACCESS_INSTANCED_PROP(Props, Type) == 2)
            {
                c = tex2D(_MainTex2, IN.uv_MainTex2) * UNITY_ACCESS_INSTANCED_PROP(Props, Color2);
            }
            else if (UNITY_ACCESS_INSTANCED_PROP(Props, Type) == 3)
            {
                c = tex2D(_MainTex3, IN.uv_MainTex3) * UNITY_ACCESS_INSTANCED_PROP(Props, Color2);
            }


            o.Albedo = c.rgb ;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
