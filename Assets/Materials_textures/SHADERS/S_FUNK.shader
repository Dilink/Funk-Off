// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FUNK_SHADER"
{
	Properties
	{
		_TEXTURE_TRAME("TEXTURE_TRAME", 2D) = "white" {}
		_TRAME_LVL("TRAME_LVL", Float) = 0.21
		_emissive("emissive", 2D) = "white" {}
		_COLOR_UNIFORME_OR_SWITCH("COLOR_UNIFORME_OR_SWITCH", Color) = (1,0.8057632,0,0)
		_Intensity_emissive("Intensity_emissive", Float) = 1
		_TEXTURE_OBJECT("TEXTURE_OBJECT", 2D) = "white" {}
		_SHADOW_FULL("SHADOW_FULL", Float) = -0.8
		[Toggle(_SWITCH_COLOR_TEXTURE_ON)] _SWITCH_COLOR_TEXTURE("SWITCH_COLOR_TEXTURE", Float) = 0
		_SHADOW_COLOR("SHADOW_COLOR", Color) = (0.1373863,0.122241,0.3867925,1)
		_GENERAL_COLOR("GENERAL_COLOR", Color) = (0,0,0,1)
		_HIGHLIGHTCOLOR("HIGH LIGHT COLOR", Color) = (1,0.5613208,0.5613208,0.454902)
		_HIGH_LIGHT_LVL("HIGH_LIGHT_LVL", Float) = 0.62
		_OPACITY_SHADOW("OPACITY_SHADOW", Float) = 0.1
		[Toggle(_EMISSIVE_ON)] _EMISSIVE("EMISSIVE", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma shader_feature _EMISSIVE_ON
		#pragma shader_feature _SWITCH_COLOR_TEXTURE_ON
		struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
			float3 worldPos;
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

		uniform float _Intensity_emissive;
		uniform sampler2D _emissive;
		uniform float4 _emissive_ST;
		uniform float4 _SHADOW_COLOR;
		uniform float _TRAME_LVL;
		uniform sampler2D _TEXTURE_TRAME;
		uniform float4 _TEXTURE_TRAME_ST;
		uniform float _SHADOW_FULL;
		uniform float4 _COLOR_UNIFORME_OR_SWITCH;
		uniform float4 _GENERAL_COLOR;
		uniform sampler2D _TEXTURE_OBJECT;
		uniform float4 _TEXTURE_OBJECT_ST;
		uniform float _OPACITY_SHADOW;
		uniform float4 _HIGHLIGHTCOLOR;
		uniform float _HIGH_LIGHT_LVL;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			float4 temp_cast_2 = (_TRAME_LVL).xxxx;
			float3 ase_worldNormal = i.worldNormal;
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult48 = dot( ase_worldNormal , ase_worldlightDir );
			float2 uv_TEXTURE_TRAME = i.uv_texcoord * _TEXTURE_TRAME_ST.xy + _TEXTURE_TRAME_ST.zw;
			float4 temp_cast_3 = (step( _SHADOW_FULL , dotResult48 )).xxxx;
			float4 blendOpSrc129 = ( 1.0 - ( ( 1.0 - dotResult48 ) * tex2D( _TEXTURE_TRAME, uv_TEXTURE_TRAME ) ) );
			float4 blendOpDest129 = temp_cast_3;
			float4 temp_output_85_0 = step( temp_cast_2 , ( saturate( min( blendOpSrc129 , blendOpDest129 ) )) );
			float4 lerpResult121 = lerp( _SHADOW_COLOR , float4( 0,0,0,0 ) , temp_output_85_0);
			float2 uv_TEXTURE_OBJECT = i.uv_texcoord * _TEXTURE_OBJECT_ST.xy + _TEXTURE_OBJECT_ST.zw;
			float4 tex2DNode119 = tex2D( _TEXTURE_OBJECT, uv_TEXTURE_OBJECT );
			float4 lerpResult143 = lerp( _GENERAL_COLOR , tex2DNode119 , _GENERAL_COLOR.a);
			#ifdef _SWITCH_COLOR_TEXTURE_ON
				float4 staticSwitch117 = lerpResult143;
			#else
				float4 staticSwitch117 = _COLOR_UNIFORME_OR_SWITCH;
			#endif
			float4 temp_cast_4 = (_TRAME_LVL).xxxx;
			float4 temp_cast_5 = (_OPACITY_SHADOW).xxxx;
			float4 blendOpSrc170 = temp_output_85_0;
			float4 blendOpDest170 = temp_cast_5;
			float4 lerpResult105 = lerp( lerpResult121 , staticSwitch117 , ( saturate( ( blendOpDest170/ ( 1.0 - blendOpSrc170 ) ) )));
			float4 blendOpSrc164 = tex2DNode119;
			float4 blendOpDest164 = _HIGHLIGHTCOLOR;
			float4 lerpResult161 = lerp( lerpResult105 , ( saturate(  (( blendOpSrc164 > 0.5 ) ? ( 1.0 - ( 1.0 - 2.0 * ( blendOpSrc164 - 0.5 ) ) * ( 1.0 - blendOpDest164 ) ) : ( 2.0 * blendOpSrc164 * blendOpDest164 ) ) )) , ( _HIGHLIGHTCOLOR.a * step( _HIGH_LIGHT_LVL , dotResult48 ) ));
			c.rgb = lerpResult161.rgb;
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
			float4 temp_cast_0 = (0.0).xxxx;
			float2 uv_emissive = i.uv_texcoord * _emissive_ST.xy + _emissive_ST.zw;
			#ifdef _EMISSIVE_ON
				float4 staticSwitch175 = ( _Intensity_emissive * tex2D( _emissive, uv_emissive ) );
			#else
				float4 staticSwitch175 = temp_cast_0;
			#endif
			o.Emission = staticSwitch175.rgb;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardCustomLighting keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputCustomLightingCustom o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputCustomLightingCustom, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
0;0;1920;1019;-685.4587;724.1371;2.013971;True;True
Node;AmplifyShaderEditor.WorldNormalVector;46;-1578.084,349.4279;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;47;-1630.121,506.1794;Float;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DotProductOpNode;48;-1308.015,451.5381;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;111;-800.6162,-22.27221;Float;True;Property;_TEXTURE_TRAME;TEXTURE_TRAME;0;0;Create;True;0;0;False;0;e4e8d406a0c61a747add86208b257ef4;e4e8d406a0c61a747add86208b257ef4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;63;-778.0287,305;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-446.9178,290.1368;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;122;-764.5406,630.941;Float;False;Property;_SHADOW_FULL;SHADOW_FULL;6;0;Create;True;0;0;False;0;-0.8;-0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;88;-585.8388,618.8432;Float;True;2;0;FLOAT;-0.8;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;84;-220.1772,296.3608;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;133;-56.61833,-227.5846;Float;False;Property;_GENERAL_COLOR;GENERAL_COLOR;9;0;Create;True;0;0;False;0;0,0,0,1;0,0,0,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;119;-141.5224,-450.5797;Float;True;Property;_TEXTURE_OBJECT;TEXTURE_OBJECT;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;86;-1311.428,211.2318;Float;False;Property;_TRAME_LVL;TRAME_LVL;1;0;Create;True;0;0;False;0;0.21;0.21;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;129;-27.3251,294.1793;Float;True;Darken;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;171;491.5956,38.46854;Float;False;Property;_OPACITY_SHADOW;OPACITY_SHADOW;12;0;Create;True;0;0;False;0;0.1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;143;416.8742,-309.4414;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;106;390.5292,-556.8342;Float;False;Property;_COLOR_UNIFORME_OR_SWITCH;COLOR_UNIFORME_OR_SWITCH;3;0;Create;True;0;0;False;0;1,0.8057632,0,0;0.8962264,0.7119444,0.4861605,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;85;274.7988,212.1826;Float;True;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;120;148.058,568.0972;Float;False;Property;_SHADOW_COLOR;SHADOW_COLOR;8;0;Create;True;0;0;False;0;0.1373863,0.122241,0.3867925,1;0.2830189,0.137282,0.1081346,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;160;959.9731,645.6286;Float;False;Property;_HIGH_LIGHT_LVL;HIGH_LIGHT_LVL;11;0;Create;True;0;0;False;0;0.62;0.62;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;117;727.5306,-381.5383;Float;False;Property;_SWITCH_COLOR_TEXTURE;SWITCH_COLOR_TEXTURE;7;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;173;2320.492,-473.8785;Float;False;Property;_Intensity_emissive;Intensity_emissive;4;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;121;594.5258,324.1297;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;170;788.5956,46.46854;Float;True;ColorDodge;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;159;1196.222,499.1391;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;162;1341.999,-75.25652;Float;False;Property;_HIGHLIGHTCOLOR;HIGH LIGHT COLOR;10;0;Create;True;0;0;False;0;1,0.5613208,0.5613208,0.454902;1,0.5613208,0.5613208,0.454902;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;172;2020.766,-330.0066;Float;True;Property;_emissive;emissive;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;176;2587.695,-178.5834;Float;False;Constant;_Float0;Float 0;14;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;164;1654.356,-225.4164;Float;True;HardLight;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;174;2569.246,-415.746;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;163;1497.993,482.7651;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;105;1236.857,136.8253;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;175;2796.695,-209.5833;Float;False;Property;_EMISSIVE;EMISSIVE;13;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;161;2115.61,151.6673;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;61;3116.393,-98.72762;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;FUNK_SHADER;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;48;0;46;0
WireConnection;48;1;47;0
WireConnection;63;0;48;0
WireConnection;65;0;63;0
WireConnection;65;1;111;0
WireConnection;88;0;122;0
WireConnection;88;1;48;0
WireConnection;84;0;65;0
WireConnection;129;0;84;0
WireConnection;129;1;88;0
WireConnection;143;0;133;0
WireConnection;143;1;119;0
WireConnection;143;2;133;4
WireConnection;85;0;86;0
WireConnection;85;1;129;0
WireConnection;117;1;106;0
WireConnection;117;0;143;0
WireConnection;121;0;120;0
WireConnection;121;2;85;0
WireConnection;170;0;85;0
WireConnection;170;1;171;0
WireConnection;159;0;160;0
WireConnection;159;1;48;0
WireConnection;164;0;119;0
WireConnection;164;1;162;0
WireConnection;174;0;173;0
WireConnection;174;1;172;0
WireConnection;163;0;162;4
WireConnection;163;1;159;0
WireConnection;105;0;121;0
WireConnection;105;1;117;0
WireConnection;105;2;170;0
WireConnection;175;1;176;0
WireConnection;175;0;174;0
WireConnection;161;0;105;0
WireConnection;161;1;164;0
WireConnection;161;2;163;0
WireConnection;61;2;175;0
WireConnection;61;13;161;0
ASEEND*/
//CHKSM=A9436D2361F29E51B89AAD34E4F80F34FB58FFDB