// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_CHARA"
{
	Properties
	{
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
		_outline("outline", Float) = 0.39
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
		uniform float _SHADOW_FULL;
		uniform float4 _COLOR_UNIFORME_OR_SWITCH;
		uniform float4 _GENERAL_COLOR;
		uniform sampler2D _TEXTURE_OBJECT;
		uniform float4 _TEXTURE_OBJECT_ST;
		uniform float _OPACITY_SHADOW;
		uniform float4 _HIGHLIGHTCOLOR;
		uniform float _HIGH_LIGHT_LVL;
		uniform float _outline;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			float3 ase_worldNormal = i.worldNormal;
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult3 = dot( ase_worldNormal , ase_worldlightDir );
			float temp_output_8_0 = step( _SHADOW_FULL , dotResult3 );
			float temp_output_17_0 = step( _TRAME_LVL , temp_output_8_0 );
			float4 lerpResult22 = lerp( _SHADOW_COLOR , float4( 0,0,0,0 ) , temp_output_17_0);
			float2 uv_TEXTURE_OBJECT = i.uv_texcoord * _TEXTURE_OBJECT_ST.xy + _TEXTURE_OBJECT_ST.zw;
			float4 tex2DNode11 = tex2D( _TEXTURE_OBJECT, uv_TEXTURE_OBJECT );
			float4 lerpResult16 = lerp( _GENERAL_COLOR , tex2DNode11 , _GENERAL_COLOR.a);
			#ifdef _SWITCH_COLOR_TEXTURE_ON
				float4 staticSwitch20 = lerpResult16;
			#else
				float4 staticSwitch20 = _COLOR_UNIFORME_OR_SWITCH;
			#endif
			float blendOpSrc23 = temp_output_17_0;
			float blendOpDest23 = _OPACITY_SHADOW;
			float4 lerpResult30 = lerp( lerpResult22 , staticSwitch20 , ( saturate( ( blendOpDest23/ ( 1.0 - blendOpSrc23 ) ) )));
			float4 blendOpSrc27 = tex2DNode11;
			float4 blendOpDest27 = _HIGHLIGHTCOLOR;
			float4 lerpResult32 = lerp( lerpResult30 , ( saturate(  (( blendOpSrc27 > 0.5 ) ? ( 1.0 - ( 1.0 - 2.0 * ( blendOpSrc27 - 0.5 ) ) * ( 1.0 - blendOpDest27 ) ) : ( 2.0 * blendOpSrc27 * blendOpDest27 ) ) )) , ( _HIGHLIGHTCOLOR.a * step( _HIGH_LIGHT_LVL , dotResult3 ) ));
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float fresnelNdotV44 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode44 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV44, 5.0 ) );
			float4 lerpResult47 = lerp( lerpResult32 , float4( 0,0,0,0 ) , step( _outline , fresnelNode44 ));
			c.rgb = lerpResult47.rgb;
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
				float4 staticSwitch33 = ( _Intensity_emissive * tex2D( _emissive, uv_emissive ) );
			#else
				float4 staticSwitch33 = temp_cast_0;
			#endif
			o.Emission = staticSwitch33.rgb;
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
0;0;1920;1019;725.8481;-20.36221;1;True;True
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;2;-4935.844,771.8527;Float;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;1;-4883.808,615.1012;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;7;-4070.265,896.6142;Float;False;Property;_SHADOW_FULL;SHADOW_FULL;6;0;Create;True;0;0;False;0;-0.8;-0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;3;-4613.739,717.2114;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-4617.152,476.9052;Float;False;Property;_TRAME_LVL;TRAME_LVL;1;0;Create;True;0;0;False;0;0.21;0.21;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;10;-3362.343,38.08878;Float;False;Property;_GENERAL_COLOR;GENERAL_COLOR;9;0;Create;True;0;0;False;0;0,0,0,1;0,0,0,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;8;-3891.563,884.5164;Float;True;2;0;FLOAT;-0.8;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;11;-3447.247,-184.9064;Float;True;Property;_TEXTURE_OBJECT;TEXTURE_OBJECT;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;14;-2345.751,911.3018;Float;False;Property;_HIGH_LIGHT_LVL;HIGH_LIGHT_LVL;11;0;Create;True;0;0;False;0;0.62;0.62;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-2814.129,304.1419;Float;False;Property;_OPACITY_SHADOW;OPACITY_SHADOW;12;0;Create;True;0;0;False;0;0.1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;17;-3042.258,470.5269;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;16;-2888.85,-43.76797;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;15;-3157.667,833.7705;Float;False;Property;_SHADOW_COLOR;SHADOW_COLOR;8;0;Create;True;0;0;False;0;0.1373863,0.122241,0.3867925,1;0.2830189,0.137282,0.1081346,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;18;-2915.196,-291.1609;Float;False;Property;_COLOR_UNIFORME_OR_SWITCH;COLOR_UNIFORME_OR_SWITCH;3;0;Create;True;0;0;False;0;1,0.8057632,0,0;0.8962264,0.7119444,0.4861605,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;22;-2656.545,575.14;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;25;-1963.725,190.4168;Float;False;Property;_HIGHLIGHTCOLOR;HIGH LIGHT COLOR;10;0;Create;True;0;0;False;0;1,0.5613208,0.5613208,0.454902;1,0.5613208,0.5613208,0.454902;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;20;-2578.194,-115.8649;Float;False;Property;_SWITCH_COLOR_TEXTURE;SWITCH_COLOR_TEXTURE;7;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;23;-2533.628,318.1414;Float;True;ColorDodge;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;24;-2109.502,764.8124;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;44;-537.1999,226.807;Float;True;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-985.2319,-208.2052;Float;False;Property;_Intensity_emissive;Intensity_emissive;4;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;27;-1651.368,40.257;Float;True;HardLight;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;30;-2124.365,418.9974;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-1807.731,748.4384;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-217.9626,353.1844;Float;False;Property;_outline;outline;15;0;Create;True;0;0;False;0;0.39;0.39;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;26;-1284.958,-64.33319;Float;True;Property;_emissive;emissive;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;45;-22.96256,287.1844;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;32;-1146.049,419.0566;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-718.0288,87.08997;Float;False;Constant;_Float0;Float 0;14;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-736.4778,-150.0726;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;47;353.643,540.9478;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;13;-3331.437,537.1537;Float;True;Darken;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;36;126.7704,966.3566;Float;False;Property;_OUTLINE;OUTLINE;14;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;375.9958,998.5849;Float;False;Constant;_Float1;Float 1;15;0;Create;True;0;0;False;0;3.52;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OutlineNode;34;286.2411,852.7874;Float;False;0;True;None;0;0;Front;3;0;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;33;-509.0288,56.09007;Float;False;Property;_EMISSIVE;EMISSIVE;13;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;35;51.12099,797.8494;Float;False;Constant;_Color0;Color 0;14;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-4106.341,243.4011;Float;True;Property;_TEXTURE_TRAME;TEXTURE_TRAME;0;0;Create;True;0;0;False;0;e4e8d406a0c61a747add86208b257ef4;e4e8d406a0c61a747add86208b257ef4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-3752.642,555.8102;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;5;-4083.753,570.6733;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;555.7388,925.5556;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;9;-3525.902,562.0342;Float;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;841.8863,189.275;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;S_CHARA;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;1;0
WireConnection;3;1;2;0
WireConnection;8;0;7;0
WireConnection;8;1;3;0
WireConnection;17;0;12;0
WireConnection;17;1;8;0
WireConnection;16;0;10;0
WireConnection;16;1;11;0
WireConnection;16;2;10;4
WireConnection;22;0;15;0
WireConnection;22;2;17;0
WireConnection;20;1;18;0
WireConnection;20;0;16;0
WireConnection;23;0;17;0
WireConnection;23;1;19;0
WireConnection;24;0;14;0
WireConnection;24;1;3;0
WireConnection;27;0;11;0
WireConnection;27;1;25;0
WireConnection;30;0;22;0
WireConnection;30;1;20;0
WireConnection;30;2;23;0
WireConnection;31;0;25;4
WireConnection;31;1;24;0
WireConnection;45;0;46;0
WireConnection;45;1;44;0
WireConnection;32;0;30;0
WireConnection;32;1;27;0
WireConnection;32;2;31;0
WireConnection;28;0;21;0
WireConnection;28;1;26;0
WireConnection;47;0;32;0
WireConnection;47;2;45;0
WireConnection;13;0;9;0
WireConnection;13;1;8;0
WireConnection;34;0;35;0
WireConnection;34;1;36;0
WireConnection;33;1;29;0
WireConnection;33;0;28;0
WireConnection;6;0;5;0
WireConnection;6;1;4;0
WireConnection;5;0;3;0
WireConnection;40;0;34;0
WireConnection;40;1;41;0
WireConnection;9;0;6;0
WireConnection;0;2;33;0
WireConnection;0;13;47;0
ASEEND*/
//CHKSM=890819A60FF6D7CBB4E25020427265B7EE0ED779