// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_FLOOR"
{
	Properties
	{
		_sol("sol", 2D) = "white" {}
		_Color0("Color 0", Color) = (0.4150943,0.3682327,0.2721609,0)
		_Color1("Color 1", Color) = (0.3490566,0.1363465,0.09385012,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityPBSLighting.cginc"
		#pragma target 3.0
		#pragma surface surf StandardCustomLighting keepalpha addshadow fullforwardshadows 
		struct Input
		{
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

		uniform float4 _Color0;
		uniform sampler2D _sol;
		uniform float4 _sol_ST;
		uniform float4 _Color1;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			#ifdef UNITY_PASS_FORWARDBASE
			float ase_lightAtten = data.atten;
			if( _LightColor0.a == 0)
			ase_lightAtten = 0;
			#else
			float3 ase_lightAttenRGB = gi.light.color / ( ( _LightColor0.rgb ) + 0.000001 );
			float ase_lightAtten = max( max( ase_lightAttenRGB.r, ase_lightAttenRGB.g ), ase_lightAttenRGB.b );
			#endif
			#if defined(HANDLE_SHADOWS_BLENDING_IN_GI)
			half bakedAtten = UnitySampleBakedOcclusion(data.lightmapUV.xy, data.worldPos);
			float zDist = dot(_WorldSpaceCameraPos - data.worldPos, UNITY_MATRIX_V[2].xyz);
			float fadeDist = UnityComputeShadowFadeDistance(data.worldPos, zDist);
			ase_lightAtten = UnityMixRealtimeAndBakedShadows(data.atten, bakedAtten, UnityComputeShadowFade(fadeDist));
			#endif
			float2 uv_sol = i.uv_texcoord * _sol_ST.xy + _sol_ST.zw;
			float4 temp_output_20_0 = step( tex2D( _sol, uv_sol ) , float4( 0,0,0,0 ) );
			float4 lerpResult8 = lerp( _Color0 , float4( 0,0,0,0 ) , temp_output_20_0);
			float4 lerpResult10 = lerp( _Color1 , float4( 0,0,0,0 ) , ( 1.0 - temp_output_20_0 ));
			float4 lerpResult12 = lerp( lerpResult8 , lerpResult10 , temp_output_20_0);
			float temp_output_3_0_g1 = ( ase_lightAtten - 0.25 );
			float4 lerpResult6 = lerp( float4( 0,0,0,0 ) , lerpResult12 , saturate( ( temp_output_3_0_g1 / fwidth( temp_output_3_0_g1 ) ) ));
			c.rgb = lerpResult6.rgb;
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
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
0;0;1920;1019;2109.326;957.4498;1.3;True;True
Node;AmplifyShaderEditor.SamplerNode;5;-1910.241,-266.4935;Float;True;Property;_sol;sol;0;0;Create;True;0;0;False;0;ecc9549568fb45741ad7415bb2aa201e;ecc9549568fb45741ad7415bb2aa201e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;20;-1533.267,-324.2742;Float;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;7;-1323.268,-604.9897;Float;False;Property;_Color0;Color 0;1;0;Create;True;0;0;False;0;0.4150943,0.3682327,0.2721609,0;0.4150943,0.3682327,0.2721609,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;9;-1125.225,-268.1255;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;11;-1240.269,-164.6825;Float;False;Property;_Color1;Color 1;2;0;Create;True;0;0;False;0;0.3490566,0.1363465,0.09385012,0;0.3490566,0.1363465,0.09385012,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;10;-926.7703,-206.9088;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;8;-918.2681,-491.9897;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LightAttenuation;2;-810.812,35.01892;Float;True;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;12;-525.7274,-414.1302;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;4;-567.8124,28.01892;Float;True;Step Antialiasing;-1;;1;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0.25;False;2;FLOAT;0.08;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;6;496.6672,-64.85876;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-79.91745,383.1775;Float;False;Property;_OPACITY_SHADOW;OPACITY_SHADOW;4;0;Create;True;0;0;False;0;0.1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;18;217.0825,391.1775;Float;True;ColorDodge;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;19;-309.9738,893.6646;Float;False;Property;_SHADOW_COLOR;SHADOW_COLOR;3;0;Create;True;0;0;False;0;0.1373863,0.122241,0.3867925,1;0.2830189,0.137282,0.1081346,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;16;-296.7142,556.8915;Float;True;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;17;23.01272,668.8386;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;849.5861,-283.7336;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;S_FLOOR;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;20;0;5;0
WireConnection;9;0;20;0
WireConnection;10;0;11;0
WireConnection;10;2;9;0
WireConnection;8;0;7;0
WireConnection;8;2;20;0
WireConnection;12;0;8;0
WireConnection;12;1;10;0
WireConnection;12;2;20;0
WireConnection;4;2;2;0
WireConnection;6;1;12;0
WireConnection;6;2;4;0
WireConnection;18;0;16;0
WireConnection;18;1;15;0
WireConnection;17;0;19;0
WireConnection;17;2;16;0
WireConnection;0;13;6;0
ASEEND*/
//CHKSM=AE91805A0C315EBC0992A7461299BE42D19C61A1