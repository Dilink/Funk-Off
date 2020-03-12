// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_touche"
{
	Properties
	{
		_T_degra("T_degra", 2D) = "white" {}
		_STEP("STEP", Float) = 0.63
		_COLO("COLO", Color) = (1,0,0,0)
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
				float4 temp_cast_0 = (( 1.0 - _STEP )).xxxx;
				float4 lerpResult7 = lerp( _COLO , color8 , step( tex2D( _T_degra, uv_T_degra ) , temp_cast_0 ));
				
				
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
0;0;1920;1019;1017.469;298.6642;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;3;-909,312;Float;False;Property;_STEP;STEP;1;0;Create;True;0;0;False;0;0.63;0.14;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1011,-38;Float;True;Property;_T_degra;T_degra;0;0;Create;True;0;0;False;0;140bda0babc22a84eb9fa4ffaf75c9c2;140bda0babc22a84eb9fa4ffaf75c9c2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;14;-691.2664,218.137;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;2;-525,-43;Float;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;8;-346.9816,-284.6712;Float;False;Constant;_COLO_PAS_REMPLIE;COLO_PAS_REMPLIE;2;0;Create;True;0;0;False;0;0.5063899,0.4764151,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;6;-412.1072,285.6019;Float;False;Property;_COLO;COLO;2;0;Create;True;0;0;False;0;1,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;7;40.89282,-38.3981;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;4;443,7;Float;False;True;2;Float;ASEMaterialInspector;0;1;S_touche;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;14;0;3;0
WireConnection;2;0;1;0
WireConnection;2;1;14;0
WireConnection;7;0;6;0
WireConnection;7;1;8;0
WireConnection;7;2;2;0
WireConnection;4;0;7;0
ASEEND*/
//CHKSM=08DDF48455CA0A031C893572A293869166DCE3E4