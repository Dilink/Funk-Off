// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "New Amplify Shader"
{
	Properties
	{
		_dots_paillettes_001("dots_paillettes_001", 2D) = "white" {}
		_Color0("Color 0", Color) = (0.7735849,0.07662869,0.1973516,0)
		_canap_002_DefaultMaterial_Normal("canap_002_DefaultMaterial_Normal", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
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

		uniform sampler2D _canap_002_DefaultMaterial_Normal;
		uniform float4 _Color0;
		uniform sampler2D _TextureSample0;
		uniform sampler2D _dots_paillettes_001;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Normal = tex2D( _canap_002_DefaultMaterial_Normal, i.uv_texcoord ).rgb;
			o.Albedo = _Color0.rgb;
			float4 temp_cast_2 = (0.0).xxxx;
			float4 color39 = IsGammaSpace() ? float4(1,0,0,0) : float4(1,0,0,0);
			float2 uv_TexCoord24 = i.uv_texcoord * float2( 4,4 );
			float4 lerpResult37 = lerp( temp_cast_2 , color39 , (0.0 + (( abs( ( 10.0 / _SinTime.x ) ) * tex2D( _TextureSample0, uv_TexCoord24 ) ) - 0.0) * (1.0 - 0.0) / (5.0 - 0.0)));
			o.Emission = lerpResult37.rgb;
			float2 uv_TexCoord3 = i.uv_texcoord * float2( 2,2 );
			o.Smoothness = tex2D( _dots_paillettes_001, uv_TexCoord3 ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
0;0;1920;1019;3337.075;1832.386;3.242113;True;True
Node;AmplifyShaderEditor.SinTimeNode;18;-1275.98,-69.59126;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;31;-1302.854,-200.5775;Float;True;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;30;-1079.854,-106.5775;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;24;-1278.146,148.3811;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;4,4;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;16;-1002.731,143.3361;Float;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;True;0;0;False;0;2a3f7594b748a0548b6a14a3d13cd016;2a3f7594b748a0548b6a14a3d13cd016;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;20;-947.7314,-1.663948;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-642.5549,215.9611;Float;False;Constant;_Float2;Float 2;4;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-712.7313,40.33606;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-636.8401,146.2412;Float;False;Constant;_Float1;Float 1;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-664.2528,290.886;Float;False;Constant;_Float3;Float 3;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-645.2528,372.886;Float;False;Constant;_Float4;Float 4;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-179.99,-68.07977;Float;False;Constant;_Float5;Float 5;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;29;-387.2827,146.622;Float;False;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-466.9724,-296.1546;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;39;-305.7701,3.158081;Float;False;Constant;_Color1;Color 1;4;0;Create;True;0;0;False;0;1,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-621.1713,504.7227;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2,2;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;13;-135.9882,-463.09;Float;False;Property;_Color0;Color 0;1;0;Create;True;0;0;False;0;0.7735849,0.07662869,0.1973516,0;1,0.2494922,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BlendOpsNode;36;122.9362,506.9775;Float;False;ColorBurn;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;4;-339.1713,479.7227;Float;True;Property;_dots_paillettes_001;dots_paillettes_001;0;0;Create;True;0;0;False;0;2a3f7594b748a0548b6a14a3d13cd016;2a3f7594b748a0548b6a14a3d13cd016;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;37;45.32105,44.24672;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;14;-197.9724,-283.1546;Float;True;Property;_canap_002_DefaultMaterial_Normal;canap_002_DefaultMaterial_Normal;2;0;Create;True;0;0;False;0;e1e721195721e9e4ca9dda7007f19bd3;e1e721195721e9e4ca9dda7007f19bd3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;368,-46;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;New Amplify Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;30;0;31;0
WireConnection;30;1;18;1
WireConnection;16;1;24;0
WireConnection;20;0;30;0
WireConnection;17;0;20;0
WireConnection;17;1;16;0
WireConnection;29;0;17;0
WireConnection;29;1;32;0
WireConnection;29;2;33;0
WireConnection;29;3;34;0
WireConnection;29;4;35;0
WireConnection;4;1;3;0
WireConnection;37;0;38;0
WireConnection;37;1;39;0
WireConnection;37;2;29;0
WireConnection;14;1;15;0
WireConnection;0;0;13;0
WireConnection;0;1;14;0
WireConnection;0;2;37;0
WireConnection;0;4;4;0
ASEEND*/
//CHKSM=44CD9C2A0BE693FDE70AD7C3A0A9CE8995345DE4