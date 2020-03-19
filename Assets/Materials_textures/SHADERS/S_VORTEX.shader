// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_VORTEX"
{
	Properties
	{
		_Color0("Color 0", Color) = (0.6745098,0.7254902,1,1)
		_T_Vortex("T_Vortex", 2D) = "white" {}
		_Float2("Float 2", Float) = 1
		_T_Vortex1("T_Vortex1", 2D) = "white" {}
		_T_Vortex2("T_Vortex2", 2D) = "white" {}
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

		uniform float4 _Color0;
		uniform sampler2D _T_Vortex;
		uniform sampler2D _T_Vortex1;
		uniform sampler2D _T_Vortex2;
		uniform float _Float2;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 _Vector0 = float2(0.5,0.5);
			float cos15 = cos( 2.0 * _Time.y );
			float sin15 = sin( 2.0 * _Time.y );
			float2 rotator15 = mul( i.uv_texcoord - _Vector0 , float2x2( cos15 , -sin15 , sin15 , cos15 )) + _Vector0;
			float4 tex2DNode11 = tex2D( _T_Vortex, rotator15 );
			float4 layeredBlendVar28 = float4( 0.5,0,0.03755474,0 );
			float4 layeredBlend28 = ( lerp( lerp( lerp( lerp( tex2DNode11 , float4( 1,1,1,0 ) , layeredBlendVar28.x ) , float4( 0,0,0,0 ) , layeredBlendVar28.y ) , float4( 0,0,0,0 ) , layeredBlendVar28.z ) , float4( 0,0,0,0 ) , layeredBlendVar28.w ) );
			float cos18 = cos( 4.0 * _Time.y );
			float sin18 = sin( 4.0 * _Time.y );
			float2 rotator18 = mul( i.uv_texcoord - _Vector0 , float2x2( cos18 , -sin18 , sin18 , cos18 )) + _Vector0;
			float4 lerpResult8 = lerp( float4( 0,0,0,0 ) , layeredBlend28 , tex2D( _T_Vortex1, rotator18 ));
			float4 layeredBlendVar54 = float4( 0.5377358,0,0,0 );
			float4 layeredBlend54 = ( lerp( lerp( lerp( lerp( lerpResult8 , float4( 1,1,1,0 ) , layeredBlendVar54.x ) , float4( 0,0,0,0 ) , layeredBlendVar54.y ) , float4( 0,0,0,0 ) , layeredBlendVar54.z ) , float4( 0,0,0,0 ) , layeredBlendVar54.w ) );
			float4 temp_output_6_0 = ( ( _Color0 * layeredBlend54 ) * 1.5 );
			o.Albedo = ( temp_output_6_0 * 1.1 ).rgb;
			float cos39 = cos( 0.5 * _Time.y );
			float sin39 = sin( 0.5 * _Time.y );
			float2 rotator39 = mul( i.uv_texcoord - _Vector0 , float2x2( cos39 , -sin39 , sin39 , cos39 )) + _Vector0;
			float mulTime33 = _Time.y * 3.0;
			float4 lerpResult41 = lerp( tex2D( _T_Vortex2, rotator39 ) , float4( 0,0,0,0 ) , ( (0.0 + (abs( sin( mulTime33 ) ) - 0.0) * (0.1 - 0.0) / (0.1 - 0.0)) * _Float2 ));
			o.Emission = ( ( temp_output_6_0 * lerpResult41 ) * 0.2 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
0;0;1920;1019;1011.145;92.9144;1;True;False
Node;AmplifyShaderEditor.Vector2Node;17;-4411.399,-85.90992;Float;False;Constant;_Vector0;Vector 0;6;0;Create;True;0;0;False;0;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-4451.791,-229.1632;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;32;-4195.758,550.9035;Float;False;Constant;_speed;speed;10;0;Create;True;0;0;False;0;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;15;-3702.412,-192.0677;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;18;-3710.512,46.24499;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;4;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;11;-3445.997,-216.6011;Float;True;Property;_T_Vortex;T_Vortex;1;0;Create;True;0;0;False;0;6e446aeebbd6f0c45ba29307e2369545;6e446aeebbd6f0c45ba29307e2369545;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;33;-3991.758,583.9035;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;34;-3815.757,590.9035;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LayeredBlendNode;28;-3056.112,-211.4649;Float;True;6;0;COLOR;0.5,0,0.03755474,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;12;-3452.787,38.29681;Float;True;Property;_T_Vortex1;T_Vortex1;4;0;Create;True;0;0;False;0;835e291ba0835ef4c837683c782ef842;835e291ba0835ef4c837683c782ef842;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;35;-3682.757,601.9036;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-3797.757,723.9036;Float;False;Constant;_min_emi;min_emi;5;0;Create;True;0;0;False;0;0;0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-3794.757,841.9037;Float;False;Constant;_max_emi;max_emi;8;0;Create;True;0;0;False;0;0.1;1.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;8;-2672.511,-3.266944;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;3;-2102.552,-279.3516;Float;False;Property;_Color0;Color 0;0;0;Create;True;0;0;False;0;0.6745098,0.7254902,1,1;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotatorNode;39;-3674.218,299.8436;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;30;-3543.756,602.9036;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-3457.696,775.9797;Float;False;Property;_Float2;Float 2;2;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LayeredBlendNode;54;-2414.302,-0.2412572;Float;True;6;0;COLOR;0.5377358,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-1027.941,31.95486;Float;False;Constant;_Float0;Float 0;12;0;Create;True;0;0;False;0;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-1795.552,-140.3516;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;13;-3452.87,304.7254;Float;True;Property;_T_Vortex2;T_Vortex2;6;0;Create;True;0;0;False;0;c56d2a82a9ebb38479d41b025fc4cc2f;c56d2a82a9ebb38479d41b025fc4cc2f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-3340.756,592.9036;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-827.529,-65.19775;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;41;-2953.489,456.5015;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-235.3414,-74.17478;Float;False;Constant;_Float6;Float 6;12;0;Create;True;0;0;False;0;1.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-137.9119,458.8082;Float;False;Constant;_Float7;Float 7;9;0;Create;True;0;0;False;0;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-451.6887,337.4589;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;27;-3411.225,-736.0333;Float;True;Screen;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-1170.068,86.8138;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;62.08813,303.8082;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-1627.069,217.8138;Float;False;Property;_Float3;Float 3;5;0;Create;True;0;0;False;0;0.5;0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;9.538389,-281.0561;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.AbsOpNode;48;-1512.069,95.81381;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;49;-1329.008,264.8899;Float;False;Property;_Float5;Float 5;3;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;45;-1645.069,84.81362;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;50;-1373.068,96.81381;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;47;-1624.069,335.814;Float;False;Property;_Float4;Float 4;7;0;Create;True;0;0;False;0;1;1.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;44;-1821.07,77.81362;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-1978.576,80.68014;Float;False;Property;_Float1;Float 1;8;0;Create;True;0;0;False;0;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;525.7387,66.37856;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_VORTEX;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;15;0;14;0
WireConnection;15;1;17;0
WireConnection;18;0;14;0
WireConnection;18;1;17;0
WireConnection;11;1;15;0
WireConnection;33;0;32;0
WireConnection;34;0;33;0
WireConnection;28;1;11;0
WireConnection;12;1;18;0
WireConnection;35;0;34;0
WireConnection;8;1;28;0
WireConnection;8;2;12;0
WireConnection;39;0;14;0
WireConnection;39;1;17;0
WireConnection;30;0;35;0
WireConnection;30;1;36;0
WireConnection;30;2;37;0
WireConnection;30;3;36;0
WireConnection;30;4;37;0
WireConnection;54;1;8;0
WireConnection;4;0;3;0
WireConnection;4;1;54;0
WireConnection;13;1;39;0
WireConnection;38;0;30;0
WireConnection;38;1;31;0
WireConnection;6;0;4;0
WireConnection;6;1;53;0
WireConnection;41;0;13;0
WireConnection;41;2;38;0
WireConnection;52;0;6;0
WireConnection;52;1;41;0
WireConnection;27;0;11;0
WireConnection;51;0;50;0
WireConnection;51;1;49;0
WireConnection;57;0;52;0
WireConnection;57;1;58;0
WireConnection;55;0;6;0
WireConnection;55;1;56;0
WireConnection;48;0;45;0
WireConnection;45;0;44;0
WireConnection;50;0;48;0
WireConnection;50;3;46;0
WireConnection;50;4;47;0
WireConnection;44;0;43;0
WireConnection;0;0;55;0
WireConnection;0;2;57;0
ASEEND*/
//CHKSM=10D6558833FDC8657812DE5CA56481501A3AD74D