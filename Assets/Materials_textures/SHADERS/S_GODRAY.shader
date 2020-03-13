// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_GODRAY"
{
	Properties
	{
		_Float0("Float 0", Float) = 0.14
		_Float3("Float 3", Float) = 0
		_T_cloudblur("T_cloudblur", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+1" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float _Float3;
		uniform float _Float0;
		uniform sampler2D _T_cloudblur;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color8 = IsGammaSpace() ? float4(1,0,0,0) : float4(1,0,0,0);
			o.Albedo = color8.rgb;
			float4 color3 = IsGammaSpace() ? float4(0.972549,0.4976859,0.0509804,1) : float4(0.9386859,0.2119055,0.004024717,1);
			o.Emission = ( color3 * 0.82 ).rgb;
			float3 ase_worldPos = i.worldPos;
			float smoothstepResult20 = smoothstep( _Float3 , _Float0 , ( ase_worldPos.y * 0.4 ));
			float cos62 = cos( radians( 122.21 ) );
			float sin62 = sin( radians( 122.21 ) );
			float2 rotator62 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos62 , -sin62 , sin62 , cos62 )) + float2( 0.5,0.5 );
			float2 panner26 = ( 1.0 * _Time.y * float2( 0.09,-0.09 ) + ( ase_worldPos.y * rotator62 ));
			float2 uv_TexCoord67 = i.uv_texcoord * tex2D( _T_cloudblur, panner26 ).rg;
			float cos69 = cos( radians( 122.21 ) );
			float sin69 = sin( radians( 122.21 ) );
			float2 rotator69 = mul( uv_TexCoord67 - float2( 0.5,0.5 ) , float2x2( cos69 , -sin69 , sin69 , cos69 )) + float2( 0.5,0.5 );
			o.Alpha = ( smoothstepResult20 * ( rotator69 * 1.59 ) ).x;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

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
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
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
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
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
0;0;1920;1019;451.9921;-377.9736;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;63;-1165.157,1144.104;Float;False;Constant;_Float4;Float 4;4;0;Create;True;0;0;False;0;122.21;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;61;-1096.057,976.387;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RadiansOpNode;64;-986.1574,1145.104;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;56;-872.1011,756.6383;Float;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RotatorNode;62;-818.0573,1002.387;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-592.3718,895.0872;Float;True;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;26;-353.0706,898.6162;Float;False;3;0;FLOAT2;1,1;False;2;FLOAT2;0.09,-0.09;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;40;-902.5846,114.4405;Float;False;969.4046;475.5865;Comment;6;16;18;17;21;2;20;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;66;250.0058,1258.43;Float;False;Constant;_Float5;Float 5;4;0;Create;True;0;0;False;0;122.21;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;23;-140.9945,865.9785;Float;True;Property;_T_cloudblur;T_cloudblur;3;0;Create;True;0;0;False;0;e8d5233656db5da48bb5434f09556e12;e8d5233656db5da48bb5434f09556e12;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RadiansOpNode;68;441.0052,1262.43;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;67;315.1056,968.7133;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;18;-813.0381,440.8604;Float;False;Constant;_Float2;Float 2;3;0;Create;True;0;0;False;0;0.4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;16;-852.5846,232.074;Float;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RotatorNode;69;647.0372,989.245;Float;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;41;-915.7645,-436.3416;Float;False;985.1833;529.578;Comment;4;5;3;8;4;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-350.9718,475.027;Float;False;Property;_Float0;Float 0;1;0;Create;True;0;0;False;0;0.14;0.64;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-582.0232,257.6329;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-339.3277,394.6204;Float;False;Property;_Float3;Float 3;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;71;437.0079,728.9736;Float;False;Constant;_Float6;Float 6;4;0;Create;True;0;0;False;0;1.59;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-783.1976,-40.76391;Float;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;False;0;0.82;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;20;-174.88,164.4406;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;3;-865.7646,-270.7886;Float;False;Constant;_Color0;Color 0;1;0;Create;True;0;0;False;0;0.972549,0.4976859,0.0509804,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;623.0079,695.9736;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-545.1976,-159.7639;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;8;-164.5808,-386.3416;Float;False;Constant;_Color1;Color 1;0;0;Create;True;0;0;False;0;1,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;768.6486,407.16;Float;True;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;15;1195.585,-233.7658;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;S_GODRAY;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;1;True;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;64;0;63;0
WireConnection;62;0;61;0
WireConnection;62;2;64;0
WireConnection;65;0;56;2
WireConnection;65;1;62;0
WireConnection;26;0;65;0
WireConnection;23;1;26;0
WireConnection;68;0;66;0
WireConnection;67;0;23;0
WireConnection;69;0;67;0
WireConnection;69;2;68;0
WireConnection;17;0;16;2
WireConnection;17;1;18;0
WireConnection;20;0;17;0
WireConnection;20;1;21;0
WireConnection;20;2;2;0
WireConnection;70;0;69;0
WireConnection;70;1;71;0
WireConnection;4;0;3;0
WireConnection;4;1;5;0
WireConnection;54;0;20;0
WireConnection;54;1;70;0
WireConnection;15;0;8;0
WireConnection;15;2;4;0
WireConnection;15;9;54;0
ASEEND*/
//CHKSM=A86C5C80E012D6E814D3918F9F01D4AEF364A7D2