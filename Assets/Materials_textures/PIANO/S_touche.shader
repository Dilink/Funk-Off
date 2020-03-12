// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_touche"
{
	Properties
	{
		_T_degra("T_degra", 2D) = "white" {}
		_STEP("STEP", Float) = 0.63
		_COLO("COLO", Color) = (1,0,0,0)
		_trame_test2("trame_test2", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		

		Pass
		{
			Name "Unlit"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform float4 _COLO;
			uniform sampler2D _T_degra;
			uniform float4 _T_degra_ST;
			uniform float _STEP;
			uniform sampler2D _trame_test2;
			uniform float4 _trame_test2_ST;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float4 color8 = IsGammaSpace() ? float4(0.5063899,0.4764151,1,0) : float4(0.2200033,0.192857,1,0);
				float2 uv_T_degra = i.ase_texcoord.xy * _T_degra_ST.xy + _T_degra_ST.zw;
				float4 tex2DNode1 = tex2D( _T_degra, uv_T_degra );
				float4 temp_cast_0 = (( 1.0 - _STEP )).xxxx;
				float4 temp_cast_1 = (( 1.0 - ( _STEP + -1.09 ) )).xxxx;
				float4 temp_cast_2 = (( 1.0 - ( _STEP + 1.0 ) )).xxxx;
				float4 smoothstepResult15 = smoothstep( temp_cast_1 , temp_cast_2 , tex2DNode1);
				float2 uv_trame_test2 = i.ase_texcoord.xy * _trame_test2_ST.xy + _trame_test2_ST.zw;
				float mulTime93 = _Time.y * 3.86;
				float clampResult32 = clamp( ( 0.39 * (0.93 + (abs( sin( mulTime93 ) ) - 0.0) * (1.09 - 0.93) / (1.0 - 0.0)) ) , 0.0 , 1.0 );
				float4 temp_cast_3 = (clampResult32).xxxx;
				float4 lerpResult7 = lerp( _COLO , color8 , ( step( tex2DNode1 , temp_cast_0 ) * step( ( ( 1.0 - smoothstepResult15 ) * tex2D( _trame_test2, uv_trame_test2 ) ) , temp_cast_3 ) ));
				
				
				finalColor = lerpResult7;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16100
0;0;1920;1019;591.1117;-232.9372;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;41;-1171.846,515.3735;Float;False;Constant;_Float4;Float 4;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;87;-1147.775,351.2248;Float;False;Constant;_Float5;Float 5;4;0;Create;True;0;0;False;0;-1.09;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;93;-111.6501,751.0529;Float;False;1;0;FLOAT;3.86;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-1181.691,176.7491;Float;False;Property;_STEP;STEP;1;0;Create;True;0;0;False;0;0.63;0.28;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;86;-738.8356,452.4351;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;88;-734.5883,207.6982;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;94;60.39441,746.963;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1089.794,-123.3361;Float;True;Property;_T_degra;T_degra;0;0;Create;True;0;0;False;0;140bda0babc22a84eb9fa4ffaf75c9c2;5fad52eb9b2e513409c61d3b89678f09;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;90;-501.3283,400.4203;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;89;-514.235,198.1156;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;95;202.3944,742.963;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;96;331.3944,725.963;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.93;False;4;FLOAT;1.09;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;138.1037,454.305;Float;False;Constant;_Float1;Float 1;4;0;Create;True;0;0;False;0;0.39;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;15;-223.5107,168.4242;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;34;608.2763,623.9326;Float;False;Constant;_Float3;Float 3;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;92;378.8499,465.7532;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;603.476,549.9324;Float;False;Constant;_Float2;Float 2;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;91;59.40098,215.4039;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;27;-221.0513,441.2316;Float;True;Property;_trame_test2;trame_test2;3;0;Create;True;0;0;False;0;e4e8d406a0c61a747add86208b257ef4;65dcdded5f83af9439e1f0ed82b5ae75;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;32;773.3124,404.0292;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;304.7748,172.6612;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;14;-808.1307,120.3881;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;2;-395.4152,-134.1936;Float;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;30;1029.69,142.9447;Float;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;1433.939,-51.32615;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;8;17.59786,-266.8074;Float;False;Constant;_COLO_PAS_REMPLIE;COLO_PAS_REMPLIE;2;0;Create;True;0;0;False;0;0.5063899,0.4764151,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;6;1195.101,-399.8483;Float;False;Property;_COLO;COLO;2;0;Create;True;0;0;False;0;1,0,0,0;1,0.8646695,0.514151,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;7;1762.727,-222.6636;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;4;2211.566,-218.2167;Float;False;True;2;Float;ASEMaterialInspector;0;1;S_touche;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;86;0;3;0
WireConnection;86;1;41;0
WireConnection;88;0;3;0
WireConnection;88;1;87;0
WireConnection;94;0;93;0
WireConnection;90;0;86;0
WireConnection;89;0;88;0
WireConnection;95;0;94;0
WireConnection;96;0;95;0
WireConnection;15;0;1;0
WireConnection;15;1;89;0
WireConnection;15;2;90;0
WireConnection;92;0;31;0
WireConnection;92;1;96;0
WireConnection;91;0;15;0
WireConnection;32;0;92;0
WireConnection;32;1;33;0
WireConnection;32;2;34;0
WireConnection;29;0;91;0
WireConnection;29;1;27;0
WireConnection;14;0;3;0
WireConnection;2;0;1;0
WireConnection;2;1;14;0
WireConnection;30;0;29;0
WireConnection;30;1;32;0
WireConnection;37;0;2;0
WireConnection;37;1;30;0
WireConnection;7;0;6;0
WireConnection;7;1;8;0
WireConnection;7;2;37;0
WireConnection;4;0;7;0
ASEEND*/
//CHKSM=4E5E0F118C7AF38EEFE8E8BF6E458919A68E4F61