–¬Shader "Hidden/CameraMotionBlur" {
Properties {
 _MainTex ("-", 2D) = "" {}
 _NoiseTex ("-", 2D) = "grey" {}
 _VelTex ("-", 2D) = "black" {}
 _NeighbourMaxTex ("-", 2D) = "black" {}
}
SubShader { 
 Pass {
  ZTest Always
  Cull Off
  Fog { Mode Off }
Program "vp" {
SubProgram "opengl " {
"!!GLSL
#ifdef VERTEX

varying vec2 xlv_TEXCOORD0;
void main ()
{
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = gl_MultiTexCoord0.xy;
}


#endif
#ifdef FRAGMENT
uniform sampler2D _CameraDepthTexture;
uniform vec4 _MainTex_TexelSize;
uniform mat4 _ToPrevViewProjCombined;
uniform float _VelocityScale;
uniform float _MaxVelocity;
varying vec2 xlv_TEXCOORD0;
void main ()
{
  vec2 vel_1;
  vec4 prevClipPos_2;
  vec3 tmpvar_3;
  tmpvar_3.xy = ((xlv_TEXCOORD0 * vec2(2.0, 2.0)) - vec2(1.0, 1.0));
  tmpvar_3.z = texture2D (_CameraDepthTexture, xlv_TEXCOORD0).x;
  vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = tmpvar_3;
  vec4 tmpvar_5;
  tmpvar_5 = (_ToPrevViewProjCombined * tmpvar_4);
  prevClipPos_2.w = tmpvar_5.w;
  prevClipPos_2.xyz = (tmpvar_5.xyz / tmpvar_5.w);
  vec2 tmpvar_6;
  tmpvar_6 = ((_VelocityScale * (tmpvar_3.xy - prevClipPos_2.xy)) / 2.0);
  vel_1 = tmpvar_6;
  float tmpvar_7;
  vec2 x_8;
  x_8 = (_MainTex_TexelSize.xy * _MaxVelocity);
  tmpvar_7 = sqrt(dot (x_8, x_8));
  float tmpvar_9;
  tmpvar_9 = sqrt(dot (tmpvar_6, tmpvar_6));
  if ((tmpvar_9 > tmpvar_7)) {
    vel_1 = (normalize(tmpvar_6) * tmpvar_7);
  };
  vec4 tmpvar_10;
  tmpvar_10.zw = vec2(0.0, 0.0);
  tmpvar_10.xy = vel_1;
  gl_FragData[0] = tmpvar_10;
}


#endif
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
"vs_3_0
dcl_position o0
dcl_texcoord0 o1
dcl_position0 v0
dcl_texcoord0 v1
mov o1.xy, v1
dp4 o0.w, v0, c3
dp4 o0.z, v0, c2
dp4 o0.y, v0, c1
dp4 o0.x, v0, c0
"
}
SubProgram "d3d11 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
BindCB  "UnityPerDraw" 0
"vs_4_0
eefiecedgcclnnbgpijgpddakojponflfpghdgniabaaaaaaoeabaaaaadaaaaaa
cmaaaaaaiaaaaaaaniaaaaaaejfdeheoemaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaaebaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafaepfdejfeejepeoaafeeffiedepepfceeaaklkl
epfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaa
fdfgfpfagphdgjhegjgpgoaafeeffiedepepfceeaaklklklfdeieefcaeabaaaa
eaaaabaaebaaaaaafjaaaaaeegiocaaaaaaaaaaaaeaaaaaafpaaaaadpcbabaaa
aaaaaaaafpaaaaaddcbabaaaabaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaa
gfaaaaaddccabaaaabaaaaaagiaaaaacabaaaaaadiaaaaaipcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiocaaaaaaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaa
egiocaaaaaaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaak
pcaabaaaaaaaaaaaegiocaaaaaaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaa
aaaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaaaaaaaaaaadaaaaaapgbpbaaa
aaaaaaaaegaobaaaaaaaaaaadgaaaaafdccabaaaabaaaaaaegbabaaaabaaaaaa
doaaaaab"
}
}
Program "fp" {
SubProgram "opengl " {
"!!GLSL"
}
SubProgram "d3d9 " {
Matrix 0 [_ToPrevViewProjCombined]
Vector 4 [_MainTex_TexelSize]
Float 5 [_VelocityScale]
Float 6 [_MaxVelocity]
SetTexture 0 [_CameraDepthTexture] 2D 0
"ps_3_0
dcl_2d s0
def c7, 2.00000000, -1.00000000, 1.00000000, 0.50000000
def c8, 0.00000000, 0, 0, 0
dcl_texcoord0 v0.xy
add r0.y, -v0, c7.z
mov r0.w, c7.z
cmp r0.y, c4, v0, r0
mov r0.x, v0
texld r0.x, r0, s0
mov r0.z, r0.x
mad r0.x, v0, c7, c7.y
mad r0.y, v0, c7.x, c7
dp4 r1.x, r0, c3
rcp r1.z, r1.x
dp4 r1.y, r0, c1
dp4 r1.x, r0, c0
mad r0.xy, -r1, r1.z, r0
mul r0.xy, r0, c5.x
mul r0.xy, r0, c7.w
mul r1.xy, r0, r0
add r1.x, r1, r1.y
mov r0.zw, c4.xyxy
mul r0.zw, c6.x, r0
mul r0.zw, r0, r0
add r1.y, r0.z, r0.w
rsq r1.x, r1.x
mul r0.zw, r1.x, r0.xyxy
rsq r1.y, r1.y
rcp r1.y, r1.y
rcp r1.x, r1.x
mul r0.zw, r1.y, r0
add r1.x, r1, -r1.y
cmp oC0.xy, -r1.x, r0, r0.zwzw
mov oC0.zw, c8.x
"
}
SubProgram "d3d11 " {
SetTexture 0 [_CameraDepthTexture] 2D 0
ConstBuffer "$Globals" 320
Matrix 208 [_ToPrevViewProjCombined]
Vector 32 [_MainTex_TexelSize]
Float 272 [_VelocityScale]
Float 280 [_MaxVelocity]
BindCB  "$Globals" 0
"ps_4_0
eefiecedmejiaffgollhjgihjgfdmepdgfbkladgabaaaaaacaaeaaaaadaaaaaa
cmaaaaaaieaaaaaaliaaaaaaejfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcgaadaaaa
eaaaaaaaniaaaaaafjaaaaaeegiocaaaaaaaaaaabcaaaaaafkaaaaadaagabaaa
aaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaa
gfaaaaadpccabaaaaaaaaaaagiaaaaacacaaaaaadbaaaaaibcaabaaaaaaaaaaa
bkiacaaaaaaaaaaaacaaaaaaabeaaaaaaaaaaaaaaaaaaaaiccaabaaaaaaaaaaa
bkbabaiaebaaaaaaabaaaaaaabeaaaaaaaaaiadpdhaaaaajccaabaaaaaaaaaaa
akaabaaaaaaaaaaabkaabaaaaaaaaaaabkbabaaaabaaaaaadgaaaaafbcaabaaa
aaaaaaaaakbabaaaabaaaaaaefaaaaajpcaabaaaaaaaaaaaegaabaaaaaaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaadcaaaaapgcaabaaaaaaaaaaaagbbbaaa
abaaaaaaaceaaaaaaaaaaaaaaaaaaaeaaaaaaaeaaaaaaaaaaceaaaaaaaaaaaaa
aaaaialpaaaaialpaaaaaaaadiaaaaaihcaabaaaabaaaaaakgakbaaaaaaaaaaa
egidcaaaaaaaaaaaaoaaaaaadcaaaaakhcaabaaaabaaaaaaegidcaaaaaaaaaaa
anaaaaaafgafbaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaabaaaaaa
egidcaaaaaaaaaaaapaaaaaaagaabaaaaaaaaaaaegacbaaaabaaaaaaaaaaaaai
hcaabaaaabaaaaaaegacbaaaabaaaaaaegidcaaaaaaaaaaabaaaaaaaaoaaaaah
jcaabaaaaaaaaaaaagaebaaaabaaaaaakgakbaaaabaaaaaaaaaaaaaidcaabaaa
aaaaaaaamgaabaiaebaaaaaaaaaaaaaajgafbaaaaaaaaaaadiaaaaaidcaabaaa
aaaaaaaaegaabaaaaaaaaaaaagiacaaaaaaaaaaabbaaaaaadiaaaaakdcaabaaa
aaaaaaaaegaabaaaaaaaaaaaaceaaaaaaaaaaadpaaaaaadpaaaaaaaaaaaaaaaa
apaaaaahecaabaaaaaaaaaaaegaabaaaaaaaaaaaegaabaaaaaaaaaaaeeaaaaaf
icaabaaaaaaaaaaackaabaaaaaaaaaaadiaaaaahdcaabaaaabaaaaaapgapbaaa
aaaaaaaaegaabaaaaaaaaaaadiaaaaajmcaabaaaabaaaaaaagiecaaaaaaaaaaa
acaaaaaakgikcaaaaaaaaaaabbaaaaaaapaaaaahicaabaaaaaaaaaaaogakbaaa
abaaaaaaogakbaaaabaaaaaaelaaaaafmcaabaaaaaaaaaaakgaobaaaaaaaaaaa
diaaaaahdcaabaaaabaaaaaapgapbaaaaaaaaaaaegaabaaaabaaaaaadbaaaaah
ecaabaaaaaaaaaaadkaabaaaaaaaaaaackaabaaaaaaaaaaadhaaaaajdccabaaa
aaaaaaaakgakbaaaaaaaaaaaegaabaaaabaaaaaaegaabaaaaaaaaaaadgaaaaai
mccabaaaaaaaaaaaaceaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaadoaaaaab
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

varying vec2 xlv_TEXCOORD0;
void main ()
{
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = gl_MultiTexCoord0.xy;
}


#endif
#ifdef FRAGMENT
uniform sampler2D _MainTex;
uniform float _DisplayVelocityScale;
varying vec2 xlv_TEXCOORD0;
void main ()
{
  vec4 tmpvar_1;
  vec4 cse_2;
  cse_2 = texture2D (_MainTex, xlv_TEXCOORD0);
  tmpvar_1.x = cse_2.x;
  tmpvar_1.y = abs(cse_2.y);
  tmpvar_1.zw = -(cse_2.xy);
  gl_FragData[0] = clamp ((tmpvar_1 * _DisplayVelocityScale), 0.0, 1.0);
}


#endif
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
"vs_3_0
dcl_position o0
dcl_texcoord0 o1
dcl_position0 v0
dcl_texcoord0 v1
mov o1.xy, v1
dp4 o0.w, v0, c3
dp4 o0.z, v0, c2
dp4 o0.y, v0, c1
dp4 o0.x, v0, c0
"
}
SubProgram "d3d11 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
BindCB  "UnityPerDraw" 0
"vs_4_0
eefiecedgcclnnbgpijgpddakojponflfpghdgniabaaaaaaoeabaaaaadaaaaaa
cmaaaaaaiaaaaaaaniaaaaaaejfdeheoemaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaaebaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafaepfdejfeejepeoaafeeffiedepepfceeaaklkl
epfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaa
fdfgfpfagphdgjhegjgpgoaafeeffiedepepfceeaaklklklfdeieefcaeabaaaa
eaaaabaaebaaaaaafjaaaaaeegiocaaaaaaaaaaaaeaaaaaafpaaaaadpcbabaaa
aaaaaaaafpaaaaaddcbabaaaabaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaa
gfaaaaaddccabaaaabaaaaaagiaaaaacabaaaaaadiaaaaaipcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiocaaaaaaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaa
egiocaaaaaaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaak
pcaabaaaaaaaaaaaegiocaaaaaaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaa
aaaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaaaaaaaaaaadaaaaaapgbpbaaa
aaaaaaaaegaobaaaaaaaaaaadgaaaaafdccabaaaabaaaaaaegbabaaaabaaaaaa
doaaaaab"
}
}
Program "fp" {
SubProgram "opengl " {
"!!GLSL"
}
SubProgram "d3d9 " {
Float 0 [_DisplayVelocityScale]
SetTexture 0 [_MainTex] 2D 0
"ps_3_0
dcl_2d s0
dcl_texcoord0 v0.xy
texld r0.xy, v0, s0
mov r0.z, r0.x
abs r0.w, r0.y
mov r0.y, -r0
mov r0.x, -r0
mul_sat oC0, r0.zwxy, c0.x
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
ConstBuffer "$Globals" 320
Float 276 [_DisplayVelocityScale]
BindCB  "$Globals" 0
"ps_4_0
eefiecedfcnlnfgfapelfmpflhlpgfncjhohnkalabaaaaaajmabaaaaadaaaaaa
cmaaaaaaieaaaaaaliaaaaaaejfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcnmaaaaaa
eaaaaaaadhaaaaaafjaaaaaeegiocaaaaaaaaaaabcaaaaaafkaaaaadaagabaaa
aaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaa
gfaaaaadpccabaaaaaaaaaaagiaaaaacacaaaaaaefaaaaajpcaabaaaaaaaaaaa
egbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaadgaaaaagccaabaaa
abaaaaaabkaabaiaibaaaaaaaaaaaaaadiaaaaakncaabaaaabaaaaaaagaebaaa
aaaaaaaaaceaaaaaaaaaiadpaaaaaaaaaaaaialpaaaaialpdicaaaaipccabaaa
aaaaaaaaegaobaaaabaaaaaafgifcaaaaaaaaaaabbaaaaaadoaaaaab"
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

varying vec2 xlv_TEXCOORD0;
void main ()
{
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = gl_MultiTexCoord0.xy;
}


#endif
#ifdef FRAGMENT
#extension GL_ARB_shader_texture_lod : enable
uniform float _MaxRadiusOrKInPaper;
uniform sampler2D _MainTex;
uniform vec4 _MainTex_TexelSize;
varying vec2 xlv_TEXCOORD0;
void main ()
{
  vec2 mx_2;
  vec2 uvCorner_3;
  vec2 tmpvar_4;
  tmpvar_4 = (xlv_TEXCOORD0 - (_MainTex_TexelSize.xy * (vec2(
    (_MaxRadiusOrKInPaper * 0.5)
  ) - vec2(0.5, 0.5))));
  uvCorner_3 = tmpvar_4;
  mx_2 = texture2D (_MainTex, tmpvar_4).xy;
  for (int j_1 = 0; float(j_1) <= (float(int(
(_MaxRadiusOrKInPaper - 1.0)
)) * (_MaxRadiusOrKInPaper - 1.0)); j_1++) {
    vec2 tmpvar_5;
    tmpvar_5.x = (fract((
      float(j_1)
     / _MaxRadiusOrKInPaper)) * _MaxRadiusOrKInPaper);
    tmpvar_5.y = float((j_1 / int(_MaxRadiusOrKInPaper)));
    vec4 tmpvar_6;
    tmpvar_6.zw = vec2(0.0, 0.0);
    tmpvar_6.xy = (uvCorner_3 + (tmpvar_5 * _MainTex_TexelSize.xy));
    vec4 tmpvar_7;
    tmpvar_7 = texture2DLod (_MainTex, tmpvar_6.xy, 0.0);
    vec2 b_8;
    b_8 = tmpvar_7.xy;
    float tmpvar_9;
    tmpvar_9 = dot (mx_2, mx_2);
    float tmpvar_10;
    tmpvar_10 = dot (tmpvar_7.xy, tmpvar_7.xy);
    vec2 tmpvar_11;
    if ((tmpvar_9 > tmpvar_10)) {
      tmpvar_11 = mx_2;
    } else {
      tmpvar_11 = b_8;
    };
    mx_2 = tmpvar_11;
  };
  vec4 tmpvar_12;
  tmpvar_12.zw = vec2(0.0, 0.0);
  tmpvar_12.xy = mx_2;
  gl_FragData[0] = tmpvar_12;
}


#endif
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
"vs_3_0
dcl_position o0
dcl_texcoord0 o1
dcl_position0 v0
dcl_texcoord0 v1
mov o1.xy, v1
dp4 o0.w, v0, c3
dp4 o0.z, v0, c2
dp4 o0.y, v0, c1
dp4 o0.x, v0, c0
"
}
SubProgram "d3d11 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
BindCB  "UnityPerDraw" 0
"vs_4_0
eefiecedgcclnnbgpijgpddakojponflfpghdgniabaaaaaaoeabaaaaadaaaaaa
cmaaaaaaiaaaaaaaniaaaaaaejfdeheoemaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaaebaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafaepfdejfeejepeoaafeeffiedepepfceeaaklkl
epfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaa
fdfgfpfagphdgjhegjgpgoaafeeffiedepepfceeaaklklklfdeieefcaeabaaaa
eaaaabaaebaaaaaafjaaaaaeegiocaaaaaaaaaaaaeaaaaaafpaaaaadpcbabaaa
aaaaaaaafpaaaaaddcbabaaaabaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaa
gfaaaaaddccabaaaabaaaaaagiaaaaacabaaaaaadiaaaaaipcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiocaaaaaaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaa
egiocaaaaaaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaak
pcaabaaaaaaaaaaaegiocaaaaaaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaa
aaaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaaaaaaaaaaadaaaaaapgbpbaaa
aaaaaaaaegaobaaaaaaaaaaadgaaaaafdccabaaaabaaaaaaegbabaaaabaaaaaa
doaaaaab"
}
}
Program "fp" {
SubProgram "opengl " {
"!!GLSL"
}
SubProgram "d3d9 " {
Float 0 [_MaxRadiusOrKInPaper]
Vector 1 [_MainTex_TexelSize]
SetTexture 0 [_MainTex] 2D 0
"ps_3_0
dcl_2d s0
def c2, 0.50000000, 0.00000000, -1.00000000, 1.00000000
defi i0, 255, 0, 1, 0
dcl_texcoord0 v0.xy
mov r0.x, c0
mad r0.x, r0, c2, -c2
mad r0.xy, -r0.x, c1, v0
texld r1.xy, r0, s0
mov r0.zw, r1.xyxy
mov r2.z, c2.y
loop aL, i0
mov r1.x, c0
add r1.x, c2.z, r1
abs r1.y, r1.x
frc r1.z, r1.y
add r1.y, r1, -r1.z
cmp r1.y, r1.x, r1, -r1
mul r1.x, r1.y, r1
break_gt r2.z, r1.x
abs r1.x, c0
frc r1.y, r1.x
add r1.x, r1, -r1.y
cmp r1.x, c0, r1, -r1
rcp r1.x, r1.x
mul r1.y, r2.z, r1.x
abs r1.z, r1.y
frc r1.w, r1.z
add r1.z, r1, -r1.w
cmp r1.y, r1, r1.z, -r1.z
rcp r1.x, c0.x
mul r1.x, r2.z, r1
frc r1.x, r1
mul r1.x, r1, c0
mov r1.z, c2.y
mad r1.xy, r1, c1, r0
texldl r1.xy, r1.xyzz, s0
mul r1.zw, r0, r0
mul r2.xy, r1, r1
add r2.x, r2, r2.y
add r1.z, r1, r1.w
add r1.z, r1, -r2.x
cmp r0.zw, -r1.z, r1.xyxy, r0
add r2.z, r2, c2.w
endloop
mov oC0.xy, r0.zwzw
mov oC0.zw, c2.y
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
ConstBuffer "$Globals" 320
Float 16 [_MaxRadiusOrKInPaper]
Vector 32 [_MainTex_TexelSize]
BindCB  "$Globals" 0
"ps_4_0
eefiecedacbpnecbpakipimofnecdkmgcbdlfibnabaaaaaaliaeaaaaadaaaaaa
cmaaaaaaieaaaaaaliaaaaaaejfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcpiadaaaa
eaaaaaaapoaaaaaafjaaaaaeegiocaaaaaaaaaaaadaaaaaafkaaaaadaagabaaa
aaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaa
gfaaaaadpccabaaaaaaaaaaagiaaaaacaeaaaaaadcaaaaakbcaabaaaaaaaaaaa
akiacaaaaaaaaaaaabaaaaaaabeaaaaaaaaaaadpabeaaaaaaaaaaalpdcaaaaal
dcaabaaaaaaaaaaaegiacaiaebaaaaaaaaaaaaaaacaaaaaaagaabaaaaaaaaaaa
egbabaaaabaaaaaaefaaaaajpcaabaaaabaaaaaaegaabaaaaaaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaaaaaaaaaiecaabaaaaaaaaaaaakiacaaaaaaaaaaa
abaaaaaaabeaaaaaaaaaialpedaaaaaficaabaaaaaaaaaaackaabaaaaaaaaaaa
diaaaaahecaabaaaaaaaaaaackaabaaaaaaaaaaadkaabaaaaaaaaaaablaaaaag
icaabaaaaaaaaaaaakiacaaaaaaaaaaaabaaaaaaceaaaaaiecaabaaaabaaaaaa
dkaabaaaaaaaaaaadkaabaiaebaaaaaaaaaaaaaadgaaaaafdcaabaaaacaaaaaa
egaabaaaabaaaaaadgaaaaaficaabaaaabaaaaaaabeaaaaaaaaaaaaadaaaaaab
claaaaafecaabaaaacaaaaaadkaabaaaabaaaaaadbaaaaahicaabaaaacaaaaaa
ckaabaaaaaaaaaaackaabaaaacaaaaaaadaaaeaddkaabaaaacaaaaaaaoaaaaai
ecaabaaaacaaaaaackaabaaaacaaaaaaakiacaaaaaaaaaaaabaaaaaabkaaaaaf
ecaabaaaacaaaaaackaabaaaacaaaaaadiaaaaaibcaabaaaadaaaaaackaabaaa
acaaaaaaakiacaaaaaaaaaaaabaaaaaafhaaaaahecaabaaaacaaaaaadkaabaaa
aaaaaaaadkaabaaaabaaaaaaceaaaaaiicaabaaaacaaaaaadkaabaaaabaaaaaa
dkaabaiaebaaaaaaabaaaaaaeoaaaaaiicaabaaaacaaaaaaaanaaaaadkaabaaa
acaaaaaackaabaaaabaaaaaaciaaaaafecaabaaaadaaaaaadkaabaaaacaaaaaa
abaaaaahecaabaaaacaaaaaackaabaaaacaaaaaaabeaaaaaaaaaaaiadhaaaaaj
ecaabaaaacaaaaaackaabaaaacaaaaaackaabaaaadaaaaaadkaabaaaacaaaaaa
claaaaafccaabaaaadaaaaaackaabaaaacaaaaaadcaaaaakmcaabaaaacaaaaaa
agaebaaaadaaaaaaagiecaaaaaaaaaaaacaaaaaaagaebaaaaaaaaaaaeiaaaaal
pcaabaaaadaaaaaaogakbaaaacaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
abeaaaaaaaaaaaaaapaaaaahecaabaaaacaaaaaaegaabaaaacaaaaaaegaabaaa
acaaaaaaapaaaaahicaabaaaacaaaaaaegaabaaaadaaaaaaegaabaaaadaaaaaa
dbaaaaahecaabaaaacaaaaaadkaabaaaacaaaaaackaabaaaacaaaaaadhaaaaaj
dcaabaaaacaaaaaakgakbaaaacaaaaaaegaabaaaacaaaaaaegaabaaaadaaaaaa
boaaaaahicaabaaaabaaaaaadkaabaaaabaaaaaaabeaaaaaabaaaaaabgaaaaab
dgaaaaafdccabaaaaaaaaaaaegaabaaaacaaaaaadgaaaaaimccabaaaaaaaaaaa
aceaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaadoaaaaab"
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

varying vec2 xlv_TEXCOORD0;
void main ()
{
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = gl_MultiTexCoord0.xy;
}


#endif
#ifdef FRAGMENT
uniform sampler2D _MainTex;
uniform vec4 _MainTex_TexelSize;
varying vec2 xlv_TEXCOORD0;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.9, 0.9) * _MainTex_TexelSize.xy)));
  vec2 tmpvar_2;
  tmpvar_2 = tmpvar_1.xy;
  vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.9, 0.0) * _MainTex_TexelSize.xy)));
  vec2 b_4;
  b_4 = tmpvar_3.xy;
  float tmpvar_5;
  tmpvar_5 = dot (tmpvar_1.xy, tmpvar_1.xy);
  float tmpvar_6;
  tmpvar_6 = dot (tmpvar_3.xy, tmpvar_3.xy);
  vec2 tmpvar_7;
  if ((tmpvar_5 > tmpvar_6)) {
    tmpvar_7 = tmpvar_2;
  } else {
    tmpvar_7 = b_4;
  };
  vec4 tmpvar_8;
  tmpvar_8 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.9, -0.9) * _MainTex_TexelSize.xy)));
  vec2 b_9;
  b_9 = tmpvar_8.xy;
  float tmpvar_10;
  tmpvar_10 = dot (tmpvar_7, tmpvar_7);
  float tmpvar_11;
  tmpvar_11 = dot (tmpvar_8.xy, tmpvar_8.xy);
  vec2 tmpvar_12;
  if ((tmpvar_10 > tmpvar_11)) {
    tmpvar_12 = tmpvar_7;
  } else {
    tmpvar_12 = b_9;
  };
  vec4 tmpvar_13;
  tmpvar_13 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, 0.9) * _MainTex_TexelSize.xy)));
  vec2 b_14;
  b_14 = tmpvar_13.xy;
  float tmpvar_15;
  tmpvar_15 = dot (tmpvar_12, tmpvar_12);
  float tmpvar_16;
  tmpvar_16 = dot (tmpvar_13.xy, tmpvar_13.xy);
  vec2 tmpvar_17;
  if ((tmpvar_15 > tmpvar_16)) {
    tmpvar_17 = tmpvar_12;
  } else {
    tmpvar_17 = b_14;
  };
  vec4 tmpvar_18;
  tmpvar_18 = texture2D (_MainTex, xlv_TEXCOORD0);
  vec2 b_19;
  b_19 = tmpvar_18.xy;
  float tmpvar_20;
  tmpvar_20 = dot (tmpvar_17, tmpvar_17);
  float tmpvar_21;
  tmpvar_21 = dot (tmpvar_18.xy, tmpvar_18.xy);
  vec2 tmpvar_22;
  if ((tmpvar_20 > tmpvar_21)) {
    tmpvar_22 = tmpvar_17;
  } else {
    tmpvar_22 = b_19;
  };
  vec4 tmpvar_23;
  tmpvar_23 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, -0.9) * _MainTex_TexelSize.xy)));
  vec2 b_24;
  b_24 = tmpvar_23.xy;
  float tmpvar_25;
  tmpvar_25 = dot (tmpvar_22, tmpvar_22);
  float tmpvar_26;
  tmpvar_26 = dot (tmpvar_23.xy, tmpvar_23.xy);
  vec2 tmpvar_27;
  if ((tmpvar_25 > tmpvar_26)) {
    tmpvar_27 = tmpvar_22;
  } else {
    tmpvar_27 = b_24;
  };
  vec4 tmpvar_28;
  tmpvar_28 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(-0.9, 0.9) * _MainTex_TexelSize.xy)));
  vec2 b_29;
  b_29 = tmpvar_28.xy;
  float tmpvar_30;
  tmpvar_30 = dot (tmpvar_27, tmpvar_27);
  float tmpvar_31;
  tmpvar_31 = dot (tmpvar_28.xy, tmpvar_28.xy);
  vec2 tmpvar_32;
  if ((tmpvar_30 > tmpvar_31)) {
    tmpvar_32 = tmpvar_27;
  } else {
    tmpvar_32 = b_29;
  };
  vec4 tmpvar_33;
  tmpvar_33 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(-0.9, 0.0) * _MainTex_TexelSize.xy)));
  vec2 b_34;
  b_34 = tmpvar_33.xy;
  float tmpvar_35;
  tmpvar_35 = dot (tmpvar_32, tmpvar_32);
  float tmpvar_36;
  tmpvar_36 = dot (tmpvar_33.xy, tmpvar_33.xy);
  vec2 tmpvar_37;
  if ((tmpvar_35 > tmpvar_36)) {
    tmpvar_37 = tmpvar_32;
  } else {
    tmpvar_37 = b_34;
  };
  vec4 tmpvar_38;
  tmpvar_38 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(-0.9, -0.9) * _MainTex_TexelSize.xy)));
  vec2 b_39;
  b_39 = tmpvar_38.xy;
  float tmpvar_40;
  tmpvar_40 = dot (tmpvar_37, tmpvar_37);
  float tmpvar_41;
  tmpvar_41 = dot (tmpvar_38.xy, tmpvar_38.xy);
  vec2 tmpvar_42;
  if ((tmpvar_40 > tmpvar_41)) {
    tmpvar_42 = tmpvar_37;
  } else {
    tmpvar_42 = b_39;
  };
  vec4 tmpvar_43;
  tmpvar_43.zw = vec2(0.0, 0.0);
  tmpvar_43.xy = tmpvar_42;
  gl_FragData[0] = tmpvar_43;
}


#endif
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
"vs_3_0
dcl_position o0
dcl_texcoord0 o1
dcl_position0 v0
dcl_texcoord0 v1
mov o1.xy, v1
dp4 o0.w, v0, c3
dp4 o0.z, v0, c2
dp4 o0.y, v0, c1
dp4 o0.x, v0, c0
"
}
SubProgram "d3d11 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
BindCB  "UnityPerDraw" 0
"vs_4_0
eefiecedgcclnnbgpijgpddakojponflfpghdgniabaaaaaaoeabaaaaadaaaaaa
cmaaaaaaiaaaaaaaniaaaaaaejfdeheoemaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaaebaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafaepfdejfeejepeoaafeeffiedepepfceeaaklkl
epfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaa
fdfgfpfagphdgjhegjgpgoaafeeffiedepepfceeaaklklklfdeieefcaeabaaaa
eaaaabaaebaaaaaafjaaaaaeegiocaaaaaaaaaaaaeaaaaaafpaaaaadpcbabaaa
aaaaaaaafpaaaaaddcbabaaaabaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaa
gfaaaaaddccabaaaabaaaaaagiaaaaacabaaaaaadiaaaaaipcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiocaaaaaaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaa
egiocaaaaaaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaak
pcaabaaaaaaaaaaaegiocaaaaaaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaa
aaaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaaaaaaaaaaadaaaaaapgbpbaaa
aaaaaaaaegaobaaaaaaaaaaadgaaaaafdccabaaaabaaaaaaegbabaaaabaaaaaa
doaaaaab"
}
}
Program "fp" {
SubProgram "opengl " {
"!!GLSL"
}
SubProgram "d3d9 " {
Vector 0 [_MainTex_TexelSize]
SetTexture 0 [_MainTex] 2D 0
"ps_3_0
dcl_2d s0
def c1, 0.89999998, 0.00000000, -0.89999998, 0
dcl_texcoord0 v0.xy
mov r0.zw, c0.xyxy
mad r1.xy, c1, r0.zwzw, v0
texld r1.xy, r1, s0
mul r1.zw, r1.xyxy, r1.xyxy
mov r0.xy, c0
mad r0.xy, c1.x, r0, v0
texld r0.xy, r0, s0
mul r0.zw, r0.xyxy, r0.xyxy
add r1.z, r1, r1.w
add r0.z, r0, r0.w
add r1.z, r0, -r1
cmp r1.xy, -r1.z, r1, r0
mov r0.zw, c0.xyxy
mad r0.xy, c1.xzzw, r0.zwzw, v0
mul r0.zw, r1.xyxy, r1.xyxy
texld r0.xy, r0, s0
add r2.z, r0, r0.w
mul r1.zw, r0.xyxy, r0.xyxy
mov r0.zw, c0.xyxy
mad r2.xy, c1.yxzw, r0.zwzw, v0
add r1.z, r1, r1.w
add r0.z, r2, -r1
cmp r0.zw, -r0.z, r0.xyxy, r1.xyxy
texld r2.xy, r2, s0
mul r1.xy, r2, r2
mul r0.xy, r0.zwzw, r0.zwzw
add r1.x, r1, r1.y
add r0.x, r0, r0.y
add r1.x, r0, -r1
cmp r1.xy, -r1.x, r2, r0.zwzw
texld r0.xy, v0, s0
mul r1.zw, r0.xyxy, r0.xyxy
mul r0.zw, r1.xyxy, r1.xyxy
add r1.z, r1, r1.w
add r0.z, r0, r0.w
add r1.z, r0, -r1
cmp r1.xy, -r1.z, r0, r1
mov r0.zw, c0.xyxy
mad r0.xy, c1.yzzw, r0.zwzw, v0
mul r0.zw, r1.xyxy, r1.xyxy
texld r0.xy, r0, s0
mul r1.zw, r0.xyxy, r0.xyxy
add r2.z, r0, r0.w
mov r0.zw, c0.xyxy
mad r2.xy, c1.zxzw, r0.zwzw, v0
add r1.z, r1, r1.w
add r0.z, r2, -r1
cmp r0.zw, -r0.z, r0.xyxy, r1.xyxy
texld r2.xy, r2, s0
mul r1.xy, r2, r2
mul r0.xy, r0.zwzw, r0.zwzw
add r1.x, r1, r1.y
add r0.x, r0, r0.y
add r1.x, r0, -r1
cmp r0.zw, -r1.x, r2.xyxy, r0
mul r1.xy, r0.zwzw, r0.zwzw
add r2.x, r1, r1.y
mov r0.xy, c0
mad r0.xy, c1.zyzw, r0, v0
texld r0.xy, r0, s0
mul r1.zw, r0.xyxy, r0.xyxy
add r1.z, r1, r1.w
add r1.z, r2.x, -r1
cmp r0.zw, -r1.z, r0.xyxy, r0
mov r1.xy, c0
mad r1.xy, c1.z, r1, v0
texld r1.xy, r1, s0
mul r1.zw, r1.xyxy, r1.xyxy
mul r0.xy, r0.zwzw, r0.zwzw
add r1.z, r1, r1.w
add r0.x, r0, r0.y
add r0.x, r0, -r1.z
cmp oC0.xy, -r0.x, r1, r0.zwzw
mov oC0.zw, c1.y
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
ConstBuffer "$Globals" 320
Vector 32 [_MainTex_TexelSize]
BindCB  "$Globals" 0
"ps_4_0
eefiecedcbbbcofddlifkodhfhacjadggjnklmfdabaaaaaaamahaaaaadaaaaaa
cmaaaaaaieaaaaaaliaaaaaaejfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcemagaaaa
eaaaaaaajdabaaaafjaaaaaeegiocaaaaaaaaaaaadaaaaaafkaaaaadaagabaaa
aaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaa
gfaaaaadpccabaaaaaaaaaaagiaaaaacadaaaaaadcaaaaanpcaabaaaaaaaaaaa
egiecaaaaaaaaaaaacaaaaaaaceaaaaaggggggdpggggggdpggggggdpaaaaaaaa
egbebaaaabaaaaaaefaaaaajpcaabaaaabaaaaaaegaabaaaaaaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaaaaaaaaaogakbaaaaaaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaaapaaaaahecaabaaaaaaaaaaaegaabaaa
abaaaaaaegaabaaaabaaaaaaapaaaaahicaabaaaaaaaaaaaegaabaaaaaaaaaaa
egaabaaaaaaaaaaadbaaaaahecaabaaaaaaaaaaadkaabaaaaaaaaaaackaabaaa
aaaaaaaadhaaaaajdcaabaaaaaaaaaaakgakbaaaaaaaaaaaegaabaaaabaaaaaa
egaabaaaaaaaaaaaapaaaaahecaabaaaaaaaaaaaegaabaaaaaaaaaaaegaabaaa
aaaaaaaadcaaaaanpcaabaaaabaaaaaaegiecaaaaaaaaaaaacaaaaaaaceaaaaa
ggggggdpgggggglpaaaaaaaaggggggdpegbebaaaabaaaaaaefaaaaajpcaabaaa
acaaaaaaegaabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaaj
pcaabaaaabaaaaaaogakbaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
apaaaaahicaabaaaaaaaaaaaegaabaaaacaaaaaaegaabaaaacaaaaaadbaaaaah
ecaabaaaaaaaaaaadkaabaaaaaaaaaaackaabaaaaaaaaaaadhaaaaajdcaabaaa
aaaaaaaakgakbaaaaaaaaaaaegaabaaaaaaaaaaaegaabaaaacaaaaaaapaaaaah
ecaabaaaaaaaaaaaegaabaaaaaaaaaaaegaabaaaaaaaaaaaapaaaaahicaabaaa
aaaaaaaaegaabaaaabaaaaaaegaabaaaabaaaaaadbaaaaahecaabaaaaaaaaaaa
dkaabaaaaaaaaaaackaabaaaaaaaaaaadhaaaaajdcaabaaaaaaaaaaakgakbaaa
aaaaaaaaegaabaaaaaaaaaaaegaabaaaabaaaaaaapaaaaahecaabaaaaaaaaaaa
egaabaaaaaaaaaaaegaabaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaaegbabaaa
abaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaapaaaaahicaabaaaaaaaaaaa
egaabaaaabaaaaaaegaabaaaabaaaaaadbaaaaahecaabaaaaaaaaaaadkaabaaa
aaaaaaaackaabaaaaaaaaaaadhaaaaajdcaabaaaaaaaaaaakgakbaaaaaaaaaaa
egaabaaaaaaaaaaaegaabaaaabaaaaaaapaaaaahecaabaaaaaaaaaaaegaabaaa
aaaaaaaaegaabaaaaaaaaaaadcaaaaanpcaabaaaabaaaaaaegiecaaaaaaaaaaa
acaaaaaaaceaaaaaaaaaaaaagggggglpgggggglpggggggdpegbebaaaabaaaaaa
efaaaaajpcaabaaaacaaaaaaegaabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaaefaaaaajpcaabaaaabaaaaaaogakbaaaabaaaaaaeghobaaaaaaaaaaa
aagabaaaaaaaaaaaapaaaaahicaabaaaaaaaaaaaegaabaaaacaaaaaaegaabaaa
acaaaaaadbaaaaahecaabaaaaaaaaaaadkaabaaaaaaaaaaackaabaaaaaaaaaaa
dhaaaaajdcaabaaaaaaaaaaakgakbaaaaaaaaaaaegaabaaaaaaaaaaaegaabaaa
acaaaaaaapaaaaahecaabaaaaaaaaaaaegaabaaaaaaaaaaaegaabaaaaaaaaaaa
apaaaaahicaabaaaaaaaaaaaegaabaaaabaaaaaaegaabaaaabaaaaaadbaaaaah
ecaabaaaaaaaaaaadkaabaaaaaaaaaaackaabaaaaaaaaaaadhaaaaajdcaabaaa
aaaaaaaakgakbaaaaaaaaaaaegaabaaaaaaaaaaaegaabaaaabaaaaaaapaaaaah
ecaabaaaaaaaaaaaegaabaaaaaaaaaaaegaabaaaaaaaaaaadcaaaaanpcaabaaa
abaaaaaaegiecaaaaaaaaaaaacaaaaaaaceaaaaagggggglpaaaaaaaagggggglp
gggggglpegbebaaaabaaaaaaefaaaaajpcaabaaaacaaaaaaegaabaaaabaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaaogakbaaa
abaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaapaaaaahicaabaaaaaaaaaaa
egaabaaaacaaaaaaegaabaaaacaaaaaadbaaaaahecaabaaaaaaaaaaadkaabaaa
aaaaaaaackaabaaaaaaaaaaadhaaaaajdcaabaaaaaaaaaaakgakbaaaaaaaaaaa
egaabaaaaaaaaaaaegaabaaaacaaaaaaapaaaaahecaabaaaaaaaaaaaegaabaaa
aaaaaaaaegaabaaaaaaaaaaaapaaaaahicaabaaaaaaaaaaaegaabaaaabaaaaaa
egaabaaaabaaaaaadbaaaaahecaabaaaaaaaaaaadkaabaaaaaaaaaaackaabaaa
aaaaaaaadhaaaaajdccabaaaaaaaaaaakgakbaaaaaaaaaaaegaabaaaaaaaaaaa
egaabaaaabaaaaaadgaaaaaimccabaaaaaaaaaaaaceaaaaaaaaaaaaaaaaaaaaa
aaaaaaaaaaaaaaaadoaaaaab"
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

varying vec2 xlv_TEXCOORD0;
void main ()
{
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = gl_MultiTexCoord0.xy;
}


#endif
#ifdef FRAGMENT
uniform vec4 _ZBufferParams;
uniform sampler2D _MainTex;
uniform sampler2D _CameraDepthTexture;
uniform sampler2D _VelTex;
uniform sampler2D _NeighbourMaxTex;
uniform sampler2D _NoiseTex;
uniform vec4 _MainTex_TexelSize;
uniform float _SoftZDistance;
varying vec2 xlv_TEXCOORD0;
void main ()
{
  vec4 sum_2;
  float weight_3;
  float j_4;
  float zx_5;
  vec2 vx_6;
  vec2 vn_7;
  vec2 x_8;
  x_8 = xlv_TEXCOORD0;
  vn_7 = texture2D (_NeighbourMaxTex, xlv_TEXCOORD0).xy;
  vec4 tmpvar_9;
  tmpvar_9 = texture2D (_VelTex, xlv_TEXCOORD0);
  vx_6 = tmpvar_9.xy;
  zx_5 = -((1.0/((
    (_ZBufferParams.x * texture2D (_CameraDepthTexture, xlv_TEXCOORD0).x)
   + _ZBufferParams.y))));
  j_4 = (((texture2D (_NoiseTex, 
    (xlv_TEXCOORD0 * 11.0)
  ).x * 2.0) - 1.0) * 0.125);
  vec2 x_10;
  x_10 = (tmpvar_9.xy * _MainTex_TexelSize.zw);
  float tmpvar_11;
  tmpvar_11 = (1.0/((1.0 + sqrt(
    dot (x_10, x_10)
  ))));
  weight_3 = tmpvar_11;
  sum_2 = (texture2D (_MainTex, xlv_TEXCOORD0) * tmpvar_11);
  for (int l_1 = 0; l_1 < 13; l_1++) {
    float contrib_12;
    contrib_12 = 1.0;
    if ((l_1 == 6)) {
      contrib_12 = 0.0;
    };
    vec2 tmpvar_13;
    tmpvar_13 = (x_8 + (vn_7 * mix (-1.0, 1.0, 
      (((float(l_1) + j_4) + 1.0) / 14.0)
    )));
    vec4 tmpvar_14;
    tmpvar_14 = texture2D (_VelTex, tmpvar_13);
    float tmpvar_15;
    tmpvar_15 = -((1.0/((
      (_ZBufferParams.x * texture2D (_CameraDepthTexture, tmpvar_13).x)
     + _ZBufferParams.y))));
    vec2 x_16;
    x_16 = (tmpvar_13 - x_8);
    vec2 x_17;
    x_17 = (x_8 - tmpvar_13);
    float tmpvar_18;
    tmpvar_18 = sqrt(dot (tmpvar_14.xy, tmpvar_14.xy));
    vec2 x_19;
    x_19 = (tmpvar_13 - x_8);
    float edge0_20;
    edge0_20 = (0.95 * tmpvar_18);
    float tmpvar_21;
    tmpvar_21 = clamp (((
      sqrt(dot (x_19, x_19))
     - edge0_20) / (
      (1.05 * tmpvar_18)
     - edge0_20)), 0.0, 1.0);
    float tmpvar_22;
    tmpvar_22 = sqrt(dot (vx_6, vx_6));
    vec2 x_23;
    x_23 = (x_8 - tmpvar_13);
    float edge0_24;
    edge0_24 = (0.95 * tmpvar_22);
    float tmpvar_25;
    tmpvar_25 = clamp (((
      sqrt(dot (x_23, x_23))
     - edge0_24) / (
      (1.05 * tmpvar_22)
     - edge0_24)), 0.0, 1.0);
    float tmpvar_26;
    tmpvar_26 = (((
      clamp ((1.0 - ((zx_5 - tmpvar_15) / _SoftZDistance)), 0.0, 1.0)
     * 
      clamp ((1.0 - (sqrt(
        dot (x_16, x_16)
      ) / sqrt(
        dot (tmpvar_14.xy, tmpvar_14.xy)
      ))), 0.0, 1.0)
    ) + (
      clamp ((1.0 - ((tmpvar_15 - zx_5) / _SoftZDistance)), 0.0, 1.0)
     * 
      clamp ((1.0 - (sqrt(
        dot (x_17, x_17)
      ) / sqrt(
        dot (vx_6, vx_6)
      ))), 0.0, 1.0)
    )) + ((
      (1.0 - (tmpvar_21 * (tmpvar_21 * (3.0 - 
        (2.0 * tmpvar_21)
      ))))
     * 
      (1.0 - (tmpvar_25 * (tmpvar_25 * (3.0 - 
        (2.0 * tmpvar_25)
      ))))
    ) * 2.0));
    sum_2 = (sum_2 + ((texture2D (_MainTex, tmpvar_13) * tmpvar_26) * contrib_12));
    weight_3 = (weight_3 + (tmpvar_26 * contrib_12));
  };
  vec4 tmpvar_27;
  tmpvar_27 = (sum_2 / weight_3);
  sum_2 = tmpvar_27;
  gl_FragData[0] = tmpvar_27;
}


#endif
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
"vs_3_0
dcl_position o0
dcl_texcoord0 o1
dcl_position0 v0
dcl_texcoord0 v1
mov o1.xy, v1
dp4 o0.w, v0, c3
dp4 o0.z, v0, c2
dp4 o0.y, v0, c1
dp4 o0.x, v0, c0
"
}
SubProgram "d3d11 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
BindCB  "UnityPerDraw" 0
"vs_4_0
eefiecedgcclnnbgpijgpddakojponflfpghdgniabaaaaaaoeabaaaaadaaaaaa
cmaaaaaaiaaaaaaaniaaaaaaejfdeheoemaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaaebaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafaepfdejfeejepeoaafeeffiedepepfceeaaklkl
epfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaa
fdfgfpfagphdgjhegjgpgoaafeeffiedepepfceeaaklklklfdeieefcaeabaaaa
eaaaabaaebaaaaaafjaaaaaeegiocaaaaaaaaaaaaeaaaaaafpaaaaadpcbabaaa
aaaaaaaafpaaaaaddcbabaaaabaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaa
gfaaaaaddccabaaaabaaaaaagiaaaaacabaaaaaadiaaaaaipcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiocaaaaaaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaa
egiocaaaaaaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaak
pcaabaaaaaaaaaaaegiocaaaaaaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaa
aaaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaaaaaaaaaaadaaaaaapgbpbaaa
aaaaaaaaegaobaaaaaaaaaaadgaaaaafdccabaaaabaaaaaaegbabaaaabaaaaaa
doaaaaab"
}
}
Program "fp" {
SubProgram "opengl " {
"!!GLSL"
}
SubProgram "d3d9 " {
Vector 0 [_ZBufferParams]
Vector 1 [_MainTex_TexelSize]
Float 2 [_SoftZDistance]
SetTexture 0 [_NeighbourMaxTex] 2D 0
SetTexture 1 [_MainTex] 2D 1
SetTexture 2 [_VelTex] 2D 2
SetTexture 3 [_CameraDepthTexture] 2D 3
SetTexture 4 [_NoiseTex] 2D 4
"ps_3_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
def c3, 1.00000000, 11.00000000, 2.00000000, -1.00000000
def c4, 0.12500000, 0.00000000, 0.14285715, -1.00000000
defi i0, 13, 0, 1, 0
def c5, 0.94999999, 1.04999995, 2.00000000, 3.00000000
def c6, -6.00000000, 0.00000000, 1.00000000, 0
dcl_texcoord0 v0.xy
add r0.x, -v0.y, c3
cmp r1.y, c1, v0, r0.x
mov r1.x, v0
texld r0.xy, r1, s2
mul r0.zw, r0.xyxy, c1
texld r1.xy, r1, s0
mov r3.xy, r0
mul r0.zw, r0, r0
add r0.z, r0, r0.w
rsq r0.z, r0.z
rcp r0.z, r0.z
add r0.z, r0, c3.x
mov r1.zw, r1.xyxy
mul r1.xy, v0, c3.y
texld r0.x, v0, s3
texld r1.x, r1, s4
mad r0.x, r0, c0, c0.y
mad r0.y, r1.x, c3.z, c3.w
rcp r0.x, r0.x
texld r2, v0, s1
rcp r3.z, r0.z
mul r2, r3.z, r2
mul r3.w, r0.y, c4.x
mov r4.x, -r0
mov r4.y, c4
loop aL, i0
add r0.x, r4.y, r3.w
add r0.x, r0, c3
mad r0.x, r0, c4.z, c4.w
mul r0.zw, r1, r0.x
add r1.xy, v0, r0.zwzw
add r0.y, -r1, c3.x
mov r0.x, r1
cmp r0.y, c1, r1, r0
texld r0.xy, r0, s2
mul r0.xy, r0, r0
add r0.x, r0, r0.y
rsq r4.z, r0.x
rcp r4.w, r4.z
mul r5.x, r4.w, c5
mad r5.x, r4.w, c5.y, -r5
mul r0.xy, r3, r3
add r0.x, r0, r0.y
rcp r5.y, r5.x
rsq r5.x, r0.x
mul r0.xy, r0.zwzw, r0.zwzw
add r0.x, r0, r0.y
rcp r0.z, r5.x
mul r0.y, r0.z, c5.x
mad r0.w, r0.z, c5.y, -r0.y
rsq r0.x, r0.x
rcp r0.y, r0.x
mad r0.x, -r0.z, c5, r0.y
rcp r0.w, r0.w
mul_sat r0.z, r0.x, r0.w
mad r0.x, -r4.w, c5, r0.y
mad r0.w, -r0.z, c5.z, c5
mul r0.z, r0, r0
mad r0.w, -r0.z, r0, c3.x
mul_sat r0.x, r0, r5.y
mad r0.z, -r0.x, c5, c5.w
mul r0.x, r0, r0
mad r0.x, -r0, r0.z, c3
mul r0.z, r0.x, r0.w
texld r0.x, r1, s3
mad r0.x, r0, c0, c0.y
rcp r0.x, r0.x
mad_sat r0.w, r4.z, -r0.y, c3.x
mad_sat r4.w, -r0.y, r5.x, c3.x
rcp r0.y, c2.x
add r0.x, r4, r0
mad_sat r4.z, r0.x, r0.y, c3.x
mad_sat r0.x, -r0, r0.y, c3
mul r4.z, r4, r4.w
add r0.y, r4, c6.x
mad r0.x, r0, r0.w, r4.z
mad r4.z, r0, c3, r0.x
abs r4.w, r0.y
texld r0, r1, s1
cmp r1.x, -r4.w, c6.y, c6.z
mul r0, r0, r4.z
mad r2, r0, r1.x, r2
mad r3.z, r4, r1.x, r3
add r4.y, r4, c3.x
endloop
rcp r0.x, r3.z
mul oC0, r2, r0.x
"
}
SubProgram "d3d11 " {
SetTexture 0 [_NeighbourMaxTex] 2D 3
SetTexture 1 [_MainTex] 2D 0
SetTexture 2 [_VelTex] 2D 2
SetTexture 3 [_CameraDepthTexture] 2D 1
SetTexture 4 [_NoiseTex] 2D 4
ConstBuffer "$Globals" 320
Vector 32 [_MainTex_TexelSize]
Float 304 [_SoftZDistance]
ConstBuffer "UnityPerCamera" 128
Vector 112 [_ZBufferParams]
BindCB  "$Globals" 0
BindCB  "UnityPerCamera" 1
"ps_4_0
eefiecedjgjmggekfoclmlbdodaeoaphninnabfcabaaaaaahialaaaaadaaaaaa
cmaaaaaaieaaaaaaliaaaaaaejfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcliakaaaa
eaaaaaaakoacaaaafjaaaaaeegiocaaaaaaaaaaabeaaaaaafjaaaaaeegiocaaa
abaaaaaaaiaaaaaafkaaaaadaagabaaaaaaaaaaafkaaaaadaagabaaaabaaaaaa
fkaaaaadaagabaaaacaaaaaafkaaaaadaagabaaaadaaaaaafkaaaaadaagabaaa
aeaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaafibiaaaeaahabaaaabaaaaaa
ffffaaaafibiaaaeaahabaaaacaaaaaaffffaaaafibiaaaeaahabaaaadaaaaaa
ffffaaaafibiaaaeaahabaaaaeaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaa
gfaaaaadpccabaaaaaaaaaaagiaaaaacajaaaaaadbaaaaaibcaabaaaaaaaaaaa
bkiacaaaaaaaaaaaacaaaaaaabeaaaaaaaaaaaaaaaaaaaaiccaabaaaaaaaaaaa
bkbabaiaebaaaaaaabaaaaaaabeaaaaaaaaaiadpdhaaaaajccaabaaaabaaaaaa
akaabaaaaaaaaaaabkaabaaaaaaaaaaabkbabaaaabaaaaaadgaaaaafbcaabaaa
abaaaaaaakbabaaaabaaaaaaefaaaaajpcaabaaaacaaaaaaegaabaaaabaaaaaa
eghobaaaaaaaaaaaaagabaaaadaaaaaaefaaaaajpcaabaaaadaaaaaaegbabaaa
abaaaaaaeghobaaaabaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaa
egaabaaaabaaaaaaeghobaaaacaaaaaaaagabaaaacaaaaaaefaaaaajpcaabaaa
aeaaaaaaegbabaaaabaaaaaaeghobaaaadaaaaaaaagabaaaabaaaaaadcaaaaal
ccaabaaaaaaaaaaaakiacaaaabaaaaaaahaaaaaaakaabaaaaeaaaaaabkiacaaa
abaaaaaaahaaaaaaaoaaaaakccaabaaaaaaaaaaaaceaaaaaaaaaiadpaaaaiadp
aaaaiadpaaaaiadpbkaabaaaaaaaaaaadiaaaaakmcaabaaaaaaaaaaaagbebaaa
abaaaaaaaceaaaaaaaaaaaaaaaaaaaaaaaaadaebaaaadaebefaaaaajpcaabaaa
aeaaaaaaogakbaaaaaaaaaaaeghobaaaaeaaaaaaaagabaaaaeaaaaaadcaaaaaj
ecaabaaaaaaaaaaaakaabaaaaeaaaaaaabeaaaaaaaaaaaeaabeaaaaaaaaaialp
diaaaaaimcaabaaaabaaaaaaagaebaaaabaaaaaakgiocaaaaaaaaaaaacaaaaaa
apaaaaahicaabaaaaaaaaaaaogakbaaaabaaaaaaogakbaaaabaaaaaaelaaaaaf
icaabaaaaaaaaaaadkaabaaaaaaaaaaaaaaaaaahicaabaaaaaaaaaaadkaabaaa
aaaaaaaaabeaaaaaaaaaiadpaoaaaaakicaabaaaaaaaaaaaaceaaaaaaaaaiadp
aaaaiadpaaaaiadpaaaaiadpdkaabaaaaaaaaaaadiaaaaahpcaabaaaadaaaaaa
pgapbaaaaaaaaaaaegaobaaaadaaaaaaapaaaaahbcaabaaaabaaaaaaegaabaaa
abaaaaaaegaabaaaabaaaaaaelaaaaafbcaabaaaabaaaaaaakaabaaaabaaaaaa
diaaaaahccaabaaaabaaaaaaakaabaaaabaaaaaaabeaaaaamimmmmdnaoaaaaak
ccaabaaaabaaaaaaaceaaaaaaaaaiadpaaaaiadpaaaaiadpaaaaiadpbkaabaaa
abaaaaaadgaaaaafpcaabaaaaeaaaaaaegaobaaaadaaaaaadgaaaaafecaabaaa
abaaaaaadkaabaaaaaaaaaaadgaaaaaficaabaaaabaaaaaaabeaaaaaaaaaaaaa
daaaaaabcbaaaaahecaabaaaacaaaaaadkaabaaaabaaaaaaabeaaaaaanaaaaaa
adaaaeadckaabaaaacaaaaaacaaaaaahecaabaaaacaaaaaadkaabaaaabaaaaaa
abeaaaaaagaaaaaabpaaaeadckaabaaaacaaaaaadgaaaaaficaabaaaabaaaaaa
abeaaaaaahaaaaaaahaaaaabbfaaaaabclaaaaafecaabaaaacaaaaaadkaabaaa
abaaaaaadcaaaaajecaabaaaacaaaaaackaabaaaaaaaaaaaabeaaaaaaaaaaado
ckaabaaaacaaaaaaaaaaaaahecaabaaaacaaaaaackaabaaaacaaaaaaabeaaaaa
aaaaiadpdcaaaaajecaabaaaacaaaaaackaabaaaacaaaaaaabeaaaaacfejbcdo
abeaaaaaaaaaialpdiaaaaahdcaabaaaafaaaaaakgakbaaaacaaaaaaegaabaaa
acaaaaaadcaaaaajdcaabaaaagaaaaaaegaabaaaacaaaaaakgakbaaaacaaaaaa
egbabaaaabaaaaaaaaaaaaaiecaabaaaacaaaaaabkaabaiaebaaaaaaagaaaaaa
abeaaaaaaaaaiadpdhaaaaajecaabaaaagaaaaaaakaabaaaaaaaaaaackaabaaa
acaaaaaabkaabaaaagaaaaaaefaaaaajpcaabaaaahaaaaaaigaabaaaagaaaaaa
eghobaaaacaaaaaaaagabaaaacaaaaaaefaaaaajpcaabaaaaiaaaaaaegaabaaa
agaaaaaaeghobaaaadaaaaaaaagabaaaabaaaaaadcaaaaalecaabaaaacaaaaaa
akiacaaaabaaaaaaahaaaaaaakaabaaaaiaaaaaabkiacaaaabaaaaaaahaaaaaa
aoaaaaakecaabaaaacaaaaaaaceaaaaaaaaaiadpaaaaiadpaaaaiadpaaaaiadp
ckaabaaaacaaaaaaaaaaaaaiicaabaaaacaaaaaabkaabaiaebaaaaaaaaaaaaaa
ckaabaaaacaaaaaaaoaaaaaiicaabaaaacaaaaaadkaabaaaacaaaaaaakiacaaa
aaaaaaaabdaaaaaaaaaaaaaiecaabaaaacaaaaaabkaabaaaaaaaaaaackaabaia
ebaaaaaaacaaaaaaaoaaaaaiecaabaaaacaaaaaackaabaaaacaaaaaaakiacaaa
aaaaaaaabdaaaaaaaacaaaalmcaabaaaacaaaaaakgaobaiaebaaaaaaacaaaaaa
aceaaaaaaaaaaaaaaaaaaaaaaaaaiadpaaaaiadpapaaaaahbcaabaaaafaaaaaa
egaabaaaafaaaaaaegaabaaaafaaaaaaapaaaaahccaabaaaafaaaaaaegaabaaa
ahaaaaaaegaabaaaahaaaaaaelaaaaafdcaabaaaafaaaaaaegaabaaaafaaaaaa
aoaaaaahecaabaaaafaaaaaaakaabaaaafaaaaaabkaabaaaafaaaaaaaaaaaaai
ecaabaaaafaaaaaackaabaiaebaaaaaaafaaaaaaabeaaaaaaaaaiadpdeaaaaah
ecaabaaaafaaaaaackaabaaaafaaaaaaabeaaaaaaaaaaaaaaaaaaaaimcaabaaa
agaaaaaaagaebaiaebaaaaaaagaaaaaaagbebaaaabaaaaaaapaaaaahicaabaaa
afaaaaaaogakbaaaagaaaaaaogakbaaaagaaaaaaelaaaaaficaabaaaafaaaaaa
dkaabaaaafaaaaaaaoaaaaahecaabaaaagaaaaaadkaabaaaafaaaaaaakaabaaa
abaaaaaaaaaaaaaiecaabaaaagaaaaaackaabaiaebaaaaaaagaaaaaaabeaaaaa
aaaaiadpdeaaaaahecaabaaaagaaaaaackaabaaaagaaaaaaabeaaaaaaaaaaaaa
diaaaaahecaabaaaacaaaaaackaabaaaacaaaaaackaabaaaagaaaaaadcaaaaaj
ecaabaaaacaaaaaadkaabaaaacaaaaaackaabaaaafaaaaaackaabaaaacaaaaaa
diaaaaahicaabaaaacaaaaaabkaabaaaafaaaaaaabeaaaaamimmmmdndcaaaaak
bcaabaaaafaaaaaabkaabaiaebaaaaaaafaaaaaaabeaaaaaddddhddpakaabaaa
afaaaaaaaoaaaaakicaabaaaacaaaaaaaceaaaaaaaaaiadpaaaaiadpaaaaiadp
aaaaiadpdkaabaaaacaaaaaadicaaaahicaabaaaacaaaaaadkaabaaaacaaaaaa
akaabaaaafaaaaaadcaaaaajbcaabaaaafaaaaaadkaabaaaacaaaaaaabeaaaaa
aaaaaamaabeaaaaaaaaaeaeadiaaaaahicaabaaaacaaaaaadkaabaaaacaaaaaa
dkaabaaaacaaaaaadcaaaaakicaabaaaacaaaaaaakaabaiaebaaaaaaafaaaaaa
dkaabaaaacaaaaaaabeaaaaaaaaaiadpdcaaaaakbcaabaaaafaaaaaaakaabaia
ebaaaaaaabaaaaaaabeaaaaaddddhddpdkaabaaaafaaaaaadicaaaahbcaabaaa
afaaaaaabkaabaaaabaaaaaaakaabaaaafaaaaaadcaaaaajccaabaaaafaaaaaa
akaabaaaafaaaaaaabeaaaaaaaaaaamaabeaaaaaaaaaeaeadiaaaaahbcaabaaa
afaaaaaaakaabaaaafaaaaaaakaabaaaafaaaaaadcaaaaakbcaabaaaafaaaaaa
bkaabaiaebaaaaaaafaaaaaaakaabaaaafaaaaaaabeaaaaaaaaaiadpapaaaaah
icaabaaaacaaaaaapgapbaaaacaaaaaaagaabaaaafaaaaaaaaaaaaahecaabaaa
acaaaaaadkaabaaaacaaaaaackaabaaaacaaaaaaefaaaaajpcaabaaaafaaaaaa
egaabaaaagaaaaaaeghobaaaabaaaaaaaagabaaaaaaaaaaadcaaaaajpcaabaaa
aeaaaaaaegaobaaaafaaaaaakgakbaaaacaaaaaaegaobaaaaeaaaaaaaaaaaaah
ecaabaaaabaaaaaackaabaaaabaaaaaackaabaaaacaaaaaaboaaaaahicaabaaa
abaaaaaadkaabaaaabaaaaaaabeaaaaaabaaaaaabgaaaaabaoaaaaahpccabaaa
aaaaaaaaegaobaaaaeaaaaaakgakbaaaabaaaaaadoaaaaab"
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

varying vec2 xlv_TEXCOORD0;
void main ()
{
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = gl_MultiTexCoord0.xy;
}


#endif
#ifdef FRAGMENT
uniform sampler2D _MainTex;
uniform sampler2D _VelTex;
varying vec2 xlv_TEXCOORD0;
void main ()
{
  vec4 sum_2;
  vec2 vx_3;
  vec2 x_4;
  x_4 = xlv_TEXCOORD0;
  vx_3 = texture2D (_VelTex, xlv_TEXCOORD0).xy;
  sum_2 = vec4(0.0, 0.0, 0.0, 0.0);
  for (int l_1 = 0; l_1 < 13; l_1++) {
    sum_2 = (sum_2 + texture2D (_MainTex, (x_4 - (vx_3 * 
      ((float(l_1) / 12.0) - 0.5)
    ))));
  };
  vec4 tmpvar_5;
  tmpvar_5 = (sum_2 / 13.0);
  sum_2 = tmpvar_5;
  gl_FragData[0] = tmpvar_5;
}


#endif
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
"vs_3_0
dcl_position o0
dcl_texcoord0 o1
dcl_position0 v0
dcl_texcoord0 v1
mov o1.xy, v1
dp4 o0.w, v0, c3
dp4 o0.z, v0, c2
dp4 o0.y, v0, c1
dp4 o0.x, v0, c0
"
}
SubProgram "d3d11 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
BindCB  "UnityPerDraw" 0
"vs_4_0
eefiecedgcclnnbgpijgpddakojponflfpghdgniabaaaaaaoeabaaaaadaaaaaa
cmaaaaaaiaaaaaaaniaaaaaaejfdeheoemaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaaebaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafaepfdejfeejepeoaafeeffiedepepfceeaaklkl
epfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaa
fdfgfpfagphdgjhegjgpgoaafeeffiedepepfceeaaklklklfdeieefcaeabaaaa
eaaaabaaebaaaaaafjaaaaaeegiocaaaaaaaaaaaaeaaaaaafpaaaaadpcbabaaa
aaaaaaaafpaaaaaddcbabaaaabaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaa
gfaaaaaddccabaaaabaaaaaagiaaaaacabaaaaaadiaaaaaipcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiocaaaaaaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaa
egiocaaaaaaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaak
pcaabaaaaaaaaaaaegiocaaaaaaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaa
aaaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaaaaaaaaaaadaaaaaapgbpbaaa
aaaaaaaaegaobaaaaaaaaaaadgaaaaafdccabaaaabaaaaaaegbabaaaabaaaaaa
doaaaaab"
}
}
Program "fp" {
SubProgram "opengl " {
"!!GLSL"
}
SubProgram "d3d9 " {
Vector 0 [_MainTex_TexelSize]
SetTexture 0 [_VelTex] 2D 0
SetTexture 1 [_MainTex] 2D 1
"ps_3_0
dcl_2d s0
dcl_2d s1
def c1, 1.00000000, 0.50000000, -0.50000000, -0.41666666
def c2, -0.33333331, -0.25000000, -0.16666666, -0.08333334
def c3, 0.08333331, 0.16666669, 0.25000000, 0.33333331
def c4, 0.41666669, 0.07692308, 0, 0
dcl_texcoord0 v0.xy
add r0.x, -v0.y, c1
cmp r1.y, c0, v0, r0.x
mov r1.x, v0
texld r0.xy, r1, s0
mad r1.xy, -r0, c1.z, v0
mad r2.xy, -r0, c1.w, v0
mad r3.xy, -r0, c2.x, v0
texld r2, r2, s1
texld r1, r1, s1
add r1, r1, r2
texld r2, r3, s1
mad r3.xy, -r0, c2.y, v0
add r1, r1, r2
texld r2, r3, s1
add r1, r1, r2
mad r3.xy, -r0, c2.z, v0
texld r2, r3, s1
mad r3.xy, -r0, c2.w, v0
add r2, r1, r2
texld r3, r3, s1
add r2, r2, r3
texld r1, v0, s1
add r2, r2, r1
mad r3.xy, -r0, c3.x, v0
texld r3, r3, s1
add r2, r2, r3
mad r1.xy, -r0, c3.y, v0
texld r1, r1, s1
add r2, r2, r1
mad r3.xy, -r0, c3.z, v0
mad r1.xy, -r0, c3.w, v0
texld r3, r3, s1
add r2, r2, r3
texld r1, r1, s1
add r1, r2, r1
mad r2.xy, -r0, c4.x, v0
mad r0.xy, -r0, c1.y, v0
texld r2, r2, s1
texld r0, r0, s1
add r1, r1, r2
add r0, r1, r0
mul oC0, r0, c4.y
"
}
SubProgram "d3d11 " {
SetTexture 0 [_VelTex] 2D 1
SetTexture 1 [_MainTex] 2D 0
ConstBuffer "$Globals" 320
Vector 32 [_MainTex_TexelSize]
BindCB  "$Globals" 0
"ps_4_0
eefiecedndnfgkihdojglmpadlgjkflmbadpnagmabaaaaaabiadaaaaadaaaaaa
cmaaaaaaieaaaaaaliaaaaaaejfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcfiacaaaa
eaaaaaaajgaaaaaafjaaaaaeegiocaaaaaaaaaaaadaaaaaafkaaaaadaagabaaa
aaaaaaaafkaaaaadaagabaaaabaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaa
fibiaaaeaahabaaaabaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaagfaaaaad
pccabaaaaaaaaaaagiaaaaacadaaaaaadbaaaaaibcaabaaaaaaaaaaabkiacaaa
aaaaaaaaacaaaaaaabeaaaaaaaaaaaaaaaaaaaaiccaabaaaaaaaaaaabkbabaia
ebaaaaaaabaaaaaaabeaaaaaaaaaiadpdhaaaaajccaabaaaaaaaaaaaakaabaaa
aaaaaaaabkaabaaaaaaaaaaabkbabaaaabaaaaaadgaaaaafbcaabaaaaaaaaaaa
akbabaaaabaaaaaaefaaaaajpcaabaaaaaaaaaaaegaabaaaaaaaaaaaeghobaaa
aaaaaaaaaagabaaaabaaaaaadgaaaaaipcaabaaaabaaaaaaaceaaaaaaaaaaaaa
aaaaaaaaaaaaaaaaaaaaaaaadgaaaaafecaabaaaaaaaaaaaabeaaaaaaaaaaaaa
daaaaaabcbaaaaahicaabaaaaaaaaaaackaabaaaaaaaaaaaabeaaaaaanaaaaaa
adaaaeaddkaabaaaaaaaaaaaclaaaaaficaabaaaaaaaaaaackaabaaaaaaaaaaa
dcaaaaajicaabaaaaaaaaaaadkaabaaaaaaaaaaaabeaaaaaklkkkkdnabeaaaaa
aaaaaalpdcaaaaakdcaabaaaacaaaaaaegaabaiaebaaaaaaaaaaaaaapgapbaaa
aaaaaaaaegbabaaaabaaaaaaefaaaaajpcaabaaaacaaaaaaegaabaaaacaaaaaa
eghobaaaabaaaaaaaagabaaaaaaaaaaaaaaaaaahpcaabaaaabaaaaaaegaobaaa
abaaaaaaegaobaaaacaaaaaaboaaaaahecaabaaaaaaaaaaackaabaaaaaaaaaaa
abeaaaaaabaaaaaabgaaaaabdiaaaaakpccabaaaaaaaaaaaegaobaaaabaaaaaa
aceaaaaanjijjndnnjijjndnnjijjndnnjijjndndoaaaaab"
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

varying vec2 xlv_TEXCOORD0;
void main ()
{
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = gl_MultiTexCoord0.xy;
}


#endif
#ifdef FRAGMENT
uniform sampler2D _MainTex;
uniform vec4 _MainTex_TexelSize;
uniform float _VelocityScale;
uniform float _MaxVelocity;
uniform float _MinVelocity;
uniform vec4 _BlurDirectionPacked;
varying vec2 xlv_TEXCOORD0;
void main ()
{
  vec4 sum_2;
  float velMag_3;
  vec2 blurDir_4;
  vec2 x_5;
  x_5 = xlv_TEXCOORD0;
  vec2 tmpvar_6;
  tmpvar_6.x = 1.0;
  tmpvar_6.y = (_MainTex_TexelSize.w / _MainTex_TexelSize.z);
  vec2 tmpvar_7;
  tmpvar_7 = (((xlv_TEXCOORD0 * 2.0) - 1.0) * tmpvar_6);
  vec2 tmpvar_8;
  tmpvar_8.x = tmpvar_7.y;
  tmpvar_8.y = -(tmpvar_7.x);
  vec2 tmpvar_9;
  tmpvar_9 = (((
    ((_BlurDirectionPacked.x * vec2(0.0, 1.0)) + (_BlurDirectionPacked.y * vec2(1.0, 0.0)))
   + 
    (_BlurDirectionPacked.z * tmpvar_8)
  ) + (_BlurDirectionPacked.w * tmpvar_7)) * _VelocityScale);
  blurDir_4 = tmpvar_9;
  float tmpvar_10;
  tmpvar_10 = sqrt(dot (tmpvar_9, tmpvar_9));
  velMag_3 = tmpvar_10;
  if ((tmpvar_10 > _MaxVelocity)) {
    blurDir_4 = (tmpvar_9 * (_MaxVelocity / tmpvar_10));
    velMag_3 = _MaxVelocity;
  };
  sum_2 = texture2D (_MainTex, xlv_TEXCOORD0);
  float edge0_11;
  edge0_11 = (_MinVelocity * 0.25);
  float tmpvar_12;
  tmpvar_12 = clamp (((velMag_3 - edge0_11) / (
    (_MinVelocity * 2.5)
   - edge0_11)), 0.0, 1.0);
  blurDir_4 = (((blurDir_4 * 
    (tmpvar_12 * (tmpvar_12 * (3.0 - (2.0 * tmpvar_12))))
  ) * _MainTex_TexelSize.xy) / 16.0);
  for (int i_1_1 = 0; i_1_1 < 16; i_1_1++) {
    sum_2 = (sum_2 + texture2D (_MainTex, (x_5 + (
      float(i_1_1)
     * blurDir_4))));
  };
  gl_FragData[0] = (sum_2 / 17.0);
}


#endif
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
"vs_3_0
dcl_position o0
dcl_texcoord0 o1
dcl_position0 v0
dcl_texcoord0 v1
mov o1.xy, v1
dp4 o0.w, v0, c3
dp4 o0.z, v0, c2
dp4 o0.y, v0, c1
dp4 o0.x, v0, c0
"
}
SubProgram "d3d11 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
BindCB  "UnityPerDraw" 0
"vs_4_0
eefiecedgcclnnbgpijgpddakojponflfpghdgniabaaaaaaoeabaaaaadaaaaaa
cmaaaaaaiaaaaaaaniaaaaaaejfdeheoemaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaaebaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafaepfdejfeejepeoaafeeffiedepepfceeaaklkl
epfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaa
fdfgfpfagphdgjhegjgpgoaafeeffiedepepfceeaaklklklfdeieefcaeabaaaa
eaaaabaaebaaaaaafjaaaaaeegiocaaaaaaaaaaaaeaaaaaafpaaaaadpcbabaaa
aaaaaaaafpaaaaaddcbabaaaabaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaa
gfaaaaaddccabaaaabaaaaaagiaaaaacabaaaaaadiaaaaaipcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiocaaaaaaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaa
egiocaaaaaaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaak
pcaabaaaaaaaaaaaegiocaaaaaaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaa
aaaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaaaaaaaaaaadaaaaaapgbpbaaa
aaaaaaaaegaobaaaaaaaaaaadgaaaaafdccabaaaabaaaaaaegbabaaaabaaaaaa
doaaaaab"
}
}
Program "fp" {
SubProgram "opengl " {
"!!GLSL"
}
SubProgram "d3d9 " {
Vector 0 [_MainTex_TexelSize]
Float 1 [_VelocityScale]
Float 2 [_MaxVelocity]
Float 3 [_MinVelocity]
Vector 4 [_BlurDirectionPacked]
SetTexture 0 [_MainTex] 2D 0
"ps_3_0
dcl_2d s0
def c5, 2.00000000, -1.00000000, 1.00000000, 0.00000000
def c6, 0.25000000, 2.50000000, 2.00000000, 3.00000000
def c7, 0.06250000, 15.00000000, 4.00000000, 5.00000000
def c8, 6.00000000, 7.00000000, 8.00000000, 9.00000000
def c9, 10.00000000, 11.00000000, 12.00000000, 13.00000000
def c10, 14.00000000, 0.05882353, 0, 0
dcl_texcoord0 v0.xy
rcp r0.x, c0.z
mul r0.y, r0.x, c0.w
mad r0.zw, v0.xyxy, c5.x, c5.y
mov r0.x, c5.z
mul r0.xy, r0.zwzw, r0
mov r0.w, c4.y
mul r1.xy, c5.zwzw, r0.w
mov r0.w, c4.x
mad r1.xy, c5.wzzw, r0.w, r1
mov r0.z, r0.y
mov r0.w, -r0.x
mad r0.zw, r0, c4.z, r1.xyxy
mad r0.xy, r0, c4.w, r0.zwzw
mul r0.xy, r0, c1.x
mul r0.zw, r0.xyxy, r0.xyxy
add r0.z, r0, r0.w
rsq r0.z, r0.z
rcp r0.w, r0.z
add r1.x, r0.w, -c2
mov r1.y, c3.x
mul r1.z, c6.x, r1.y
mov r1.y, c3.x
mad r1.z, c6.y, r1.y, -r1
cmp r0.w, -r1.x, r0, c2.x
mov r1.y, c3.x
mad r1.y, c6.x, -r1, r0.w
rcp r0.w, r1.z
mul_sat r1.y, r1, r0.w
mad r1.z, -r1.y, c6, c6.w
mul r0.z, r0, c2.x
mul r0.zw, r0.xyxy, r0.z
mul r1.y, r1, r1
mul r1.y, r1, r1.z
cmp r0.xy, -r1.x, r0, r0.zwzw
mul r0.xy, r0, r1.y
mul r0.xy, r0, c0
mul r4.xy, r0, c7.x
mad r1.xy, r4, c6.w, v0
mad r0.xy, r4, c5.x, v0
texld r2, r0, s0
texld r3, r1, s0
add r1.xy, v0, r4
texld r0, v0, s0
texld r1, r1, s0
add r0, r0, r0
add r0, r0, r1
add r0, r0, r2
add r2, r0, r3
mad r0.xy, r4, c7.z, v0
mad r1.xy, r4, c7.w, v0
texld r0, r0, s0
add r0, r2, r0
texld r1, r1, s0
add r2, r0, r1
mad r0.xy, r4, c8.x, v0
mad r1.xy, r4, c8.y, v0
texld r0, r0, s0
add r0, r2, r0
texld r1, r1, s0
add r2, r0, r1
mad r0.xy, r4, c8.z, v0
mad r1.xy, r4, c8.w, v0
texld r0, r0, s0
add r0, r2, r0
texld r1, r1, s0
add r2, r0, r1
mad r0.xy, r4, c9.x, v0
mad r1.xy, r4, c9.y, v0
texld r0, r0, s0
add r0, r2, r0
texld r1, r1, s0
add r2, r0, r1
mad r0.xy, r4, c9.z, v0
mad r1.xy, r4, c9.w, v0
texld r0, r0, s0
add r0, r2, r0
texld r1, r1, s0
add r0, r0, r1
mad r2.xy, r4, c10.x, v0
mad r1.xy, r4, c7.y, v0
texld r2, r2, s0
texld r1, r1, s0
add r0, r0, r2
add r0, r0, r1
mul oC0, r0, c10.y
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
ConstBuffer "$Globals" 320
Vector 32 [_MainTex_TexelSize]
Float 272 [_VelocityScale]
Float 280 [_MaxVelocity]
Float 284 [_MinVelocity]
Vector 288 [_BlurDirectionPacked]
BindCB  "$Globals" 0
"ps_4_0
eefiecedeooakcjjbmkabkiibpoopodaafabodababaaaaaamiafaaaaadaaaaaa
cmaaaaaaieaaaaaaliaaaaaaejfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcaiafaaaa
eaaaaaaaecabaaaafjaaaaaeegiocaaaaaaaaaaabdaaaaaafkaaaaadaagabaaa
aaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaa
gfaaaaadpccabaaaaaaaaaaagiaaaaacaeaaaaaadcaaaaapdcaabaaaaaaaaaaa
egbabaaaabaaaaaaaceaaaaaaaaaaaeaaaaaaaeaaaaaaaaaaaaaaaaaaceaaaaa
aaaaialpaaaaialpaaaaaaaaaaaaaaaaaoaaaaajccaabaaaabaaaaaadkiacaaa
aaaaaaaaacaaaaaackiacaaaaaaaaaaaacaaaaaadgaaaaafbcaabaaaabaaaaaa
abeaaaaaaaaaiadpdiaaaaahdcaabaaaaaaaaaaaegaabaaaaaaaaaaaegaabaaa
abaaaaaadiaaaaalpcaabaaaabaaaaaaagifcaaaaaaaaaaabcaaaaaaaceaaaaa
aaaaaaaaaaaaiadpaaaaiadpaaaaaaaaaaaaaaahdcaabaaaabaaaaaaogakbaaa
abaaaaaaegaabaaaabaaaaaadgaaaaagecaabaaaaaaaaaaaakaabaiaebaaaaaa
aaaaaaaadcaaaaakmcaabaaaaaaaaaaakgikcaaaaaaaaaaabcaaaaaafgajbaaa
aaaaaaaaagaebaaaabaaaaaadcaaaaakdcaabaaaaaaaaaaapgipcaaaaaaaaaaa
bcaaaaaaegaabaaaaaaaaaaaogakbaaaaaaaaaaadiaaaaaidcaabaaaaaaaaaaa
egaabaaaaaaaaaaaagiacaaaaaaaaaaabbaaaaaaapaaaaahicaabaaaaaaaaaaa
egaabaaaaaaaaaaaegaabaaaaaaaaaaaelaaaaafecaabaaaaaaaaaaadkaabaaa
aaaaaaaadbaaaaaiicaabaaaaaaaaaaackiacaaaaaaaaaaabbaaaaaackaabaaa
aaaaaaaaaoaaaaaibcaabaaaabaaaaaackiacaaaaaaaaaaabbaaaaaackaabaaa
aaaaaaaadiaaaaahdcaabaaaabaaaaaaegaabaaaaaaaaaaaagaabaaaabaaaaaa
dgaaaaagecaabaaaabaaaaaackiacaaaaaaaaaaabbaaaaaadhaaaaajhcaabaaa
aaaaaaaapgapbaaaaaaaaaaaegacbaaaabaaaaaaegacbaaaaaaaaaaaefaaaaaj
pcaabaaaabaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
diaaaaaiicaabaaaaaaaaaaadkiacaaaaaaaaaaabbaaaaaaabeaaaaaaaaabaea
dcaaaaalecaabaaaaaaaaaaadkiacaiaebaaaaaaaaaaaaaabbaaaaaaabeaaaaa
aaaaiadockaabaaaaaaaaaaaaoaaaaakicaabaaaaaaaaaaaaceaaaaaaaaaiadp
aaaaiadpaaaaiadpaaaaiadpdkaabaaaaaaaaaaadicaaaahecaabaaaaaaaaaaa
dkaabaaaaaaaaaaackaabaaaaaaaaaaadcaaaaajicaabaaaaaaaaaaackaabaaa
aaaaaaaaabeaaaaaaaaaaamaabeaaaaaaaaaeaeadiaaaaahecaabaaaaaaaaaaa
ckaabaaaaaaaaaaackaabaaaaaaaaaaadiaaaaahecaabaaaaaaaaaaackaabaaa
aaaaaaaadkaabaaaaaaaaaaadiaaaaahdcaabaaaaaaaaaaakgakbaaaaaaaaaaa
egaabaaaaaaaaaaadiaaaaaidcaabaaaaaaaaaaaegaabaaaaaaaaaaaegiacaaa
aaaaaaaaacaaaaaadiaaaaakdcaabaaaaaaaaaaaegaabaaaaaaaaaaaaceaaaaa
aaaaiadnaaaaiadnaaaaaaaaaaaaaaaadgaaaaafpcaabaaaacaaaaaaegaobaaa
abaaaaaadgaaaaafecaabaaaaaaaaaaaabeaaaaaaaaaaaaadaaaaaabcbaaaaah
icaabaaaaaaaaaaackaabaaaaaaaaaaaabeaaaaabaaaaaaaadaaaeaddkaabaaa
aaaaaaaaclaaaaaficaabaaaaaaaaaaackaabaaaaaaaaaaadcaaaaajdcaabaaa
adaaaaaapgapbaaaaaaaaaaaegaabaaaaaaaaaaaegbabaaaabaaaaaaefaaaaaj
pcaabaaaadaaaaaaegaabaaaadaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
aaaaaaahpcaabaaaacaaaaaaegaobaaaacaaaaaaegaobaaaadaaaaaaboaaaaah
ecaabaaaaaaaaaaackaabaaaaaaaaaaaabeaaaaaabaaaaaabgaaaaabdiaaaaak
pccabaaaaaaaaaaaegaobaaaacaaaaaaaceaaaaapbpahadnpbpahadnpbpahadn
pbpahadndoaaaaab"
}
}
 }
}
Fallback Off
}