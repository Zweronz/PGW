ÆkShader "Hidden/FXAA III (Console)" {
Properties {
 _MainTex ("-", 2D) = "white" {}
 _EdgeThresholdMin ("Edge threshold min", Float) = 0.125
 _EdgeThreshold ("Edge Threshold", Float) = 0.25
 _EdgeSharpness ("Edge sharpness", Float) = 4
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  Fog { Mode Off }
Program "vp" {
SubProgram "opengl " {
"!!GLSL
#ifdef VERTEX

uniform vec4 _MainTex_TexelSize;
varying vec2 xlv_TEXCOORD0;
varying vec4 xlv_TEXCOORD1;
varying vec4 xlv_TEXCOORD2;
varying vec4 xlv_TEXCOORD3;
void main ()
{
  vec4 rcpSize_1;
  vec4 extents_2;
  vec4 tmpvar_3;
  vec2 cse_4;
  cse_4 = (_MainTex_TexelSize.xy * 0.5);
  extents_2.xy = (gl_MultiTexCoord0.xy - cse_4);
  extents_2.zw = (gl_MultiTexCoord0.xy + cse_4);
  rcpSize_1.xy = (-(_MainTex_TexelSize.xy) * 0.5);
  rcpSize_1.zw = cse_4;
  tmpvar_3.xy = (rcpSize_1.xy * 4.0);
  tmpvar_3.zw = (cse_4 * 4.0);
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = gl_MultiTexCoord0.xy;
  xlv_TEXCOORD1 = extents_2;
  xlv_TEXCOORD2 = rcpSize_1;
  xlv_TEXCOORD3 = tmpvar_3;
}


#endif
#ifdef FRAGMENT
#extension GL_ARB_shader_texture_lod : enable
uniform sampler2D _MainTex;
uniform float _EdgeThresholdMin;
uniform float _EdgeThreshold;
uniform float _EdgeSharpness;
varying vec2 xlv_TEXCOORD0;
varying vec4 xlv_TEXCOORD1;
varying vec4 xlv_TEXCOORD2;
varying vec4 xlv_TEXCOORD3;
void main ()
{
  vec3 tmpvar_1;
  vec2 dir_2;
  float tmpvar_3;
  tmpvar_3 = dot (texture2DLod (_MainTex, xlv_TEXCOORD1.xy, 0.0).xyz, vec3(0.22, 0.707, 0.071));
  float tmpvar_4;
  tmpvar_4 = dot (texture2DLod (_MainTex, xlv_TEXCOORD1.xw, 0.0).xyz, vec3(0.22, 0.707, 0.071));
  float tmpvar_5;
  tmpvar_5 = dot (texture2DLod (_MainTex, xlv_TEXCOORD1.zw, 0.0).xyz, vec3(0.22, 0.707, 0.071));
  vec4 tmpvar_6;
  tmpvar_6 = texture2DLod (_MainTex, xlv_TEXCOORD0, 0.0);
  float tmpvar_7;
  tmpvar_7 = dot (tmpvar_6.xyz, vec3(0.22, 0.707, 0.071));
  float tmpvar_8;
  tmpvar_8 = (dot (texture2DLod (_MainTex, xlv_TEXCOORD1.zy, 0.0).xyz, vec3(0.22, 0.707, 0.071)) + 0.00260417);
  float tmpvar_9;
  tmpvar_9 = max (max (tmpvar_8, tmpvar_5), max (tmpvar_3, tmpvar_4));
  float tmpvar_10;
  tmpvar_10 = min (min (tmpvar_8, tmpvar_5), min (tmpvar_3, tmpvar_4));
  float tmpvar_11;
  tmpvar_11 = max (_EdgeThresholdMin, (tmpvar_9 * _EdgeThreshold));
  float tmpvar_12;
  tmpvar_12 = (tmpvar_4 - tmpvar_8);
  float tmpvar_13;
  tmpvar_13 = (max (tmpvar_9, tmpvar_7) - min (tmpvar_10, tmpvar_7));
  float tmpvar_14;
  tmpvar_14 = (tmpvar_5 - tmpvar_3);
  if ((tmpvar_13 < tmpvar_11)) {
    tmpvar_1 = tmpvar_6.xyz;
  } else {
    dir_2.x = (tmpvar_12 + tmpvar_14);
    dir_2.y = (tmpvar_12 - tmpvar_14);
    vec2 tmpvar_15;
    tmpvar_15 = normalize(dir_2);
    vec4 tmpvar_16;
    tmpvar_16.zw = vec2(0.0, 0.0);
    tmpvar_16.xy = (xlv_TEXCOORD0 - (tmpvar_15 * xlv_TEXCOORD2.zw));
    vec4 tmpvar_17;
    tmpvar_17.zw = vec2(0.0, 0.0);
    tmpvar_17.xy = (xlv_TEXCOORD0 + (tmpvar_15 * xlv_TEXCOORD2.zw));
    vec2 tmpvar_18;
    tmpvar_18 = clamp ((tmpvar_15 / (
      min (abs(tmpvar_15.x), abs(tmpvar_15.y))
     * _EdgeSharpness)), vec2(-2.0, -2.0), vec2(2.0, 2.0));
    dir_2 = tmpvar_18;
    vec4 tmpvar_19;
    tmpvar_19.zw = vec2(0.0, 0.0);
    tmpvar_19.xy = (xlv_TEXCOORD0 - (tmpvar_18 * xlv_TEXCOORD3.zw));
    vec4 tmpvar_20;
    tmpvar_20.zw = vec2(0.0, 0.0);
    tmpvar_20.xy = (xlv_TEXCOORD0 + (tmpvar_18 * xlv_TEXCOORD3.zw));
    vec3 tmpvar_21;
    tmpvar_21 = (texture2DLod (_MainTex, tmpvar_16.xy, 0.0).xyz + texture2DLod (_MainTex, tmpvar_17.xy, 0.0).xyz);
    vec3 tmpvar_22;
    tmpvar_22 = (((texture2DLod (_MainTex, tmpvar_19.xy, 0.0).xyz + texture2DLod (_MainTex, tmpvar_20.xy, 0.0).xyz) * 0.25) + (tmpvar_21 * 0.25));
    float tmpvar_23;
    tmpvar_23 = dot (tmpvar_21, vec3(0.22, 0.707, 0.071));
    bool tmpvar_24;
    if ((tmpvar_23 < tmpvar_10)) {
      tmpvar_24 = bool(1);
    } else {
      tmpvar_24 = (dot (tmpvar_22, vec3(0.22, 0.707, 0.071)) > tmpvar_9);
    };
    if (tmpvar_24) {
      tmpvar_1 = (tmpvar_21 * 0.5);
    } else {
      tmpvar_1 = tmpvar_22;
    };
  };
  vec4 tmpvar_25;
  tmpvar_25.w = 1.0;
  tmpvar_25.xyz = tmpvar_1;
  gl_FragData[0] = tmpvar_25;
}


#endif
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Vector 4 [_MainTex_TexelSize]
"vs_3_0
dcl_position o0
dcl_texcoord0 o1
dcl_texcoord1 o2
dcl_texcoord2 o3
dcl_texcoord3 o4
def c5, 0.50000000, 4.00000000, 0, 0
dcl_position0 v0
dcl_texcoord0 v1
mov r0.xy, c4
mul r1.xy, c5.x, r0
mul r0, r1.xyxy, c5.y
mov o4.zw, r0
mov o4.xy, -r0
mov r0.zw, c4.xyxy
mov r0.xy, c4
mov o1.xy, v1
mad o2.zw, c5.x, r0, v1.xyxy
mad o2.xy, c5.x, -r0, v1
mov o3.zw, r1.xyxy
mov o3.xy, -r1
dp4 o0.w, v0, c3
dp4 o0.z, v0, c2
dp4 o0.y, v0, c1
dp4 o0.x, v0, c0
"
}
SubProgram "d3d11 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
ConstBuffer "$Globals" 48
Vector 32 [_MainTex_TexelSize]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
"vs_4_0
eefiecedmellolgmfkmjmicbbmbbdohhblnkpdadabaaaaaafmadaaaaadaaaaaa
cmaaaaaaiaaaaaaacaabaaaaejfdeheoemaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaaebaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafaepfdejfeejepeoaafeeffiedepepfceeaaklkl
epfdeheojiaaaaaaafaaaaaaaiaaaaaaiaaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaaimaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaa
imaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaaimaaaaaaacaaaaaa
aaaaaaaaadaaaaaaadaaaaaaapaaaaaaimaaaaaaadaaaaaaaaaaaaaaadaaaaaa
aeaaaaaaapaaaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklkl
fdeieefcdeacaaaaeaaaabaainaaaaaafjaaaaaeegiocaaaaaaaaaaaadaaaaaa
fjaaaaaeegiocaaaabaaaaaaaeaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaad
dcbabaaaabaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaagfaaaaaddccabaaa
abaaaaaagfaaaaadpccabaaaacaaaaaagfaaaaadpccabaaaadaaaaaagfaaaaad
pccabaaaaeaaaaaagiaaaaacabaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaa
aaaaaaaaegiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaa
abaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaa
aaaaaaaaegiocaaaabaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaa
dcaaaaakpccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgbpbaaaaaaaaaaa
egaobaaaaaaaaaaadgaaaaafdccabaaaabaaaaaaegbabaaaabaaaaaadcaaaaao
dccabaaaacaaaaaaegiacaiaebaaaaaaaaaaaaaaacaaaaaaaceaaaaaaaaaaadp
aaaaaadpaaaaaaaaaaaaaaaaegbabaaaabaaaaaadcaaaaanmccabaaaacaaaaaa
agiecaaaaaaaaaaaacaaaaaaaceaaaaaaaaaaaaaaaaaaaaaaaaaaadpaaaaaadp
agbebaaaabaaaaaadiaaaaalpcaabaaaaaaaaaaaegiecaaaaaaaaaaaacaaaaaa
aceaaaaaaaaaaalpaaaaaalpaaaaaadpaaaaaadpdgaaaaafpccabaaaadaaaaaa
egaobaaaaaaaaaaadiaaaaaldccabaaaaeaaaaaaegiacaaaaaaaaaaaacaaaaaa
aceaaaaaaaaaaamaaaaaaamaaaaaaaaaaaaaaaaaaaaaaaajmccabaaaaeaaaaaa
agiecaaaaaaaaaaaacaaaaaaagiecaaaaaaaaaaaacaaaaaadoaaaaab"
}
}
Program "fp" {
SubProgram "opengl " {
"!!GLSL"
}
SubProgram "d3d9 " {
Float 0 [_EdgeThresholdMin]
Float 1 [_EdgeThreshold]
Float 2 [_EdgeSharpness]
SetTexture 0 [_MainTex] 2D 0
"ps_3_0
dcl_2d s0
def c3, 0.00000000, 0.21997070, 0.70703125, 0.07098389
def c4, 0.00260353, 1.00000000, 0.00000000, 2.00000000
def c5, -2.00000000, 0.25000000, 0.50000000, 0
dcl_texcoord0 v0.xy
dcl_texcoord1 v1
dcl_texcoord2 v2.xyzw
dcl_texcoord3 v3.xyzw
mov r0.z, c3.x
mov r0.xy, v1.xwzw
texldl r2.xyz, r0.xyzz, s0
dp3_pp r2.w, r2, c3.yzww
mov r0.z, c3.x
mov r0.xy, v1
texldl r0.xyz, r0.xyzz, s0
dp3_pp r3.x, r0, c3.yzww
min_pp r3.z, r3.x, r2.w
max_pp r0.w, r3.x, r2
mov r0.z, c3.x
mov r0.xy, v1.zyzw
texldl r0.xyz, r0.xyzz, s0
dp3_pp r0.x, r0, c3.yzww
mov r2.z, c3.x
mov r2.xy, v1.zwzw
texldl r2.xyz, r2.xyzz, s0
dp3_pp r2.x, r2, c3.yzww
add_pp r2.y, r0.x, c4.x
max_pp r0.x, r2.y, r2
max_pp r0.w, r0.x, r0
mul_pp r0.x, r0.w, c1
max_pp r2.z, r0.x, c0.x
min_pp r1.w, r2.y, r2.x
mov r0.z, c3.x
mov r0.xy, v0
texldl r0.xyz, r0.xyzz, s0
dp3_pp r3.y, r0, c3.yzww
min_pp r1.w, r1, r3.z
min_pp r3.z, r1.w, r3.y
max_pp r3.y, r0.w, r3
add_pp r3.y, r3, -r3.z
add_pp r2.z, r3.y, -r2
cmp_pp r1.xyz, r2.z, r1, r0
cmp_pp r0.z, r2, c4.y, c4
add_pp r0.x, -r2.y, r2.w
add_pp r0.y, r2.x, -r3.x
if_gt r0.z, c3.x
add_pp r1.x, r0, r0.y
add_pp r1.y, r0.x, -r0
mul_pp r0.xy, r1, r1
add_pp r0.x, r0, r0.y
rsq_pp r0.x, r0.x
mul_pp r3.xy, r0.x, r1
abs_pp r0.y, r3
abs_pp r0.x, r3
min_pp r0.x, r0, r0.y
mul_pp r0.x, r0, c2
rcp_pp r0.x, r0.x
mul_pp r0.xy, r3, r0.x
min_pp r0.xy, r0, c4.w
max_pp r0.xy, r0, c5.x
mul r0.xy, -r0, v3.zwzw
add r1.xy, v0, -r0
mov r1.z, c3.x
texldl r1.xyz, r1.xyzz, s0
mov r0.z, c3.x
add r0.xy, v0, r0
texldl r0.xyz, r0.xyzz, s0
add_pp r2.xyz, r0, r1
mul r0.xy, -r3, v2.zwzw
add r1.xy, -r0, v0
mov r1.z, c3.x
texldl r1.xyz, r1.xyzz, s0
mov r0.z, c3.x
add r0.xy, r0, v0
texldl r0.xyz, r0.xyzz, s0
add_pp r0.xyz, r0, r1
add_pp r1.xyz, r0, r2
mul_pp r1.xyz, r1, c5.y
dp3_pp r2.y, r1, c3.yzww
add_pp r2.y, -r2, r0.w
dp3_pp r2.x, r0, c3.yzww
add_pp r0.w, r2.x, -r1
cmp_pp r1.w, r2.y, c4.z, c4.y
cmp_pp r0.w, r0, c4.z, c4.y
mul_pp r2.xyz, r0, c5.z
add_pp_sat r0.w, r0, r1
abs_pp r0.x, r0.w
cmp_pp r1.xyz, -r0.x, r1, r2
endif
mov_pp oC0.xyz, r1
mov_pp oC0.w, c4.y
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
ConstBuffer "$Globals" 48
Float 16 [_EdgeThresholdMin]
Float 20 [_EdgeThreshold]
Float 24 [_EdgeSharpness]
BindCB  "$Globals" 0
"ps_4_0
eefiecedbaijopdaikaaijeibklonbfehihbjknhabaaaaaamaaiaaaaadaaaaaa
cmaaaaaammaaaaaaaaabaaaaejfdeheojiaaaaaaafaaaaaaaiaaaaaaiaaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaimaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaaimaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaa
apapaaaaimaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaapamaaaaimaaaaaa
adaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapamaaaafdfgfpfaepfdejfeejepeoaa
feeffiedepepfceeaaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklkl
fdeieefcliahaaaaeaaaaaaaooabaaaafjaaaaaeegiocaaaaaaaaaaaacaaaaaa
fkaaaaadaagabaaaaaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaad
dcbabaaaabaaaaaagcbaaaadpcbabaaaacaaaaaagcbaaaadmcbabaaaadaaaaaa
gcbaaaadmcbabaaaaeaaaaaagfaaaaadpccabaaaaaaaaaaagiaaaaacagaaaaaa
eiaaaaalpcaabaaaaaaaaaaaegbabaaaacaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaaabeaaaaaaaaaaaaabaaaaaakbcaabaaaaaaaaaaaegacbaaaaaaaaaaa
aceaaaaakoehgbdopepndedphdgijbdnaaaaaaaaeiaaaaalpcaabaaaabaaaaaa
mgbabaaaacaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaabeaaaaaaaaaaaaa
baaaaaakccaabaaaaaaaaaaaegacbaaaabaaaaaaaceaaaaakoehgbdopepndedp
hdgijbdnaaaaaaaaeiaaaaalpcaabaaaabaaaaaaggbkbaaaacaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaaabeaaaaaaaaaaaaabaaaaaakecaabaaaaaaaaaaa
egacbaaaabaaaaaaaceaaaaakoehgbdopepndedphdgijbdnaaaaaaaaeiaaaaal
pcaabaaaabaaaaaaogbkbaaaacaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
abeaaaaaaaaaaaaabaaaaaakicaabaaaaaaaaaaaegacbaaaabaaaaaaaceaaaaa
koehgbdopepndedphdgijbdnaaaaaaaaeiaaaaalpcaabaaaabaaaaaaegbabaaa
abaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaabeaaaaaaaaaaaaabaaaaaak
icaabaaaabaaaaaaegacbaaaabaaaaaaaceaaaaakoehgbdopepndedphdgijbdn
aaaaaaaaaaaaaaahecaabaaaaaaaaaaackaabaaaaaaaaaaaabeaaaaaklkkckdl
deaaaaahfcaabaaaacaaaaaafgahbaaaaaaaaaaaagacbaaaaaaaaaaaddaaaaah
kcaabaaaacaaaaaafganbaaaaaaaaaaaagaibaaaaaaaaaaadeaaaaahbcaabaaa
acaaaaaaakaabaaaacaaaaaackaabaaaacaaaaaaddaaaaahccaabaaaacaaaaaa
bkaabaaaacaaaaaadkaabaaaacaaaaaadiaaaaaiecaabaaaacaaaaaaakaabaaa
acaaaaaabkiacaaaaaaaaaaaabaaaaaaddaaaaahicaabaaaacaaaaaadkaabaaa
abaaaaaabkaabaaaacaaaaaadeaaaaaiecaabaaaacaaaaaackaabaaaacaaaaaa
akiacaaaaaaaaaaaabaaaaaadeaaaaahicaabaaaabaaaaaadkaabaaaabaaaaaa
akaabaaaacaaaaaaaaaaaaaiicaabaaaabaaaaaadkaabaiaebaaaaaaacaaaaaa
dkaabaaaabaaaaaabnaaaaahicaabaaaabaaaaaadkaabaaaabaaaaaackaabaaa
acaaaaaabpaaaeaddkaabaaaabaaaaaaaaaaaaaidcaabaaaaaaaaaaaigaabaia
ebaaaaaaaaaaaaaahgapbaaaaaaaaaaaaaaaaaahbcaabaaaadaaaaaaakaabaaa
aaaaaaaabkaabaaaaaaaaaaaaaaaaaaiccaabaaaadaaaaaaakaabaiaebaaaaaa
aaaaaaaabkaabaaaaaaaaaaaapaaaaahbcaabaaaaaaaaaaaegaabaaaadaaaaaa
egaabaaaadaaaaaaeeaaaaafbcaabaaaaaaaaaaaakaabaaaaaaaaaaadiaaaaah
dcaabaaaaaaaaaaaagaabaaaaaaaaaaaegaabaaaadaaaaaadcaaaaakmcaabaaa
aaaaaaaaagaebaiaebaaaaaaaaaaaaaakgbobaaaadaaaaaaagbebaaaabaaaaaa
eiaaaaalpcaabaaaadaaaaaaogakbaaaaaaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaaabeaaaaaaaaaaaaadcaaaaajmcaabaaaaaaaaaaaagaebaaaaaaaaaaa
kgbobaaaadaaaaaaagbebaaaabaaaaaaeiaaaaalpcaabaaaaeaaaaaaogakbaaa
aaaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaabeaaaaaaaaaaaaaddaaaaaj
ecaabaaaaaaaaaaabkaabaiaibaaaaaaaaaaaaaaakaabaiaibaaaaaaaaaaaaaa
diaaaaaiecaabaaaaaaaaaaackaabaaaaaaaaaaackiacaaaaaaaaaaaabaaaaaa
aoaaaaahdcaabaaaaaaaaaaaegaabaaaaaaaaaaakgakbaaaaaaaaaaadeaaaaak
dcaabaaaaaaaaaaaegaabaaaaaaaaaaaaceaaaaaaaaaaamaaaaaaamaaaaaaaaa
aaaaaaaaddaaaaakdcaabaaaaaaaaaaaegaabaaaaaaaaaaaaceaaaaaaaaaaaea
aaaaaaeaaaaaaaaaaaaaaaaadcaaaaakmcaabaaaaaaaaaaaagaebaiaebaaaaaa
aaaaaaaakgbobaaaaeaaaaaaagbebaaaabaaaaaaeiaaaaalpcaabaaaafaaaaaa
ogakbaaaaaaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaabeaaaaaaaaaaaaa
dcaaaaajdcaabaaaaaaaaaaaegaabaaaaaaaaaaaogbkbaaaaeaaaaaaegbabaaa
abaaaaaaeiaaaaalpcaabaaaaaaaaaaaegaabaaaaaaaaaaaeghobaaaaaaaaaaa
aagabaaaaaaaaaaaabeaaaaaaaaaaaaaaaaaaaahhcaabaaaadaaaaaaegacbaaa
adaaaaaaegacbaaaaeaaaaaaaaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaa
egacbaaaafaaaaaadiaaaaakhcaabaaaaeaaaaaaegacbaaaadaaaaaaaceaaaaa
aaaaiadoaaaaiadoaaaaiadoaaaaaaaadcaaaaamhcaabaaaaaaaaaaaegacbaaa
aaaaaaaaaceaaaaaaaaaiadoaaaaiadoaaaaiadoaaaaaaaaegacbaaaaeaaaaaa
baaaaaakicaabaaaaaaaaaaaegacbaaaadaaaaaaaceaaaaakoehgbdopepndedp
hdgijbdnaaaaaaaadbaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaabkaabaaa
acaaaaaabaaaaaakicaabaaaabaaaaaaegacbaaaaaaaaaaaaceaaaaakoehgbdo
pepndedphdgijbdnaaaaaaaadbaaaaahicaabaaaabaaaaaaakaabaaaacaaaaaa
dkaabaaaabaaaaaadmaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaadkaabaaa
abaaaaaadiaaaaakhcaabaaaacaaaaaaegacbaaaadaaaaaaaceaaaaaaaaaaadp
aaaaaadpaaaaaadpaaaaaaaadhaaaaajhcaabaaaabaaaaaapgapbaaaaaaaaaaa
egacbaaaacaaaaaaegacbaaaaaaaaaaabfaaaaabdgaaaaafhccabaaaaaaaaaaa
egacbaaaabaaaaaadgaaaaaficcabaaaaaaaaaaaabeaaaaaaaaaiadpdoaaaaab
"
}
}
 }
}
Fallback Off
}