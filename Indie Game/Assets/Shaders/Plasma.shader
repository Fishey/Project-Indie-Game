Shader "Custom/Plasma" {

	SubShader {
	
		Pass {
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
 			// Use shader model 3.0 target, to get nicer looking lighting
  			#pragma target 3.0			
			#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex;
			uniform float2 _MousePos;
			
			struct AppData {
				float4 pos : POSITION;
				float4 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};
			
			struct VertexToFragment {
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			VertexToFragment vert(AppData v) {
				VertexToFragment o;
				o.pos = mul(UNITY_MATRIX_MVP, v.pos);
				o.uv = v.uv;
				return o;
			}		
			
			float4 frag(VertexToFragment v) : SV_Target {
			    const float PI = 3.14159;
			    	
			    float horizontal = 0;

				float rotate = sin(10*(v.uv.x*sin(_Time.x/2)+v.uv.y*cos(_Time.x/3))) +_Time.x*10;
				
				float cx = v.uv.x + 0.5 * sin(_Time.x/5 + (_MousePos.x * 0.5));
				float cy = v.uv.y + 0.5 * cos(_Time.x/3 + (_MousePos.y * 0.5));
				float circle = sin(sqrt(100*((cx*cx) + (cy*cy)) + 1)+ (_Time*10));
				
				float num = horizontal + rotate + circle;
			
				float c = fmod(num, 1);
			 	//return float4(1, 0.075, 0.576, 1);
				return float4(sin(c*PI) +0.5, sin(c*PI)-0.4, 0, 1);
			}
			ENDCG
		}
	
	}
	FallBack Off
}
