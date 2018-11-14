// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32719,y:32712,varname:node_2865,prsc:2|diff-224-OUT,spec-8979-OUT,gloss-3266-OUT,normal-7800-OUT,emission-513-RGB,voffset-3544-OUT;n:type:ShaderForge.SFN_Tex2d,id:9350,x:31716,y:32464,ptovrint:False,ptlb:Dissolve_Noise,ptin:_Dissolve_Noise,varname:node_9350,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:3980,x:31179,y:32651,ptovrint:False,ptlb:Dissolve_Value,ptin:_Dissolve_Value,varname:node_3980,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:2;n:type:ShaderForge.SFN_Add,id:6035,x:31891,y:32652,varname:node_6035,prsc:2|A-8813-OUT,B-9350-R;n:type:ShaderForge.SFN_Tex2d,id:2705,x:32055,y:32132,ptovrint:False,ptlb:Colour_Top,ptin:_Colour_Top,varname:node_2705,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c3d66a8056f9db345b1ea380aa7e815d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_OneMinus,id:3457,x:31542,y:32634,varname:node_3457,prsc:2|IN-3980-OUT;n:type:ShaderForge.SFN_RemapRange,id:2718,x:31195,y:32795,varname:node_2718,prsc:2,frmn:0,frmx:1,tomn:-8,tomx:8|IN-6035-OUT;n:type:ShaderForge.SFN_Clamp01,id:2157,x:31368,y:32795,varname:node_2157,prsc:2|IN-2718-OUT;n:type:ShaderForge.SFN_OneMinus,id:5861,x:31537,y:32795,varname:node_5861,prsc:2|IN-2157-OUT;n:type:ShaderForge.SFN_Append,id:539,x:31722,y:32811,varname:node_539,prsc:2|A-5861-OUT,B-4206-OUT;n:type:ShaderForge.SFN_Vector1,id:4206,x:31537,y:32936,varname:node_4206,prsc:2,v1:0;n:type:ShaderForge.SFN_Tex2d,id:513,x:31921,y:32811,varname:node_513,prsc:2,tex:271f5ee3273dd7f4fae6e204d4f8c4bf,ntxv:0,isnm:False|UVIN-539-OUT,MIP-4206-OUT,TEX-46-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:46,x:31722,y:32962,ptovrint:False,ptlb:Dissolve_Gradient,ptin:_Dissolve_Gradient,varname:node_46,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:271f5ee3273dd7f4fae6e204d4f8c4bf,ntxv:0,isnm:False;n:type:ShaderForge.SFN_RemapRange,id:8813,x:31716,y:32634,varname:node_8813,prsc:2,frmn:0,frmx:1,tomn:0.1,tomx:0.6|IN-3457-OUT;n:type:ShaderForge.SFN_Tex2d,id:4628,x:32055,y:32313,ptovrint:False,ptlb:Colour_Bottom,ptin:_Colour_Bottom,varname:node_4628,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:aa762ee7680f35f4fb9a9210fa94ce61,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:224,x:32224,y:32220,varname:node_224,prsc:2|A-2705-RGB,B-4628-RGB,T-5861-OUT;n:type:ShaderForge.SFN_Slider,id:8979,x:32193,y:32573,ptovrint:False,ptlb:Metalness,ptin:_Metalness,varname:node_8979,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:830,x:32204,y:32701,ptovrint:False,ptlb:Roughness_Top,ptin:_Roughness_Top,varname:node_830,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a1049172718a508459482bf763b390fd,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3037,x:32204,y:32884,ptovrint:False,ptlb:Rougness_Bottom,ptin:_Rougness_Bottom,varname:node_3037,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:830884c18cf5e7a4895ee02c2dcd8e12,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:9749,x:32388,y:32822,varname:node_9749,prsc:2|A-830-RGB,B-3037-RGB,T-5861-OUT;n:type:ShaderForge.SFN_Desaturate,id:3266,x:32388,y:32701,varname:node_3266,prsc:2|COL-9749-OUT;n:type:ShaderForge.SFN_Tex2d,id:4632,x:32036,y:33106,ptovrint:False,ptlb:Normal_Top,ptin:_Normal_Top,varname:node_4632,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:cf20bfced7e912046a9ce991a4d775ec,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:1096,x:32036,y:33290,ptovrint:False,ptlb:Normal_Bottom,ptin:_Normal_Bottom,varname:node_1096,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:630f164e44edb4d848a0fc4d010cfb42,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Lerp,id:7800,x:32202,y:33201,varname:node_7800,prsc:2|A-4632-RGB,B-1096-RGB,T-5861-OUT;n:type:ShaderForge.SFN_Divide,id:7968,x:32441,y:33442,varname:node_7968,prsc:2|A-3601-OUT,B-3958-OUT;n:type:ShaderForge.SFN_Multiply,id:8771,x:32605,y:33495,varname:node_8771,prsc:2|A-7968-OUT,B-7248-OUT;n:type:ShaderForge.SFN_NormalVector,id:7248,x:32441,y:33587,prsc:2,pt:True;n:type:ShaderForge.SFN_ValueProperty,id:3601,x:32202,y:33587,ptovrint:False,ptlb:Vertex_Offset,ptin:_Vertex_Offset,varname:node_3601,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Lerp,id:3544,x:32781,y:33542,varname:node_3544,prsc:2|A-8771-OUT,B-3794-OUT,T-5861-OUT;n:type:ShaderForge.SFN_Multiply,id:3794,x:32605,y:33639,varname:node_3794,prsc:2|A-7968-OUT,B-7427-OUT;n:type:ShaderForge.SFN_Vector1,id:7427,x:32441,y:33747,varname:node_7427,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:3958,x:32286,y:33657,varname:node_3958,prsc:2|A-1747-OUT,B-1747-OUT;n:type:ShaderForge.SFN_Vector1,id:1747,x:32113,y:33657,varname:node_1747,prsc:2,v1:125;proporder:2705-830-4632-4628-3037-1096-8979-3980-9350-46-3601;pass:END;sub:END;*/

Shader "Shader Forge/Dissolve" {
    Properties {
        _Colour_Top ("Colour_Top", 2D) = "white" {}
        _Roughness_Top ("Roughness_Top", 2D) = "white" {}
        _Normal_Top ("Normal_Top", 2D) = "bump" {}
        _Colour_Bottom ("Colour_Bottom", 2D) = "white" {}
        _Rougness_Bottom ("Rougness_Bottom", 2D) = "white" {}
        _Normal_Bottom ("Normal_Bottom", 2D) = "black" {}
        _Metalness ("Metalness", Range(0, 1)) = 0
        _Dissolve_Value ("Dissolve_Value", Range(0, 2)) = 0
        _Dissolve_Noise ("Dissolve_Noise", 2D) = "white" {}
        _Dissolve_Gradient ("Dissolve_Gradient", 2D) = "white" {}
        _Vertex_Offset ("Vertex_Offset", Float ) = 2
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Dissolve_Noise; uniform float4 _Dissolve_Noise_ST;
            uniform float _Dissolve_Value;
            uniform sampler2D _Colour_Top; uniform float4 _Colour_Top_ST;
            uniform sampler2D _Dissolve_Gradient; uniform float4 _Dissolve_Gradient_ST;
            uniform sampler2D _Colour_Bottom; uniform float4 _Colour_Bottom_ST;
            uniform float _Metalness;
            uniform sampler2D _Roughness_Top; uniform float4 _Roughness_Top_ST;
            uniform sampler2D _Rougness_Bottom; uniform float4 _Rougness_Bottom_ST;
            uniform sampler2D _Normal_Top; uniform float4 _Normal_Top_ST;
            uniform sampler2D _Normal_Bottom; uniform float4 _Normal_Bottom_ST;
            uniform float _Vertex_Offset;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float node_1747 = 125.0;
                float node_7968 = (_Vertex_Offset/(node_1747*node_1747));
                float node_3794 = (node_7968*(-1.0));
                float4 _Dissolve_Noise_var = tex2Dlod(_Dissolve_Noise,float4(TRANSFORM_TEX(o.uv0, _Dissolve_Noise),0.0,0));
                float node_5861 = (1.0 - saturate(((((1.0 - _Dissolve_Value)*0.5+0.1)+_Dissolve_Noise_var.r)*16.0+-8.0)));
                v.vertex.xyz += lerp((node_7968*v.normal),float3(node_3794,node_3794,node_3794),node_5861);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_Top_var = UnpackNormal(tex2D(_Normal_Top,TRANSFORM_TEX(i.uv0, _Normal_Top)));
                float4 _Normal_Bottom_var = tex2D(_Normal_Bottom,TRANSFORM_TEX(i.uv0, _Normal_Bottom));
                float4 _Dissolve_Noise_var = tex2D(_Dissolve_Noise,TRANSFORM_TEX(i.uv0, _Dissolve_Noise));
                float node_5861 = (1.0 - saturate(((((1.0 - _Dissolve_Value)*0.5+0.1)+_Dissolve_Noise_var.r)*16.0+-8.0)));
                float3 normalLocal = lerp(_Normal_Top_var.rgb,_Normal_Bottom_var.rgb,node_5861);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _Roughness_Top_var = tex2D(_Roughness_Top,TRANSFORM_TEX(i.uv0, _Roughness_Top));
                float4 _Rougness_Bottom_var = tex2D(_Rougness_Bottom,TRANSFORM_TEX(i.uv0, _Rougness_Bottom));
                float gloss = dot(lerp(_Roughness_Top_var.rgb,_Rougness_Bottom_var.rgb,node_5861),float3(0.3,0.59,0.11));
                float perceptualRoughness = 1.0 - dot(lerp(_Roughness_Top_var.rgb,_Rougness_Bottom_var.rgb,node_5861),float3(0.3,0.59,0.11));
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _Metalness;
                float specularMonochrome;
                float4 _Colour_Top_var = tex2D(_Colour_Top,TRANSFORM_TEX(i.uv0, _Colour_Top));
                float4 _Colour_Bottom_var = tex2D(_Colour_Bottom,TRANSFORM_TEX(i.uv0, _Colour_Bottom));
                float3 diffuseColor = lerp(_Colour_Top_var.rgb,_Colour_Bottom_var.rgb,node_5861); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float node_4206 = 0.0;
                float2 node_539 = float2(node_5861,node_4206);
                float4 node_513 = tex2Dlod(_Dissolve_Gradient,float4(TRANSFORM_TEX(node_539, _Dissolve_Gradient),0.0,node_4206));
                float3 emissive = node_513.rgb;
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Dissolve_Noise; uniform float4 _Dissolve_Noise_ST;
            uniform float _Dissolve_Value;
            uniform sampler2D _Colour_Top; uniform float4 _Colour_Top_ST;
            uniform sampler2D _Dissolve_Gradient; uniform float4 _Dissolve_Gradient_ST;
            uniform sampler2D _Colour_Bottom; uniform float4 _Colour_Bottom_ST;
            uniform float _Metalness;
            uniform sampler2D _Roughness_Top; uniform float4 _Roughness_Top_ST;
            uniform sampler2D _Rougness_Bottom; uniform float4 _Rougness_Bottom_ST;
            uniform sampler2D _Normal_Top; uniform float4 _Normal_Top_ST;
            uniform sampler2D _Normal_Bottom; uniform float4 _Normal_Bottom_ST;
            uniform float _Vertex_Offset;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float node_1747 = 125.0;
                float node_7968 = (_Vertex_Offset/(node_1747*node_1747));
                float node_3794 = (node_7968*(-1.0));
                float4 _Dissolve_Noise_var = tex2Dlod(_Dissolve_Noise,float4(TRANSFORM_TEX(o.uv0, _Dissolve_Noise),0.0,0));
                float node_5861 = (1.0 - saturate(((((1.0 - _Dissolve_Value)*0.5+0.1)+_Dissolve_Noise_var.r)*16.0+-8.0)));
                v.vertex.xyz += lerp((node_7968*v.normal),float3(node_3794,node_3794,node_3794),node_5861);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_Top_var = UnpackNormal(tex2D(_Normal_Top,TRANSFORM_TEX(i.uv0, _Normal_Top)));
                float4 _Normal_Bottom_var = tex2D(_Normal_Bottom,TRANSFORM_TEX(i.uv0, _Normal_Bottom));
                float4 _Dissolve_Noise_var = tex2D(_Dissolve_Noise,TRANSFORM_TEX(i.uv0, _Dissolve_Noise));
                float node_5861 = (1.0 - saturate(((((1.0 - _Dissolve_Value)*0.5+0.1)+_Dissolve_Noise_var.r)*16.0+-8.0)));
                float3 normalLocal = lerp(_Normal_Top_var.rgb,_Normal_Bottom_var.rgb,node_5861);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _Roughness_Top_var = tex2D(_Roughness_Top,TRANSFORM_TEX(i.uv0, _Roughness_Top));
                float4 _Rougness_Bottom_var = tex2D(_Rougness_Bottom,TRANSFORM_TEX(i.uv0, _Rougness_Bottom));
                float gloss = dot(lerp(_Roughness_Top_var.rgb,_Rougness_Bottom_var.rgb,node_5861),float3(0.3,0.59,0.11));
                float perceptualRoughness = 1.0 - dot(lerp(_Roughness_Top_var.rgb,_Rougness_Bottom_var.rgb,node_5861),float3(0.3,0.59,0.11));
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _Metalness;
                float specularMonochrome;
                float4 _Colour_Top_var = tex2D(_Colour_Top,TRANSFORM_TEX(i.uv0, _Colour_Top));
                float4 _Colour_Bottom_var = tex2D(_Colour_Bottom,TRANSFORM_TEX(i.uv0, _Colour_Bottom));
                float3 diffuseColor = lerp(_Colour_Top_var.rgb,_Colour_Bottom_var.rgb,node_5861); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Dissolve_Noise; uniform float4 _Dissolve_Noise_ST;
            uniform float _Dissolve_Value;
            uniform float _Vertex_Offset;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float2 uv1 : TEXCOORD2;
                float2 uv2 : TEXCOORD3;
                float4 posWorld : TEXCOORD4;
                float3 normalDir : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float node_1747 = 125.0;
                float node_7968 = (_Vertex_Offset/(node_1747*node_1747));
                float node_3794 = (node_7968*(-1.0));
                float4 _Dissolve_Noise_var = tex2Dlod(_Dissolve_Noise,float4(TRANSFORM_TEX(o.uv0, _Dissolve_Noise),0.0,0));
                float node_5861 = (1.0 - saturate(((((1.0 - _Dissolve_Value)*0.5+0.1)+_Dissolve_Noise_var.r)*16.0+-8.0)));
                v.vertex.xyz += lerp((node_7968*v.normal),float3(node_3794,node_3794,node_3794),node_5861);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Dissolve_Noise; uniform float4 _Dissolve_Noise_ST;
            uniform float _Dissolve_Value;
            uniform sampler2D _Colour_Top; uniform float4 _Colour_Top_ST;
            uniform sampler2D _Dissolve_Gradient; uniform float4 _Dissolve_Gradient_ST;
            uniform sampler2D _Colour_Bottom; uniform float4 _Colour_Bottom_ST;
            uniform float _Metalness;
            uniform sampler2D _Roughness_Top; uniform float4 _Roughness_Top_ST;
            uniform sampler2D _Rougness_Bottom; uniform float4 _Rougness_Bottom_ST;
            uniform float _Vertex_Offset;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float node_1747 = 125.0;
                float node_7968 = (_Vertex_Offset/(node_1747*node_1747));
                float node_3794 = (node_7968*(-1.0));
                float4 _Dissolve_Noise_var = tex2Dlod(_Dissolve_Noise,float4(TRANSFORM_TEX(o.uv0, _Dissolve_Noise),0.0,0));
                float node_5861 = (1.0 - saturate(((((1.0 - _Dissolve_Value)*0.5+0.1)+_Dissolve_Noise_var.r)*16.0+-8.0)));
                v.vertex.xyz += lerp((node_7968*v.normal),float3(node_3794,node_3794,node_3794),node_5861);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : SV_Target {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _Dissolve_Noise_var = tex2D(_Dissolve_Noise,TRANSFORM_TEX(i.uv0, _Dissolve_Noise));
                float node_5861 = (1.0 - saturate(((((1.0 - _Dissolve_Value)*0.5+0.1)+_Dissolve_Noise_var.r)*16.0+-8.0)));
                float node_4206 = 0.0;
                float2 node_539 = float2(node_5861,node_4206);
                float4 node_513 = tex2Dlod(_Dissolve_Gradient,float4(TRANSFORM_TEX(node_539, _Dissolve_Gradient),0.0,node_4206));
                o.Emission = node_513.rgb;
                
                float4 _Colour_Top_var = tex2D(_Colour_Top,TRANSFORM_TEX(i.uv0, _Colour_Top));
                float4 _Colour_Bottom_var = tex2D(_Colour_Bottom,TRANSFORM_TEX(i.uv0, _Colour_Bottom));
                float3 diffColor = lerp(_Colour_Top_var.rgb,_Colour_Bottom_var.rgb,node_5861);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, _Metalness, specColor, specularMonochrome );
                float4 _Roughness_Top_var = tex2D(_Roughness_Top,TRANSFORM_TEX(i.uv0, _Roughness_Top));
                float4 _Rougness_Bottom_var = tex2D(_Rougness_Bottom,TRANSFORM_TEX(i.uv0, _Rougness_Bottom));
                float roughness = 1.0 - dot(lerp(_Roughness_Top_var.rgb,_Rougness_Bottom_var.rgb,node_5861),float3(0.3,0.59,0.11));
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
