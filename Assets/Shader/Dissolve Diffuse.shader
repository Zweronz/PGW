��Shader "TestBed/Dissolve/Dissolve Diffuse" {
Properties {
 _Burn ("Burn Amount", Range(-0.25,1.25)) = 0
 _LineWidth ("Burn Line Size", Range(0,0.2)) = 0.1
 _BurnColor ("Burn Color", Color) = (1,0,0,1)
 _MainTex ("Main Texture", 2D) = "white" {}
 _BurnMap ("Burn Map", 2D) = "white" {}
}
SubShader { 
 Tags { "QUEUE"="Transparent" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "SHADOWSUPPORT"="true" "QUEUE"="Transparent" }
  Blend SrcAlpha OneMinusSrcAlpha
Program "vp" {
// Platform d3d11 had shader errors
//   Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
//   Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
//   Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
//   Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
// Platform d3d11_9x had shader errors
//   Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
//   Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
//   Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
//   Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Vector 9 [unity_SHAr]
Vector 10 [unity_SHAg]
Vector 11 [unity_SHAb]
Vector 12 [unity_SHBr]
Vector 13 [unity_SHBg]
Vector 14 [unity_SHBb]
Vector 15 [unity_SHC]
Vector 16 [unity_Scale]
Vector 17 [_MainTex_ST]
Vector 18 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[19] = { { 1 },
		state.matrix.mvp,
		program.local[5..18] };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
MUL R1.xyz, vertex.normal, c[16].w;
DP3 R3.w, R1, c[6];
DP3 R2.w, R1, c[7];
DP3 R0.x, R1, c[5];
MOV R0.y, R3.w;
MOV R0.z, R2.w;
MUL R1, R0.xyzz, R0.yzzx;
MOV R0.w, c[0].x;
DP4 R2.z, R0, c[11];
DP4 R2.y, R0, c[10];
DP4 R2.x, R0, c[9];
MUL R0.y, R3.w, R3.w;
DP4 R3.z, R1, c[14];
DP4 R3.y, R1, c[13];
DP4 R3.x, R1, c[12];
MAD R0.y, R0.x, R0.x, -R0;
MUL R1.xyz, R0.y, c[15];
ADD R2.xyz, R2, R3;
ADD result.texcoord[2].xyz, R2, R1;
MOV result.texcoord[1].z, R2.w;
MOV result.texcoord[1].y, R3.w;
MOV result.texcoord[1].x, R0;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[18].xyxy, c[18];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[17], c[17].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 28 instructions, 4 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Vector 8 [unity_SHAr]
Vector 9 [unity_SHAg]
Vector 10 [unity_SHAb]
Vector 11 [unity_SHBr]
Vector 12 [unity_SHBg]
Vector 13 [unity_SHBb]
Vector 14 [unity_SHC]
Vector 15 [unity_Scale]
Vector 16 [_MainTex_ST]
Vector 17 [_BurnMap_ST]
"vs_2_0
def c18, 1.00000000, 0, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r1.xyz, v1, c15.w
dp3 r3.w, r1, c5
dp3 r2.w, r1, c6
dp3 r0.x, r1, c4
mov r0.y, r3.w
mov r0.z, r2.w
mul r1, r0.xyzz, r0.yzzx
mov r0.w, c18.x
dp4 r2.z, r0, c10
dp4 r2.y, r0, c9
dp4 r2.x, r0, c8
mul r0.y, r3.w, r3.w
dp4 r3.z, r1, c13
dp4 r3.y, r1, c12
dp4 r3.x, r1, c11
mad r0.y, r0.x, r0.x, -r0
mul r1.xyz, r0.y, c14
add r2.xyz, r2, r3
add oT2.xyz, r2, r1
mov oT1.z, r2.w
mov oT1.y, r3.w
mov oT1.x, r0
mad oT0.zw, v2.xyxy, c17.xyxy, c17
mad oT0.xy, v2, c16, c16.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Vector 9 [unity_LightmapST]
Vector 10 [_MainTex_ST]
Vector 11 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[12] = { program.local[0],
		state.matrix.mvp,
		program.local[5..11] };
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[11].xyxy, c[11];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[10], c[10].zwzw;
MAD result.texcoord[1].xy, vertex.texcoord[1], c[9], c[9].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 7 instructions, 0 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 0 [glstate_matrix_mvp]
Vector 8 [unity_LightmapST]
Vector 9 [_MainTex_ST]
Vector 10 [_BurnMap_ST]
"vs_2_0
dcl_position0 v0
dcl_texcoord0 v2
dcl_texcoord1 v3
mad oT0.zw, v2.xyxy, c10.xyxy, c10
mad oT0.xy, v2, c9, c9.zwzw
mad oT1.xy, v3, c8, c8.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Vector 9 [unity_LightmapST]
Vector 10 [_MainTex_ST]
Vector 11 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[12] = { program.local[0],
		state.matrix.mvp,
		program.local[5..11] };
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[11].xyxy, c[11];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[10], c[10].zwzw;
MAD result.texcoord[1].xy, vertex.texcoord[1], c[9], c[9].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 7 instructions, 0 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 0 [glstate_matrix_mvp]
Vector 8 [unity_LightmapST]
Vector 9 [_MainTex_ST]
Vector 10 [_BurnMap_ST]
"vs_2_0
dcl_position0 v0
dcl_texcoord0 v2
dcl_texcoord1 v3
mad oT0.zw, v2.xyxy, c10.xyxy, c10
mad oT0.xy, v2, c9, c9.zwzw
mad oT1.xy, v3, c8, c8.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Vector 9 [_ProjectionParams]
Vector 10 [unity_SHAr]
Vector 11 [unity_SHAg]
Vector 12 [unity_SHAb]
Vector 13 [unity_SHBr]
Vector 14 [unity_SHBg]
Vector 15 [unity_SHBb]
Vector 16 [unity_SHC]
Vector 17 [unity_Scale]
Vector 18 [_MainTex_ST]
Vector 19 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[20] = { { 1, 0.5 },
		state.matrix.mvp,
		program.local[5..19] };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
MUL R0.xyz, vertex.normal, c[17].w;
DP3 R3.w, R0, c[6];
DP3 R2.w, R0, c[7];
DP3 R1.w, R0, c[5];
MOV R1.x, R3.w;
MOV R1.y, R2.w;
MOV R1.z, c[0].x;
MUL R0, R1.wxyy, R1.xyyw;
DP4 R2.z, R1.wxyz, c[12];
DP4 R2.y, R1.wxyz, c[11];
DP4 R2.x, R1.wxyz, c[10];
DP4 R1.z, R0, c[15];
DP4 R1.y, R0, c[14];
DP4 R1.x, R0, c[13];
MUL R3.x, R3.w, R3.w;
MAD R0.x, R1.w, R1.w, -R3;
ADD R3.xyz, R2, R1;
MUL R2.xyz, R0.x, c[16];
DP4 R0.w, vertex.position, c[4];
DP4 R0.z, vertex.position, c[3];
DP4 R0.x, vertex.position, c[1];
DP4 R0.y, vertex.position, c[2];
MUL R1.xyz, R0.xyww, c[0].y;
MUL R1.y, R1, c[9].x;
ADD result.texcoord[2].xyz, R3, R2;
ADD result.texcoord[3].xy, R1, R1.z;
MOV result.position, R0;
MOV result.texcoord[3].zw, R0;
MOV result.texcoord[1].z, R2.w;
MOV result.texcoord[1].y, R3.w;
MOV result.texcoord[1].x, R1.w;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[19].xyxy, c[19];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[18], c[18].zwzw;
END
# 33 instructions, 4 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Vector 8 [_ProjectionParams]
Vector 9 [_ScreenParams]
Vector 10 [unity_SHAr]
Vector 11 [unity_SHAg]
Vector 12 [unity_SHAb]
Vector 13 [unity_SHBr]
Vector 14 [unity_SHBg]
Vector 15 [unity_SHBb]
Vector 16 [unity_SHC]
Vector 17 [unity_Scale]
Vector 18 [_MainTex_ST]
Vector 19 [_BurnMap_ST]
"vs_2_0
def c20, 1.00000000, 0.50000000, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r0.xyz, v1, c17.w
dp3 r3.w, r0, c5
dp3 r2.w, r0, c6
dp3 r1.w, r0, c4
mov r1.x, r3.w
mov r1.y, r2.w
mov r1.z, c20.x
mul r0, r1.wxyy, r1.xyyw
dp4 r2.z, r1.wxyz, c12
dp4 r2.y, r1.wxyz, c11
dp4 r2.x, r1.wxyz, c10
dp4 r1.z, r0, c15
dp4 r1.y, r0, c14
dp4 r1.x, r0, c13
mul r3.x, r3.w, r3.w
mad r0.x, r1.w, r1.w, -r3
add r3.xyz, r2, r1
mul r2.xyz, r0.x, c16
dp4 r0.w, v0, c3
dp4 r0.z, v0, c2
dp4 r0.x, v0, c0
dp4 r0.y, v0, c1
mul r1.xyz, r0.xyww, c20.y
mul r1.y, r1, c8.x
add oT2.xyz, r3, r2
mad oT3.xy, r1.z, c9.zwzw, r1
mov oPos, r0
mov oT3.zw, r0
mov oT1.z, r2.w
mov oT1.y, r3.w
mov oT1.x, r1.w
mad oT0.zw, v2.xyxy, c19.xyxy, c19
mad oT0.xy, v2, c18, c18.zwzw
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Vector 9 [_ProjectionParams]
Vector 10 [unity_LightmapST]
Vector 11 [_MainTex_ST]
Vector 12 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[13] = { { 0.5 },
		state.matrix.mvp,
		program.local[5..12] };
TEMP R0;
TEMP R1;
DP4 R0.w, vertex.position, c[4];
DP4 R0.z, vertex.position, c[3];
DP4 R0.x, vertex.position, c[1];
DP4 R0.y, vertex.position, c[2];
MUL R1.xyz, R0.xyww, c[0].x;
MUL R1.y, R1, c[9].x;
ADD result.texcoord[2].xy, R1, R1.z;
MOV result.position, R0;
MOV result.texcoord[2].zw, R0;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[12].xyxy, c[12];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[11], c[11].zwzw;
MAD result.texcoord[1].xy, vertex.texcoord[1], c[10], c[10].zwzw;
END
# 12 instructions, 2 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 0 [glstate_matrix_mvp]
Vector 8 [_ProjectionParams]
Vector 9 [_ScreenParams]
Vector 10 [unity_LightmapST]
Vector 11 [_MainTex_ST]
Vector 12 [_BurnMap_ST]
"vs_2_0
def c13, 0.50000000, 0, 0, 0
dcl_position0 v0
dcl_texcoord0 v2
dcl_texcoord1 v3
dp4 r0.w, v0, c3
dp4 r0.z, v0, c2
dp4 r0.x, v0, c0
dp4 r0.y, v0, c1
mul r1.xyz, r0.xyww, c13.x
mul r1.y, r1, c8.x
mad oT2.xy, r1.z, c9.zwzw, r1
mov oPos, r0
mov oT2.zw, r0
mad oT0.zw, v2.xyxy, c12.xyxy, c12
mad oT0.xy, v2, c11, c11.zwzw
mad oT1.xy, v3, c10, c10.zwzw
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Vector 9 [_ProjectionParams]
Vector 10 [unity_LightmapST]
Vector 11 [_MainTex_ST]
Vector 12 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[13] = { { 0.5 },
		state.matrix.mvp,
		program.local[5..12] };
TEMP R0;
TEMP R1;
DP4 R0.w, vertex.position, c[4];
DP4 R0.z, vertex.position, c[3];
DP4 R0.x, vertex.position, c[1];
DP4 R0.y, vertex.position, c[2];
MUL R1.xyz, R0.xyww, c[0].x;
MUL R1.y, R1, c[9].x;
ADD result.texcoord[2].xy, R1, R1.z;
MOV result.position, R0;
MOV result.texcoord[2].zw, R0;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[12].xyxy, c[12];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[11], c[11].zwzw;
MAD result.texcoord[1].xy, vertex.texcoord[1], c[10], c[10].zwzw;
END
# 12 instructions, 2 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 0 [glstate_matrix_mvp]
Vector 8 [_ProjectionParams]
Vector 9 [_ScreenParams]
Vector 10 [unity_LightmapST]
Vector 11 [_MainTex_ST]
Vector 12 [_BurnMap_ST]
"vs_2_0
def c13, 0.50000000, 0, 0, 0
dcl_position0 v0
dcl_texcoord0 v2
dcl_texcoord1 v3
dp4 r0.w, v0, c3
dp4 r0.z, v0, c2
dp4 r0.x, v0, c0
dp4 r0.y, v0, c1
mul r1.xyz, r0.xyww, c13.x
mul r1.y, r1, c8.x
mad oT2.xy, r1.z, c9.zwzw, r1
mov oPos, r0
mov oT2.zw, r0
mad oT0.zw, v2.xyxy, c12.xyxy, c12
mad oT0.xy, v2, c11, c11.zwzw
mad oT1.xy, v3, c10, c10.zwzw
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Vector 9 [unity_4LightPosX0]
Vector 10 [unity_4LightPosY0]
Vector 11 [unity_4LightPosZ0]
Vector 12 [unity_4LightAtten0]
Vector 13 [unity_LightColor0]
Vector 14 [unity_LightColor1]
Vector 15 [unity_LightColor2]
Vector 16 [unity_LightColor3]
Vector 17 [unity_SHAr]
Vector 18 [unity_SHAg]
Vector 19 [unity_SHAb]
Vector 20 [unity_SHBr]
Vector 21 [unity_SHBg]
Vector 22 [unity_SHBb]
Vector 23 [unity_SHC]
Vector 24 [unity_Scale]
Vector 25 [_MainTex_ST]
Vector 26 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[27] = { { 1, 0 },
		state.matrix.mvp,
		program.local[5..26] };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
MUL R3.xyz, vertex.normal, c[24].w;
DP3 R4.x, R3, c[5];
DP3 R3.w, R3, c[6];
DP3 R3.x, R3, c[7];
DP4 R0.x, vertex.position, c[6];
ADD R1, -R0.x, c[10];
MUL R2, R3.w, R1;
DP4 R0.x, vertex.position, c[5];
ADD R0, -R0.x, c[9];
MUL R1, R1, R1;
MOV R4.z, R3.x;
MOV R4.w, c[0].x;
MAD R2, R4.x, R0, R2;
DP4 R4.y, vertex.position, c[7];
MAD R1, R0, R0, R1;
ADD R0, -R4.y, c[11];
MAD R1, R0, R0, R1;
MAD R0, R3.x, R0, R2;
MUL R2, R1, c[12];
MOV R4.y, R3.w;
RSQ R1.x, R1.x;
RSQ R1.y, R1.y;
RSQ R1.w, R1.w;
RSQ R1.z, R1.z;
MUL R0, R0, R1;
ADD R1, R2, c[0].x;
DP4 R2.z, R4, c[19];
DP4 R2.y, R4, c[18];
DP4 R2.x, R4, c[17];
RCP R1.x, R1.x;
RCP R1.y, R1.y;
RCP R1.w, R1.w;
RCP R1.z, R1.z;
MAX R0, R0, c[0].y;
MUL R0, R0, R1;
MUL R1.xyz, R0.y, c[14];
MAD R1.xyz, R0.x, c[13], R1;
MAD R0.xyz, R0.z, c[15], R1;
MAD R1.xyz, R0.w, c[16], R0;
MUL R0, R4.xyzz, R4.yzzx;
MUL R1.w, R3, R3;
DP4 R4.w, R0, c[22];
DP4 R4.z, R0, c[21];
DP4 R4.y, R0, c[20];
MAD R1.w, R4.x, R4.x, -R1;
MUL R0.xyz, R1.w, c[23];
ADD R2.xyz, R2, R4.yzww;
ADD R0.xyz, R2, R0;
ADD result.texcoord[2].xyz, R0, R1;
MOV result.texcoord[1].z, R3.x;
MOV result.texcoord[1].y, R3.w;
MOV result.texcoord[1].x, R4;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[26].xyxy, c[26];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[25], c[25].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 58 instructions, 5 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Vector 8 [unity_4LightPosX0]
Vector 9 [unity_4LightPosY0]
Vector 10 [unity_4LightPosZ0]
Vector 11 [unity_4LightAtten0]
Vector 12 [unity_LightColor0]
Vector 13 [unity_LightColor1]
Vector 14 [unity_LightColor2]
Vector 15 [unity_LightColor3]
Vector 16 [unity_SHAr]
Vector 17 [unity_SHAg]
Vector 18 [unity_SHAb]
Vector 19 [unity_SHBr]
Vector 20 [unity_SHBg]
Vector 21 [unity_SHBb]
Vector 22 [unity_SHC]
Vector 23 [unity_Scale]
Vector 24 [_MainTex_ST]
Vector 25 [_BurnMap_ST]
"vs_2_0
def c26, 1.00000000, 0.00000000, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r3.xyz, v1, c23.w
dp3 r4.x, r3, c4
dp3 r3.w, r3, c5
dp3 r3.x, r3, c6
dp4 r0.x, v0, c5
add r1, -r0.x, c9
mul r2, r3.w, r1
dp4 r0.x, v0, c4
add r0, -r0.x, c8
mul r1, r1, r1
mov r4.z, r3.x
mov r4.w, c26.x
mad r2, r4.x, r0, r2
dp4 r4.y, v0, c6
mad r1, r0, r0, r1
add r0, -r4.y, c10
mad r1, r0, r0, r1
mad r0, r3.x, r0, r2
mul r2, r1, c11
mov r4.y, r3.w
rsq r1.x, r1.x
rsq r1.y, r1.y
rsq r1.w, r1.w
rsq r1.z, r1.z
mul r0, r0, r1
add r1, r2, c26.x
dp4 r2.z, r4, c18
dp4 r2.y, r4, c17
dp4 r2.x, r4, c16
rcp r1.x, r1.x
rcp r1.y, r1.y
rcp r1.w, r1.w
rcp r1.z, r1.z
max r0, r0, c26.y
mul r0, r0, r1
mul r1.xyz, r0.y, c13
mad r1.xyz, r0.x, c12, r1
mad r0.xyz, r0.z, c14, r1
mad r1.xyz, r0.w, c15, r0
mul r0, r4.xyzz, r4.yzzx
mul r1.w, r3, r3
dp4 r4.w, r0, c21
dp4 r4.z, r0, c20
dp4 r4.y, r0, c19
mad r1.w, r4.x, r4.x, -r1
mul r0.xyz, r1.w, c22
add r2.xyz, r2, r4.yzww
add r0.xyz, r2, r0
add oT2.xyz, r0, r1
mov oT1.z, r3.x
mov oT1.y, r3.w
mov oT1.x, r4
mad oT0.zw, v2.xyxy, c25.xyxy, c25
mad oT0.xy, v2, c24, c24.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Vector 9 [_ProjectionParams]
Vector 10 [unity_4LightPosX0]
Vector 11 [unity_4LightPosY0]
Vector 12 [unity_4LightPosZ0]
Vector 13 [unity_4LightAtten0]
Vector 14 [unity_LightColor0]
Vector 15 [unity_LightColor1]
Vector 16 [unity_LightColor2]
Vector 17 [unity_LightColor3]
Vector 18 [unity_SHAr]
Vector 19 [unity_SHAg]
Vector 20 [unity_SHAb]
Vector 21 [unity_SHBr]
Vector 22 [unity_SHBg]
Vector 23 [unity_SHBb]
Vector 24 [unity_SHC]
Vector 25 [unity_Scale]
Vector 26 [_MainTex_ST]
Vector 27 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[28] = { { 1, 0, 0.5 },
		state.matrix.mvp,
		program.local[5..27] };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
MUL R3.xyz, vertex.normal, c[25].w;
DP3 R4.x, R3, c[5];
DP3 R3.w, R3, c[6];
DP3 R3.x, R3, c[7];
DP4 R0.x, vertex.position, c[6];
ADD R1, -R0.x, c[11];
MUL R2, R3.w, R1;
DP4 R0.x, vertex.position, c[5];
ADD R0, -R0.x, c[10];
MUL R1, R1, R1;
MOV R4.z, R3.x;
MOV R4.w, c[0].x;
MAD R2, R4.x, R0, R2;
DP4 R4.y, vertex.position, c[7];
MAD R1, R0, R0, R1;
ADD R0, -R4.y, c[12];
MAD R1, R0, R0, R1;
MAD R0, R3.x, R0, R2;
MUL R2, R1, c[13];
MOV R4.y, R3.w;
RSQ R1.x, R1.x;
RSQ R1.y, R1.y;
RSQ R1.w, R1.w;
RSQ R1.z, R1.z;
MUL R0, R0, R1;
ADD R1, R2, c[0].x;
DP4 R2.z, R4, c[20];
DP4 R2.y, R4, c[19];
DP4 R2.x, R4, c[18];
RCP R1.x, R1.x;
RCP R1.y, R1.y;
RCP R1.w, R1.w;
RCP R1.z, R1.z;
MAX R0, R0, c[0].y;
MUL R0, R0, R1;
MUL R1.xyz, R0.y, c[15];
MAD R1.xyz, R0.x, c[14], R1;
MAD R0.xyz, R0.z, c[16], R1;
MAD R1.xyz, R0.w, c[17], R0;
MUL R0, R4.xyzz, R4.yzzx;
MUL R1.w, R3, R3;
DP4 R4.w, R0, c[23];
DP4 R4.z, R0, c[22];
DP4 R4.y, R0, c[21];
MAD R1.w, R4.x, R4.x, -R1;
MUL R0.xyz, R1.w, c[24];
ADD R2.xyz, R2, R4.yzww;
ADD R4.yzw, R2.xxyz, R0.xxyz;
DP4 R0.w, vertex.position, c[4];
DP4 R0.z, vertex.position, c[3];
DP4 R0.x, vertex.position, c[1];
DP4 R0.y, vertex.position, c[2];
MUL R2.xyz, R0.xyww, c[0].z;
ADD result.texcoord[2].xyz, R4.yzww, R1;
MOV R1.x, R2;
MUL R1.y, R2, c[9].x;
ADD result.texcoord[3].xy, R1, R2.z;
MOV result.position, R0;
MOV result.texcoord[3].zw, R0;
MOV result.texcoord[1].z, R3.x;
MOV result.texcoord[1].y, R3.w;
MOV result.texcoord[1].x, R4;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[27].xyxy, c[27];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[26], c[26].zwzw;
END
# 64 instructions, 5 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Vector 8 [_ProjectionParams]
Vector 9 [_ScreenParams]
Vector 10 [unity_4LightPosX0]
Vector 11 [unity_4LightPosY0]
Vector 12 [unity_4LightPosZ0]
Vector 13 [unity_4LightAtten0]
Vector 14 [unity_LightColor0]
Vector 15 [unity_LightColor1]
Vector 16 [unity_LightColor2]
Vector 17 [unity_LightColor3]
Vector 18 [unity_SHAr]
Vector 19 [unity_SHAg]
Vector 20 [unity_SHAb]
Vector 21 [unity_SHBr]
Vector 22 [unity_SHBg]
Vector 23 [unity_SHBb]
Vector 24 [unity_SHC]
Vector 25 [unity_Scale]
Vector 26 [_MainTex_ST]
Vector 27 [_BurnMap_ST]
"vs_2_0
def c28, 1.00000000, 0.00000000, 0.50000000, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r3.xyz, v1, c25.w
dp3 r4.x, r3, c4
dp3 r3.w, r3, c5
dp3 r3.x, r3, c6
dp4 r0.x, v0, c5
add r1, -r0.x, c11
mul r2, r3.w, r1
dp4 r0.x, v0, c4
add r0, -r0.x, c10
mul r1, r1, r1
mov r4.z, r3.x
mov r4.w, c28.x
mad r2, r4.x, r0, r2
dp4 r4.y, v0, c6
mad r1, r0, r0, r1
add r0, -r4.y, c12
mad r1, r0, r0, r1
mad r0, r3.x, r0, r2
mul r2, r1, c13
mov r4.y, r3.w
rsq r1.x, r1.x
rsq r1.y, r1.y
rsq r1.w, r1.w
rsq r1.z, r1.z
mul r0, r0, r1
add r1, r2, c28.x
dp4 r2.z, r4, c20
dp4 r2.y, r4, c19
dp4 r2.x, r4, c18
rcp r1.x, r1.x
rcp r1.y, r1.y
rcp r1.w, r1.w
rcp r1.z, r1.z
max r0, r0, c28.y
mul r0, r0, r1
mul r1.xyz, r0.y, c15
mad r1.xyz, r0.x, c14, r1
mad r0.xyz, r0.z, c16, r1
mad r1.xyz, r0.w, c17, r0
mul r0, r4.xyzz, r4.yzzx
mul r1.w, r3, r3
dp4 r4.w, r0, c23
dp4 r4.z, r0, c22
dp4 r4.y, r0, c21
mad r1.w, r4.x, r4.x, -r1
mul r0.xyz, r1.w, c24
add r2.xyz, r2, r4.yzww
add r4.yzw, r2.xxyz, r0.xxyz
dp4 r0.w, v0, c3
dp4 r0.z, v0, c2
dp4 r0.x, v0, c0
dp4 r0.y, v0, c1
mul r2.xyz, r0.xyww, c28.z
add oT2.xyz, r4.yzww, r1
mov r1.x, r2
mul r1.y, r2, c8.x
mad oT3.xy, r2.z, c9.zwzw, r1
mov oPos, r0
mov oT3.zw, r0
mov oT1.z, r3.x
mov oT1.y, r3.w
mov oT1.x, r4
mad oT0.zw, v2.xyxy, c27.xyxy, c27
mad oT0.xy, v2, c26, c26.zwzw
"
}
}
Program "fp" {
// Platform d3d11 had shader errors
//   Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
//   Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
// Platform d3d11_9x had shader errors
//   Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
//   Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
//   Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
Vector 0 [_WorldSpaceLightPos0]
Vector 1 [_LightColor0]
Vector 2 [_BurnColor]
Float 3 [_LineWidth]
Float 4 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
"!!ARBfp1.0
PARAM c[6] = { program.local[0..4],
		{ 0, 0.99000001, 2, 1 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEX R1.x, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R0.xyz, fragment.texcoord[0], texture[0], 2D;
MOV R0.w, c[3].x;
ADD R0.w, R0, c[4].x;
ADD R0.w, R1.x, -R0;
ADD R1.y, R0.w, c[5];
ADD R1.w, R1.x, -c[4].x;
ADD R1.x, R1.w, c[5].y;
ABS R2.x, R1;
ABS R1.y, R1;
FLR R1.y, R1;
SLT R0.w, R0, -c[5].y;
CMP R0.w, -R0, -R1.y, R1.y;
MAX R1.y, R0.w, c[5].x;
MAD R1.xyz, R1.y, -c[2], c[2];
ADD R1.xyz, R1, -R0;
FLR R2.x, R2;
SLT R1.w, R1, -c[5].y;
CMP R1.w, -R1, -R2.x, R2.x;
MAX R1.w, R1, c[5].x;
MAD R0.xyz, R1.w, R1, R0;
MUL R1.xyz, R0, fragment.texcoord[2];
DP3 R1.w, fragment.texcoord[1], c[0];
MUL R0.xyz, R0, c[1];
MAX R1.w, R1, c[5].x;
MUL R0.xyz, R1.w, R0;
MAD result.color.xyz, R0, c[5].z, R1;
ADD result.color.w, -R0, c[5];
END
# 28 instructions, 3 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
Vector 0 [_WorldSpaceLightPos0]
Vector 1 [_LightColor0]
Vector 2 [_BurnColor]
Float 3 [_LineWidth]
Float 4 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
"ps_2_0
dcl_2d s0
dcl_2d s1
def c5, 0.00000000, 0.99000001, 2.00000000, 1.00000000
dcl t0
dcl t1.xyz
dcl t2.xyz
texld r6, t0, s0
mov r0.y, t0.w
mov r0.x, t0.z
texld r1, r0, s1
mov r0.x, c4
add r0.x, c3, r0
add r0.x, r1, -r0
add r0.x, r0, c5.y
abs r4.x, r0
add r1.x, r1, -c4
add r1.x, r1, c5.y
abs r2.x, r1
frc r3.x, r2
add r2.x, r2, -r3
cmp r1.x, r1, r2, -r2
frc r5.x, r4
add r4.x, r4, -r5
cmp r0.x, r0, r4, -r4
max r2.x, r0, c5
mad r2.xyz, r2.x, -c2, c2
max r1.x, r1, c5
add_pp r2.xyz, r2, -r6
mad_pp r2.xyz, r1.x, r2, r6
mul_pp r3.xyz, r2, c1
dp3_pp r1.x, t1, c0
max_pp r1.x, r1, c5
add r0.w, -r0.x, c5
mul_pp r1.xyz, r1.x, r3
mul_pp r2.xyz, r2, t2
mad_pp r0.xyz, r1, c5.z, r2
mov_pp oC0, r0
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [unity_Lightmap] 2D 2
"!!ARBfp1.0
PARAM c[4] = { program.local[0..2],
		{ 1, 0.99000001, 0, 8 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEX R2.x, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R0, fragment.texcoord[1], texture[2], 2D;
TEX R1.xyz, fragment.texcoord[0], texture[0], 2D;
MOV R1.w, c[1].x;
ADD R1.w, R1, c[2].x;
ADD R1.w, R2.x, -R1;
ADD R2.y, R1.w, c[3];
ADD R2.w, R2.x, -c[2].x;
ADD R2.x, R2.w, c[3].y;
ABS R3.x, R2;
ABS R2.y, R2;
FLR R2.y, R2;
SLT R1.w, R1, -c[3].y;
CMP R1.w, -R1, -R2.y, R2.y;
MAX R2.y, R1.w, c[3].z;
MAD R2.xyz, R2.y, -c[0], c[0];
FLR R3.x, R3;
SLT R2.w, R2, -c[3].y;
CMP R2.w, -R2, -R3.x, R3.x;
ADD R2.xyz, R2, -R1;
MAX R2.w, R2, c[3].z;
MAD R1.xyz, R2.w, R2, R1;
MUL R0.xyz, R0.w, R0;
MUL R0.xyz, R0, R1;
MUL result.color.xyz, R0, c[3].w;
ADD result.color.w, -R1, c[3].x;
END
# 26 instructions, 4 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [unity_Lightmap] 2D 2
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
def c3, 0.99000001, 0.00000000, 1.00000000, 8.00000000
dcl t0
dcl t1.xy
texld r6, t1, s2
texld r7, t0, s0
mov r0.x, t0.z
mov r0.y, t0.w
texld r1, r0, s1
mov r0.x, c2
add r0.x, c1, r0
add r0.x, r1, -r0
add r0.x, r0, c3
abs r4.x, r0
add r1.x, r1, -c2
add r1.x, r1, c3
abs r2.x, r1
frc r5.x, r4
frc r3.x, r2
add r2.x, r2, -r3
cmp r1.x, r1, r2, -r2
add r4.x, r4, -r5
cmp r0.x, r0, r4, -r4
max r2.x, r0, c3.y
mad r2.xyz, r2.x, -c0, c0
add_pp r2.xyz, r2, -r7
max r1.x, r1, c3.y
mad_pp r1.xyz, r1.x, r2, r7
mul_pp r2.xyz, r6.w, r6
mul_pp r1.xyz, r2, r1
mul_pp r1.xyz, r1, c3.w
add r1.w, -r0.x, c3.z
mov_pp oC0, r1
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [unity_Lightmap] 2D 2
"!!ARBfp1.0
PARAM c[4] = { program.local[0..2],
		{ 1, 0.99000001, 0, 8 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEX R2.x, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R0, fragment.texcoord[1], texture[2], 2D;
TEX R1.xyz, fragment.texcoord[0], texture[0], 2D;
MOV R1.w, c[1].x;
ADD R1.w, R1, c[2].x;
ADD R1.w, R2.x, -R1;
ADD R2.y, R1.w, c[3];
ADD R2.w, R2.x, -c[2].x;
ADD R2.x, R2.w, c[3].y;
ABS R3.x, R2;
ABS R2.y, R2;
FLR R2.y, R2;
SLT R1.w, R1, -c[3].y;
CMP R1.w, -R1, -R2.y, R2.y;
MAX R2.y, R1.w, c[3].z;
MAD R2.xyz, R2.y, -c[0], c[0];
FLR R3.x, R3;
SLT R2.w, R2, -c[3].y;
CMP R2.w, -R2, -R3.x, R3.x;
ADD R2.xyz, R2, -R1;
MAX R2.w, R2, c[3].z;
MAD R1.xyz, R2.w, R2, R1;
MUL R0.xyz, R0.w, R0;
MUL R0.xyz, R0, R1;
MUL result.color.xyz, R0, c[3].w;
ADD result.color.w, -R1, c[3].x;
END
# 26 instructions, 4 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [unity_Lightmap] 2D 2
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
def c3, 0.99000001, 0.00000000, 1.00000000, 8.00000000
dcl t0
dcl t1.xy
texld r6, t1, s2
texld r7, t0, s0
mov r0.x, t0.z
mov r0.y, t0.w
texld r1, r0, s1
mov r0.x, c2
add r0.x, c1, r0
add r0.x, r1, -r0
add r0.x, r0, c3
abs r4.x, r0
add r1.x, r1, -c2
add r1.x, r1, c3
abs r2.x, r1
frc r5.x, r4
frc r3.x, r2
add r2.x, r2, -r3
cmp r1.x, r1, r2, -r2
add r4.x, r4, -r5
cmp r0.x, r0, r4, -r4
max r2.x, r0, c3.y
mad r2.xyz, r2.x, -c0, c0
add_pp r2.xyz, r2, -r7
max r1.x, r1, c3.y
mad_pp r1.xyz, r1.x, r2, r7
mul_pp r2.xyz, r6.w, r6
mul_pp r1.xyz, r2, r1
mul_pp r1.xyz, r1, c3.w
add r1.w, -r0.x, c3.z
mov_pp oC0, r1
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
Vector 0 [_WorldSpaceLightPos0]
Vector 1 [_LightColor0]
Vector 2 [_BurnColor]
Float 3 [_LineWidth]
Float 4 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_ShadowMapTexture] 2D 2
"!!ARBfp1.0
PARAM c[6] = { program.local[0..4],
		{ 0, 0.99000001, 2, 1 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEX R2.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R1.x, fragment.texcoord[0].zwzw, texture[1], 2D;
TXP R0.x, fragment.texcoord[3], texture[2], 2D;
MOV R0.y, c[3].x;
ADD R0.y, R0, c[4].x;
ADD R0.y, R1.x, -R0;
ADD R0.z, R0.y, c[5].y;
ABS R0.z, R0;
FLR R0.w, R0.z;
SLT R0.y, R0, -c[5];
CMP R0.y, -R0, -R0.w, R0.w;
ADD R0.z, R1.x, -c[4].x;
ADD R0.w, R0.z, c[5].y;
MAX R1.x, R0.y, c[5];
ABS R0.w, R0;
MAD R1.xyz, R1.x, -c[2], c[2];
ADD R1.xyz, R1, -R2;
FLR R0.w, R0;
SLT R0.z, R0, -c[5].y;
CMP R0.z, -R0, -R0.w, R0.w;
MAX R0.z, R0, c[5].x;
MAD R1.xyz, R0.z, R1, R2;
MUL R2.xyz, R1, fragment.texcoord[2];
DP3 R0.z, fragment.texcoord[1], c[0];
MAX R0.z, R0, c[5].x;
MUL R1.xyz, R1, c[1];
MUL R0.x, R0.z, R0;
MUL R1.xyz, R0.x, R1;
MAD result.color.xyz, R1, c[5].z, R2;
ADD result.color.w, -R0.y, c[5];
END
# 30 instructions, 3 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
Vector 0 [_WorldSpaceLightPos0]
Vector 1 [_LightColor0]
Vector 2 [_BurnColor]
Float 3 [_LineWidth]
Float 4 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_ShadowMapTexture] 2D 2
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
def c5, 0.00000000, 0.99000001, 2.00000000, 1.00000000
dcl t0
dcl t1.xyz
dcl t2.xyz
dcl t3
texldp r7, t3, s2
texld r6, t0, s0
mov r0.y, t0.w
mov r0.x, t0.z
texld r1, r0, s1
mov r0.x, c4
add r0.x, c3, r0
add r0.x, r1, -r0
add r0.x, r0, c5.y
abs r4.x, r0
add r1.x, r1, -c4
add r1.x, r1, c5.y
abs r2.x, r1
frc r3.x, r2
add r2.x, r2, -r3
cmp r1.x, r1, r2, -r2
frc r5.x, r4
add r4.x, r4, -r5
cmp r0.x, r0, r4, -r4
max r2.x, r0, c5
mad r2.xyz, r2.x, -c2, c2
max r1.x, r1, c5
add_pp r2.xyz, r2, -r6
mad_pp r2.xyz, r1.x, r2, r6
mul_pp r3.xyz, r2, c1
dp3_pp r1.x, t1, c0
max_pp r1.x, r1, c5
mul_pp r1.x, r1, r7
add r0.w, -r0.x, c5
mul_pp r1.xyz, r1.x, r3
mul_pp r2.xyz, r2, t2
mad_pp r0.xyz, r1, c5.z, r2
mov_pp oC0, r0
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_ShadowMapTexture] 2D 2
SetTexture 3 [unity_Lightmap] 2D 3
"!!ARBfp1.0
PARAM c[5] = { program.local[0..2],
		{ 1, 0.99000001, 0, 8 },
		{ 2 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEX R2, fragment.texcoord[1], texture[3], 2D;
TEX R0.x, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R3.xyz, fragment.texcoord[0], texture[0], 2D;
TXP R1.x, fragment.texcoord[2], texture[2], 2D;
MUL R0.yzw, R2.w, R2.xxyz;
MUL R2.xyz, R2, R1.x;
MOV R1.y, c[1].x;
ADD R1.y, R1, c[2].x;
MUL R0.yzw, R0, c[3].w;
ADD R1.w, R0.x, -R1.y;
MUL R2.xyz, R2, c[4].x;
MIN R2.xyz, R0.yzww, R2;
MUL R1.xyz, R0.yzww, R1.x;
ADD R0.y, R1.w, c[3];
ABS R0.z, R0.y;
SLT R0.y, R1.w, -c[3];
ADD R1.w, R0.x, -c[2].x;
ADD R0.x, R1.w, c[3].y;
MAX R1.xyz, R2, R1;
FLR R0.z, R0;
CMP R0.w, -R0.y, -R0.z, R0.z;
ABS R2.x, R0;
MAX R0.y, R0.w, c[3].z;
MAD R0.xyz, R0.y, -c[0], c[0];
FLR R2.x, R2;
SLT R1.w, R1, -c[3].y;
CMP R1.w, -R1, -R2.x, R2.x;
ADD R0.xyz, R0, -R3;
MAX R1.w, R1, c[3].z;
MAD R0.xyz, R1.w, R0, R3;
MUL result.color.xyz, R0, R1;
ADD result.color.w, -R0, c[3].x;
END
# 32 instructions, 4 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_ShadowMapTexture] 2D 2
SetTexture 3 [unity_Lightmap] 2D 3
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
def c3, 0.99000001, 0.00000000, 1.00000000, 8.00000000
def c4, 2.00000000, 0, 0, 0
dcl t0
dcl t1.xy
dcl t2
texldp r3, t2, s2
texld r6, t0, s0
mov r0.y, t0.w
mov r0.x, t0.z
texld r1, r0, s1
texld r0, t1, s3
mul_pp r2.xyz, r0, r3.x
mul_pp r0.xyz, r0.w, r0
mul_pp r0.xyz, r0, c3.w
mul_pp r2.xyz, r2, c4.x
min_pp r2.xyz, r0, r2
mul_pp r3.xyz, r0, r3.x
mov r0.x, c2
add r0.x, c1, r0
add r0.x, r1, -r0
add r0.x, r0, c3
abs r4.x, r0
add r1.x, r1, -c2
frc r5.x, r4
add r4.x, r4, -r5
cmp r0.x, r0, r4, -r4
max_pp r7.xyz, r2, r3
add r1.x, r1, c3
abs r2.x, r1
frc r3.x, r2
add r2.x, r2, -r3
cmp r1.x, r1, r2, -r2
max r2.x, r0, c3.y
mad r2.xyz, r2.x, -c0, c0
add_pp r2.xyz, r2, -r6
max r1.x, r1, c3.y
mad_pp r1.xyz, r1.x, r2, r6
mul_pp r1.xyz, r1, r7
add r1.w, -r0.x, c3.z
mov_pp oC0, r1
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_ShadowMapTexture] 2D 2
SetTexture 3 [unity_Lightmap] 2D 3
"!!ARBfp1.0
PARAM c[5] = { program.local[0..2],
		{ 1, 0.99000001, 0, 8 },
		{ 2 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEX R2, fragment.texcoord[1], texture[3], 2D;
TEX R0.x, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R3.xyz, fragment.texcoord[0], texture[0], 2D;
TXP R1.x, fragment.texcoord[2], texture[2], 2D;
MUL R0.yzw, R2.w, R2.xxyz;
MUL R2.xyz, R2, R1.x;
MOV R1.y, c[1].x;
ADD R1.y, R1, c[2].x;
MUL R0.yzw, R0, c[3].w;
ADD R1.w, R0.x, -R1.y;
MUL R2.xyz, R2, c[4].x;
MIN R2.xyz, R0.yzww, R2;
MUL R1.xyz, R0.yzww, R1.x;
ADD R0.y, R1.w, c[3];
ABS R0.z, R0.y;
SLT R0.y, R1.w, -c[3];
ADD R1.w, R0.x, -c[2].x;
ADD R0.x, R1.w, c[3].y;
MAX R1.xyz, R2, R1;
FLR R0.z, R0;
CMP R0.w, -R0.y, -R0.z, R0.z;
ABS R2.x, R0;
MAX R0.y, R0.w, c[3].z;
MAD R0.xyz, R0.y, -c[0], c[0];
FLR R2.x, R2;
SLT R1.w, R1, -c[3].y;
CMP R1.w, -R1, -R2.x, R2.x;
ADD R0.xyz, R0, -R3;
MAX R1.w, R1, c[3].z;
MAD R0.xyz, R1.w, R0, R3;
MUL result.color.xyz, R0, R1;
ADD result.color.w, -R0, c[3].x;
END
# 32 instructions, 4 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_ShadowMapTexture] 2D 2
SetTexture 3 [unity_Lightmap] 2D 3
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
def c3, 0.99000001, 0.00000000, 1.00000000, 8.00000000
def c4, 2.00000000, 0, 0, 0
dcl t0
dcl t1.xy
dcl t2
texldp r3, t2, s2
texld r6, t0, s0
mov r0.y, t0.w
mov r0.x, t0.z
texld r1, r0, s1
texld r0, t1, s3
mul_pp r2.xyz, r0, r3.x
mul_pp r0.xyz, r0.w, r0
mul_pp r0.xyz, r0, c3.w
mul_pp r2.xyz, r2, c4.x
min_pp r2.xyz, r0, r2
mul_pp r3.xyz, r0, r3.x
mov r0.x, c2
add r0.x, c1, r0
add r0.x, r1, -r0
add r0.x, r0, c3
abs r4.x, r0
add r1.x, r1, -c2
frc r5.x, r4
add r4.x, r4, -r5
cmp r0.x, r0, r4, -r4
max_pp r7.xyz, r2, r3
add r1.x, r1, c3
abs r2.x, r1
frc r3.x, r2
add r2.x, r2, -r3
cmp r1.x, r1, r2, -r2
max r2.x, r0, c3.y
mad r2.xyz, r2.x, -c0, c0
add_pp r2.xyz, r2, -r6
max r1.x, r1, c3.y
mad_pp r1.xyz, r1.x, r2, r6
mul_pp r1.xyz, r1, r7
add r1.w, -r0.x, c3.z
mov_pp oC0, r1
"
}
}
 }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardAdd" "QUEUE"="Transparent" }
  ZWrite Off
  Fog {
   Color (0,0,0,0)
  }
  Blend One One
Program "vp" {
// Platform d3d11 had shader errors
//   Keywords { "POINT" }
//   Keywords { "DIRECTIONAL" }
//   Keywords { "SPOT" }
//   Keywords { "POINT_COOKIE" }
//   Keywords { "DIRECTIONAL_COOKIE" }
// Platform d3d11_9x had shader errors
//   Keywords { "POINT" }
//   Keywords { "DIRECTIONAL" }
//   Keywords { "SPOT" }
//   Keywords { "POINT_COOKIE" }
//   Keywords { "DIRECTIONAL_COOKIE" }
SubProgram "opengl " {
Keywords { "POINT" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Matrix 9 [_LightMatrix0]
Vector 13 [_WorldSpaceLightPos0]
Vector 14 [unity_Scale]
Vector 15 [_MainTex_ST]
Vector 16 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[17] = { program.local[0],
		state.matrix.mvp,
		program.local[5..16] };
TEMP R0;
TEMP R1;
MUL R1.xyz, vertex.normal, c[14].w;
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
DP4 R0.w, vertex.position, c[8];
DP4 result.texcoord[3].z, R0, c[11];
DP4 result.texcoord[3].y, R0, c[10];
DP4 result.texcoord[3].x, R0, c[9];
DP3 result.texcoord[1].z, R1, c[7];
DP3 result.texcoord[1].y, R1, c[6];
DP3 result.texcoord[1].x, R1, c[5];
ADD result.texcoord[2].xyz, -R0, c[13];
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[16].xyxy, c[16];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[15], c[15].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 18 instructions, 2 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "POINT" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_LightMatrix0]
Vector 12 [_WorldSpaceLightPos0]
Vector 13 [unity_Scale]
Vector 14 [_MainTex_ST]
Vector 15 [_BurnMap_ST]
"vs_2_0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r1.xyz, v1, c13.w
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
dp4 r0.w, v0, c7
dp4 oT3.z, r0, c10
dp4 oT3.y, r0, c9
dp4 oT3.x, r0, c8
dp3 oT1.z, r1, c6
dp3 oT1.y, r1, c5
dp3 oT1.x, r1, c4
add oT2.xyz, -r0, c12
mad oT0.zw, v2.xyxy, c15.xyxy, c15
mad oT0.xy, v2, c14, c14.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Vector 9 [_WorldSpaceLightPos0]
Vector 10 [unity_Scale]
Vector 11 [_MainTex_ST]
Vector 12 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[13] = { program.local[0],
		state.matrix.mvp,
		program.local[5..12] };
TEMP R0;
MUL R0.xyz, vertex.normal, c[10].w;
DP3 result.texcoord[1].z, R0, c[7];
DP3 result.texcoord[1].y, R0, c[6];
DP3 result.texcoord[1].x, R0, c[5];
MOV result.texcoord[2].xyz, c[9];
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[12].xyxy, c[12];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[11], c[11].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 11 instructions, 1 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Vector 8 [_WorldSpaceLightPos0]
Vector 9 [unity_Scale]
Vector 10 [_MainTex_ST]
Vector 11 [_BurnMap_ST]
"vs_2_0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r0.xyz, v1, c9.w
dp3 oT1.z, r0, c6
dp3 oT1.y, r0, c5
dp3 oT1.x, r0, c4
mov oT2.xyz, c8
mad oT0.zw, v2.xyxy, c11.xyxy, c11
mad oT0.xy, v2, c10, c10.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
SubProgram "opengl " {
Keywords { "SPOT" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Matrix 9 [_LightMatrix0]
Vector 13 [_WorldSpaceLightPos0]
Vector 14 [unity_Scale]
Vector 15 [_MainTex_ST]
Vector 16 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[17] = { program.local[0],
		state.matrix.mvp,
		program.local[5..16] };
TEMP R0;
TEMP R1;
MUL R1.xyz, vertex.normal, c[14].w;
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
DP4 R0.w, vertex.position, c[8];
DP4 result.texcoord[3].w, R0, c[12];
DP4 result.texcoord[3].z, R0, c[11];
DP4 result.texcoord[3].y, R0, c[10];
DP4 result.texcoord[3].x, R0, c[9];
DP3 result.texcoord[1].z, R1, c[7];
DP3 result.texcoord[1].y, R1, c[6];
DP3 result.texcoord[1].x, R1, c[5];
ADD result.texcoord[2].xyz, -R0, c[13];
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[16].xyxy, c[16];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[15], c[15].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 19 instructions, 2 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "SPOT" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_LightMatrix0]
Vector 12 [_WorldSpaceLightPos0]
Vector 13 [unity_Scale]
Vector 14 [_MainTex_ST]
Vector 15 [_BurnMap_ST]
"vs_2_0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r1.xyz, v1, c13.w
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
dp4 r0.w, v0, c7
dp4 oT3.w, r0, c11
dp4 oT3.z, r0, c10
dp4 oT3.y, r0, c9
dp4 oT3.x, r0, c8
dp3 oT1.z, r1, c6
dp3 oT1.y, r1, c5
dp3 oT1.x, r1, c4
add oT2.xyz, -r0, c12
mad oT0.zw, v2.xyxy, c15.xyxy, c15
mad oT0.xy, v2, c14, c14.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
SubProgram "opengl " {
Keywords { "POINT_COOKIE" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Matrix 9 [_LightMatrix0]
Vector 13 [_WorldSpaceLightPos0]
Vector 14 [unity_Scale]
Vector 15 [_MainTex_ST]
Vector 16 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[17] = { program.local[0],
		state.matrix.mvp,
		program.local[5..16] };
TEMP R0;
TEMP R1;
MUL R1.xyz, vertex.normal, c[14].w;
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
DP4 R0.w, vertex.position, c[8];
DP4 result.texcoord[3].z, R0, c[11];
DP4 result.texcoord[3].y, R0, c[10];
DP4 result.texcoord[3].x, R0, c[9];
DP3 result.texcoord[1].z, R1, c[7];
DP3 result.texcoord[1].y, R1, c[6];
DP3 result.texcoord[1].x, R1, c[5];
ADD result.texcoord[2].xyz, -R0, c[13];
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[16].xyxy, c[16];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[15], c[15].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 18 instructions, 2 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_LightMatrix0]
Vector 12 [_WorldSpaceLightPos0]
Vector 13 [unity_Scale]
Vector 14 [_MainTex_ST]
Vector 15 [_BurnMap_ST]
"vs_2_0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r1.xyz, v1, c13.w
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
dp4 r0.w, v0, c7
dp4 oT3.z, r0, c10
dp4 oT3.y, r0, c9
dp4 oT3.x, r0, c8
dp3 oT1.z, r1, c6
dp3 oT1.y, r1, c5
dp3 oT1.x, r1, c4
add oT2.xyz, -r0, c12
mad oT0.zw, v2.xyxy, c15.xyxy, c15
mad oT0.xy, v2, c14, c14.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL_COOKIE" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Matrix 9 [_LightMatrix0]
Vector 13 [_WorldSpaceLightPos0]
Vector 14 [unity_Scale]
Vector 15 [_MainTex_ST]
Vector 16 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[17] = { program.local[0],
		state.matrix.mvp,
		program.local[5..16] };
TEMP R0;
TEMP R1;
MUL R1.xyz, vertex.normal, c[14].w;
DP4 R0.w, vertex.position, c[8];
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
DP4 result.texcoord[3].y, R0, c[10];
DP4 result.texcoord[3].x, R0, c[9];
DP3 result.texcoord[1].z, R1, c[7];
DP3 result.texcoord[1].y, R1, c[6];
DP3 result.texcoord[1].x, R1, c[5];
MOV result.texcoord[2].xyz, c[13];
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[16].xyxy, c[16];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[15], c[15].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 17 instructions, 2 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_LightMatrix0]
Vector 12 [_WorldSpaceLightPos0]
Vector 13 [unity_Scale]
Vector 14 [_MainTex_ST]
Vector 15 [_BurnMap_ST]
"vs_2_0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r1.xyz, v1, c13.w
dp4 r0.w, v0, c7
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
dp4 oT3.y, r0, c9
dp4 oT3.x, r0, c8
dp3 oT1.z, r1, c6
dp3 oT1.y, r1, c5
dp3 oT1.x, r1, c4
mov oT2.xyz, c12
mad oT0.zw, v2.xyxy, c15.xyxy, c15
mad oT0.xy, v2, c14, c14.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
}
Program "fp" {
// Platform d3d11 had shader errors
//   Keywords { "POINT" }
//   Keywords { "DIRECTIONAL" }
//   Keywords { "SPOT" }
//   Keywords { "POINT_COOKIE" }
//   Keywords { "DIRECTIONAL_COOKIE" }
// Platform d3d11_9x had shader errors
//   Keywords { "POINT" }
//   Keywords { "DIRECTIONAL" }
//   Keywords { "SPOT" }
//   Keywords { "POINT_COOKIE" }
//   Keywords { "DIRECTIONAL_COOKIE" }
SubProgram "opengl " {
Keywords { "POINT" }
Vector 0 [_LightColor0]
Vector 1 [_BurnColor]
Float 2 [_LineWidth]
Float 3 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightTexture0] 2D 2
"!!ARBfp1.0
PARAM c[5] = { program.local[0..3],
		{ 0, 0.99000001, 2 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEX R1.xyz, fragment.texcoord[0], texture[0], 2D;
DP3 R0.x, fragment.texcoord[3], fragment.texcoord[3];
MOV R0.y, c[2].x;
ADD R0.y, R0, c[3].x;
MOV result.color.w, c[4].x;
TEX R0.w, R0.x, texture[2], 2D;
TEX R0.x, fragment.texcoord[0].zwzw, texture[1], 2D;
ADD R0.y, R0.x, -R0;
ADD R0.z, R0.y, c[4].y;
ABS R0.z, R0;
ADD R1.w, R0.x, -c[3].x;
FLR R0.z, R0;
SLT R0.y, R0, -c[4];
CMP R0.y, -R0, -R0.z, R0.z;
MAX R0.y, R0, c[4].x;
MAD R2.xyz, R0.y, -c[1], c[1];
ADD R0.xyz, R2, -R1;
ADD R2.x, R1.w, c[4].y;
ABS R2.y, R2.x;
DP3 R2.z, fragment.texcoord[2], fragment.texcoord[2];
FLR R2.y, R2;
SLT R1.w, R1, -c[4].y;
CMP R1.w, -R1, -R2.y, R2.y;
RSQ R2.x, R2.z;
MAX R1.w, R1, c[4].x;
MAD R0.xyz, R1.w, R0, R1;
MUL R2.xyz, R2.x, fragment.texcoord[2];
DP3 R1.x, fragment.texcoord[1], R2;
MAX R1.x, R1, c[4];
MUL R0.xyz, R0, c[0];
MUL R0.w, R1.x, R0;
MUL R0.xyz, R0.w, R0;
MUL result.color.xyz, R0, c[4].z;
END
# 33 instructions, 3 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "POINT" }
Vector 0 [_LightColor0]
Vector 1 [_BurnColor]
Float 2 [_LineWidth]
Float 3 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightTexture0] 2D 2
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
def c4, 0.99000001, 0.00000000, 2.00000000, 0
dcl t0
dcl t1.xyz
dcl t2.xyz
dcl t3.xyz
texld r4, t0, s0
dp3 r0.x, t3, t3
mov r1.xy, r0.x
mov r0.y, t0.w
mov r0.x, t0.z
texld r6, r1, s2
texld r0, r0, s1
mov r1.x, c3
add r1.x, c2, r1
add r1.x, r0, -r1
add r1.x, r1, c4
abs r3.x, r1
frc r5.x, r3
add r5.x, r3, -r5
add r0.x, r0, -c3
add r0.x, r0, c4
abs r2.x, r0
frc r3.x, r2
cmp r5.x, r1, r5, -r5
add r1.x, r2, -r3
cmp r1.x, r0, r1, -r1
max r2.x, r5, c4.y
mad r2.xyz, r2.x, -c1, c1
dp3_pp r0.x, t2, t2
rsq_pp r0.x, r0.x
mul_pp r0.xyz, r0.x, t2
dp3_pp r0.x, t1, r0
add_pp r2.xyz, r2, -r4
max r1.x, r1, c4.y
mad_pp r1.xyz, r1.x, r2, r4
mul_pp r1.xyz, r1, c0
max_pp r0.x, r0, c4.y
mul_pp r0.x, r0, r6
mul_pp r0.xyz, r0.x, r1
mul_pp r0.xyz, r0, c4.z
mov_pp r0.w, c4.y
mov_pp oC0, r0
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" }
Vector 0 [_LightColor0]
Vector 1 [_BurnColor]
Float 2 [_LineWidth]
Float 3 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
"!!ARBfp1.0
PARAM c[5] = { program.local[0..3],
		{ 0, 0.99000001, 2 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEX R1.x, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R0.xyz, fragment.texcoord[0], texture[0], 2D;
MOV R0.w, c[2].x;
ADD R0.w, R0, c[3].x;
ADD R0.w, R1.x, -R0;
ADD R1.y, R0.w, c[4];
ABS R1.y, R1;
SLT R0.w, R0, -c[4].y;
FLR R1.y, R1;
CMP R1.y, -R0.w, -R1, R1;
ADD R0.w, R1.x, -c[3].x;
ADD R1.w, R0, c[4].y;
MAX R1.x, R1.y, c[4];
MAD R1.xyz, R1.x, -c[1], c[1];
ABS R1.w, R1;
ADD R1.xyz, R1, -R0;
FLR R1.w, R1;
SLT R0.w, R0, -c[4].y;
CMP R0.w, -R0, -R1, R1;
MAX R0.w, R0, c[4].x;
MAD R0.xyz, R0.w, R1, R0;
MOV R2.xyz, fragment.texcoord[2];
DP3 R0.w, fragment.texcoord[1], R2;
MUL R0.xyz, R0, c[0];
MAX R0.w, R0, c[4].x;
MUL R0.xyz, R0.w, R0;
MUL result.color.xyz, R0, c[4].z;
MOV result.color.w, c[4].x;
END
# 28 instructions, 3 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" }
Vector 0 [_LightColor0]
Vector 1 [_BurnColor]
Float 2 [_LineWidth]
Float 3 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
"ps_2_0
dcl_2d s0
dcl_2d s1
def c4, 0.99000001, 0.00000000, 2.00000000, 0
dcl t0
dcl t1.xyz
dcl t2.xyz
texld r6, t0, s0
mov r1.x, c3
mov r0.y, t0.w
mov r0.x, t0.z
add r1.x, c2, r1
texld r0, r0, s1
add r1.x, r0, -r1
add r1.x, r1, c4
abs r4.x, r1
add r0.x, r0, -c3
add r0.x, r0, c4
abs r2.x, r0
frc r5.x, r4
frc r3.x, r2
add r2.x, r2, -r3
cmp r0.x, r0, r2, -r2
add r4.x, r4, -r5
cmp r1.x, r1, r4, -r4
max r1.x, r1, c4.y
mad r1.xyz, r1.x, -c1, c1
max r0.x, r0, c4.y
add_pp r1.xyz, r1, -r6
mad_pp r1.xyz, r0.x, r1, r6
mov_pp r2.xyz, t2
dp3_pp r0.x, t1, r2
mul_pp r1.xyz, r1, c0
max_pp r0.x, r0, c4.y
mul_pp r0.xyz, r0.x, r1
mul_pp r0.xyz, r0, c4.z
mov_pp r0.w, c4.y
mov_pp oC0, r0
"
}
SubProgram "opengl " {
Keywords { "SPOT" }
Vector 0 [_LightColor0]
Vector 1 [_BurnColor]
Float 2 [_LineWidth]
Float 3 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightTexture0] 2D 2
SetTexture 3 [_LightTextureB0] 2D 3
"!!ARBfp1.0
PARAM c[5] = { program.local[0..3],
		{ 0, 0.99000001, 0.5, 2 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEX R1.xyz, fragment.texcoord[0], texture[0], 2D;
DP3 R0.z, fragment.texcoord[3], fragment.texcoord[3];
RCP R0.x, fragment.texcoord[3].w;
MAD R0.xy, fragment.texcoord[3], R0.x, c[4].z;
DP3 R2.z, fragment.texcoord[2], fragment.texcoord[2];
MOV result.color.w, c[4].x;
TEX R0.w, R0, texture[2], 2D;
TEX R0.x, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R1.w, R0.z, texture[3], 2D;
ADD R2.x, R0, -c[3];
ADD R2.y, R2.x, c[4];
MOV R0.y, c[2].x;
ADD R0.y, R0, c[3].x;
ADD R0.y, R0.x, -R0;
ADD R0.z, R0.y, c[4].y;
ABS R0.z, R0;
ABS R2.y, R2;
FLR R0.z, R0;
SLT R0.y, R0, -c[4];
CMP R0.y, -R0, -R0.z, R0.z;
MAX R0.x, R0.y, c[4];
MAD R0.xyz, R0.x, -c[1], c[1];
ADD R0.xyz, R0, -R1;
FLR R2.y, R2;
SLT R2.x, R2, -c[4].y;
CMP R2.x, -R2, -R2.y, R2.y;
MAX R2.y, R2.x, c[4].x;
MAD R0.xyz, R2.y, R0, R1;
RSQ R2.x, R2.z;
MUL R1.xyz, R2.x, fragment.texcoord[2];
DP3 R1.x, fragment.texcoord[1], R1;
SLT R1.y, c[4].x, fragment.texcoord[3].z;
MUL R0.w, R1.y, R0;
MUL R1.y, R0.w, R1.w;
MAX R0.w, R1.x, c[4].x;
MUL R0.xyz, R0, c[0];
MUL R0.w, R0, R1.y;
MUL R0.xyz, R0.w, R0;
MUL result.color.xyz, R0, c[4].w;
END
# 39 instructions, 3 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "SPOT" }
Vector 0 [_LightColor0]
Vector 1 [_BurnColor]
Float 2 [_LineWidth]
Float 3 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightTexture0] 2D 2
SetTexture 3 [_LightTextureB0] 2D 3
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
def c4, 0.99000001, 0.00000000, 0.50000000, 1.00000000
def c5, 2.00000000, 0, 0, 0
dcl t0
dcl t1.xyz
dcl t2.xyz
dcl t3
texld r6, t0, s0
dp3 r2.x, t3, t3
mov r2.xy, r2.x
mov r0.y, t0.w
mov r0.x, t0.z
mov r1.xy, r0
rcp r0.x, t3.w
mad r0.xy, t3, r0.x, c4.z
texld r7, r2, s3
texld r0, r0, s2
texld r2, r1, s1
mov r0.x, c3
add r0.x, c2, r0
add r1.x, r2, -r0
add r0.x, r2, -c3
add r1.x, r1, c4
abs r4.x, r1
add r0.x, r0, c4
abs r2.x, r0
frc r5.x, r4
frc r3.x, r2
add r2.x, r2, -r3
cmp r0.x, r0, r2, -r2
add r4.x, r4, -r5
cmp r1.x, r1, r4, -r4
max r1.x, r1, c4.y
mad r1.xyz, r1.x, -c1, c1
add_pp r1.xyz, r1, -r6
max r0.x, r0, c4.y
mad_pp r0.xyz, r0.x, r1, r6
mul_pp r2.xyz, r0, c0
dp3_pp r0.x, t2, t2
rsq_pp r1.x, r0.x
cmp r0.x, -t3.z, c4.y, c4.w
mul_pp r0.x, r0, r0.w
mul_pp r1.xyz, r1.x, t2
dp3_pp r1.x, t1, r1
mul_pp r0.x, r0, r7
max_pp r1.x, r1, c4.y
mul_pp r0.x, r1, r0
mul_pp r0.xyz, r0.x, r2
mul_pp r0.xyz, r0, c5.x
mov_pp r0.w, c4.y
mov_pp oC0, r0
"
}
SubProgram "opengl " {
Keywords { "POINT_COOKIE" }
Vector 0 [_LightColor0]
Vector 1 [_BurnColor]
Float 2 [_LineWidth]
Float 3 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightTextureB0] 2D 2
SetTexture 3 [_LightTexture0] CUBE 3
"!!ARBfp1.0
PARAM c[5] = { program.local[0..3],
		{ 0, 0.99000001, 2 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEX R1.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R1.w, fragment.texcoord[3], texture[3], CUBE;
DP3 R0.x, fragment.texcoord[3], fragment.texcoord[3];
MOV R0.y, c[2].x;
ADD R0.y, R0, c[3].x;
DP3 R2.z, fragment.texcoord[2], fragment.texcoord[2];
MOV result.color.w, c[4].x;
TEX R0.w, R0.x, texture[2], 2D;
TEX R0.x, fragment.texcoord[0].zwzw, texture[1], 2D;
ADD R0.y, R0.x, -R0;
ADD R0.z, R0.y, c[4].y;
ADD R2.x, R0, -c[3];
ADD R2.y, R2.x, c[4];
ABS R0.z, R0;
ABS R2.y, R2;
FLR R0.z, R0;
SLT R0.y, R0, -c[4];
CMP R0.y, -R0, -R0.z, R0.z;
MAX R0.x, R0.y, c[4];
MAD R0.xyz, R0.x, -c[1], c[1];
ADD R0.xyz, R0, -R1;
FLR R2.y, R2;
SLT R2.x, R2, -c[4].y;
CMP R2.x, -R2, -R2.y, R2.y;
MAX R2.y, R2.x, c[4].x;
MAD R0.xyz, R2.y, R0, R1;
RSQ R2.x, R2.z;
MUL R1.xyz, R2.x, fragment.texcoord[2];
DP3 R1.x, fragment.texcoord[1], R1;
MUL R1.y, R0.w, R1.w;
MAX R0.w, R1.x, c[4].x;
MUL R0.xyz, R0, c[0];
MUL R0.w, R0, R1.y;
MUL R0.xyz, R0.w, R0;
MUL result.color.xyz, R0, c[4].z;
END
# 35 instructions, 3 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" }
Vector 0 [_LightColor0]
Vector 1 [_BurnColor]
Float 2 [_LineWidth]
Float 3 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightTextureB0] 2D 2
SetTexture 3 [_LightTexture0] CUBE 3
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_cube s3
def c4, 0.99000001, 0.00000000, 2.00000000, 0
dcl t0
dcl t1.xyz
dcl t2.xyz
dcl t3.xyz
texld r6, t0, s0
dp3 r0.x, t3, t3
mov r2.xy, r0.x
mov r1.y, t0.w
mov r1.x, t0.z
texld r7, r2, s2
texld r0, t3, s3
texld r2, r1, s1
mov r0.x, c3
add r0.x, c2, r0
add r1.x, r2, -r0
add r0.x, r2, -c3
add r1.x, r1, c4
abs r4.x, r1
add r0.x, r0, c4
abs r2.x, r0
frc r5.x, r4
frc r3.x, r2
add r2.x, r2, -r3
cmp r0.x, r0, r2, -r2
add r4.x, r4, -r5
cmp r1.x, r1, r4, -r4
max r1.x, r1, c4.y
mad r1.xyz, r1.x, -c1, c1
max r0.x, r0, c4.y
add_pp r1.xyz, r1, -r6
mad_pp r1.xyz, r0.x, r1, r6
dp3_pp r0.x, t2, t2
rsq_pp r0.x, r0.x
mul_pp r0.xyz, r0.x, t2
dp3_pp r0.x, t1, r0
mul r2.x, r7, r0.w
max_pp r0.x, r0, c4.y
mul_pp r1.xyz, r1, c0
mul_pp r0.x, r0, r2
mul_pp r0.xyz, r0.x, r1
mul_pp r0.xyz, r0, c4.z
mov_pp r0.w, c4.y
mov_pp oC0, r0
"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL_COOKIE" }
Vector 0 [_LightColor0]
Vector 1 [_BurnColor]
Float 2 [_LineWidth]
Float 3 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightTexture0] 2D 2
"!!ARBfp1.0
PARAM c[5] = { program.local[0..3],
		{ 0, 0.99000001, 2 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEX R0.x, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R1.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R0.w, fragment.texcoord[3], texture[2], 2D;
ADD R1.w, R0.x, -c[3].x;
ADD R2.x, R1.w, c[4].y;
MOV R0.y, c[2].x;
ADD R0.y, R0, c[3].x;
ADD R0.y, R0.x, -R0;
ADD R0.z, R0.y, c[4].y;
ABS R0.z, R0;
ABS R2.x, R2;
FLR R0.z, R0;
SLT R0.y, R0, -c[4];
CMP R0.y, -R0, -R0.z, R0.z;
MAX R0.x, R0.y, c[4];
MAD R0.xyz, R0.x, -c[1], c[1];
ADD R0.xyz, R0, -R1;
FLR R2.x, R2;
SLT R1.w, R1, -c[4].y;
CMP R1.w, -R1, -R2.x, R2.x;
MAX R1.w, R1, c[4].x;
MAD R0.xyz, R1.w, R0, R1;
MOV R2.xyz, fragment.texcoord[2];
DP3 R1.x, fragment.texcoord[1], R2;
MAX R1.x, R1, c[4];
MUL R0.xyz, R0, c[0];
MUL R0.w, R1.x, R0;
MUL R0.xyz, R0.w, R0;
MUL result.color.xyz, R0, c[4].z;
MOV result.color.w, c[4].x;
END
# 30 instructions, 3 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" }
Vector 0 [_LightColor0]
Vector 1 [_BurnColor]
Float 2 [_LineWidth]
Float 3 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightTexture0] 2D 2
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
def c4, 0.99000001, 0.00000000, 2.00000000, 0
dcl t0
dcl t1.xyz
dcl t2.xyz
dcl t3.xy
texld r6, t0, s0
mov r0.y, t0.w
mov r0.x, t0.z
mov r1.xy, r0
texld r0, t3, s2
texld r2, r1, s1
mov r0.x, c3
add r0.x, c2, r0
add r1.x, r2, -r0
add r0.x, r2, -c3
add r1.x, r1, c4
abs r4.x, r1
add r0.x, r0, c4
abs r2.x, r0
frc r5.x, r4
frc r3.x, r2
add r4.x, r4, -r5
add r2.x, r2, -r3
cmp r1.x, r1, r4, -r4
cmp r0.x, r0, r2, -r2
max r1.x, r1, c4.y
mad r1.xyz, r1.x, -c1, c1
max r0.x, r0, c4.y
add_pp r1.xyz, r1, -r6
mad_pp r1.xyz, r0.x, r1, r6
mov_pp r0.xyz, t2
dp3_pp r0.x, t1, r0
max_pp r0.x, r0, c4.y
mul_pp r0.x, r0, r0.w
mul_pp r1.xyz, r1, c0
mul_pp r0.xyz, r0.x, r1
mul_pp r0.xyz, r0, c4.z
mov_pp r0.w, c4.y
mov_pp oC0, r0
"
}
}
 }
 Pass {
  Name "PREPASS"
  Tags { "LIGHTMODE"="PrePassBase" "QUEUE"="Transparent" }
  Fog { Mode Off }
  Blend SrcAlpha OneMinusSrcAlpha
Program "vp" {
// Platform d3d11 skipped due to earlier errors
// Platform d3d11_9x skipped due to earlier errors
// Platform d3d11 had shader errors
//   <no keywords>
// Platform d3d11_9x had shader errors
//   <no keywords>
SubProgram "opengl " {
Bind "vertex" Vertex
Bind "normal" Normal
Matrix 5 [_Object2World]
Vector 9 [unity_Scale]
"!!ARBvp1.0
PARAM c[10] = { program.local[0],
		state.matrix.mvp,
		program.local[5..9] };
TEMP R0;
MUL R0.xyz, vertex.normal, c[9].w;
DP3 result.texcoord[0].z, R0, c[7];
DP3 result.texcoord[0].y, R0, c[6];
DP3 result.texcoord[0].x, R0, c[5];
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 8 instructions, 1 R-regs
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "normal" Normal
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Vector 8 [unity_Scale]
"vs_2_0
dcl_position0 v0
dcl_normal0 v1
mul r0.xyz, v1, c8.w
dp3 oT0.z, r0, c6
dp3 oT0.y, r0, c5
dp3 oT0.x, r0, c4
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
}
Program "fp" {
// Platform d3d11 skipped due to earlier errors
// Platform d3d11_9x skipped due to earlier errors
// Platform d3d11 had shader errors
//   <no keywords>
// Platform d3d11_9x had shader errors
//   <no keywords>
SubProgram "opengl " {
"!!ARBfp1.0
PARAM c[1] = { { 0, 0.5 } };
MAD result.color.xyz, fragment.texcoord[0], c[0].y, c[0].y;
MOV result.color.w, c[0].x;
END
# 2 instructions, 0 R-regs
"
}
SubProgram "d3d9 " {
"ps_2_0
def c0, 0.50000000, 0.00000000, 0, 0
dcl t0.xyz
mad_pp r0.xyz, t0, c0.x, c0.x
mov_pp r0.w, c0.y
mov_pp oC0, r0
"
}
}
 }
 Pass {
  Name "PREPASS"
  Tags { "LIGHTMODE"="PrePassFinal" "QUEUE"="Transparent" }
  ZWrite Off
  Blend SrcAlpha OneMinusSrcAlpha
Program "vp" {
// Platform d3d11 had shader errors
//   Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_OFF" }
//   Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_ON" }
// Platform d3d11_9x had shader errors
//   Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_OFF" }
//   Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_ON" }
SubProgram "opengl " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Vector 9 [_ProjectionParams]
Vector 10 [unity_SHAr]
Vector 11 [unity_SHAg]
Vector 12 [unity_SHAb]
Vector 13 [unity_SHBr]
Vector 14 [unity_SHBg]
Vector 15 [unity_SHBb]
Vector 16 [unity_SHC]
Vector 17 [unity_Scale]
Vector 18 [_MainTex_ST]
Vector 19 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[20] = { { 0.5, 1 },
		state.matrix.mvp,
		program.local[5..19] };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
MUL R1.xyz, vertex.normal, c[17].w;
DP3 R2.w, R1, c[6];
DP3 R0.x, R1, c[5];
DP3 R0.z, R1, c[7];
MOV R0.y, R2.w;
MUL R1, R0.xyzz, R0.yzzx;
MOV R0.w, c[0].y;
DP4 R2.z, R0, c[12];
DP4 R2.y, R0, c[11];
DP4 R2.x, R0, c[10];
MUL R0.y, R2.w, R2.w;
DP4 R3.z, R1, c[15];
DP4 R3.y, R1, c[14];
DP4 R3.x, R1, c[13];
DP4 R1.w, vertex.position, c[4];
DP4 R1.z, vertex.position, c[3];
MAD R0.x, R0, R0, -R0.y;
ADD R3.xyz, R2, R3;
MUL R2.xyz, R0.x, c[16];
DP4 R1.x, vertex.position, c[1];
DP4 R1.y, vertex.position, c[2];
MUL R0.xyz, R1.xyww, c[0].x;
MUL R0.y, R0, c[9].x;
ADD result.texcoord[2].xyz, R3, R2;
ADD result.texcoord[1].xy, R0, R0.z;
MOV result.position, R1;
MOV result.texcoord[1].zw, R1;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[19].xyxy, c[19];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[18], c[18].zwzw;
END
# 29 instructions, 4 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Vector 8 [_ProjectionParams]
Vector 9 [_ScreenParams]
Vector 10 [unity_SHAr]
Vector 11 [unity_SHAg]
Vector 12 [unity_SHAb]
Vector 13 [unity_SHBr]
Vector 14 [unity_SHBg]
Vector 15 [unity_SHBb]
Vector 16 [unity_SHC]
Vector 17 [unity_Scale]
Vector 18 [_MainTex_ST]
Vector 19 [_BurnMap_ST]
"vs_2_0
def c20, 0.50000000, 1.00000000, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r1.xyz, v1, c17.w
dp3 r2.w, r1, c5
dp3 r0.x, r1, c4
dp3 r0.z, r1, c6
mov r0.y, r2.w
mul r1, r0.xyzz, r0.yzzx
mov r0.w, c20.y
dp4 r2.z, r0, c12
dp4 r2.y, r0, c11
dp4 r2.x, r0, c10
mul r0.y, r2.w, r2.w
dp4 r3.z, r1, c15
dp4 r3.y, r1, c14
dp4 r3.x, r1, c13
dp4 r1.w, v0, c3
dp4 r1.z, v0, c2
mad r0.x, r0, r0, -r0.y
add r3.xyz, r2, r3
mul r2.xyz, r0.x, c16
dp4 r1.x, v0, c0
dp4 r1.y, v0, c1
mul r0.xyz, r1.xyww, c20.x
mul r0.y, r0, c8.x
add oT2.xyz, r3, r2
mad oT1.xy, r0.z, c9.zwzw, r0
mov oPos, r1
mov oT1.zw, r1
mad oT0.zw, v2.xyxy, c19.xyxy, c19
mad oT0.xy, v2, c18, c18.zwzw
"
}
SubProgram "opengl " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 9 [_Object2World]
Vector 13 [_ProjectionParams]
Vector 14 [unity_ShadowFadeCenterAndType]
Vector 15 [unity_LightmapST]
Vector 16 [_MainTex_ST]
Vector 17 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[18] = { { 0.5, 1 },
		state.matrix.modelview[0],
		state.matrix.mvp,
		program.local[9..17] };
TEMP R0;
TEMP R1;
DP4 R0.w, vertex.position, c[8];
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
MUL R1.xyz, R0.xyww, c[0].x;
MUL R1.y, R1, c[13].x;
ADD result.texcoord[1].xy, R1, R1.z;
MOV result.position, R0;
MOV R0.x, c[0].y;
ADD R0.y, R0.x, -c[14].w;
DP4 R0.x, vertex.position, c[3];
DP4 R1.z, vertex.position, c[11];
DP4 R1.x, vertex.position, c[9];
DP4 R1.y, vertex.position, c[10];
ADD R1.xyz, R1, -c[14];
MOV result.texcoord[1].zw, R0;
MUL result.texcoord[3].xyz, R1, c[14].w;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[17].xyxy, c[17];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[16], c[16].zwzw;
MAD result.texcoord[2].xy, vertex.texcoord[1], c[15], c[15].zwzw;
MUL result.texcoord[3].w, -R0.x, R0.y;
END
# 21 instructions, 2 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 0 [glstate_matrix_modelview0]
Matrix 4 [glstate_matrix_mvp]
Matrix 8 [_Object2World]
Vector 12 [_ProjectionParams]
Vector 13 [_ScreenParams]
Vector 14 [unity_ShadowFadeCenterAndType]
Vector 15 [unity_LightmapST]
Vector 16 [_MainTex_ST]
Vector 17 [_BurnMap_ST]
"vs_2_0
def c18, 0.50000000, 1.00000000, 0, 0
dcl_position0 v0
dcl_texcoord0 v1
dcl_texcoord1 v2
dp4 r0.w, v0, c7
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
mul r1.xyz, r0.xyww, c18.x
mul r1.y, r1, c12.x
mad oT1.xy, r1.z, c13.zwzw, r1
mov oPos, r0
mov r0.x, c14.w
add r0.y, c18, -r0.x
dp4 r0.x, v0, c2
dp4 r1.z, v0, c10
dp4 r1.x, v0, c8
dp4 r1.y, v0, c9
add r1.xyz, r1, -c14
mov oT1.zw, r0
mul oT3.xyz, r1, c14.w
mad oT0.zw, v1.xyxy, c17.xyxy, c17
mad oT0.xy, v1, c16, c16.zwzw
mad oT2.xy, v2, c15, c15.zwzw
mul oT3.w, -r0.x, r0.y
"
}
SubProgram "opengl " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_OFF" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Vector 5 [_ProjectionParams]
Vector 6 [unity_LightmapST]
Vector 7 [_MainTex_ST]
Vector 8 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[9] = { { 0.5 },
		state.matrix.mvp,
		program.local[5..8] };
TEMP R0;
TEMP R1;
DP4 R0.w, vertex.position, c[4];
DP4 R0.z, vertex.position, c[3];
DP4 R0.x, vertex.position, c[1];
DP4 R0.y, vertex.position, c[2];
MUL R1.xyz, R0.xyww, c[0].x;
MUL R1.y, R1, c[5].x;
ADD result.texcoord[1].xy, R1, R1.z;
MOV result.position, R0;
MOV result.texcoord[1].zw, R0;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[8].xyxy, c[8];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[7], c[7].zwzw;
MAD result.texcoord[2].xy, vertex.texcoord[1], c[6], c[6].zwzw;
END
# 12 instructions, 2 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_OFF" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 0 [glstate_matrix_mvp]
Vector 4 [_ProjectionParams]
Vector 5 [_ScreenParams]
Vector 6 [unity_LightmapST]
Vector 7 [_MainTex_ST]
Vector 8 [_BurnMap_ST]
"vs_2_0
def c9, 0.50000000, 0, 0, 0
dcl_position0 v0
dcl_texcoord0 v1
dcl_texcoord1 v2
dp4 r0.w, v0, c3
dp4 r0.z, v0, c2
dp4 r0.x, v0, c0
dp4 r0.y, v0, c1
mul r1.xyz, r0.xyww, c9.x
mul r1.y, r1, c4.x
mad oT1.xy, r1.z, c5.zwzw, r1
mov oPos, r0
mov oT1.zw, r0
mad oT0.zw, v1.xyxy, c8.xyxy, c8
mad oT0.xy, v1, c7, c7.zwzw
mad oT2.xy, v2, c6, c6.zwzw
"
}
SubProgram "opengl " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Vector 9 [_ProjectionParams]
Vector 10 [unity_SHAr]
Vector 11 [unity_SHAg]
Vector 12 [unity_SHAb]
Vector 13 [unity_SHBr]
Vector 14 [unity_SHBg]
Vector 15 [unity_SHBb]
Vector 16 [unity_SHC]
Vector 17 [unity_Scale]
Vector 18 [_MainTex_ST]
Vector 19 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[20] = { { 0.5, 1 },
		state.matrix.mvp,
		program.local[5..19] };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
MUL R1.xyz, vertex.normal, c[17].w;
DP3 R2.w, R1, c[6];
DP3 R0.x, R1, c[5];
DP3 R0.z, R1, c[7];
MOV R0.y, R2.w;
MUL R1, R0.xyzz, R0.yzzx;
MOV R0.w, c[0].y;
DP4 R2.z, R0, c[12];
DP4 R2.y, R0, c[11];
DP4 R2.x, R0, c[10];
MUL R0.y, R2.w, R2.w;
DP4 R3.z, R1, c[15];
DP4 R3.y, R1, c[14];
DP4 R3.x, R1, c[13];
DP4 R1.w, vertex.position, c[4];
DP4 R1.z, vertex.position, c[3];
MAD R0.x, R0, R0, -R0.y;
ADD R3.xyz, R2, R3;
MUL R2.xyz, R0.x, c[16];
DP4 R1.x, vertex.position, c[1];
DP4 R1.y, vertex.position, c[2];
MUL R0.xyz, R1.xyww, c[0].x;
MUL R0.y, R0, c[9].x;
ADD result.texcoord[2].xyz, R3, R2;
ADD result.texcoord[1].xy, R0, R0.z;
MOV result.position, R1;
MOV result.texcoord[1].zw, R1;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[19].xyxy, c[19];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[18], c[18].zwzw;
END
# 29 instructions, 4 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Vector 8 [_ProjectionParams]
Vector 9 [_ScreenParams]
Vector 10 [unity_SHAr]
Vector 11 [unity_SHAg]
Vector 12 [unity_SHAb]
Vector 13 [unity_SHBr]
Vector 14 [unity_SHBg]
Vector 15 [unity_SHBb]
Vector 16 [unity_SHC]
Vector 17 [unity_Scale]
Vector 18 [_MainTex_ST]
Vector 19 [_BurnMap_ST]
"vs_2_0
def c20, 0.50000000, 1.00000000, 0, 0
dcl_position0 v0
dcl_normal0 v1
dcl_texcoord0 v2
mul r1.xyz, v1, c17.w
dp3 r2.w, r1, c5
dp3 r0.x, r1, c4
dp3 r0.z, r1, c6
mov r0.y, r2.w
mul r1, r0.xyzz, r0.yzzx
mov r0.w, c20.y
dp4 r2.z, r0, c12
dp4 r2.y, r0, c11
dp4 r2.x, r0, c10
mul r0.y, r2.w, r2.w
dp4 r3.z, r1, c15
dp4 r3.y, r1, c14
dp4 r3.x, r1, c13
dp4 r1.w, v0, c3
dp4 r1.z, v0, c2
mad r0.x, r0, r0, -r0.y
add r3.xyz, r2, r3
mul r2.xyz, r0.x, c16
dp4 r1.x, v0, c0
dp4 r1.y, v0, c1
mul r0.xyz, r1.xyww, c20.x
mul r0.y, r0, c8.x
add oT2.xyz, r3, r2
mad oT1.xy, r0.z, c9.zwzw, r0
mov oPos, r1
mov oT1.zw, r1
mad oT0.zw, v2.xyxy, c19.xyxy, c19
mad oT0.xy, v2, c18, c18.zwzw
"
}
SubProgram "opengl " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 9 [_Object2World]
Vector 13 [_ProjectionParams]
Vector 14 [unity_ShadowFadeCenterAndType]
Vector 15 [unity_LightmapST]
Vector 16 [_MainTex_ST]
Vector 17 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[18] = { { 0.5, 1 },
		state.matrix.modelview[0],
		state.matrix.mvp,
		program.local[9..17] };
TEMP R0;
TEMP R1;
DP4 R0.w, vertex.position, c[8];
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
MUL R1.xyz, R0.xyww, c[0].x;
MUL R1.y, R1, c[13].x;
ADD result.texcoord[1].xy, R1, R1.z;
MOV result.position, R0;
MOV R0.x, c[0].y;
ADD R0.y, R0.x, -c[14].w;
DP4 R0.x, vertex.position, c[3];
DP4 R1.z, vertex.position, c[11];
DP4 R1.x, vertex.position, c[9];
DP4 R1.y, vertex.position, c[10];
ADD R1.xyz, R1, -c[14];
MOV result.texcoord[1].zw, R0;
MUL result.texcoord[3].xyz, R1, c[14].w;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[17].xyxy, c[17];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[16], c[16].zwzw;
MAD result.texcoord[2].xy, vertex.texcoord[1], c[15], c[15].zwzw;
MUL result.texcoord[3].w, -R0.x, R0.y;
END
# 21 instructions, 2 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 0 [glstate_matrix_modelview0]
Matrix 4 [glstate_matrix_mvp]
Matrix 8 [_Object2World]
Vector 12 [_ProjectionParams]
Vector 13 [_ScreenParams]
Vector 14 [unity_ShadowFadeCenterAndType]
Vector 15 [unity_LightmapST]
Vector 16 [_MainTex_ST]
Vector 17 [_BurnMap_ST]
"vs_2_0
def c18, 0.50000000, 1.00000000, 0, 0
dcl_position0 v0
dcl_texcoord0 v1
dcl_texcoord1 v2
dp4 r0.w, v0, c7
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
mul r1.xyz, r0.xyww, c18.x
mul r1.y, r1, c12.x
mad oT1.xy, r1.z, c13.zwzw, r1
mov oPos, r0
mov r0.x, c14.w
add r0.y, c18, -r0.x
dp4 r0.x, v0, c2
dp4 r1.z, v0, c10
dp4 r1.x, v0, c8
dp4 r1.y, v0, c9
add r1.xyz, r1, -c14
mov oT1.zw, r0
mul oT3.xyz, r1, c14.w
mad oT0.zw, v1.xyxy, c17.xyxy, c17
mad oT0.xy, v1, c16, c16.zwzw
mad oT2.xy, v2, c15, c15.zwzw
mul oT3.w, -r0.x, r0.y
"
}
SubProgram "opengl " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_ON" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Vector 5 [_ProjectionParams]
Vector 6 [unity_LightmapST]
Vector 7 [_MainTex_ST]
Vector 8 [_BurnMap_ST]
"!!ARBvp1.0
PARAM c[9] = { { 0.5 },
		state.matrix.mvp,
		program.local[5..8] };
TEMP R0;
TEMP R1;
DP4 R0.w, vertex.position, c[4];
DP4 R0.z, vertex.position, c[3];
DP4 R0.x, vertex.position, c[1];
DP4 R0.y, vertex.position, c[2];
MUL R1.xyz, R0.xyww, c[0].x;
MUL R1.y, R1, c[5].x;
ADD result.texcoord[1].xy, R1, R1.z;
MOV result.position, R0;
MOV result.texcoord[1].zw, R0;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[8].xyxy, c[8];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[7], c[7].zwzw;
MAD result.texcoord[2].xy, vertex.texcoord[1], c[6], c[6].zwzw;
END
# 12 instructions, 2 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_ON" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 0 [glstate_matrix_mvp]
Vector 4 [_ProjectionParams]
Vector 5 [_ScreenParams]
Vector 6 [unity_LightmapST]
Vector 7 [_MainTex_ST]
Vector 8 [_BurnMap_ST]
"vs_2_0
def c9, 0.50000000, 0, 0, 0
dcl_position0 v0
dcl_texcoord0 v1
dcl_texcoord1 v2
dp4 r0.w, v0, c3
dp4 r0.z, v0, c2
dp4 r0.x, v0, c0
dp4 r0.y, v0, c1
mul r1.xyz, r0.xyww, c9.x
mul r1.y, r1, c4.x
mad oT1.xy, r1.z, c5.zwzw, r1
mov oPos, r0
mov oT1.zw, r0
mad oT0.zw, v1.xyxy, c8.xyxy, c8
mad oT0.xy, v1, c7, c7.zwzw
mad oT2.xy, v2, c6, c6.zwzw
"
}
}
Program "fp" {
// Platform d3d11 had shader errors
//   Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_OFF" }
//   Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_ON" }
// Platform d3d11_9x had shader errors
//   Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_OFF" }
//   Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
//   Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_ON" }
SubProgram "opengl " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightBuffer] 2D 2
"!!ARBfp1.0
PARAM c[4] = { program.local[0..2],
		{ 1, 0.99000001, 0 } };
TEMP R0;
TEMP R1;
TEMP R2;
TXP R0.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R2.x, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R1.xyz, fragment.texcoord[0], texture[0], 2D;
MOV R0.w, c[1].x;
ADD R0.w, R0, c[2].x;
ADD R0.w, R2.x, -R0;
ADD R1.w, R0, c[3].y;
ABS R1.w, R1;
FLR R1.w, R1;
SLT R0.w, R0, -c[3].y;
CMP R0.w, -R0, -R1, R1;
MAX R1.w, R0, c[3].z;
MAD R2.yzw, R1.w, -c[0].xxyz, c[0].xxyz;
ADD R1.w, R2.x, -c[2].x;
ADD R2.x, R1.w, c[3].y;
ABS R2.x, R2;
ADD R2.yzw, R2, -R1.xxyz;
LG2 R0.x, R0.x;
LG2 R0.y, R0.y;
LG2 R0.z, R0.z;
FLR R2.x, R2;
SLT R1.w, R1, -c[3].y;
CMP R1.w, -R1, -R2.x, R2.x;
MAX R1.w, R1, c[3].z;
ADD R0.xyz, -R0, fragment.texcoord[2];
MAD R1.xyz, R1.w, R2.yzww, R1;
MUL result.color.xyz, R1, R0;
ADD result.color.w, -R0, c[3].x;
END
# 28 instructions, 3 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightBuffer] 2D 2
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
def c3, 0.99000001, 0.00000000, 1.00000000, 0
dcl t0
dcl t1
dcl t2.xyz
texldp r4, t1, s2
texld r5, t0, s0
mov r0.x, t0.z
mov r0.y, t0.w
log_pp r2.z, r4.z
log_pp r2.y, r4.y
texld r3, r0, s1
mov r0.x, c2
add r0.x, c1, r0
add r0.x, r3, -r0
add r0.x, r0, c3
abs r1.x, r0
frc r2.x, r1
add r1.x, r1, -r2
cmp r0.x, r0, r1, -r1
add r1.x, r3, -c2
max r2.x, r0, c3.y
mad r6.xyz, r2.x, -c0, c0
add r1.x, r1, c3
abs r2.x, r1
frc r3.x, r2
add r2.x, r2, -r3
cmp r1.x, r1, r2, -r2
log_pp r2.x, r4.x
add_pp r6.xyz, r6, -r5
max r1.x, r1, c3.y
add_pp r2.xyz, -r2, t2
mad_pp r1.xyz, r1.x, r6, r5
mul_pp r1.xyz, r1, r2
add r1.w, -r0.x, c3.z
mov_pp oC0, r1
"
}
SubProgram "opengl " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
Vector 3 [unity_LightmapFade]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightBuffer] 2D 2
SetTexture 3 [unity_Lightmap] 2D 3
SetTexture 4 [unity_LightmapInd] 2D 4
"!!ARBfp1.0
PARAM c[5] = { program.local[0..3],
		{ 1, 0.99000001, 0, 8 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEX R0, fragment.texcoord[2], texture[4], 2D;
TEX R1, fragment.texcoord[2], texture[3], 2D;
TXP R3.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R2.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R4.x, fragment.texcoord[0].zwzw, texture[1], 2D;
MUL R1.xyz, R1.w, R1;
MUL R0.xyz, R0.w, R0;
DP4 R1.w, fragment.texcoord[3], fragment.texcoord[3];
RSQ R0.w, R1.w;
MUL R0.xyz, R0, c[4].w;
RCP R0.w, R0.w;
MAD R1.xyz, R1, c[4].w, -R0;
MAD_SAT R0.w, R0, c[3].z, c[3];
MAD R0.xyz, R0.w, R1, R0;
MOV R0.w, c[1].x;
ADD R0.w, R0, c[2].x;
ADD R0.w, R4.x, -R0;
ADD R1.w, R0, c[4].y;
LG2 R1.x, R3.x;
LG2 R1.y, R3.y;
LG2 R1.z, R3.z;
ADD R0.xyz, -R1, R0;
ABS R1.x, R1.w;
FLR R1.x, R1;
SLT R0.w, R0, -c[4].y;
CMP R0.w, -R0, -R1.x, R1.x;
ADD R1.w, R4.x, -c[2].x;
ADD R1.x, R1.w, c[4].y;
ABS R2.w, R1.x;
MAX R1.y, R0.w, c[4].z;
MAD R1.xyz, R1.y, -c[0], c[0];
FLR R2.w, R2;
SLT R1.w, R1, -c[4].y;
CMP R1.w, -R1, -R2, R2;
ADD R1.xyz, R1, -R2;
MAX R1.w, R1, c[4].z;
MAD R1.xyz, R1.w, R1, R2;
MUL result.color.xyz, R1, R0;
ADD result.color.w, -R0, c[4].x;
END
# 39 instructions, 5 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
Vector 3 [unity_LightmapFade]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightBuffer] 2D 2
SetTexture 3 [unity_Lightmap] 2D 3
SetTexture 4 [unity_LightmapInd] 2D 4
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
def c4, 0.99000001, 0.00000000, 1.00000000, 8.00000000
dcl t0
dcl t1
dcl t2.xy
dcl t3
texldp r1, t1, s2
texld r2, t0, s0
texld r3, t2, s3
mov r0.y, t0.w
mov r0.x, t0.z
log_pp r1.x, r1.x
log_pp r1.y, r1.y
log_pp r1.z, r1.z
mul_pp r3.xyz, r3.w, r3
texld r5, r0, s1
texld r0, t2, s4
mul_pp r4.xyz, r0.w, r0
mul_pp r4.xyz, r4, c4.w
dp4 r0.x, t3, t3
rsq r0.x, r0.x
rcp r0.x, r0.x
mad_pp r3.xyz, r3, c4.w, -r4
mad_sat r0.x, r0, c3.z, c3.w
mad_pp r3.xyz, r0.x, r3, r4
mov r0.x, c2
add_pp r7.xyz, -r1, r3
add r1.x, r5, -c2
add r1.x, r1, c4
abs r3.x, r1
add r0.x, c1, r0
add r0.x, r5, -r0
add r0.x, r0, c4
abs r5.x, r0
frc r6.x, r5
frc r4.x, r3
add r3.x, r3, -r4
cmp r1.x, r1, r3, -r3
add r5.x, r5, -r6
cmp r0.x, r0, r5, -r5
max r3.x, r0, c4.y
mad r3.xyz, r3.x, -c0, c0
add_pp r3.xyz, r3, -r2
max r1.x, r1, c4.y
mad_pp r1.xyz, r1.x, r3, r2
mul_pp r1.xyz, r1, r7
add r1.w, -r0.x, c4.z
mov_pp oC0, r1
"
}
SubProgram "opengl " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_OFF" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightBuffer] 2D 2
SetTexture 3 [unity_Lightmap] 2D 3
"!!ARBfp1.0
PARAM c[4] = { program.local[0..2],
		{ 1, 0.99000001, 0, 8 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TXP R2.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R0, fragment.texcoord[2], texture[3], 2D;
TEX R1.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R3.x, fragment.texcoord[0].zwzw, texture[1], 2D;
MUL R0.xyz, R0.w, R0;
MOV R0.w, c[1].x;
ADD R0.w, R0, c[2].x;
ADD R0.w, R3.x, -R0;
ADD R1.w, R0, c[3].y;
LG2 R2.x, R2.x;
LG2 R2.y, R2.y;
LG2 R2.z, R2.z;
MAD R0.xyz, R0, c[3].w, -R2;
ABS R1.w, R1;
FLR R2.x, R1.w;
SLT R0.w, R0, -c[3].y;
CMP R0.w, -R0, -R2.x, R2.x;
ADD R1.w, R3.x, -c[2].x;
ADD R2.x, R1.w, c[3].y;
ABS R2.w, R2.x;
MAX R2.y, R0.w, c[3].z;
MAD R2.xyz, R2.y, -c[0], c[0];
FLR R2.w, R2;
SLT R1.w, R1, -c[3].y;
CMP R1.w, -R1, -R2, R2;
ADD R2.xyz, R2, -R1;
MAX R1.w, R1, c[3].z;
MAD R1.xyz, R1.w, R2, R1;
MUL result.color.xyz, R1, R0;
ADD result.color.w, -R0, c[3].x;
END
# 30 instructions, 4 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_OFF" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightBuffer] 2D 2
SetTexture 3 [unity_Lightmap] 2D 3
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
def c3, 0.99000001, 0.00000000, 1.00000000, 8.00000000
dcl t0
dcl t1
dcl t2.xy
texldp r1, t1, s2
texld r6, t0, s0
mov r0.y, t0.w
mov r0.x, t0.z
log_pp r1.x, r1.x
log_pp r1.y, r1.y
log_pp r1.z, r1.z
texld r2, r0, s1
texld r0, t2, s3
mul_pp r3.xyz, r0.w, r0
mov r0.x, c2
mad_pp r7.xyz, r3, c3.w, -r1
add r1.x, r2, -c2
add r0.x, c1, r0
add r0.x, r2, -r0
add r0.x, r0, c3
abs r4.x, r0
add r1.x, r1, c3
abs r2.x, r1
frc r5.x, r4
frc r3.x, r2
add r2.x, r2, -r3
cmp r1.x, r1, r2, -r2
add r4.x, r4, -r5
cmp r0.x, r0, r4, -r4
max r2.x, r0, c3.y
mad r2.xyz, r2.x, -c0, c0
add_pp r2.xyz, r2, -r6
max r1.x, r1, c3.y
mad_pp r1.xyz, r1.x, r2, r6
mul_pp r1.xyz, r1, r7
add r1.w, -r0.x, c3.z
mov_pp oC0, r1
"
}
SubProgram "opengl " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightBuffer] 2D 2
"!!ARBfp1.0
PARAM c[4] = { program.local[0..2],
		{ 1, 0.99000001, 0 } };
TEMP R0;
TEMP R1;
TEMP R2;
TXP R1.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R2.x, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R0.xyz, fragment.texcoord[0], texture[0], 2D;
MOV R0.w, c[1].x;
ADD R0.w, R0, c[2].x;
ADD R0.w, R2.x, -R0;
ADD R1.w, R0, c[3].y;
ABS R1.w, R1;
FLR R1.w, R1;
SLT R0.w, R0, -c[3].y;
CMP R0.w, -R0, -R1, R1;
ADD R1.w, R2.x, -c[2].x;
ADD R2.w, R1, c[3].y;
MAX R2.x, R0.w, c[3].z;
MAD R2.xyz, R2.x, -c[0], c[0];
ABS R2.w, R2;
ADD R2.xyz, R2, -R0;
FLR R2.w, R2;
SLT R1.w, R1, -c[3].y;
CMP R1.w, -R1, -R2, R2;
MAX R1.w, R1, c[3].z;
ADD R1.xyz, R1, fragment.texcoord[2];
MAD R0.xyz, R1.w, R2, R0;
MUL result.color.xyz, R0, R1;
ADD result.color.w, -R0, c[3].x;
END
# 25 instructions, 3 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightBuffer] 2D 2
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
def c3, 0.99000001, 0.00000000, 1.00000000, 0
dcl t0
dcl t1
dcl t2.xyz
texldp r4, t1, s2
texld r5, t0, s0
mov r0.x, t0.z
mov r0.y, t0.w
texld r1, r0, s1
mov r0.x, c2
add r0.x, c1, r0
add r0.x, r1, -r0
add r0.x, r0, c3
abs r3.x, r0
frc r6.x, r3
add r6.x, r3, -r6
cmp r0.x, r0, r6, -r6
add r1.x, r1, -c2
add r1.x, r1, c3
abs r2.x, r1
frc r3.x, r2
add r2.x, r2, -r3
cmp r1.x, r1, r2, -r2
max r3.x, r0, c3.y
mad r3.xyz, r3.x, -c0, c0
add_pp r3.xyz, r3, -r5
max r1.x, r1, c3.y
add_pp r2.xyz, r4, t2
mad_pp r1.xyz, r1.x, r3, r5
mul_pp r1.xyz, r1, r2
add r1.w, -r0.x, c3.z
mov_pp oC0, r1
"
}
SubProgram "opengl " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
Vector 3 [unity_LightmapFade]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightBuffer] 2D 2
SetTexture 3 [unity_Lightmap] 2D 3
SetTexture 4 [unity_LightmapInd] 2D 4
"!!ARBfp1.0
PARAM c[5] = { program.local[0..3],
		{ 1, 0.99000001, 0, 8 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEX R1, fragment.texcoord[2], texture[3], 2D;
TEX R0, fragment.texcoord[2], texture[4], 2D;
TEX R2.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R4.x, fragment.texcoord[0].zwzw, texture[1], 2D;
TXP R3.xyz, fragment.texcoord[1], texture[2], 2D;
MUL R0.xyz, R0.w, R0;
MUL R1.xyz, R1.w, R1;
MUL R0.xyz, R0, c[4].w;
DP4 R1.w, fragment.texcoord[3], fragment.texcoord[3];
MOV R0.w, c[1].x;
RSQ R1.w, R1.w;
ADD R0.w, R0, c[2].x;
RCP R1.w, R1.w;
MAD R1.xyz, R1, c[4].w, -R0;
MAD_SAT R1.w, R1, c[3].z, c[3];
MAD R0.xyz, R1.w, R1, R0;
ADD R0.w, R4.x, -R0;
ADD R1.x, R0.w, c[4].y;
ABS R1.x, R1;
FLR R1.x, R1;
SLT R0.w, R0, -c[4].y;
CMP R0.w, -R0, -R1.x, R1.x;
ADD R1.w, R4.x, -c[2].x;
ADD R1.x, R1.w, c[4].y;
ABS R2.w, R1.x;
MAX R1.y, R0.w, c[4].z;
MAD R1.xyz, R1.y, -c[0], c[0];
ADD R0.xyz, R3, R0;
FLR R2.w, R2;
SLT R1.w, R1, -c[4].y;
CMP R1.w, -R1, -R2, R2;
ADD R1.xyz, R1, -R2;
MAX R1.w, R1, c[4].z;
MAD R1.xyz, R1.w, R1, R2;
MUL result.color.xyz, R1, R0;
ADD result.color.w, -R0, c[4].x;
END
# 36 instructions, 5 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
Vector 3 [unity_LightmapFade]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightBuffer] 2D 2
SetTexture 3 [unity_Lightmap] 2D 3
SetTexture 4 [unity_LightmapInd] 2D 4
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
def c4, 0.99000001, 0.00000000, 1.00000000, 8.00000000
dcl t0
dcl t1
dcl t2.xy
dcl t3
texldp r1, t1, s2
texld r2, t0, s0
texld r3, t2, s4
mul_pp r3.xyz, r3.w, r3
mov r0.y, t0.w
mov r0.x, t0.z
mul_pp r3.xyz, r3, c4.w
texld r5, r0, s1
texld r0, t2, s3
mul_pp r4.xyz, r0.w, r0
dp4 r0.x, t3, t3
rsq r0.x, r0.x
rcp r0.x, r0.x
mad_pp r4.xyz, r4, c4.w, -r3
mad_sat r0.x, r0, c3.z, c3.w
mad_pp r3.xyz, r0.x, r4, r3
mov r0.x, c2
add_pp r7.xyz, r1, r3
add r1.x, r5, -c2
add r1.x, r1, c4
abs r3.x, r1
add r0.x, c1, r0
add r0.x, r5, -r0
add r0.x, r0, c4
abs r5.x, r0
frc r6.x, r5
frc r4.x, r3
add r3.x, r3, -r4
cmp r1.x, r1, r3, -r3
add r5.x, r5, -r6
cmp r0.x, r0, r5, -r5
max r3.x, r0, c4.y
mad r3.xyz, r3.x, -c0, c0
add_pp r3.xyz, r3, -r2
max r1.x, r1, c4.y
mad_pp r1.xyz, r1.x, r3, r2
mul_pp r1.xyz, r1, r7
add r1.w, -r0.x, c4.z
mov_pp oC0, r1
"
}
SubProgram "opengl " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_ON" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightBuffer] 2D 2
SetTexture 3 [unity_Lightmap] 2D 3
"!!ARBfp1.0
PARAM c[4] = { program.local[0..2],
		{ 1, 0.99000001, 0, 8 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEX R0, fragment.texcoord[2], texture[3], 2D;
TEX R1.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R3.x, fragment.texcoord[0].zwzw, texture[1], 2D;
TXP R2.xyz, fragment.texcoord[1], texture[2], 2D;
MUL R0.xyz, R0.w, R0;
MOV R1.w, c[1].x;
ADD R1.w, R1, c[2].x;
ADD R1.w, R3.x, -R1;
MAD R2.xyz, R0, c[3].w, R2;
ADD R0.w, R1, c[3].y;
SLT R0.x, R1.w, -c[3].y;
ABS R0.y, R0.w;
FLR R0.y, R0;
CMP R0.w, -R0.x, -R0.y, R0.y;
ADD R1.w, R3.x, -c[2].x;
ADD R0.x, R1.w, c[3].y;
ABS R2.w, R0.x;
MAX R0.y, R0.w, c[3].z;
MAD R0.xyz, R0.y, -c[0], c[0];
FLR R2.w, R2;
SLT R1.w, R1, -c[3].y;
CMP R1.w, -R1, -R2, R2;
ADD R0.xyz, R0, -R1;
MAX R1.w, R1, c[3].z;
MAD R0.xyz, R1.w, R0, R1;
MUL result.color.xyz, R0, R2;
ADD result.color.w, -R0, c[3].x;
END
# 27 instructions, 4 R-regs
"
}
SubProgram "d3d9 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_ON" }
Vector 0 [_BurnColor]
Float 1 [_LineWidth]
Float 2 [_Burn]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BurnMap] 2D 1
SetTexture 2 [_LightBuffer] 2D 2
SetTexture 3 [unity_Lightmap] 2D 3
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
def c3, 0.99000001, 0.00000000, 1.00000000, 8.00000000
dcl t0
dcl t1
dcl t2.xy
texldp r1, t1, s2
texld r6, t0, s0
mov r0.y, t0.w
mov r0.x, t0.z
texld r2, r0, s1
texld r0, t2, s3
mul_pp r3.xyz, r0.w, r0
mov r0.x, c2
mad_pp r7.xyz, r3, c3.w, r1
add r1.x, r2, -c2
add r0.x, c1, r0
add r0.x, r2, -r0
add r0.x, r0, c3
abs r4.x, r0
add r1.x, r1, c3
abs r2.x, r1
frc r5.x, r4
frc r3.x, r2
add r2.x, r2, -r3
cmp r1.x, r1, r2, -r2
add r4.x, r4, -r5
cmp r0.x, r0, r4, -r4
max r2.x, r0, c3.y
mad r2.xyz, r2.x, -c0, c0
add_pp r2.xyz, r2, -r6
max r1.x, r1, c3.y
mad_pp r1.xyz, r1.x, r2, r6
mul_pp r1.xyz, r1, r7
add r1.w, -r0.x, c3.z
mov_pp oC0, r1
"
}
}
 }
}
Fallback "VertexLit"
}