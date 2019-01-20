// Modified logic from Alan Zucconi

Shader "XAble/ColorBlindness"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_RedPattern("RedPattern", 2D) = "red" {}
		_GreenPattern("GreenPattern", 2D) = "green" {}
		_R("Red Mixing", Color) = (1,0,0,1)
		_G("Green Mixing", Color) = (0,1,0,1)
		_B("Blue Mixing", Color) = (0,0,1,1)
		_DR("DeutMix R", Color) = (0.56667, 0.43333, 0, 1)
		_DG("DeutMix G", Color) = (0.55833, 0.44167, 0, 1)
		_DB("DeutMix D", Color) = (0, .24167, 0.75833, 1)
		[Toggle(DEUTERANOPIA)]
		_DeutColorMode("Toggle Deuteranopia", Float) = 0
		_PR("ProtanMix R", Color) = (0.56667, 0.43333, 0, 1)
		_PG("ProtanMix G", Color) = (0.55833, 0.44167, 0, 1)
		_PB("ProtanMix B", Color) = (0, .24167, 0.75833, 1)
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
			sampler2D _RedPattern;
			sampler2D _GreenPattern;
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
			fixed4 redPat = tex2D(_RedPattern, i.uv);
			fixed4 greenPat = tex2D(_GreenPattern, i.uv);
                
				#ifdef PROTANOPIA
				return fixed4(
					col.r * _PR[0]  + col.g * _PR[1] + col.b * _PR[2],
					col.r * _PG[0]  + col.g * _PG[1] + col.b * _PG[2],
					col.r * _PB[0]  + col.g * _PB[1] + col.b * _PB[2],
					col.a

					/*
					* greenPat.g

					* (redPat.r * (1 -_PR[0]))



					fixed3 diff = abs(c.rgb - cb);
				return fixed4(lerp(bw, fixed3(1, 0, 0), saturate((diff.r + diff.g + diff.b) / 3)), c.a);
				*/
					

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
