// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader_dancefloor"
{
	Properties
	{
		_Image_alltiles("Image_alltiles", 2D) = "white" {}
		_Image_alltiles2("Image_alltiles2", 2D) = "white" {}
		_Trame("Trame", 2D) = "white" {}
		[Toggle(_USE_TEXT_GEN_ON)] _Use_text_gen("Use_text_gen", Float) = 0
		_Multi_piste("Multi_piste", Float) = 2.82
		_color_pattern_global("color_pattern_global", Color) = (1,0.2760729,0,1)
		_simpletile("simpletile", 2D) = "white" {}
		_color_pattern_par_plaque("color_pattern_par_plaque", Color) = (1,1,1,0)
		_Tilling_simpletile("Tilling_simpletile", Vector) = (1,1,0,0)
		_OFFSET_ONETILE("OFFSET_ONETILE", Vector) = (1,1,0,0)
		_OFFSET_GLOBAL("OFFSET_GLOBAL", Vector) = (0,0,0,0)
		_OFFSET_GLOBAL2("OFFSET_GLOBAL2", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityPBSLighting.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma shader_feature _USE_TEXT_GEN_ON
		#pragma surface surf StandardCustomLighting keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv2_texcoord2;
			float2 uv_texcoord;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform float4 _color_pattern_global;
		uniform sampler2D _Image_alltiles;
		uniform float2 _OFFSET_GLOBAL;
		uniform sampler2D _Image_alltiles2;
		uniform float2 _OFFSET_GLOBAL2;
		uniform float4 _color_pattern_par_plaque;
		uniform sampler2D _simpletile;
		uniform float2 _Tilling_simpletile;
		uniform float2 _OFFSET_ONETILE;
		uniform sampler2D _Trame;
		uniform float _Multi_piste;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			c.rgb = 0;
			c.a = 1;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			float mulTime47 = _Time.y * 3.37;
			float2 uv2_TexCoord1 = i.uv2_texcoord2 + _OFFSET_GLOBAL;
			float2 uv2_TexCoord41 = i.uv2_texcoord2 + _OFFSET_GLOBAL2;
			float4 temp_output_45_0 = ( tex2D( _Image_alltiles, uv2_TexCoord1 ) + tex2D( _Image_alltiles2, uv2_TexCoord41 ) );
			float4 color19 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
			#ifdef _USE_TEXT_GEN_ON
				float4 staticSwitch16 = color19;
			#else
				float4 staticSwitch16 = ( _color_pattern_global * temp_output_45_0 );
			#endif
			float2 uv_TexCoord29 = i.uv_texcoord * _Tilling_simpletile + _OFFSET_ONETILE;
			float4 lerpResult26 = lerp( staticSwitch16 , ( _color_pattern_par_plaque * tex2D( _simpletile, uv_TexCoord29 ) ) , ( 1.0 - temp_output_45_0 ));
			float2 uv_TexCoord2 = i.uv_texcoord * float2( 3,3 );
			float4 tex2DNode5 = tex2D( _Trame, uv_TexCoord2 );
			float4 temp_output_6_0 = ( lerpResult26 * tex2DNode5 );
			float4 temp_cast_0 = (0.49).xxxx;
			float4 lerpResult13 = lerp( ( ( abs( sin( mulTime47 ) ) + 1.0 ) * ( temp_output_6_0 * _Multi_piste ) ) , ( tex2DNode5 * 0.2 ) , ( 1.0 - step( temp_cast_0 , temp_output_6_0 ) ));
			o.Emission = lerpResult13.rgb;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
0;0;1920;1019;984.4005;1255.714;2.809644;True;True
Node;AmplifyShaderEditor.Vector2Node;42;-2112.069,-347.0349;Float;False;Property;_OFFSET_GLOBAL2;OFFSET_GLOBAL2;11;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;39;-2121.059,-544.5282;Float;False;Property;_OFFSET_GLOBAL;OFFSET_GLOBAL;10;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;41;-1876.296,-359.7647;Float;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-1885.286,-557.258;Float;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;24;-1669.286,-770.104;Float;False;1340.245;706.5484;Comment;7;4;21;19;16;20;43;45;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;43;-1622.63,-357.1689;Float;True;Property;_Image_alltiles2;Image_alltiles2;1;0;Create;True;0;0;False;0;4f9b9ea0ec1a1504ca4296b9f6e9076b;4f9b9ea0ec1a1504ca4296b9f6e9076b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-1631.62,-554.6622;Float;True;Property;_Image_alltiles;Image_alltiles;0;0;Create;True;0;0;False;0;4f9b9ea0ec1a1504ca4296b9f6e9076b;4f9b9ea0ec1a1504ca4296b9f6e9076b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;38;-1688.079,-1001.498;Float;False;Property;_OFFSET_ONETILE;OFFSET_ONETILE;9;0;Create;True;0;0;False;0;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;37;-1753.074,-1151.853;Float;False;Property;_Tilling_simpletile;Tilling_simpletile;8;0;Create;True;0;0;False;0;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-1429.051,-1120.087;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;45;-1263.817,-440.282;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;20;-1333.569,-765.104;Float;False;Property;_color_pattern_global;color_pattern_global;5;0;Create;True;0;0;False;0;1,0.2760729,0,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;34;-1070.281,-1315.026;Float;False;Property;_color_pattern_par_plaque;color_pattern_par_plaque;7;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;19;-717.7513,-235.6032;Float;False;Constant;_Color0;Color 0;3;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;28;-1080.217,-1123.896;Float;True;Property;_simpletile;simpletile;6;0;Create;True;0;0;False;0;4f9b9ea0ec1a1504ca4296b9f6e9076b;4f9b9ea0ec1a1504ca4296b9f6e9076b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1015.134,-672.7747;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;16;-553.1422,-453.4939;Float;False;Property;_Use_text_gen;Use_text_gen;3;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleTimeNode;47;872.781,-523.8847;Float;False;1;0;FLOAT;3.37;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;35;-428.4884,-613.8857;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-755.4805,-1135.915;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-695.5803,85.27425;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;3,3;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-327.5803,101.2743;Float;True;Property;_Trame;Trame;2;0;Create;True;0;0;False;0;d61fdecf04c1ff848bd486d0048e53b0;d61fdecf04c1ff848bd486d0048e53b0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;26;-114.5853,-664.276;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SinOpNode;48;1060.781,-529.8847;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;517.3616,-488.3224;Float;False;Property;_Multi_piste;Multi_piste;4;0;Create;True;0;0;False;0;2.82;2.82;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;128,-144;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.AbsOpNode;49;1210.781,-531.8847;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;169,-289;Float;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;False;0;0.49;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;51;1209.781,-438.8847;Float;False;Constant;_Float1;Float 1;12;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;50;1384.781,-510.8847;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;33.24018,361.6641;Float;False;Constant;_Fond;Fond;3;0;Create;True;0;0;False;0;0.2;0.06;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;7;398,-257;Float;True;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;758.2856,-322.4604;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;14;828.5357,83.35933;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;464,318.2;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;1558.214,-235.5094;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;13;1903.688,-18.88841;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;44;-169.8174,-312.282;Float;False;ColorBurn;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2315.353,-129.4284;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;Shader_dancefloor;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;41;1;42;0
WireConnection;1;1;39;0
WireConnection;43;1;41;0
WireConnection;4;1;1;0
WireConnection;29;0;37;0
WireConnection;29;1;38;0
WireConnection;45;0;4;0
WireConnection;45;1;43;0
WireConnection;28;1;29;0
WireConnection;21;0;20;0
WireConnection;21;1;45;0
WireConnection;16;1;21;0
WireConnection;16;0;19;0
WireConnection;35;0;45;0
WireConnection;33;0;34;0
WireConnection;33;1;28;0
WireConnection;5;1;2;0
WireConnection;26;0;16;0
WireConnection;26;1;33;0
WireConnection;26;2;35;0
WireConnection;48;0;47;0
WireConnection;6;0;26;0
WireConnection;6;1;5;0
WireConnection;49;0;48;0
WireConnection;50;0;49;0
WireConnection;50;1;51;0
WireConnection;7;0;8;0
WireConnection;7;1;6;0
WireConnection;9;0;6;0
WireConnection;9;1;10;0
WireConnection;14;0;7;0
WireConnection;15;0;5;0
WireConnection;15;1;12;0
WireConnection;46;0;50;0
WireConnection;46;1;9;0
WireConnection;13;0;46;0
WireConnection;13;1;15;0
WireConnection;13;2;14;0
WireConnection;0;2;13;0
ASEEND*/
//CHKSM=09184FC566132E00537A182E66E5A032A2E01245