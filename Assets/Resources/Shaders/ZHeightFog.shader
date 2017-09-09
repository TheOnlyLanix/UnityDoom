// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ZHeightFog"
{
    Properties 
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _UpperFogColor ("Upper Fog Color", Color) = (0.5, 0.5, 0.5, 1)
		_LowerFogColor ("Lower Fog Color", Color) = (0.5, 0.5, 0.5, 1)
		//Bottom
        _FogMaxHeight ("Fog Lower Max Height", Float) = 0.0
        _FogMinHeight ("Fog Lower Min Height", Float) = -1.0
		//Top
		_FogMaxHeight1 ("Fog Upper Max Height", Float) = -1.0
        _FogMinHeight1 ("Fog Upper Min Height", Float) = 0.0
    }
  
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Back
        ZWrite On
  
        CGPROGRAM
  
        #pragma surface surf Lambert finalcolor:finalcolor vertex:vert
  
        sampler2D _MainTex;
        float4 _UpperFogColor;
		float4 _LowerFogColor;
        float _FogMaxHeight;
        float _FogMinHeight;
  
        float _FogMaxHeight1;
        float _FogMinHeight1;

        struct Input 
        {
            float2 uv_MainTex;
            float4 pos;
        };
  
        void vert (inout appdata_full v, out Input o) 
        {
            float4 hpos = UnityObjectToClipPos (v.vertex);
            o.pos = mul(unity_ObjectToWorld, v.vertex);
            o.uv_MainTex = v.texcoord.xy;
        }
  
        void surf (Input IN, inout SurfaceOutput o) 
        {
            float4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
        }
  
        void finalcolor (Input IN, SurfaceOutput o, inout fixed4 color)
        {
            #ifndef UNITY_PASS_FORWARDADD
            float lerpValue = clamp((IN.pos.y - _FogMinHeight) / (_FogMaxHeight - _FogMinHeight), 0, 1);
			float lerpValue1 = clamp((IN.pos.y - _FogMaxHeight1) / (_FogMinHeight1 - _FogMaxHeight1), 0, 1);
			float combine = lerpValue * lerpValue1;

			float3 combine1 = lerp (_UpperFogColor.rgb, color.rgb, lerpValue1);
			float3 combine2 = lerp (_LowerFogColor.rgb, combine1, lerpValue);

			color.rgb = lerp( combine2, color.rgb, combine);

            #endif
        }
  
        ENDCG
    }
  
    FallBack "Diffuse"
}