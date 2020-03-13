// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_FLOOR"
{
	Properties
	{
		_TEXT_FLOOR("TEXT_FLOOR", 2D) = "white" {}
		_SHADOW_COLOR("SHADOW_COLOR", Color) = (0.06717903,0.06194376,0.1509434,1)
		_SHADOW_OPA("SHADOW_OPA", Float) = 0.66
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

		uniform sampler2D _TEXT_FLOOR;
		uniform float4 _TEXT_FLOOR_ST;
		uniform float4 _SHADOW_COLOR;
		uniform float _SHADOW_OPA;

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
			float2 uv_TEXT_FLOOR = i.uv_texcoord * _TEXT_FLOOR_ST.xy + _TEXT_FLOOR_ST.zw;
			float temp_output_3_0_g1 = ( ase_lightAtten - 0.25 );
			float temp_output_16_0 = step( saturate( ( temp_output_3_0_g1 / fwidth( temp_output_3_0_g1 ) ) ) , 0.0 );
			float4 lerpResult17 = lerp( _SHADOW_COLOR , float4( 0,0,0,0 ) , ( 1.0 - temp_output_16_0 ));
			float4 lerpResult23 = lerp( tex2D( _TEXT_FLOOR, uv_TEXT_FLOOR ) , lerpResult17 , ( temp_output_16_0 * _SHADOW_OPA ));
			c.rgb = lerpResult23.rgb;
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
0;0;1920;1019;986.2136;733.608;1.659842;True;True
Node;AmplifyShaderEditor.LightAttenuation;2;-570.2262,-72.99521;Float;True;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;4;-305.5009,-69.05513;Float;True;Step Antialiasing;-1;;1;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0.25;False;2;FLOAT;0.08;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;16;-28.26931,-59.95135;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;22;258.2778,-103.0792;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;19;128.1622,-320.2048;Float;False;Property;_SHADOW_COLOR;SHADOW_COLOR;1;0;Create;True;0;0;False;0;0.06717903,0.06194376,0.1509434,1;0.2830189,0.137282,0.1081346,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;24;235.9622,232.615;Float;False;Property;_SHADOW_OPA;SHADOW_OPA;2;0;Create;True;0;0;False;0;0.66;0.66;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;17;460.4574,-155.0041;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;488.8598,107.656;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;21;478.7634,-418.4124;Float;True;Property;_TEXT_FLOOR;TEXT_FLOOR;0;0;Create;True;0;0;False;0;3f6f5ac23c4a179499520e5d4e762163;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;23;924.9622,-123.385;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1318.333,-279.7612;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;S_FLOOR;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;2;2;0
WireConnection;16;0;4;0
WireConnection;22;0;16;0
WireConnection;17;0;19;0
WireConnection;17;2;22;0
WireConnection;25;0;16;0
WireConnection;25;1;24;0
WireConnection;23;0;21;0
WireConnection;23;1;17;0
WireConnection;23;2;25;0
WireConnection;0;13;23;0
ASEEND*/
//CHKSM=BA8AF07643CFC44EB0B72819B98FB21E29153954