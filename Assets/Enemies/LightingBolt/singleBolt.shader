Shader "Custom/singleBolt" {
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
            

			uniform float _Alpha;
			uniform fixed4 _Color;
			uniform float _RangePercentMax;
			uniform float _RangePercentMin;
			uniform float _Float;

            float4 frag() : COLOR 
			{		 
                return _Color;  
			}
            ENDCG
        }
    }
}