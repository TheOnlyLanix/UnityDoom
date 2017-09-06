Shader "Custom/Powerups/Berserk" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
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

                float4 frag (v2f_img i) : COLOR 
				{
					float4 cColor = tex2D(_MainTex,i.uv); //this takes our sampler and turns the rgba into floats between 0 and 1

					float4 cTempColor = cColor; //a new float4 cTempColor for use later
					float4 cFinal = cTempColor;
 
					cFinal.g = 0;
					cFinal.r = (cTempColor.r + cTempColor.g + cTempColor.b)/3;
					cFinal.b = 0;

					cColor = cFinal/0.5;

                    return cColor;
                }
            ENDCG
        }
    }
    Fallback off
}

