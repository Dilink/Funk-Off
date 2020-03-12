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
				float4 temp_cast_1 = (( _STEP + 0.74 )).xxxx;
				float4 temp_cast_2 = (( _STEP + 1.0 )).xxxx;
				float4 smoothstepResult15 = smoothstep( temp_cast_1 , temp_cast_2 , tex2DNode1);
				float2 uv_trame_test2 = i.ase_texcoord.xy * _trame_test2_ST.xy + _trame_test2_ST.zw;
				float clampResult32 = clamp( 0.5 , 0.0 , 1.0 );
				float4 temp_cast_3 = (clampResult32).xxxx;
				float4 lerpResult7 = lerp( _COLO , color8 , ( step( tex2DNode1 , temp_cast_0 ) * step( ( smoothstepResult15 * tex2D( _trame_test2, uv_trame_test2 ) ) , temp_cast_3 ) ));
				
				
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
0;0;1920;1019;529.4609;354.7686;1.682553;True;True
Node;AmplifyShaderEditor.RangedFloatNode;3;-1181.691,176.7491;Float;False;Property;_STEP;STEP;1;0;Create;True;0;0;False;0;0.63;0.13;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;87;-1147.775,351.2248;Float;False;Constant;_Float5;Float 5;4;0;Create;True;0;0;False;0;0.74;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-1171.846,515.3735;Float;False;Constant;_Float4;Float 4;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;86;-757.6593,461.8469;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1089.794,-123.3361;Float;True;Property;_T_degra;T_degra;0;0;Create;True;0;0;False;0;140bda0babc22a84eb9fa4ffaf75c9c2;5fad52eb9b2e513409c61d3b89678f09;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;88;-734.5883,207.6982;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;15;-223.5107,168.4242;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;27;-276.8715,483.0967;Float;True;Property;_trame_test2;trame_test2;3;0;Create;True;0;0;False;0;e4e8d406a0c61a747add86208b257ef4;65dcdded5f83af9439e1f0ed82b5ae75;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;34;474.7767,584.3324;Float;False;Constant;_Float3;Float 3;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;469.9764,510.3324;Float;False;Constant;_Float2;Float 2;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;466.1038,442.005;Float;False;Constant;_Float1;Float 1;4;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;103.1485,181.5728;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;14;-911.5493,82.16818;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;32;648.3124,463.8292;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;30;1029.69,142.9447;Float;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;2;-535.5678,-98.5184;Float;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;6;1195.101,-399.8483;Float;False;Property;_COLO;COLO;2;0;Create;True;0;0;False;0;1,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;1433.939,-51.32615;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;8;17.59786,-266.8074;Float;False;Constant;_COLO_PAS_REMPLIE;COLO_PAS_REMPLIE;2;0;Create;True;0;0;False;0;0.5063899,0.4764151,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;48;-1669.169,878.7537;Float;False;Property;_SPEED;SPEED;4;0;Create;True;0;0;False;0;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;42;-1499.073,871.0877;Float;False;1;0;FLOAT;5.24;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;46;-734.6422,910.6019;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;43;-1317.073,900.0878;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;44;-1163.073,890.0877;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-941.2428,1097.131;Float;False;Constant;_Float0;Float 0;3;0;Create;True;0;0;False;0;1.43;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;7;1762.727,-222.6636;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;85;-578.4237,882.4149;Float;False;2;0;FLOAT;0;False;1;FLOAT;0.44;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;47;-964.8745,896.9892;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.1;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;4;2211.566,-218.2167;Float;False;True;2;Float;ASEMaterialInspector;0;1;S_touche;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;86;0;3;0
WireConnection;86;1;41;0
WireConnection;88;0;3;0
WireConnection;88;1;87;0
WireConnection;15;0;1;0
WireConnection;15;1;88;0
WireConnection;15;2;86;0
WireConnection;29;0;15;0
WireConnection;29;1;27;0
WireConnection;14;0;3;0
WireConnection;32;0;31;0
WireConnection;32;1;33;0
WireConnection;32;2;34;0
WireConnection;30;0;29;0
WireConnection;30;1;32;0
WireConnection;2;0;1;0
WireConnection;2;1;14;0
WireConnection;37;0;2;0
WireConnection;37;1;30;0
WireConnection;42;0;48;0
WireConnection;46;0;47;0
WireConnection;46;1;16;0
WireConnection;43;0;42;0
WireConnection;44;0;43;0
WireConnection;7;0;6;0
WireConnection;7;1;8;0
WireConnection;7;2;37;0
WireConnection;85;0;46;0
WireConnection;47;0;44;0
WireConnection;4;0;7;0
ASEEND*/
//CHKSM=EFDB3A0B0216802EFFDCCD6EA3B6C1CD50A8B2AF