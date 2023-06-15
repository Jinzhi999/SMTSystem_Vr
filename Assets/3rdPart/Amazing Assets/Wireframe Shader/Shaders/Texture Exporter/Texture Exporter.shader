
Shader "Hidden/Amazing Assets/Wireframe Shader/Texture Exporter"
{	
	Properties
    {        
		_WireframeShader_Thickness("", Range(0, 1)) = 0.01
        _WireframeShader_Smoothness("", Range(0, 1)) = 0  
		_WireframeShader_Diameter("", Range(0, 1)) = 1
    }


	CGINCLUDE
	#include "UnityCG.cginc" 
	

	float _WireframeShader_Thickness;
	float _WireframeShader_Smoothness;			
	float _WireframeShader_Diameter;

	struct vertOut 
	{ 
		float4 pos : SV_POSITION;
                
		float3 triangleMass : TEXCOORD0;
	};

	vertOut vert (float4 vertex:POSITION, float2 uv:TEXCOORD0, float4 texcoord3:TEXCOORD3)
    {
		vertOut o;

        float2 texcoord = uv.xy;
        texcoord.y = 1.0 - texcoord.y;
        texcoord = texcoord * 2.0 - 1.0;
                
		o.pos = float4(texcoord, 0.0, 1.0);
				
		#if defined(WIREFRAME_CALCULATE_USING_GEOMETRY_SHADER)
			o.triangleMass = vertex;          
		#else
			o.triangleMass = texcoord3;          
		#endif

		return o;
    } 

	ENDCG


	//With GeometryShaders
	SubShader
	{
		ZTest Off Cull Off ZWrite Off

		Pass
		{
			CGPROGRAM 
			#pragma vertex vert
			#pragma geometry geom
            #pragma fragment frag


			#pragma shader_feature_local WIREFRAME_READ_FROM_MESH WIREFRAME_CALCULATE_USING_GEOMETRY_SHADER
			#pragma shader_feature_local _ WIREFRAME_NORMALIZE_EDGES_ON
			#pragma shader_feature_local _ WIREFRAME_TRY_QUAD_ON

			#include "../cginc/WireframeShader.cginc"
     

			[maxvertexcount(3)]
            void geom(triangle vertOut input[3], inout TriangleStream<vertOut> triStream)
            {
				#if defined(WIREFRAME_CALCULATE_USING_GEOMETRY_SHADER)
					float3 b0, b1, b2;	
					WireframeShaderCalculateTriangleMass(input[0].triangleMass.xyz, input[1].triangleMass.xyz, input[2].triangleMass.xyz, b0, b1, b2); 
					
					input[0].triangleMass = float4(b0, 0); 
					input[1].triangleMass = float4(b1, 0); 
					input[2].triangleMass = float4(b2, 0);
				#endif


                triStream.Append(input[0]);
                triStream.Append(input[1]);
                triStream.Append(input[2]);

                triStream.RestartStrip();
            }

     
            float4 frag (vertOut i) : SV_TARGET
            {
                return saturate(WireframeShaderReadTriangleMassFromUV(i.triangleMass, _WireframeShader_Thickness, _WireframeShader_Smoothness, _WireframeShader_Diameter) * 2);
            }

            ENDCG
        }
    }


	//Without GeometryShaders
	SubShader
	{
		ZTest Off Cull Off ZWrite Off

		Pass
		{
			CGPROGRAM 
			#pragma vertex vert
            #pragma fragment frag

			#include "../cginc/WireframeShader.cginc"


            float4 frag (vertOut i) : SV_TARGET
            {
                return saturate(WireframeShaderReadTriangleMassFromUV(i.triangleMass, _WireframeShader_Thickness, _WireframeShader_Smoothness, _WireframeShader_Diameter) * 2);
            }

            ENDCG
        }
    }	
}
