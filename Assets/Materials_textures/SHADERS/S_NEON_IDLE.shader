// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_NEON_IDLE"
{
	Properties
	{
		_T_NEON3("T_NEON3", 2D) = "white" {}
		_T_NEON2("T_NEON2", 2D) = "white" {}
		_T_NEON4("T_NEON4", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _T_NEON2;
		uniform float4 _T_NEON2_ST;
		uniform sampler2D _T_NEON4;
		uniform float4 _T_NEON4_ST;
		uniform sampler2D _T_NEON3;
		uniform float4 _T_NEON3_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color13 = IsGammaSpace() ? float4(0.5801887,1,0.9400654,0) : float4(0.2959107,1,0.8690367,0);
			float2 uv_T_NEON2 = i.uv_texcoord * _T_NEON2_ST.xy + _T_NEON2_ST.zw;
			float2 uv_T_NEON4 = i.uv_texcoord * _T_NEON4_ST.xy + _T_NEON4_ST.zw;
			float2 uv_T_NEON3 = i.uv_texcoord * _T_NEON3_ST.xy + _T_NEON3_ST.zw;
			float4 ifLocalVar5 = 0;
			if( abs( sin( _Time.y ) ) > 0.5 )
				ifLocalVar5 = tex2D( _T_NEON2, uv_T_NEON2 );
			else if( abs( sin( _Time.y ) ) == 0.5 )
				ifLocalVar5 = tex2D( _T_NEON4, uv_T_NEON4 );
			else if( abs( sin( _Time.y ) ) < 0.5 )
				ifLocalVar5 = tex2D( _T_NEON3, uv_T_NEON3 );
			o.Emission = ( color13 * ( 2.35 * ifLocalVar5 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
0;0;1920;1019;1702.837;240.7211;1;True;True
Node;AmplifyShaderEditor.SimpleTimeNode;7;-1273.61,484.5455;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;6;-1066.61,479.5455;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1219.922,-2.469482;Float;True;Property;_T_NEON3;T_NEON3;0;0;Create;True;0;0;False;0;0ead020be5af639418669951a0290263;0ead020be5af639418669951a0290263;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1207.922,209.5305;Float;True;Property;_T_NEON2;T_NEON2;1;0;Create;True;0;0;False;0;e21b7d1bab6e4ef4ab79732775e1a463;e21b7d1bab6e4ef4ab79732775e1a463;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-1217.922,-217.4695;Float;True;Property;_T_NEON4;T_NEON4;2;0;Create;True;0;0;False;0;e0da929c24ba71a4585a80bed0a426f9;e0da929c24ba71a4585a80bed0a426f9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;8;-912.61,478.5455;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-880.9904,288.1492;Float;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;5;-505.614,195.8205;Float;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-506.6241,-14.56305;Float;False;Constant;_Float1;Float 1;3;0;Create;True;0;0;False;0;2.35;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-207.6241,1.436951;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;13;-211.4242,-160.5631;Float;False;Constant;_Color0;Color 0;3;0;Create;True;0;0;False;0;0.5801887,1,0.9400654,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;125.1759,-12.96305;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;572.2521,-49.61346;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_NEON_IDLE;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;7;0
WireConnection;8;0;6;0
WireConnection;5;0;8;0
WireConnection;5;1;10;0
WireConnection;5;2;2;0
WireConnection;5;3;3;0
WireConnection;5;4;1;0
WireConnection;11;0;12;0
WireConnection;11;1;5;0
WireConnection;14;0;13;0
WireConnection;14;1;11;0
WireConnection;0;2;14;0
ASEEND*/
//CHKSM=92B70EBAC85684C99D6F79CA7BBE7260CC65FB16