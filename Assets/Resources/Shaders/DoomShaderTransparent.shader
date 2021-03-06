﻿Shader "Custom/DoomShaderTransparent" {
	Properties {
		[PerRendererData]_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	}
 
	SubShader 
	{
		Tags {"Queue"="Transparent"}
		
	CGPROGRAM
		#pragma surface surf Lambert alphatest:_Cutoff
 
		sampler2D _MainTex;
		fixed4 _Color;
 
		struct Input {
			float2 uv_MainTex;
		};
 
		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
	ENDCG
	
	}
 
	Fallback "Transparent/Cutout/VertexLit"
}
