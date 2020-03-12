// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_Fence"
{
	Properties
	{
		_Fence_DefaultMaterial_BaseColor("Fence_DefaultMaterial_BaseColor", 2D) = "white" {}
		_Fence_DefaultMaterial_Emissive("Fence_DefaultMaterial_Emissive", 2D) = "white" {}
		_Float0("Float 0", Float) = 1
		_min_emi("min_emi", Float) = 0.8
		_max_emi("max_emi", Float) = 1.3
		_speed("speed", Float) = 5
		[HDR]_Color0("Color 0", Color) = (0.2321882,0,1,0)
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

		uniform sampler2D _Fence_DefaultMaterial_Emissive;
		uniform float4 _Fence_DefaultMaterial_Emissive_ST;
		uniform float4 _Color0;
		uniform float _speed;
		uniform float _min_emi;
		uniform float _max_emi;
		uniform float _Float0;
		uniform sampler2D _Fence_DefaultMaterial_BaseColor;
		uniform float4 _Fence_DefaultMaterial_BaseColor_ST;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			float2 uv_Fence_DefaultMaterial_BaseColor = i.uv_texcoord * _Fence_DefaultMaterial_BaseColor_ST.xy + _Fence_DefaultMaterial_BaseColor_ST.zw;
			c.rgb = tex2D( _Fence_DefaultMaterial_BaseColor, uv_Fence_DefaultMaterial_BaseColor ).rgb;
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
			float2 uv_Fence_DefaultMaterial_Emissive = i.uv_texcoord * _Fence_DefaultMaterial_Emissive_ST.xy + _Fence_DefaultMaterial_Emissive_ST.zw;
			float mulTime7 = _Time.y * _speed;
			o.Emission = ( tex2D( _Fence_DefaultMaterial_Emissive, uv_Fence_DefaultMaterial_Emissive ) * ( _Color0 * ( (_min_emi + (abs( sin( mulTime7 ) ) - 0.0) * (_max_emi - _min_emi) / (1.0 - 0.0)) * _Float0 ) ) ).rgb;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
1920;0;1920;1019;995.9764;677.4454;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;14;-1156.06,-208.0761;Float;False;Property;_speed;speed;5;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;7;-952.0602,-175.0761;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;8;-776.0602,-168.0761;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;9;-643.0602,-157.0761;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-758.0602,-35.07608;Float;False;Property;_min_emi;min_emi;3;0;Create;True;0;0;False;0;0.8;0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-766.0602,69.92392;Float;False;Property;_max_emi;max_emi;4;0;Create;True;0;0;False;0;1.3;1.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;11;-504.0602,-156.0761;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-418,17;Float;False;Property;_Float0;Float 0;2;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-301.0602,-166.0761;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;16;-352.1965,-357.371;Float;False;Property;_Color0;Color 0;6;1;[HDR];Create;True;0;0;False;0;0.2321882,0,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-113.0719,-173.0559;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;4;-212.4944,-577.5465;Float;True;Property;_Fence_DefaultMaterial_Emissive;Fence_DefaultMaterial_Emissive;1;0;Create;True;0;0;False;0;ab5c37aee8b83b746bf2db3414ddd3fc;aa3e60c042867a54fa76038c83e97a5f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;90.10857,325.8103;Float;True;Property;_Fence_DefaultMaterial_BaseColor;Fence_DefaultMaterial_BaseColor;0;0;Create;True;0;0;False;0;5fb5e905a5bfe4847af4b520d2d114c7;5fb5e905a5bfe4847af4b520d2d114c7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;145.3658,-204.8023;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;2;643.7362,-206.5184;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;S_Fence;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;14;0
WireConnection;8;0;7;0
WireConnection;9;0;8;0
WireConnection;11;0;9;0
WireConnection;11;3;12;0
WireConnection;11;4;13;0
WireConnection;10;0;11;0
WireConnection;10;1;6;0
WireConnection;15;0;16;0
WireConnection;15;1;10;0
WireConnection;5;0;4;0
WireConnection;5;1;15;0
WireConnection;2;2;5;0
WireConnection;2;13;3;0
ASEEND*/
//CHKSM=DE847097BCDF754CD4E2BC931ED6D54D51CF2091