Shader "Custom/Powerups/Radsuit" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
		_Tint ("Tint Color(red)", Color) = (0,0.7,0,1)
    }
    SubShader {
        Pass {
            ZTest Always Cull Off ZWrite Off Lighting Off Fog { Mode off }
            CGPROGRAM
                #pragma vertex vert_img
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
 
                sampler2D _MainTex;

				//red tint
				fixed4 _Tint = (0,0.7,0,1);

                float4 frag (v2f_img i) : COLOR 
				{
					float4 col = tex2D(_MainTex,i.uv) + _Tint/3;
                    return col;
                }
            ENDCG
        }
    }
    Fallback off
}