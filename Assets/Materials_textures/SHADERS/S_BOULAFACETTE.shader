// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "New Amplify Shader"
{
	Properties
	{
		_boule_facette("boule_facette", 2D) = "white" {}
		_boule_facette2("boule_facette2", 2D) = "white" {}
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

		uniform sampler2D _boule_facette;
		uniform sampler2D _boule_facette2;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color6 = IsGammaSpace() ? float4(0.9433962,0.6750214,0.2803489,0) : float4(0.8760344,0.4132373,0.06388318,0);
			float2 panner4 = ( 1.0 * _Time.y * float2( 0.42,0 ) + float2( 0,0 ));
			float2 uv_TexCoord3 = i.uv_texcoord * float2( 4,4 ) + panner4;
			float4 lerpResult9 = lerp( float4( 0,0,0,0 ) , ( ( color6 * tex2D( _boule_facette, uv_TexCoord3 ) ) * 1.52 ) , tex2D( _boule_facette2, uv_TexCoord3 ));
			o.Emission = lerpResult9.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
0;0;1920;1019;1144.386;554.3536;1;True;True
Node;AmplifyShaderEditor.PannerNode;4;-1065.394,-9.953421;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.42,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-806.7537,0.3273926;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;4,4;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-608.1614,-175.2524;Float;True;Property;_boule_facette;boule_facette;0;0;Create;True;0;0;False;0;0791a805f40cfcf4aa7c57275bbba2a7;0791a805f40cfcf4aa7c57275bbba2a7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;6;-515.6804,-341.1047;Float;False;Constant;_Color0;Color 0;2;0;Create;True;0;0;False;0;0.9433962,0.6750214,0.2803489,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-230.6804,-244.1047;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-250.052,-119.0173;Float;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;False;0;1.52;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-63.052,-174.0173;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;2;-223.3614,98.44758;Float;True;Property;_boule_facette2;boule_facette2;1;0;Create;True;0;0;False;0;055683aeb71d83340a8ab3b22ce0ed00;055683aeb71d83340a8ab3b22ce0ed00;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;9;129.3052,-16.48721;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;371,-76;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;New Amplify Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;1;4;0
WireConnection;1;1;3;0
WireConnection;5;0;6;0
WireConnection;5;1;1;0
WireConnection;7;0;5;0
WireConnection;7;1;8;0
WireConnection;2;1;3;0
WireConnection;9;1;7;0
WireConnection;9;2;2;0
WireConnection;0;2;9;0
ASEEND*/
//CHKSM=2403A060884DDED9C37501614BA6EE254F6DED8C