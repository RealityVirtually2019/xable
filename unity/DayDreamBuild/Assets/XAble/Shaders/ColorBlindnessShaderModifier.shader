// Modified logic from Alan Zucconi

Shader "XAble/ColorBlindness"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_R("Red Mixing", Color) = (1,0,0,1)
		_G("Green Mixing", Color) = (0,1,0,1)
		_B("Blue Mixing", Color) = (0,0,1,1)
		_DR("DeutMix", Color) = (0.56667, 0.43333, 0, 1)
		_DG("DeutMix", Color) = (0.55833, 0.44167, 0, 1)
		_DB("DeutMix", Color) = (0, .24167, 0.75833, 1)
		[Toggle(DEUTERANOPIA)]
		_DeutColorMode("Toggle Deuteranopia", Float) = 0
		_PR("DeutMix", Color) = (0.56667, 0.43333, 0, 1)
		_PG("DeutMix", Color) = (0.55833, 0.44167, 0, 1)
		_PB("DeutMix", Color) = (0, .24167, 0.75833, 1)
		[Toggle(PROTANOPIA)]
		_ProtaColorMode("Toggle Protanopia", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
			#pragma vertex vert_img
            #pragma fragment frag
			#pragma shader_feature PROTANOPIA

            // make fog work
            #include "UnityCG.cginc"


            sampler2D _MainTex;
			uniform fixed3 _R;
			uniform fixed3 _G;
			uniform fixed3 _B;
			fixed4 _PR;
			fixed4 _PG;
			fixed4 _PB;
			uniform float _ProtaColorMode;




            fixed4 frag (v2f_img i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                
				#ifdef PROTANOPIA
				return fixed4(
					col.r * _PR[0] + col.g * _PR[1] + col.b * _PR[2],
					col.r * _PG[0] + col.g * _PG[1] + col.b * _PG[2],
					col.r * _PB[0] + col.g * _PB[1] + col.b * _PB[2],
					col.a
				#else
				return fixed4(
					col.r * _R[0] + col.g * _R[1] + col.b * _R[2],
					col.r * _G[0] + col.g * _G[1] + col.b * _G[2],
					col.r * _B[0] + col.g * _B[1] + col.b * _B[2],
					col.a
				#endif
				
				);
				
			}
			ENDCG
        }
    }
}
