// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_NEON"
{
	Properties
	{
		_T_NEON("T_NEON", 2D) = "white" {}
		_Speed("Speed", Vector) = (0,0.35,0,0)
		[HDR]_COLO_NEON("COLO_NEON", Color) = (0.3160377,1,0.9618113,0)
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

		uniform float4 _COLO_NEON;
		uniform sampler2D _T_NEON;
		uniform float2 _Speed;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color13 = IsGammaSpace() ? float4(0.09350502,0.07698468,0.09433961,0) : float4(0.009044255,0.006814759,0.009166724,0);
			float2 panner2 = ( _Time.y * _Speed + i.uv_texcoord);
			float4 tex2DNode1 = tex2D( _T_NEON, panner2 );
			float4 lerpResult14 = lerp( color13 , ( _COLO_NEON * ( 3.48 * tex2DNode1 ) ) , tex2DNode1);
			o.Emission = lerpResult14.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
0;0;1920;1019;752.3687;603.5322;1;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-1122.406,-249.3254;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;3;-1119.995,221.2747;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;5;-1135.638,-14.25409;Float;False;Property;_Speed;Speed;1;0;Create;True;0;0;False;0;0,0.35;0,0.35;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;2;-755.9954,-102.7253;Float;True;3;0;FLOAT2;1,1;False;2;FLOAT2;0,0;False;1;FLOAT;8.29;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-464.4911,-331.4859;Float;True;Property;_T_NEON;T_NEON;0;0;Create;True;0;0;False;0;e21b7d1bab6e4ef4ab79732775e1a463;e76c9f26d5c671b4eb9ba5f91a4d0f20;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-122.8219,-363.8151;Float;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;False;0;3.48;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-19.34073,-215.1986;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;11;383.1172,-474.5507;Float;False;Property;_COLO_NEON;COLO_NEON;2;1;[HDR];Create;True;0;0;False;0;0.3160377,1,0.9618113,0;0.3160377,1,0.9618113,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;427.6936,-179.8373;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;13;571.3561,-461.8149;Float;False;Constant;_Color1;Color 1;1;0;Create;True;0;0;False;0;0.09350502,0.07698468,0.09433961,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-840.2579,193.0167;Float;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;17;-9.329468,109.3031;Float;False;Property;_Keyword0;Keyword 0;3;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;14;907.8138,-350.8377;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1630.173,-314.4774;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_NEON;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;8;0
WireConnection;2;2;5;0
WireConnection;2;1;3;0
WireConnection;1;1;2;0
WireConnection;9;0;10;0
WireConnection;9;1;1;0
WireConnection;12;0;11;0
WireConnection;12;1;9;0
WireConnection;14;0;13;0
WireConnection;14;1;12;0
WireConnection;14;2;1;0
WireConnection;0;2;14;0
ASEEND*/
//CHKSM=02A8B10BD6012979F6C20EE07C9FE675A489D99A