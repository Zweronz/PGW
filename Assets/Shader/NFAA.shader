š³Shader "Hidden/NFAA" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _BlurTex ("Base (RGB)", 2D) = "white" {}
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
uniform float _OffsetScale;
varying vec2 xlv_TEXCOORD0;
varying vec2 xlv_TEXCOORD0_1;
varying vec2 xlv_TEXCOORD0_2;
varying vec2 xlv_TEXCOORD0_3;
varying vec2 xlv_TEXCOORD0_4;
varying vec2 xlv_TEXCOORD0_5;
varying vec2 xlv_TEXCOORD0_6;
varying vec2 xlv_TEXCOORD0_7;
void main ()
{
  vec2 tmpvar_1;
  tmpvar_1.x = 0.0;
  tmpvar_1.y = _MainTex_TexelSize.y;
  vec2 tmpvar_2;
  tmpvar_2 = (tmpvar_1 * _OffsetScale);
  vec2 tmpvar_3;
  tmpvar_3.y = 0.0;
  tmpvar_3.x = _MainTex_TexelSize.x;
  vec2 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * _OffsetScale);
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = (gl_MultiTexCoord0.xy + tmpvar_2);
  xlv_TEXCOORD0_1 = (gl_MultiTexCoord0.xy - tmpvar_2);
  xlv_TEXCOORD0_2 = (gl_MultiTexCoord0.xy + tmpvar_4);
  xlv_TEXCOORD0_3 = (gl_MultiTexCoord0.xy - tmpvar_4);
  xlv_TEXCOORD0_4 = ((gl_MultiTexCoord0.xy - tmpvar_4) + tmpvar_2);
  xlv_TEXCOORD0_5 = ((gl_MultiTexCoord0.xy - tmpvar_4) - tmpvar_2);
  xlv_TEXCOORD0_6 = ((gl_MultiTexCoord0.xy + tmpvar_4) + tmpvar_2);
  xlv_TEXCOORD0_7 = ((gl_MultiTexCoord0.xy + tmpvar_4) - tmpvar_2);
}


#endif
#ifdef FRAGMENT
uniform sampler2D _MainTex;
uniform vec4 _MainTex_TexelSize;
uniform float _BlurRadius;
varying vec2 xlv_TEXCOORD0;
varying vec2 xlv_TEXCOORD0_1;
varying vec2 xlv_TEXCOORD0_2;
varying vec2 xlv_TEXCOORD0_3;
varying vec2 xlv_TEXCOORD0_4;
varying vec2 xlv_TEXCOORD0_5;
varying vec2 xlv_TEXCOORD0_6;
varying vec2 xlv_TEXCOORD0_7;
void main ()
{
  float tmpvar_1;
  tmpvar_1 = dot (texture2D (_MainTex, xlv_TEXCOORD0_4).xyz, vec3(0.22, 0.707, 0.071));
  float tmpvar_2;
  tmpvar_2 = dot (texture2D (_MainTex, xlv_TEXCOORD0_5).xyz, vec3(0.22, 0.707, 0.071));
  float tmpvar_3;
  tmpvar_3 = dot (texture2D (_MainTex, xlv_TEXCOORD0_6).xyz, vec3(0.22, 0.707, 0.071));
  float tmpvar_4;
  tmpvar_4 = dot (texture2D (_MainTex, xlv_TEXCOORD0_7).xyz, vec3(0.22, 0.707, 0.071));
  vec3 tmpvar_5;
  tmpvar_5.x = tmpvar_4;
  tmpvar_5.y = dot (texture2D (_MainTex, xlv_TEXCOORD0_1).xyz, vec3(0.22, 0.707, 0.071));
  tmpvar_5.z = tmpvar_1;
  vec3 tmpvar_6;
  tmpvar_6.x = tmpvar_2;
  tmpvar_6.y = dot (texture2D (_MainTex, xlv_TEXCOORD0).xyz, vec3(0.22, 0.707, 0.071));
  tmpvar_6.z = tmpvar_3;
  vec3 tmpvar_7;
  tmpvar_7.x = tmpvar_1;
  tmpvar_7.y = dot (texture2D (_MainTex, xlv_TEXCOORD0_2).xyz, vec3(0.22, 0.707, 0.071));
  tmpvar_7.z = tmpvar_2;
  vec3 tmpvar_8;
  tmpvar_8.x = tmpvar_3;
  tmpvar_8.y = dot (texture2D (_MainTex, xlv_TEXCOORD0_3).xyz, vec3(0.22, 0.707, 0.071));
  tmpvar_8.z = tmpvar_4;
  vec2 tmpvar_9;
  tmpvar_9.x = (dot (vec3(1.0, 1.0, 1.0), tmpvar_5) - dot (vec3(1.0, 1.0, 1.0), tmpvar_6));
  tmpvar_9.y = (dot (vec3(1.0, 1.0, 1.0), tmpvar_8) - dot (vec3(1.0, 1.0, 1.0), tmpvar_7));
  vec2 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (_MainTex_TexelSize.xy * _BlurRadius));
  vec2 tmpvar_11;
  tmpvar_11 = ((xlv_TEXCOORD0 + xlv_TEXCOORD0_1) * 0.5);
  vec2 tmpvar_12;
  tmpvar_12.x = tmpvar_10.x;
  tmpvar_12.y = -(tmpvar_10.y);
  vec2 tmpvar_13;
  tmpvar_13.x = tmpvar_10.x;
  tmpvar_13.y = -(tmpvar_10.y);
  gl_FragData[0] = (((
    ((texture2D (_MainTex, tmpvar_11) + texture2D (_MainTex, (tmpvar_11 + tmpvar_10))) + texture2D (_MainTex, (tmpvar_11 - tmpvar_10)))
   + texture2D (_MainTex, 
    (tmpvar_11 + tmpvar_12)
  )) + texture2D (_MainTex, (tmpvar_11 - tmpvar_13))) * 0.2);
}


#endif
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Vector 4 [_MainTex_TexelSize]
Float 5 [_OffsetScale]
"vs_2_0
def c6, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_texcoord0 v1
mov r0.x, c6
mov r0.y, c4
mov r0.w, c6.x
mov r0.z, c4.x
mad r1.xy, -r0.zwzw, c5.x, v1
mad r0.zw, r0, c5.x, v1.xyxy
mad oT4.xy, r0, c5.x, r1
mad oT5.xy, -r0, c5.x, r1
mad oT6.xy, r0, c5.x, r0.zwzw
mad oT7.xy, -r0, c5.x, r0.zwzw
mad oT0.xy, r0, c5.x, v1
mad oT1.xy, -r0, c5.x, v1
mov oT2.xy, r0.zwzw
mov oT3.xy, r1
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
SubProgram "d3d11 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
ConstBuffer "$Globals" 48
Vector 16 [_MainTex_TexelSize]
Float 32 [_OffsetScale]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
"vs_4_0
eefiecedfbfnmlkcjjbinfdpiblijdkghongpoheabaaaaaadiaeaaaaadaaaaaa
cmaaaaaaiaaaaaaaiaabaaaaejfdeheoemaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaaebaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafaepfdejfeejepeoaafeeffiedepepfceeaaklkl
epfdeheopiaaaaaaajaaaaaaaiaaaaaaoaaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaaomaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaa
omaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaadamaaaaomaaaaaaacaaaaaa
aaaaaaaaadaaaaaaadaaaaaaadamaaaaomaaaaaaadaaaaaaaaaaaaaaadaaaaaa
aeaaaaaaadamaaaaomaaaaaaaeaaaaaaaaaaaaaaadaaaaaaafaaaaaaadamaaaa
omaaaaaaafaaaaaaaaaaaaaaadaaaaaaagaaaaaaadamaaaaomaaaaaaagaaaaaa
aaaaaaaaadaaaaaaahaaaaaaadamaaaaomaaaaaaahaaaaaaaaaaaaaaadaaaaaa
aiaaaaaaadamaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfceeaaklklkl
fdeieefclaacaaaaeaaaabaakmaaaaaafjaaaaaeegiocaaaaaaaaaaaadaaaaaa
fjaaaaaeegiocaaaabaaaaaaaeaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaad
dcbabaaaabaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaagfaaaaaddccabaaa
abaaaaaagfaaaaaddccabaaaacaaaaaagfaaaaaddccabaaaadaaaaaagfaaaaad
dccabaaaaeaaaaaagfaaaaaddccabaaaafaaaaaagfaaaaaddccabaaaagaaaaaa
gfaaaaaddccabaaaahaaaaaagfaaaaaddccabaaaaiaaaaaagiaaaaacacaaaaaa
diaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaaegiocaaaabaaaaaaabaaaaaa
dcaaaaakpcaabaaaaaaaaaaaegiocaaaabaaaaaaaaaaaaaaagbabaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaabaaaaaaacaaaaaa
kgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaa
abaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaaaaaaaaaadiaaaaajgcaabaaa
aaaaaaaafgiecaaaaaaaaaaaabaaaaaaagiacaaaaaaaaaaaacaaaaaadgaaaaai
jcaabaaaaaaaaaaaaceaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaah
dccabaaaabaaaaaaegaabaaaaaaaaaaaegbabaaaabaaaaaaaaaaaaaidccabaaa
acaaaaaaegaabaiaebaaaaaaaaaaaaaaegbabaaaabaaaaaaaaaaaaahdcaabaaa
abaaaaaaogakbaaaaaaaaaaaegbabaaaabaaaaaadgaaaaafdccabaaaadaaaaaa
egaabaaaabaaaaaaaaaaaaaimcaabaaaaaaaaaaakgaobaiaebaaaaaaaaaaaaaa
agbebaaaabaaaaaadgaaaaafdccabaaaaeaaaaaaogakbaaaaaaaaaaaaaaaaaah
dccabaaaafaaaaaaegaabaaaaaaaaaaaogakbaaaaaaaaaaaaaaaaaaidccabaaa
agaaaaaaegaabaiaebaaaaaaaaaaaaaaogakbaaaaaaaaaaaaaaaaaahdccabaaa
ahaaaaaaegaabaaaaaaaaaaaegaabaaaabaaaaaaaaaaaaaidccabaaaaiaaaaaa
egaabaiaebaaaaaaaaaaaaaaegaabaaaabaaaaaadoaaaaab"
}
}
Program "fp" {
SubProgram "opengl " {
"!!GLSL"
}
SubProgram "d3d9 " {
Vector 0 [_MainTex_TexelSize]
Float 1 [_BlurRadius]
SetTexture 0 [_MainTex] 2D 0
"ps_2_0
dcl_2d s0
def c2, 0.21997070, 0.70703125, 0.07098389, 1.00000000
def c3, 0.50000000, 0.20000000, 0, 0
dcl t0.xy
dcl t1.xy
dcl t2.xy
dcl t3.xy
dcl t4.xy
dcl t5.xy
dcl t6.xy
dcl t7.xy
texld r1, t3, s0
texld r0, t2, s0
texld r2, t1, s0
texld r3, t4, s0
texld r4, t7, s0
texld r5, t0, s0
texld r7, t5, s0
texld r6, t6, s0
dp3_pp r4.x, r4, c2
dp3_pp r3.x, r3, c2
dp3_pp r7.x, r7, c2
dp3_pp r6.x, r6, c2
dp3_pp r4.y, r2, c2
mov r4.z, r3.x
dp3_pp r3.y, r0, c2
mov r3.z, r7.x
dp3_pp r6.y, r1, c2
mov r6.z, r4.x
dp3 r2.x, r4, c2.w
dp3 r0.x, r3, c2.w
dp3 r1.x, r6, c2.w
add r2.y, r1.x, -r0.x
mov r4.xy, c0
mov r1.xy, t1
dp3_pp r7.y, r5, c2
mov r7.z, r6.x
dp3 r5.x, r7, c2.w
add r2.x, r2, -r5
mul r3.xy, c1.x, r4
mul r0.xy, r2, r3
add r1.xy, t0, r1
mul r4.xy, r1, c3.x
add r3.xy, r4, r0
mov r2.x, r0
mov r2.y, -r0
add r1.xy, r4, r2
add r5.xy, r4, -r2
add r2.xy, r4, -r0
texld r0, r5, s0
texld r1, r1, s0
texld r3, r3, s0
texld r2, r2, s0
texld r4, r4, s0
add r3, r4, r3
add r2, r3, r2
add r1, r2, r1
add r0, r1, r0
mul r0, r0, c3.y
mov_pp oC0, r0
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
ConstBuffer "$Globals" 48
Vector 16 [_MainTex_TexelSize]
Float 36 [_BlurRadius]
BindCB  "$Globals" 0
"ps_4_0
eefieceddhhiaiikihdhekahbdfhomlgafjhkgipabaaaaaaeaaiaaaaadaaaaaa
cmaaaaaacmabaaaagaabaaaaejfdeheopiaaaaaaajaaaaaaaiaaaaaaoaaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaomaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaaomaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaa
adadaaaaomaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaadadaaaaomaaaaaa
adaaaaaaaaaaaaaaadaaaaaaaeaaaaaaadadaaaaomaaaaaaaeaaaaaaaaaaaaaa
adaaaaaaafaaaaaaadadaaaaomaaaaaaafaaaaaaaaaaaaaaadaaaaaaagaaaaaa
adadaaaaomaaaaaaagaaaaaaaaaaaaaaadaaaaaaahaaaaaaadadaaaaomaaaaaa
ahaaaaaaaaaaaaaaadaaaaaaaiaaaaaaadadaaaafdfgfpfagphdgjhegjgpgoaa
feeffiedepepfceeaaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklkl
fdeieefcniagaaaaeaaaaaaalgabaaaafjaaaaaeegiocaaaaaaaaaaaadaaaaaa
fkaaaaadaagabaaaaaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaad
dcbabaaaabaaaaaagcbaaaaddcbabaaaacaaaaaagcbaaaaddcbabaaaadaaaaaa
gcbaaaaddcbabaaaaeaaaaaagcbaaaaddcbabaaaafaaaaaagcbaaaaddcbabaaa
agaaaaaagcbaaaaddcbabaaaahaaaaaagcbaaaaddcbabaaaaiaaaaaagfaaaaad
pccabaaaaaaaaaaagiaaaaacaeaaaaaaefaaaaajpcaabaaaaaaaaaaaegbabaaa
adaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaabaaaaaakccaabaaaaaaaaaaa
egacbaaaaaaaaaaaaceaaaaakoehgbdopepndedphdgijbdnaaaaaaaaefaaaaaj
pcaabaaaabaaaaaaegbabaaaafaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
baaaaaakecaabaaaabaaaaaaegacbaaaabaaaaaaaceaaaaakoehgbdopepndedp
hdgijbdnaaaaaaaadgaaaaafbcaabaaaaaaaaaaackaabaaaabaaaaaaefaaaaaj
pcaabaaaacaaaaaaegbabaaaagaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
baaaaaakecaabaaaaaaaaaaaegacbaaaacaaaaaaaceaaaaakoehgbdopepndedp
hdgijbdnaaaaaaaabaaaaaakicaabaaaaaaaaaaaaceaaaaaaaaaiadpaaaaiadp
aaaaiadpaaaaaaaaegacbaaaaaaaaaaaefaaaaajpcaabaaaacaaaaaaegbabaaa
aeaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaabaaaaaakccaabaaaacaaaaaa
egacbaaaacaaaaaaaceaaaaakoehgbdopepndedphdgijbdnaaaaaaaaefaaaaaj
pcaabaaaadaaaaaaegbabaaaahaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
baaaaaakccaabaaaaaaaaaaaegacbaaaadaaaaaaaceaaaaakoehgbdopepndedp
hdgijbdnaaaaaaaadgaaaaafbcaabaaaacaaaaaabkaabaaaaaaaaaaaefaaaaaj
pcaabaaaadaaaaaaegbabaaaaiaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
baaaaaakbcaabaaaabaaaaaaegacbaaaadaaaaaaaceaaaaakoehgbdopepndedp
hdgijbdnaaaaaaaadgaaaaafecaabaaaacaaaaaaakaabaaaabaaaaaabaaaaaak
icaabaaaabaaaaaaaceaaaaaaaaaiadpaaaaiadpaaaaiadpaaaaaaaaegacbaaa
acaaaaaaaaaaaaaiccaabaaaacaaaaaadkaabaiaebaaaaaaaaaaaaaadkaabaaa
abaaaaaaefaaaaajpcaabaaaadaaaaaaegbabaaaacaaaaaaeghobaaaaaaaaaaa
aagabaaaaaaaaaaabaaaaaakccaabaaaabaaaaaaegacbaaaadaaaaaaaceaaaaa
koehgbdopepndedphdgijbdnaaaaaaaabaaaaaakicaabaaaaaaaaaaaaceaaaaa
aaaaiadpaaaaiadpaaaaiadpaaaaaaaaegacbaaaabaaaaaaefaaaaajpcaabaaa
abaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaabaaaaaak
bcaabaaaaaaaaaaaegacbaaaabaaaaaaaceaaaaakoehgbdopepndedphdgijbdn
aaaaaaaabaaaaaakbcaabaaaaaaaaaaaaceaaaaaaaaaiadpaaaaiadpaaaaiadp
aaaaaaaaegacbaaaaaaaaaaaaaaaaaaibcaabaaaacaaaaaaakaabaiaebaaaaaa
aaaaaaaadkaabaaaaaaaaaaadiaaaaajdcaabaaaaaaaaaaaegiacaaaaaaaaaaa
abaaaaaafgifcaaaaaaaaaaaacaaaaaadiaaaaahdcaabaaaaaaaaaaaegaabaaa
aaaaaaaaegaabaaaacaaaaaaaaaaaaahdcaabaaaabaaaaaaegbabaaaabaaaaaa
egbabaaaacaaaaaadcaaaaammcaabaaaabaaaaaaagaebaaaabaaaaaaaceaaaaa
aaaaaaaaaaaaaaaaaaaaaadpaaaaaadpagaebaaaaaaaaaaaefaaaaajpcaabaaa
acaaaaaaogakbaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaadiaaaaak
mcaabaaaabaaaaaaagaebaaaabaaaaaaaceaaaaaaaaaaaaaaaaaaaaaaaaaaadp
aaaaaadpefaaaaajpcaabaaaadaaaaaaogakbaaaabaaaaaaeghobaaaaaaaaaaa
aagabaaaaaaaaaaaaaaaaaahpcaabaaaacaaaaaaegaobaaaacaaaaaaegaobaaa
adaaaaaadcaaaaanmcaabaaaabaaaaaaagaebaaaabaaaaaaaceaaaaaaaaaaaaa
aaaaaaaaaaaaaadpaaaaaadpagaebaiaebaaaaaaaaaaaaaaefaaaaajpcaabaaa
adaaaaaaogakbaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaah
pcaabaaaacaaaaaaegaobaaaacaaaaaaegaobaaaadaaaaaadgaaaaagecaabaaa
aaaaaaaabkaabaiaebaaaaaaaaaaaaaadcaaaaamkcaabaaaaaaaaaaaagaebaaa
abaaaaaaaceaaaaaaaaaaaaaaaaaaadpaaaaaaaaaaaaaadpagaibaaaaaaaaaaa
dcaaaaanfcaabaaaaaaaaaaaagabbaaaabaaaaaaaceaaaaaaaaaaadpaaaaaaaa
aaaaaadpaaaaaaaaagacbaiaebaaaaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaa
igaabaaaaaaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaa
aaaaaaaangafbaaaaaaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaah
pcaabaaaaaaaaaaaegaobaaaaaaaaaaaegaobaaaacaaaaaaaaaaaaahpcaabaaa
aaaaaaaaegaobaaaabaaaaaaegaobaaaaaaaaaaadiaaaaakpccabaaaaaaaaaaa
egaobaaaaaaaaaaaaceaaaaamnmmemdomnmmemdomnmmemdomnmmemdodoaaaaab
"
}
}
 }
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
uniform float _OffsetScale;
varying vec2 xlv_TEXCOORD0;
varying vec2 xlv_TEXCOORD0_1;
varying vec2 xlv_TEXCOORD0_2;
varying vec2 xlv_TEXCOORD0_3;
varying vec2 xlv_TEXCOORD0_4;
varying vec2 xlv_TEXCOORD0_5;
varying vec2 xlv_TEXCOORD0_6;
varying vec2 xlv_TEXCOORD0_7;
void main ()
{
  vec2 tmpvar_1;
  tmpvar_1.x = 0.0;
  tmpvar_1.y = _MainTex_TexelSize.y;
  vec2 tmpvar_2;
  tmpvar_2 = (tmpvar_1 * _OffsetScale);
  vec2 tmpvar_3;
  tmpvar_3.y = 0.0;
  tmpvar_3.x = _MainTex_TexelSize.x;
  vec2 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * _OffsetScale);
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = (gl_MultiTexCoord0.xy + tmpvar_2);
  xlv_TEXCOORD0_1 = (gl_MultiTexCoord0.xy - tmpvar_2);
  xlv_TEXCOORD0_2 = (gl_MultiTexCoord0.xy + tmpvar_4);
  xlv_TEXCOORD0_3 = (gl_MultiTexCoord0.xy - tmpvar_4);
  xlv_TEXCOORD0_4 = ((gl_MultiTexCoord0.xy - tmpvar_4) + tmpvar_2);
  xlv_TEXCOORD0_5 = ((gl_MultiTexCoord0.xy - tmpvar_4) - tmpvar_2);
  xlv_TEXCOORD0_6 = ((gl_MultiTexCoord0.xy + tmpvar_4) + tmpvar_2);
  xlv_TEXCOORD0_7 = ((gl_MultiTexCoord0.xy + tmpvar_4) - tmpvar_2);
}


#endif
#ifdef FRAGMENT
uniform sampler2D _MainTex;
uniform float _BlurRadius;
varying vec2 xlv_TEXCOORD0;
varying vec2 xlv_TEXCOORD0_1;
varying vec2 xlv_TEXCOORD0_2;
varying vec2 xlv_TEXCOORD0_3;
varying vec2 xlv_TEXCOORD0_4;
varying vec2 xlv_TEXCOORD0_5;
varying vec2 xlv_TEXCOORD0_6;
varying vec2 xlv_TEXCOORD0_7;
void main ()
{
  float tmpvar_1;
  tmpvar_1 = dot (texture2D (_MainTex, xlv_TEXCOORD0_4).xyz, vec3(0.22, 0.707, 0.071));
  float tmpvar_2;
  tmpvar_2 = dot (texture2D (_MainTex, xlv_TEXCOORD0_5).xyz, vec3(0.22, 0.707, 0.071));
  float tmpvar_3;
  tmpvar_3 = dot (texture2D (_MainTex, xlv_TEXCOORD0_6).xyz, vec3(0.22, 0.707, 0.071));
  float tmpvar_4;
  tmpvar_4 = dot (texture2D (_MainTex, xlv_TEXCOORD0_7).xyz, vec3(0.22, 0.707, 0.071));
  vec3 tmpvar_5;
  tmpvar_5.x = tmpvar_4;
  tmpvar_5.y = dot (texture2D (_MainTex, xlv_TEXCOORD0_1).xyz, vec3(0.22, 0.707, 0.071));
  tmpvar_5.z = tmpvar_1;
  vec3 tmpvar_6;
  tmpvar_6.x = tmpvar_2;
  tmpvar_6.y = dot (texture2D (_MainTex, xlv_TEXCOORD0).xyz, vec3(0.22, 0.707, 0.071));
  tmpvar_6.z = tmpvar_3;
  vec3 tmpvar_7;
  tmpvar_7.x = tmpvar_1;
  tmpvar_7.y = dot (texture2D (_MainTex, xlv_TEXCOORD0_2).xyz, vec3(0.22, 0.707, 0.071));
  tmpvar_7.z = tmpvar_2;
  vec3 tmpvar_8;
  tmpvar_8.x = tmpvar_3;
  tmpvar_8.y = dot (texture2D (_MainTex, xlv_TEXCOORD0_3).xyz, vec3(0.22, 0.707, 0.071));
  tmpvar_8.z = tmpvar_4;
  vec2 tmpvar_9;
  tmpvar_9.x = (dot (vec3(1.0, 1.0, 1.0), tmpvar_5) - dot (vec3(1.0, 1.0, 1.0), tmpvar_6));
  tmpvar_9.y = (dot (vec3(1.0, 1.0, 1.0), tmpvar_8) - dot (vec3(1.0, 1.0, 1.0), tmpvar_7));
  vec3 tmpvar_10;
  tmpvar_10.z = 1.0;
  tmpvar_10.xy = (tmpvar_9 * _BlurRadius);
  vec4 tmpvar_11;
  tmpvar_11.w = 1.0;
  tmpvar_11.xyz = normalize(((tmpvar_10 * 0.5) + 0.5));
  gl_FragData[0] = tmpvar_11;
}


#endif
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Vector 4 [_MainTex_TexelSize]
Float 5 [_OffsetScale]
"vs_2_0
def c6, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_texcoord0 v1
mov r0.x, c6
mov r0.y, c4
mov r0.w, c6.x
mov r0.z, c4.x
mad r1.xy, -r0.zwzw, c5.x, v1
mad r0.zw, r0, c5.x, v1.xyxy
mad oT4.xy, r0, c5.x, r1
mad oT5.xy, -r0, c5.x, r1
mad oT6.xy, r0, c5.x, r0.zwzw
mad oT7.xy, -r0, c5.x, r0.zwzw
mad oT0.xy, r0, c5.x, v1
mad oT1.xy, -r0, c5.x, v1
mov oT2.xy, r0.zwzw
mov oT3.xy, r1
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
SubProgram "d3d11 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
ConstBuffer "$Globals" 48
Vector 16 [_MainTex_TexelSize]
Float 32 [_OffsetScale]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
"vs_4_0
eefiecedfbfnmlkcjjbinfdpiblijdkghongpoheabaaaaaadiaeaaaaadaaaaaa
cmaaaaaaiaaaaaaaiaabaaaaejfdeheoemaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaaebaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafaepfdejfeejepeoaafeeffiedepepfceeaaklkl
epfdeheopiaaaaaaajaaaaaaaiaaaaaaoaaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaaomaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaa
omaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaadamaaaaomaaaaaaacaaaaaa
aaaaaaaaadaaaaaaadaaaaaaadamaaaaomaaaaaaadaaaaaaaaaaaaaaadaaaaaa
aeaaaaaaadamaaaaomaaaaaaaeaaaaaaaaaaaaaaadaaaaaaafaaaaaaadamaaaa
omaaaaaaafaaaaaaaaaaaaaaadaaaaaaagaaaaaaadamaaaaomaaaaaaagaaaaaa
aaaaaaaaadaaaaaaahaaaaaaadamaaaaomaaaaaaahaaaaaaaaaaaaaaadaaaaaa
aiaaaaaaadamaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfceeaaklklkl
fdeieefclaacaaaaeaaaabaakmaaaaaafjaaaaaeegiocaaaaaaaaaaaadaaaaaa
fjaaaaaeegiocaaaabaaaaaaaeaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaad
dcbabaaaabaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaagfaaaaaddccabaaa
abaaaaaagfaaaaaddccabaaaacaaaaaagfaaaaaddccabaaaadaaaaaagfaaaaad
dccabaaaaeaaaaaagfaaaaaddccabaaaafaaaaaagfaaaaaddccabaaaagaaaaaa
gfaaaaaddccabaaaahaaaaaagfaaaaaddccabaaaaiaaaaaagiaaaaacacaaaaaa
diaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaaegiocaaaabaaaaaaabaaaaaa
dcaaaaakpcaabaaaaaaaaaaaegiocaaaabaaaaaaaaaaaaaaagbabaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaabaaaaaaacaaaaaa
kgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaa
abaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaaaaaaaaaadiaaaaajgcaabaaa
aaaaaaaafgiecaaaaaaaaaaaabaaaaaaagiacaaaaaaaaaaaacaaaaaadgaaaaai
jcaabaaaaaaaaaaaaceaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaah
dccabaaaabaaaaaaegaabaaaaaaaaaaaegbabaaaabaaaaaaaaaaaaaidccabaaa
acaaaaaaegaabaiaebaaaaaaaaaaaaaaegbabaaaabaaaaaaaaaaaaahdcaabaaa
abaaaaaaogakbaaaaaaaaaaaegbabaaaabaaaaaadgaaaaafdccabaaaadaaaaaa
egaabaaaabaaaaaaaaaaaaaimcaabaaaaaaaaaaakgaobaiaebaaaaaaaaaaaaaa
agbebaaaabaaaaaadgaaaaafdccabaaaaeaaaaaaogakbaaaaaaaaaaaaaaaaaah
dccabaaaafaaaaaaegaabaaaaaaaaaaaogakbaaaaaaaaaaaaaaaaaaidccabaaa
agaaaaaaegaabaiaebaaaaaaaaaaaaaaogakbaaaaaaaaaaaaaaaaaahdccabaaa
ahaaaaaaegaabaaaaaaaaaaaegaabaaaabaaaaaaaaaaaaaidccabaaaaiaaaaaa
egaabaiaebaaaaaaaaaaaaaaegaabaaaabaaaaaadoaaaaab"
}
}
Program "fp" {
SubProgram "opengl " {
"!!GLSL"
}
SubProgram "d3d9 " {
Float 0 [_BlurRadius]
SetTexture 0 [_MainTex] 2D 0
"ps_2_0
dcl_2d s0
def c1, 0.21997070, 0.70703125, 0.07098389, 1.00000000
def c2, 0.50000000, 0, 0, 0
dcl t0.xy
dcl t1.xy
dcl t2.xy
dcl t3.xy
dcl t4.xy
dcl t5.xy
dcl t6.xy
dcl t7.xy
texld r0, t3, s0
texld r1, t2, s0
texld r2, t1, s0
texld r3, t4, s0
texld r4, t7, s0
texld r5, t0, s0
texld r7, t5, s0
texld r6, t6, s0
dp3_pp r6.x, r6, c1
dp3_pp r7.x, r7, c1
dp3_pp r4.x, r4, c1
dp3_pp r3.x, r3, c1
dp3_pp r6.y, r0, c1
mov r6.z, r4.x
dp3_pp r4.y, r2, c1
mov r4.z, r3.x
dp3_pp r3.y, r1, c1
mov r3.z, r7.x
dp3 r1.x, r3, c1.w
dp3 r0.x, r6, c1.w
add r2.y, r0.x, -r1.x
mov_pp r0.z, c1.w
mov r7.z, r6.x
dp3_pp r7.y, r5, c1
dp3 r5.x, r7, c1.w
dp3 r2.x, r4, c1.w
add r2.x, r2, -r5
mul r0.xy, r2, c0.x
mad_pp r1.xyz, r0, c2.x, c2.x
dp3_pp r0.x, r1, r1
rsq_pp r0.x, r0.x
mov_pp r0.w, c1
mul_pp r0.xyz, r0.x, r1
mov_pp oC0, r0
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
ConstBuffer "$Globals" 48
Float 36 [_BlurRadius]
BindCB  "$Globals" 0
"ps_4_0
eefiecedkmfdmjnoaglnadmckmmibmakjafjppiaabaaaaaaheagaaaaadaaaaaa
cmaaaaaacmabaaaagaabaaaaejfdeheopiaaaaaaajaaaaaaaiaaaaaaoaaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaomaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaaomaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaa
adadaaaaomaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaadadaaaaomaaaaaa
adaaaaaaaaaaaaaaadaaaaaaaeaaaaaaadadaaaaomaaaaaaaeaaaaaaaaaaaaaa
adaaaaaaafaaaaaaadadaaaaomaaaaaaafaaaaaaaaaaaaaaadaaaaaaagaaaaaa
adadaaaaomaaaaaaagaaaaaaaaaaaaaaadaaaaaaahaaaaaaadadaaaaomaaaaaa
ahaaaaaaaaaaaaaaadaaaaaaaiaaaaaaadadaaaafdfgfpfagphdgjhegjgpgoaa
feeffiedepepfceeaaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklkl
fdeieefcamafaaaaeaaaaaaaedabaaaafjaaaaaeegiocaaaaaaaaaaaadaaaaaa
fkaaaaadaagabaaaaaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaad
dcbabaaaabaaaaaagcbaaaaddcbabaaaacaaaaaagcbaaaaddcbabaaaadaaaaaa
gcbaaaaddcbabaaaaeaaaaaagcbaaaaddcbabaaaafaaaaaagcbaaaaddcbabaaa
agaaaaaagcbaaaaddcbabaaaahaaaaaagcbaaaaddcbabaaaaiaaaaaagfaaaaad
pccabaaaaaaaaaaagiaaaaacaeaaaaaaefaaaaajpcaabaaaaaaaaaaaegbabaaa
adaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaabaaaaaakccaabaaaaaaaaaaa
egacbaaaaaaaaaaaaceaaaaakoehgbdopepndedphdgijbdnaaaaaaaaefaaaaaj
pcaabaaaabaaaaaaegbabaaaafaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
baaaaaakecaabaaaabaaaaaaegacbaaaabaaaaaaaceaaaaakoehgbdopepndedp
hdgijbdnaaaaaaaadgaaaaafbcaabaaaaaaaaaaackaabaaaabaaaaaaefaaaaaj
pcaabaaaacaaaaaaegbabaaaagaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
baaaaaakecaabaaaaaaaaaaaegacbaaaacaaaaaaaceaaaaakoehgbdopepndedp
hdgijbdnaaaaaaaabaaaaaakicaabaaaaaaaaaaaaceaaaaaaaaaiadpaaaaiadp
aaaaiadpaaaaaaaaegacbaaaaaaaaaaaefaaaaajpcaabaaaacaaaaaaegbabaaa
aeaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaabaaaaaakccaabaaaacaaaaaa
egacbaaaacaaaaaaaceaaaaakoehgbdopepndedphdgijbdnaaaaaaaaefaaaaaj
pcaabaaaadaaaaaaegbabaaaahaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
baaaaaakccaabaaaaaaaaaaaegacbaaaadaaaaaaaceaaaaakoehgbdopepndedp
hdgijbdnaaaaaaaadgaaaaafbcaabaaaacaaaaaabkaabaaaaaaaaaaaefaaaaaj
pcaabaaaadaaaaaaegbabaaaaiaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
baaaaaakbcaabaaaabaaaaaaegacbaaaadaaaaaaaceaaaaakoehgbdopepndedp
hdgijbdnaaaaaaaadgaaaaafecaabaaaacaaaaaaakaabaaaabaaaaaabaaaaaak
icaabaaaabaaaaaaaceaaaaaaaaaiadpaaaaiadpaaaaiadpaaaaaaaaegacbaaa
acaaaaaaaaaaaaaiccaabaaaacaaaaaadkaabaiaebaaaaaaaaaaaaaadkaabaaa
abaaaaaaefaaaaajpcaabaaaadaaaaaaegbabaaaacaaaaaaeghobaaaaaaaaaaa
aagabaaaaaaaaaaabaaaaaakccaabaaaabaaaaaaegacbaaaadaaaaaaaceaaaaa
koehgbdopepndedphdgijbdnaaaaaaaabaaaaaakicaabaaaaaaaaaaaaceaaaaa
aaaaiadpaaaaiadpaaaaiadpaaaaaaaaegacbaaaabaaaaaaefaaaaajpcaabaaa
abaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaabaaaaaak
bcaabaaaaaaaaaaaegacbaaaabaaaaaaaceaaaaakoehgbdopepndedphdgijbdn
aaaaaaaabaaaaaakbcaabaaaaaaaaaaaaceaaaaaaaaaiadpaaaaiadpaaaaiadp
aaaaaaaaegacbaaaaaaaaaaaaaaaaaaibcaabaaaacaaaaaaakaabaiaebaaaaaa
aaaaaaaadkaabaaaaaaaaaaadiaaaaaidcaabaaaaaaaaaaaegaabaaaacaaaaaa
fgifcaaaaaaaaaaaacaaaaaadiaaaaakdcaabaaaaaaaaaaaegaabaaaaaaaaaaa
aceaaaaaaaaaaadpaaaaaadpaaaaaaaaaaaaaaaadgaaaaafecaabaaaaaaaaaaa
abeaaaaaaaaaaadpaaaaaaakhcaabaaaaaaaaaaaegacbaaaaaaaaaaaaceaaaaa
aaaaaadpaaaaaadpaaaaaadpaaaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaa
aaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaa
diaaaaahhccabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaaaaaaaaaadgaaaaaf
iccabaaaaaaaaaaaabeaaaaaaaaaiadpdoaaaaab"
}
}
 }
}
Fallback Off
}