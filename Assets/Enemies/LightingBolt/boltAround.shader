Shader "Custom/boltAround" {
    Properties {
       
		_Color ("Color", Color) = (1.0, 0.6, 0.6, 1.0)
    }

    SubShader {        
	Tags{"Queue" = "Transparent" }
	Pass {
			Blend SrcAlpha OneMinusSrcAlpha 
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"
            

			
			uniform fixed4 _Color;


            float4 frag() : COLOR 
			{		 				
                return float4(_Color.xyz, 0.05);  
			}
            ENDCG
        }
    }
}