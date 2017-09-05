Shader "Custom/LightAmp" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Brightness ("Brightness", Float) = 0.5
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
                float _Test;
				float _Brightness;
 
                float4 frag (v2f_img i) : COLOR 
				{
					float4 vAdd = float4(0.1,0.1,0.1,0); // just a float4 for use later
 
					float4 cColor = tex2D(_MainTex,i.uv); //this takes our sampler and turns the rgba into floats between 0 and 1

					if(((cColor.r + cColor.b + cColor.g) / 3) > 0.9)
					{
						cColor = cColor / 4;
					}

					float4 cTempColor = cColor; //a new float4 cTempColor for use later
					float4 cFinal = cTempColor + vAdd; //adds the floats together
 
					cFinal.g = (cTempColor.r + cTempColor.g + cTempColor.b)/3; // sets green the the average of rgb
					cFinal.r = 0; //removes red and blue colors
					cFinal.b = 0;

					cColor = cFinal * _Brightness; // brighten the final image and return it

                    return cColor;
                }
            ENDCG
        }
    }
    Fallback off
}

