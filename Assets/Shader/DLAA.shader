РоShader "Hidden/DLAA" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
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
  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0);
  vec4 tmpvar_2;
  tmpvar_2.xyz = tmpvar_1.xyz;
  tmpvar_2.w = dot ((4.0 * abs(
    ((((texture2D (_MainTex, 
      (xlv_TEXCOORD0 - _MainTex_TexelSize.xy)
    ) + texture2D (_MainTex, 
      (xlv_TEXCOORD0 + (vec2(1.0, -1.0) * _MainTex_TexelSize.xy))
    )) + texture2D (_MainTex, (xlv_TEXCOORD0 + 
      (vec2(-1.0, 1.0) * _MainTex_TexelSize.xy)
    ))) + texture2D (_MainTex, (xlv_TEXCOORD0 + _MainTex_TexelSize.xy))) - (4.0 * tmpvar_1))
  )).xyz, vec3(0.33, 0.33, 0.33));
  gl_FragData[0] = tmpvar_2;
}


#endif
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
"vs_2_0
dcl_position0 v0
dcl_texcoord0 v1
mov oT0.xy, v1
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
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
"ps_2_0
dcl_2d s0
def c1, 1.00000000, -1.00000000, 4.00000000, 0.33000001
dcl t0.xy
add r4.xy, t0, -c0
mov r1.xy, c0
mov r0.x, c1.y
mov r0.y, c1.x
mad r2.xy, r0, r1, t0
mov r0.xy, c0
mad r3.xy, c1, r0, t0
add r1.xy, t0, c0
texld r0, t0, s0
texld r1, r1, s0
texld r2, r2, s0
texld r3, r3, s0
texld r4, r4, s0
add r3.xyz, r4, r3
add r2.xyz, r3, r2
add r1.xyz, r2, r1
mad r1.xyz, -r0, c1.z, r1
abs r1.xyz, r1
mul r1.xyz, r1, c1.z
dp3 r0.w, r1, c1.w
mov_pp oC0, r0
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
ConstBuffer "$Globals" 32
Vector 16 [_MainTex_TexelSize]
BindCB  "$Globals" 0
"ps_4_0
eefiecedgndebgggkpdpfehncjehfpgafcgfkoklabaaaaaadeadaaaaadaaaaaa
cmaaaaaaieaaaaaaliaaaaaaejfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcheacaaaa
eaaaaaaajnaaaaaafjaaaaaeegiocaaaaaaaaaaaacaaaaaafkaaaaadaagabaaa
aaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaa
gfaaaaadpccabaaaaaaaaaaagiaaaaacadaaaaaaaaaaaaajdcaabaaaaaaaaaaa
egbabaaaabaaaaaaegiacaiaebaaaaaaaaaaaaaaabaaaaaaefaaaaajpcaabaaa
aaaaaaaaegaabaaaaaaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaadcaaaaan
pcaabaaaabaaaaaaegiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaaiadpaaaaialp
aaaaialpaaaaiadpegbebaaaabaaaaaaefaaaaajpcaabaaaacaaaaaaegaabaaa
abaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaa
ogakbaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaahhcaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaacaaaaaaaaaaaaahhcaabaaaaaaaaaaa
egacbaaaabaaaaaaegacbaaaaaaaaaaaaaaaaaaidcaabaaaabaaaaaaegbabaaa
abaaaaaaegiacaaaaaaaaaaaabaaaaaaefaaaaajpcaabaaaabaaaaaaegaabaaa
abaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaahhcaabaaaaaaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaaefaaaaajpcaabaaaabaaaaaaegbabaaa
abaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaadcaaaaanhcaabaaaaaaaaaaa
egacbaiaebaaaaaaabaaaaaaaceaaaaaaaaaiaeaaaaaiaeaaaaaiaeaaaaaaaaa
egacbaaaaaaaaaaadgaaaaafhccabaaaaaaaaaaaegacbaaaabaaaaaadiaaaaal
hcaabaaaaaaaaaaaegacbaiaibaaaaaaaaaaaaaaaceaaaaaaaaaiaeaaaaaiaea
aaaaiaeaaaaaaaaabaaaaaakiccabaaaaaaaaaaaegacbaaaaaaaaaaaaceaaaaa
mdpfkidomdpfkidomdpfkidoaaaaaaaadoaaaaab"
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
  vec4 clr_1;
  vec4 tmpvar_2;
  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
  vec4 tmpvar_3;
  vec2 cse_4;
  cse_4 = (vec2(1.5, 0.0) * _MainTex_TexelSize.xy);
  vec2 cse_5;
  cse_5 = (vec2(-1.5, 0.0) * _MainTex_TexelSize.xy);
  tmpvar_3 = (2.0 * (texture2D (_MainTex, (xlv_TEXCOORD0 + cse_5)) + texture2D (_MainTex, (xlv_TEXCOORD0 + cse_4))));
  vec4 tmpvar_6;
  vec2 cse_7;
  cse_7 = (vec2(0.0, 1.5) * _MainTex_TexelSize.xy);
  vec2 cse_8;
  cse_8 = (vec2(0.0, -1.5) * _MainTex_TexelSize.xy);
  tmpvar_6 = (2.0 * (texture2D (_MainTex, (xlv_TEXCOORD0 + cse_8)) + texture2D (_MainTex, (xlv_TEXCOORD0 + cse_7))));
  vec4 tmpvar_9;
  tmpvar_9 = ((tmpvar_3 + (2.0 * tmpvar_2)) / 6.0);
  vec4 tmpvar_10;
  tmpvar_10 = ((tmpvar_6 + (2.0 * tmpvar_2)) / 6.0);
  vec4 tmpvar_11;
  tmpvar_11 = mix (mix (tmpvar_2, tmpvar_9, vec4(clamp (
    (((3.0 * dot (
      (abs((tmpvar_6 - (4.0 * tmpvar_2))) / 4.0)
    .xyz, vec3(0.33, 0.33, 0.33))) - 0.1) / dot (tmpvar_9.xyz, vec3(0.33, 0.33, 0.33)))
  , 0.0, 1.0))), tmpvar_10, vec4(clamp ((
    ((3.0 * dot ((
      abs((tmpvar_3 - (4.0 * tmpvar_2)))
     / 4.0).xyz, vec3(0.33, 0.33, 0.33))) - 0.1)
   / 
    dot (tmpvar_10.xyz, vec3(0.33, 0.33, 0.33))
  ), 0.0, 1.0)));
  clr_1 = tmpvar_11;
  vec4 tmpvar_12;
  tmpvar_12 = texture2D (_MainTex, (xlv_TEXCOORD0 + cse_4));
  vec4 tmpvar_13;
  tmpvar_13 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(3.5, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_14;
  tmpvar_14 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(5.5, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_15;
  tmpvar_15 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(7.5, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_16;
  tmpvar_16 = texture2D (_MainTex, (xlv_TEXCOORD0 + cse_5));
  vec4 tmpvar_17;
  tmpvar_17 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(-3.5, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_18;
  tmpvar_18 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(-5.5, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_19;
  tmpvar_19 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(-7.5, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_20;
  tmpvar_20 = texture2D (_MainTex, (xlv_TEXCOORD0 + cse_7));
  vec4 tmpvar_21;
  tmpvar_21 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, 3.5) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_22;
  tmpvar_22 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, 5.5) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_23;
  tmpvar_23 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, 7.5) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_24;
  tmpvar_24 = texture2D (_MainTex, (xlv_TEXCOORD0 + cse_8));
  vec4 tmpvar_25;
  tmpvar_25 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, -3.5) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_26;
  tmpvar_26 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, -5.5) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_27;
  tmpvar_27 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, -7.5) * _MainTex_TexelSize.xy)));
  float tmpvar_28;
  tmpvar_28 = clamp (((
    ((((
      ((((tmpvar_12.w + tmpvar_13.w) + tmpvar_14.w) + tmpvar_15.w) + tmpvar_16.w)
     + tmpvar_17.w) + tmpvar_18.w) + tmpvar_19.w) / 8.0)
   * 2.0) - 1.0), 0.0, 1.0);
  float tmpvar_29;
  tmpvar_29 = clamp (((
    ((((
      ((((tmpvar_20.w + tmpvar_21.w) + tmpvar_22.w) + tmpvar_23.w) + tmpvar_24.w)
     + tmpvar_25.w) + tmpvar_26.w) + tmpvar_27.w) / 8.0)
   * 2.0) - 1.0), 0.0, 1.0);
  vec4 tmpvar_30;
  tmpvar_30 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(-1.0, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_31;
  tmpvar_31 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(1.0, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_32;
  tmpvar_32 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, -1.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_33;
  tmpvar_33 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, 1.0) * _MainTex_TexelSize.xy)));
  if (((tmpvar_28 > 0.0) || (tmpvar_29 > 0.0))) {
    float tmpvar_34;
    tmpvar_34 = dot (((
      ((((
        ((tmpvar_12 + tmpvar_13) + tmpvar_14)
       + tmpvar_15) + tmpvar_16) + tmpvar_17) + tmpvar_18)
     + tmpvar_19) / 8.0).xyz, vec3(0.33, 0.33, 0.33));
    float tmpvar_35;
    tmpvar_35 = dot (((
      ((((
        ((tmpvar_20 + tmpvar_21) + tmpvar_22)
       + tmpvar_23) + tmpvar_24) + tmpvar_25) + tmpvar_26)
     + tmpvar_27) / 8.0).xyz, vec3(0.33, 0.33, 0.33));
    float tmpvar_36;
    tmpvar_36 = dot (tmpvar_2.xyz, vec3(0.33, 0.33, 0.33));
    float tmpvar_37;
    tmpvar_37 = dot (tmpvar_30.xyz, vec3(0.33, 0.33, 0.33));
    float tmpvar_38;
    tmpvar_38 = dot (tmpvar_31.xyz, vec3(0.33, 0.33, 0.33));
    float tmpvar_39;
    tmpvar_39 = dot (tmpvar_32.xyz, vec3(0.33, 0.33, 0.33));
    float tmpvar_40;
    tmpvar_40 = dot (tmpvar_33.xyz, vec3(0.33, 0.33, 0.33));
    float tmpvar_41;
    if ((tmpvar_36 == tmpvar_39)) {
      tmpvar_41 = 0.0;
    } else {
      tmpvar_41 = clamp (((tmpvar_34 - tmpvar_39) / (tmpvar_36 - tmpvar_39)), 0.0, 1.0);
    };
    float tmpvar_42;
    if ((tmpvar_36 == tmpvar_40)) {
      tmpvar_42 = 0.0;
    } else {
      tmpvar_42 = clamp ((1.0 + (
        (tmpvar_34 - tmpvar_36)
       / 
        (tmpvar_36 - tmpvar_40)
      )), 0.0, 1.0);
    };
    float tmpvar_43;
    if ((tmpvar_36 == tmpvar_37)) {
      tmpvar_43 = 0.0;
    } else {
      tmpvar_43 = clamp (((tmpvar_35 - tmpvar_37) / (tmpvar_36 - tmpvar_37)), 0.0, 1.0);
    };
    float tmpvar_44;
    if ((tmpvar_36 == tmpvar_38)) {
      tmpvar_44 = 0.0;
    } else {
      tmpvar_44 = clamp ((1.0 + (
        (tmpvar_35 - tmpvar_36)
       / 
        (tmpvar_36 - tmpvar_38)
      )), 0.0, 1.0);
    };
    clr_1 = mix (mix (tmpvar_11, mix (tmpvar_31, 
      mix (tmpvar_30, tmpvar_2, vec4(tmpvar_43))
    , vec4(tmpvar_44)), vec4(tmpvar_29)), mix (tmpvar_33, mix (tmpvar_32, tmpvar_2, vec4(tmpvar_41)), vec4(tmpvar_42)), vec4(tmpvar_28));
  };
  gl_FragData[0] = clr_1;
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
def c1, -1.50000000, 0.00000000, 1.50000000, 2.00000000
def c2, 4.00000000, 0.25000000, 0.99000001, -0.10000000
def c3, 0.16666667, 0.33000001, 3.50000000, 0.00000000
def c4, 5.50000000, 0.00000000, 7.50000000, -3.50000000
def c5, -5.50000000, 0.00000000, -7.50000000, -1.00000000
def c6, 0.25000000, -1.00000000, 1.00000000, 0.00000000
def c7, 0.12500000, 0, 0, 0
dcl_texcoord0 v0.xy
mov r0.zw, c0.xyxy
mad r1.xy, c1.zyzw, r0.zwzw, v0
mov r0.zw, c0.xyxy
mad r4.xy, c1.yzzw, r0.zwzw, v0
mov r0.xy, c0
mad r0.xy, c1, r0, v0
texld r2, r0, s0
texld r3, r1, s0
add r1, r2, r3
mul r6, r1, c1.w
texld r1, v0, s0
mad r7, r1, c1.w, r6
mul r7, r7, c3.x
dp3 r8.w, r7, c3.y
mov r0.xy, c0
mov r9.xy, c0
mad r9.xy, c4.ywzw, r9, v0
texld r10, r9, s0
mov r9.zw, c0.xyxy
mad r9.xy, c5.yxzw, r9.zwzw, v0
texld r11, r9, s0
mov r9.xy, c0
mad r12.xy, c5.yzzw, r9, v0
mov r9.xy, c0
mad r9.xy, c3.zwzw, r9, v0
texld r9, r9, s0
mov r13.xy, c0
mad r13.xy, c4, r13, v0
mov r14.xy, c0
mad r14.xy, c4.zyzw, r14, v0
mov r15.xy, c0
mad r15.xy, c4.wyzw, r15, v0
mov r16.xy, c0
mad r16.xy, c5, r16, v0
mov r17.xy, c0
mad r17.xy, c5.zyzw, r17, v0
add r7, -r1, r7
texld r5, r4, s0
mad r0.xy, c1.yxzw, r0, v0
texld r4, r0, s0
add r0, r4, r5
mul r0, r0, c1.w
mad r8.xyz, -r1, c2.x, r0
mov r20.xyz, r4
abs r8.xyz, r8
mul r8.xyz, r8, c2.y
dp3 r6.w, r8, c2.z
mad r0, r1, c1.w, r0
mov r4.xy, c0
mul r0, r0, c3.x
rcp r8.x, r8.w
add r6.w, r6, c2
mul_sat r6.w, r6, r8.x
mad r8.xyz, -r1, c2.x, r6
mad r6, r6.w, r7, r1
abs r7.xyz, r8
mul r8.xyz, r7, c2.y
add r7, r0, -r6
dp3 r0.w, r8, c2.z
dp3 r0.x, r0, c3.y
add r0.z, r0.w, c2.w
rcp r0.w, r0.x
mov r0.xy, c0
mad r8.xy, c3.wzzw, r0, v0
mul_sat r0.z, r0, r0.w
mad r0, r0.z, r7, r6
texld r6, r8, s0
mov r7.xy, c0
mad r7.xy, c4.yxzw, r7, v0
texld r7, r7, s0
add r5.w, r5, r6
mov r8.xy, c0
mad r8.xy, c4.yzzw, r8, v0
texld r8, r8, s0
add r5.w, r7, r5
add r5.w, r8, r5
add r4.w, r4, r5
add r4.w, r10, r4
mov r18.xyz, r7
add r4.w, r11, r4
texld r12, r12, s0
add r4.w, r12, r4
add r3.w, r3, r9
texld r13, r13, s0
add r3.w, r13, r3
texld r14, r14, s0
add r3.w, r14, r3
add r2.w, r2, r3
mad_sat r3.w, r4, c6.x, c6.y
texld r15, r15, s0
add r2.w, r15, r2
texld r16, r16, s0
add r2.w, r16, r2
texld r17, r17, s0
add r2.w, r17, r2
mad_sat r2.w, r2, c6.x, c6.y
cmp r4.w, -r2, c6, c6.z
cmp r5.w, -r3, c6, c6.z
add_pp_sat r5.w, r4, r5
mov r4.zw, c0.xyxy
mad r7.xy, c5.wyzw, r4.zwzw, v0
mad r4.xy, c6.zwzw, r4, v0
texld r7, r7, s0
mov r19.xyz, r8
texld r4, r4, s0
mov r21.xyz, r10
mov r10, r4
mov r4.xy, c0
mov r8, r7
mov r4.zw, c0.xyxy
mad r7.xy, c6.wzzw, r4.zwzw, v0
mad r4.xy, c5.ywzw, r4, v0
texld r4, r4, s0
texld r7, r7, s0
if_gt r5.w, c1.y
add r5.xyz, r5, r6
add r5.xyz, r5, r18
add r5.xyz, r5, r19
add r5.xyz, r5, r20
add r5.xyz, r5, r21
add r5.xyz, r5, r11
add r6.xyz, r5, r12
mul r6.xyz, r6, c7.x
add r3.xyz, r3, r9
add r3.xyz, r3, r13
add r3.xyz, r3, r14
add r2.xyz, r3, r2
add r2.xyz, r2, r15
add r2.xyz, r2, r16
add r2.xyz, r2, r17
mul r2.xyz, r2, c7.x
dp3 r6.w, r1, c3.y
dp3 r3.x, r4, c3.y
dp3 r2.y, r2, c3.y
add r3.y, r6.w, -r3.x
add r5, r1, -r8
dp3 r9.w, r8, c3.y
dp3 r6.x, r6, c3.y
add r6.y, r6.x, -r9.w
add r11.x, r6.w, -r9.w
rcp r6.z, r11.x
mul_sat r6.z, r6.y, r6
abs r6.y, r11.x
cmp r6.y, -r6, c1, r6.z
mad r5, r6.y, r5, r8
dp3 r6.y, r10, c3.y
add r6.y, r6.w, -r6
add r1, r1, -r4
add r2.x, r2.y, -r3
rcp r2.z, r3.y
mul_sat r2.z, r2.x, r2
abs r2.x, r3.y
cmp r2.z, -r2.x, c1.y, r2
add r6.x, -r6.w, r6
rcp r6.z, r6.y
mad_sat r6.z, r6.x, r6, c6
abs r6.x, r6.y
add r5, -r10, r5
cmp r6.x, -r6, c1.y, r6.z
mad r5, r6.x, r5, r10
add r5, r5, -r0
mad r0, r3.w, r5, r0
mad r3, r2.z, r1, r4
dp3 r2.x, r7, c3.y
add r1.x, r6.w, -r2
rcp r1.z, r1.x
add r1.y, -r6.w, r2
mad_sat r1.y, r1, r1.z, c6.z
abs r1.x, r1
add r3, -r7, r3
cmp r1.x, -r1, c1.y, r1.y
mad r1, r1.x, r3, r7
add r1, r1, -r0
mad r0, r2.w, r1, r0
endif
mov_pp oC0, r0
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
ConstBuffer "$Globals" 32
Vector 16 [_MainTex_TexelSize]
BindCB  "$Globals" 0
"ps_4_0
eefiecedebeinahmhififhgdnhjdlaicibkkchijabaaaaaanabbaaaaadaaaaaa
cmaaaaaaieaaaaaaliaaaaaaejfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcbabbaaaa
eaaaaaaaeeaeaaaafjaaaaaeegiocaaaaaaaaaaaacaaaaaafkaaaaadaagabaaa
aaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaa
gfaaaaadpccabaaaaaaaaaaagiaaaaacakaaaaaadcaaaaanpcaabaaaaaaaaaaa
egiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaaaaaaaaaamalpaaaaaaaaaaaamadp
egbebaaaabaaaaaaefaaaaajpcaabaaaabaaaaaaegaabaaaaaaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaaaaaaaaaogakbaaaaaaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaahpcaabaaaacaaaaaaegaobaaa
aaaaaaaaegaobaaaabaaaaaaaaaaaaahhcaabaaaadaaaaaaegacbaaaacaaaaaa
egacbaaaacaaaaaaefaaaaajpcaabaaaaeaaaaaaegbabaaaabaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaadcaaaaanhcaabaaaadaaaaaaegacbaiaebaaaaaa
aeaaaaaaaceaaaaaaaaaiaeaaaaaiaeaaaaaiaeaaaaaaaaaegacbaaaadaaaaaa
diaaaaalhcaabaaaadaaaaaaegacbaiaibaaaaaaadaaaaaaaceaaaaaaaaaiado
aaaaiadoaaaaiadoaaaaaaaabaaaaaakbcaabaaaadaaaaaaegacbaaaadaaaaaa
aceaaaaamdpfkidomdpfkidomdpfkidoaaaaaaaadcaaaaajbcaabaaaadaaaaaa
akaabaaaadaaaaaaabeaaaaaaaaaeaeaabeaaaaamnmmmmlndcaaaaanpcaabaaa
afaaaaaaegiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaamalpaaaaaaaaaaaamadp
aaaaaaaaegbebaaaabaaaaaaefaaaaajpcaabaaaagaaaaaaegaabaaaafaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaafaaaaaaogakbaaa
afaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaahpcaabaaaahaaaaaa
egaobaaaafaaaaaaegaobaaaagaaaaaaaaaaaaahpcaabaaaaiaaaaaaegaobaaa
aeaaaaaaegaobaaaaeaaaaaadcaaaaampcaabaaaajaaaaaaegaobaaaahaaaaaa
aceaaaaaaaaaaaeaaaaaaaeaaaaaaaeaaaaaaaeaegaobaaaaiaaaaaaaaaaaaah
ocaabaaaadaaaaaaagajbaaaahaaaaaaagajbaaaahaaaaaadcaaaaanocaabaaa
adaaaaaaagajbaiaebaaaaaaaeaaaaaaaceaaaaaaaaaaaaaaaaaiaeaaaaaiaea
aaaaiaeafgaobaaaadaaaaaadiaaaaalocaabaaaadaaaaaafgaobaiaibaaaaaa
adaaaaaaaceaaaaaaaaaaaaaaaaaiadoaaaaiadoaaaaiadobaaaaaakccaabaaa
adaaaaaajgahbaaaadaaaaaaaceaaaaamdpfkidomdpfkidomdpfkidoaaaaaaaa
dcaaaaajccaabaaaadaaaaaabkaabaaaadaaaaaaabeaaaaaaaaaeaeaabeaaaaa
mnmmmmlndcaaaaampcaabaaaacaaaaaaegaobaaaacaaaaaaaceaaaaaaaaaaaea
aaaaaaeaaaaaaaeaaaaaaaeaegaobaaaaiaaaaaadiaaaaakhcaabaaaahaaaaaa
egacbaaaajaaaaaaaceaaaaaklkkckdoklkkckdoklkkckdoaaaaaaaadcaaaaan
pcaabaaaaiaaaaaaegaobaaaajaaaaaaaceaaaaaklkkckdoklkkckdoklkkckdo
klkkckdoegaobaiaebaaaaaaaeaaaaaabaaaaaakecaabaaaadaaaaaaegacbaaa
ahaaaaaaaceaaaaamdpfkidomdpfkidomdpfkidoaaaaaaaaaocaaaahbcaabaaa
adaaaaaaakaabaaaadaaaaaackaabaaaadaaaaaadcaaaaajpcaabaaaahaaaaaa
agaabaaaadaaaaaaegaobaaaaiaaaaaaegaobaaaaeaaaaaadcaaaaanpcaabaaa
aiaaaaaaegaobaaaacaaaaaaaceaaaaaklkkckdoklkkckdoklkkckdoklkkckdo
egaobaiaebaaaaaaahaaaaaadiaaaaakhcaabaaaacaaaaaaegacbaaaacaaaaaa
aceaaaaaklkkckdoklkkckdoklkkckdoaaaaaaaabaaaaaakbcaabaaaacaaaaaa
egacbaaaacaaaaaaaceaaaaamdpfkidomdpfkidomdpfkidoaaaaaaaaaocaaaah
bcaabaaaacaaaaaabkaabaaaadaaaaaaakaabaaaacaaaaaadcaaaaajpcaabaaa
acaaaaaaagaabaaaacaaaaaaegaobaaaaiaaaaaaegaobaaaahaaaaaadcaaaaan
pcaabaaaadaaaaaaegiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaaaaaaaaaagaea
aaaaaaaaaaaalaeaegbebaaaabaaaaaaefaaaaajpcaabaaaahaaaaaaegaabaaa
adaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaadaaaaaa
ogakbaaaadaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaahpcaabaaa
aaaaaaaadgajbaaaaaaaaaaadgajbaaaahaaaaaaaaaaaaahpcaabaaaaaaaaaaa
dgajbaaaadaaaaaaegaobaaaaaaaaaaadcaaaaanpcaabaaaadaaaaaaegiecaaa
aaaaaaaaabaaaaaaaceaaaaaaaaaaaaaaaaapaeaaaaaaaaaaaaagamaegbebaaa
abaaaaaaefaaaaajpcaabaaaahaaaaaaegaabaaaadaaaaaaeghobaaaaaaaaaaa
aagabaaaaaaaaaaaefaaaaajpcaabaaaadaaaaaaogakbaaaadaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaaaaaaaaahpcaabaaaaaaaaaaaegaobaaaaaaaaaaa
dgajbaaaahaaaaaaaaaaaaahpcaabaaaaaaaaaaadgajbaaaabaaaaaaegaobaaa
aaaaaaaaaaaaaaahpcaabaaaaaaaaaaadgajbaaaadaaaaaaegaobaaaaaaaaaaa
dcaaaaanpcaabaaaabaaaaaaegiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaaaaaa
aaaalamaaaaaaaaaaaaapamaegbebaaaabaaaaaaefaaaaajpcaabaaaadaaaaaa
egaabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaa
abaaaaaaogakbaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaah
pcaabaaaaaaaaaaaegaobaaaaaaaaaaadgajbaaaadaaaaaaaaaaaaahpcaabaaa
aaaaaaaadgajbaaaabaaaaaaegaobaaaaaaaaaaadiaaaaakocaabaaaaaaaaaaa
fgaobaaaaaaaaaaaaceaaaaaaaaaaaaaaaaaaadoaaaaaadoaaaaaadodccaaaaj
bcaabaaaaaaaaaaaakaabaaaaaaaaaaaabeaaaaaaaaaiadoabeaaaaaaaaaialp
baaaaaakccaabaaaaaaaaaaajgahbaaaaaaaaaaaaceaaaaamdpfkidomdpfkido
mdpfkidoaaaaaaaadcaaaaanpcaabaaaabaaaaaaegiecaaaaaaaaaaaabaaaaaa
aceaaaaaaaaaialpaaaaaaaaaaaaiadpaaaaaaaaegbebaaaabaaaaaaefaaaaaj
pcaabaaaadaaaaaaegaabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
efaaaaajpcaabaaaabaaaaaaogakbaaaabaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaabaaaaaakecaabaaaaaaaaaaaegacbaaaadaaaaaaaceaaaaamdpfkido
mdpfkidomdpfkidoaaaaaaaaaaaaaaaiicaabaaaaaaaaaaackaabaiaebaaaaaa
aaaaaaaabkaabaaaaaaaaaaabaaaaaakbcaabaaaahaaaaaaegacbaaaaeaaaaaa
aceaaaaamdpfkidomdpfkidomdpfkidoaaaaaaaaaaaaaaaiccaabaaaahaaaaaa
ckaabaiaebaaaaaaaaaaaaaaakaabaaaahaaaaaabiaaaaahecaabaaaaaaaaaaa
ckaabaaaaaaaaaaaakaabaaaahaaaaaaaocaaaahicaabaaaaaaaaaaadkaabaaa
aaaaaaaabkaabaaaahaaaaaadhaaaaajecaabaaaaaaaaaaackaabaaaaaaaaaaa
abeaaaaaaaaaaaaadkaabaaaaaaaaaaaaaaaaaaipcaabaaaaiaaaaaaegaobaia
ebaaaaaaadaaaaaaegaobaaaaeaaaaaadcaaaaajpcaabaaaadaaaaaakgakbaaa
aaaaaaaaegaobaaaaiaaaaaaegaobaaaadaaaaaaaaaaaaaipcaabaaaadaaaaaa
egaobaiaebaaaaaaabaaaaaaegaobaaaadaaaaaaaaaaaaaiccaabaaaaaaaaaaa
bkaabaaaaaaaaaaaakaabaiaebaaaaaaahaaaaaabaaaaaakecaabaaaaaaaaaaa
egacbaaaabaaaaaaaceaaaaamdpfkidomdpfkidomdpfkidoaaaaaaaaaaaaaaai
icaabaaaaaaaaaaackaabaiaebaaaaaaaaaaaaaaakaabaaaahaaaaaabiaaaaah
ecaabaaaaaaaaaaackaabaaaaaaaaaaaakaabaaaahaaaaaaaoaaaaahccaabaaa
aaaaaaaabkaabaaaaaaaaaaadkaabaaaaaaaaaaaaacaaaahccaabaaaaaaaaaaa
bkaabaaaaaaaaaaaabeaaaaaaaaaiadpdhaaaaajccaabaaaaaaaaaaackaabaaa
aaaaaaaaabeaaaaaaaaaaaaabkaabaaaaaaaaaaadcaaaaajpcaabaaaabaaaaaa
fgafbaaaaaaaaaaaegaobaaaadaaaaaaegaobaaaabaaaaaaaaaaaaaipcaabaaa
abaaaaaaegaobaiaebaaaaaaacaaaaaaegaobaaaabaaaaaadcaaaaajpcaabaaa
abaaaaaaagaabaaaaaaaaaaaegaobaaaabaaaaaaegaobaaaacaaaaaadbaaaaah
bcaabaaaaaaaaaaaabeaaaaaaaaaaaaaakaabaaaaaaaaaaadcaaaaanpcaabaaa
adaaaaaaegiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaagaeaaaaaaaaaaaaalaea
aaaaaaaaegbebaaaabaaaaaaefaaaaajpcaabaaaaiaaaaaaegaabaaaadaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaadaaaaaaogakbaaa
adaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaahpcaabaaaafaaaaaa
dgajbaaaafaaaaaadgajbaaaaiaaaaaaaaaaaaahpcaabaaaadaaaaaadgajbaaa
adaaaaaaegaobaaaafaaaaaadcaaaaanpcaabaaaafaaaaaaegiecaaaaaaaaaaa
abaaaaaaaceaaaaaaaaapaeaaaaaaaaaaaaagamaaaaaaaaaegbebaaaabaaaaaa
efaaaaajpcaabaaaaiaaaaaaegaabaaaafaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaaefaaaaajpcaabaaaafaaaaaaogakbaaaafaaaaaaeghobaaaaaaaaaaa
aagabaaaaaaaaaaaaaaaaaahpcaabaaaadaaaaaaegaobaaaadaaaaaadgajbaaa
aiaaaaaaaaaaaaahpcaabaaaadaaaaaadgajbaaaagaaaaaaegaobaaaadaaaaaa
aaaaaaahpcaabaaaadaaaaaadgajbaaaafaaaaaaegaobaaaadaaaaaadcaaaaan
pcaabaaaafaaaaaaegiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaalamaaaaaaaaa
aaaapamaaaaaaaaaegbebaaaabaaaaaaefaaaaajpcaabaaaagaaaaaaegaabaaa
afaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaafaaaaaa
ogakbaaaafaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaahpcaabaaa
adaaaaaaegaobaaaadaaaaaadgajbaaaagaaaaaaaaaaaaahpcaabaaaadaaaaaa
dgajbaaaafaaaaaaegaobaaaadaaaaaadiaaaaakocaabaaaaaaaaaaafgaobaaa
adaaaaaaaceaaaaaaaaaaaaaaaaaaadoaaaaaadoaaaaaadodccaaaajbcaabaaa
adaaaaaaakaabaaaadaaaaaaabeaaaaaaaaaiadoabeaaaaaaaaaialpbaaaaaak
ccaabaaaaaaaaaaajgahbaaaaaaaaaaaaceaaaaamdpfkidomdpfkidomdpfkido
aaaaaaaadcaaaaanpcaabaaaafaaaaaaegiecaaaaaaaaaaaabaaaaaaaceaaaaa
aaaaaaaaaaaaialpaaaaaaaaaaaaiadpegbebaaaabaaaaaaefaaaaajpcaabaaa
agaaaaaaegaabaaaafaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaaj
pcaabaaaafaaaaaaogakbaaaafaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
baaaaaakecaabaaaaaaaaaaaegacbaaaagaaaaaaaceaaaaamdpfkidomdpfkido
mdpfkidoaaaaaaaaaaaaaaaiicaabaaaaaaaaaaackaabaiaebaaaaaaaaaaaaaa
bkaabaaaaaaaaaaaaaaaaaaiccaabaaaaaaaaaaaakaabaiaebaaaaaaahaaaaaa
bkaabaaaaaaaaaaaaaaaaaaiccaabaaaadaaaaaackaabaiaebaaaaaaaaaaaaaa
akaabaaaahaaaaaabiaaaaahecaabaaaaaaaaaaackaabaaaaaaaaaaaakaabaaa
ahaaaaaaaocaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaabkaabaaaadaaaaaa
dhaaaaajecaabaaaaaaaaaaackaabaaaaaaaaaaaabeaaaaaaaaaaaaadkaabaaa
aaaaaaaaaaaaaaaipcaabaaaaeaaaaaaegaobaaaaeaaaaaaegaobaiaebaaaaaa
agaaaaaadcaaaaajpcaabaaaaeaaaaaakgakbaaaaaaaaaaaegaobaaaaeaaaaaa
egaobaaaagaaaaaaaaaaaaaipcaabaaaaeaaaaaaegaobaiaebaaaaaaafaaaaaa
egaobaaaaeaaaaaabaaaaaakecaabaaaaaaaaaaaegacbaaaafaaaaaaaceaaaaa
mdpfkidomdpfkidomdpfkidoaaaaaaaaaaaaaaaiicaabaaaaaaaaaaackaabaia
ebaaaaaaaaaaaaaaakaabaaaahaaaaaabiaaaaahecaabaaaaaaaaaaackaabaaa
aaaaaaaaakaabaaaahaaaaaaaoaaaaahccaabaaaaaaaaaaabkaabaaaaaaaaaaa
dkaabaaaaaaaaaaaaacaaaahccaabaaaaaaaaaaabkaabaaaaaaaaaaaabeaaaaa
aaaaiadpdhaaaaajccaabaaaaaaaaaaackaabaaaaaaaaaaaabeaaaaaaaaaaaaa
bkaabaaaaaaaaaaadcaaaaajpcaabaaaaeaaaaaafgafbaaaaaaaaaaaegaobaaa
aeaaaaaaegaobaaaafaaaaaaaaaaaaaipcaabaaaaeaaaaaaegaobaiaebaaaaaa
abaaaaaaegaobaaaaeaaaaaadcaaaaajpcaabaaaabaaaaaaagaabaaaadaaaaaa
egaobaaaaeaaaaaaegaobaaaabaaaaaadbaaaaahccaabaaaaaaaaaaaabeaaaaa
aaaaaaaaakaabaaaadaaaaaadmaaaaahbcaabaaaaaaaaaaaakaabaaaaaaaaaaa
bkaabaaaaaaaaaaadhaaaaajpccabaaaaaaaaaaaagaabaaaaaaaaaaaegaobaaa
abaaaaaaegaobaaaacaaaaaadoaaaaab"
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
  vec4 clr_1;
  vec4 tmpvar_2;
  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
  vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(-1.0, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_4;
  tmpvar_4 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(1.0, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, -1.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_6;
  tmpvar_6 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, 1.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_7;
  vec2 cse_8;
  cse_8 = (vec2(1.5, 0.0) * _MainTex_TexelSize.xy);
  vec2 cse_9;
  cse_9 = (vec2(-1.5, 0.0) * _MainTex_TexelSize.xy);
  tmpvar_7 = (((2.0 * 
    (texture2D (_MainTex, (xlv_TEXCOORD0 + cse_9)) + texture2D (_MainTex, (xlv_TEXCOORD0 + cse_8)))
  ) + (2.0 * tmpvar_2)) / 6.0);
  vec4 tmpvar_10;
  vec2 cse_11;
  cse_11 = (vec2(0.0, 1.5) * _MainTex_TexelSize.xy);
  vec2 cse_12;
  cse_12 = (vec2(0.0, -1.5) * _MainTex_TexelSize.xy);
  tmpvar_10 = (((2.0 * 
    (texture2D (_MainTex, (xlv_TEXCOORD0 + cse_12)) + texture2D (_MainTex, (xlv_TEXCOORD0 + cse_11)))
  ) + (2.0 * tmpvar_2)) / 6.0);
  vec4 tmpvar_13;
  tmpvar_13 = mix (mix (tmpvar_2, tmpvar_7, vec4(clamp (
    (((3.0 * dot (
      (abs(((tmpvar_5 + tmpvar_6) - (2.0 * tmpvar_2))) / 2.0)
    .xyz, vec3(0.33, 0.33, 0.33))) - 0.1) / dot (tmpvar_7.xyz, vec3(0.33, 0.33, 0.33)))
  , 0.0, 1.0))), tmpvar_10, vec4((clamp (
    (((3.0 * dot (
      (abs(((tmpvar_3 + tmpvar_4) - (2.0 * tmpvar_2))) / 2.0)
    .xyz, vec3(0.33, 0.33, 0.33))) - 0.1) / dot (tmpvar_10.xyz, vec3(0.33, 0.33, 0.33)))
  , 0.0, 1.0) * 0.5)));
  clr_1 = tmpvar_13;
  vec4 tmpvar_14;
  tmpvar_14 = texture2D (_MainTex, (xlv_TEXCOORD0 + cse_8));
  vec4 tmpvar_15;
  tmpvar_15 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(3.5, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_16;
  tmpvar_16 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(5.5, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_17;
  tmpvar_17 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(7.5, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_18;
  tmpvar_18 = texture2D (_MainTex, (xlv_TEXCOORD0 + cse_9));
  vec4 tmpvar_19;
  tmpvar_19 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(-3.5, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_20;
  tmpvar_20 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(-5.5, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_21;
  tmpvar_21 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(-7.5, 0.0) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_22;
  tmpvar_22 = texture2D (_MainTex, (xlv_TEXCOORD0 + cse_11));
  vec4 tmpvar_23;
  tmpvar_23 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, 3.5) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_24;
  tmpvar_24 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, 5.5) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_25;
  tmpvar_25 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, 7.5) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_26;
  tmpvar_26 = texture2D (_MainTex, (xlv_TEXCOORD0 + cse_12));
  vec4 tmpvar_27;
  tmpvar_27 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, -3.5) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_28;
  tmpvar_28 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, -5.5) * _MainTex_TexelSize.xy)));
  vec4 tmpvar_29;
  tmpvar_29 = texture2D (_MainTex, (xlv_TEXCOORD0 + (vec2(0.0, -7.5) * _MainTex_TexelSize.xy)));
  float tmpvar_30;
  tmpvar_30 = clamp (((
    ((((
      ((((tmpvar_14.w + tmpvar_15.w) + tmpvar_16.w) + tmpvar_17.w) + tmpvar_18.w)
     + tmpvar_19.w) + tmpvar_20.w) + tmpvar_21.w) / 8.0)
   * 2.0) - 1.0), 0.0, 1.0);
  float tmpvar_31;
  tmpvar_31 = clamp (((
    ((((
      ((((tmpvar_22.w + tmpvar_23.w) + tmpvar_24.w) + tmpvar_25.w) + tmpvar_26.w)
     + tmpvar_27.w) + tmpvar_28.w) + tmpvar_29.w) / 8.0)
   * 2.0) - 1.0), 0.0, 1.0);
  float tmpvar_32;
  tmpvar_32 = abs((tmpvar_30 - tmpvar_31));
  if ((tmpvar_32 > 0.2)) {
    float tmpvar_33;
    tmpvar_33 = dot (((
      ((((
        ((tmpvar_14 + tmpvar_15) + tmpvar_16)
       + tmpvar_17) + tmpvar_18) + tmpvar_19) + tmpvar_20)
     + tmpvar_21) / 8.0).xyz, vec3(0.33, 0.33, 0.33));
    float tmpvar_34;
    tmpvar_34 = dot (((
      ((((
        ((tmpvar_22 + tmpvar_23) + tmpvar_24)
       + tmpvar_25) + tmpvar_26) + tmpvar_27) + tmpvar_28)
     + tmpvar_29) / 8.0).xyz, vec3(0.33, 0.33, 0.33));
    float tmpvar_35;
    tmpvar_35 = dot (tmpvar_2.xyz, vec3(0.33, 0.33, 0.33));
    float tmpvar_36;
    tmpvar_36 = dot (tmpvar_3.xyz, vec3(0.33, 0.33, 0.33));
    float tmpvar_37;
    tmpvar_37 = dot (tmpvar_4.xyz, vec3(0.33, 0.33, 0.33));
    float tmpvar_38;
    tmpvar_38 = dot (tmpvar_5.xyz, vec3(0.33, 0.33, 0.33));
    float tmpvar_39;
    tmpvar_39 = dot (tmpvar_6.xyz, vec3(0.33, 0.33, 0.33));
    float tmpvar_40;
    if ((tmpvar_35 == tmpvar_38)) {
      tmpvar_40 = 0.0;
    } else {
      tmpvar_40 = clamp (((tmpvar_33 - tmpvar_38) / (tmpvar_35 - tmpvar_38)), 0.0, 1.0);
    };
    float tmpvar_41;
    if ((tmpvar_35 == tmpvar_39)) {
      tmpvar_41 = 0.0;
    } else {
      tmpvar_41 = clamp ((1.0 + (
        (tmpvar_33 - tmpvar_35)
       / 
        (tmpvar_35 - tmpvar_39)
      )), 0.0, 1.0);
    };
    float tmpvar_42;
    if ((tmpvar_35 == tmpvar_36)) {
      tmpvar_42 = 0.0;
    } else {
      tmpvar_42 = clamp (((tmpvar_34 - tmpvar_36) / (tmpvar_35 - tmpvar_36)), 0.0, 1.0);
    };
    float tmpvar_43;
    if ((tmpvar_35 == tmpvar_37)) {
      tmpvar_43 = 0.0;
    } else {
      tmpvar_43 = clamp ((1.0 + (
        (tmpvar_34 - tmpvar_35)
       / 
        (tmpvar_35 - tmpvar_37)
      )), 0.0, 1.0);
    };
    clr_1 = mix (mix (tmpvar_13, mix (tmpvar_4, 
      mix (tmpvar_3, tmpvar_2, vec4(tmpvar_42))
    , vec4(tmpvar_43)), vec4(tmpvar_31)), mix (tmpvar_6, mix (tmpvar_5, tmpvar_2, vec4(tmpvar_40)), vec4(tmpvar_41)), vec4(tmpvar_30));
  };
  gl_FragData[0] = clr_1;
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
def c1, -1.00000000, 0.00000000, 1.00000000, 2.00000000
def c2, 0.50000000, 0.99000001, -0.10000000, 0.33333334
def c3, 0.00000000, -1.50000000, 1.50000000, 0.33000001
def c4, 3.50000000, 0.00000000, 5.50000000, 7.50000000
def c5, -3.50000000, 0.00000000, -5.50000000, -7.50000000
def c6, 0.25000000, -1.00000000, 0.20000000, 0.12500000
dcl_texcoord0 v0.xy
mov r0.zw, c0.xyxy
mad r1.xy, c3.zxzw, r0.zwzw, v0
mov r0.zw, c0.xyxy
mad r2.xy, c1.yzzw, r0.zwzw, v0
mov r14.xy, c0
mad r15.xy, c5.yxzw, r14, v0
mov r0.xy, c0
mad r0.xy, c3.yxzw, r0, v0
texld r7, r0, s0
texld r8, r1, s0
mov r0.xy, c0
mad r0.xy, c1.yxzw, r0, v0
texld r3, r0, s0
texld r4, r2, s0
mov r13.xy, c0
mad r13.xy, c4.ywzw, r13, v0
mov r14.xy, c0
mad r14.xy, c5.yzzw, r14, v0
texld r16, r14, s0
mov r14.xy, c0
mad r17.xy, c5.ywzw, r14, v0
mov r14.xy, c0
mad r14.xy, c4, r14, v0
mov r18.xy, c0
mad r18.xy, c4.zyzw, r18, v0
mov r19.xy, c0
mad r19.xy, c4.wyzw, r19, v0
mov r20.xy, c0
mad r20.xy, c5, r20, v0
mov r21.xy, c0
mad r21.xy, c5.zyzw, r21, v0
mov r22.xy, c0
mad r22.xy, c5.wyzw, r22, v0
texld r0, v0, s0
add r1, r7, r8
add r1, r0, r1
add r2.xyz, r3, r4
mad r2.xyz, -r0, c1.w, r2
abs r5.xyz, r2
mul r1, r1, c2.w
add r2, -r0, r1
dp3 r1.y, r1, c3.w
mul r5.xyz, r5, c2.x
dp3 r1.x, r5, c2.y
texld r14, r14, s0
texld r18, r18, s0
texld r19, r19, s0
texld r20, r20, s0
texld r21, r21, s0
rcp r1.y, r1.y
add r1.x, r1, c2.z
mul_sat r1.x, r1, r1.y
mad r10, r1.x, r2, r0
mov r1.zw, c0.xyxy
mad r2.xy, c3.xzzw, r1.zwzw, v0
mov r1.xy, c0
mad r1.xy, c3, r1, v0
texld r5, r1, s0
texld r6, r2, s0
add r9, r5, r6
mov r1.zw, c0.xyxy
mad r2.xy, c1.zyzw, r1.zwzw, v0
add r9, r0, r9
mov r1.xy, c0
mad r1.xy, c1, r1, v0
mul r9, r9, c2.w
texld r22, r22, s0
texld r2, r2, s0
texld r1, r1, s0
add r11.xyz, r1, r2
mad r11.xyz, -r0, c1.w, r11
abs r12.xyz, r11
add r11, r9, -r10
dp3 r9.y, r9, c3.w
mul r12.xyz, r12, c2.x
dp3 r9.x, r12, c2.y
mov r12.xy, c0
mad r12.xy, c4.yzzw, r12, v0
rcp r9.w, r9.y
add r9.z, r9.x, c2
mul_sat r9.z, r9, r9.w
mul r11, r9.z, r11
mov r9.xy, c0
mad r9.xy, c4.yxzw, r9, v0
texld r9, r9, s0
mad r10, r11, c2.x, r10
mov r11.xyz, r14
mov r14.xyz, r18
mov r18.xyz, r19
mov r19.xyz, r20
mov r20.xyz, r21
add r6.w, r6, r9
texld r12, r12, s0
add r6.w, r12, r6
texld r13, r13, s0
add r6.w, r13, r6
add r5.w, r5, r6
texld r15, r15, s0
add r5.w, r15, r5
add r6.w, r8, r14
add r6.w, r18, r6
add r6.w, r19, r6
add r6.w, r7, r6
add r6.w, r20, r6
add r6.w, r21, r6
add r6.w, r22, r6
add r5.w, r16, r5
texld r17, r17, s0
add r5.w, r17, r5
mad_sat r7.w, r5, c6.x, c6.y
mad_sat r6.w, r6, c6.x, c6.y
add r5.w, r6, -r7
abs r5.w, r5
mov r21.xyz, r22
if_gt r5.w, c6.z
add r6.xyz, r6, r9
add r6.xyz, r6, r12
add r6.xyz, r6, r13
add r5.xyz, r6, r5
add r5.xyz, r5, r15
add r5.xyz, r5, r16
add r6.xyz, r5, r17
mul r6.xyz, r6, c6.w
dp3 r8.w, r0, c3.w
add r5, r0, -r1
dp3 r9.x, r1, c3.w
dp3 r6.x, r6, c3.w
add r6.y, r6.x, -r9.x
add r9.y, r8.w, -r9.x
rcp r6.z, r9.y
mul_sat r6.z, r6.y, r6
abs r6.y, r9
cmp r6.y, -r6, c1, r6.z
mad r1, r6.y, r5, r1
dp3 r5.w, r2, c3.w
add r5.w, r8, -r5
rcp r6.y, r5.w
add r5.xyz, r8, r11
add r5.xyz, r5, r14
add r6.x, -r8.w, r6
add r0, r0, -r3
add r1, -r2, r1
add r5.xyz, r5, r18
mad_sat r6.x, r6, r6.y, c1.z
abs r5.w, r5
cmp r5.w, -r5, c1.y, r6.x
mad r1, r5.w, r1, r2
add r1, r1, -r10
add r2.xyz, r5, r7
dp3 r2.w, r3, c3.w
add r2.xyz, r2, r19
add r2.xyz, r2, r20
add r2.xyz, r2, r21
mul r2.xyz, r2, c6.w
dp3 r2.y, r2, c3.w
add r5.x, r8.w, -r2.w
mad r1, r7.w, r1, r10
add r2.x, r2.y, -r2.w
rcp r2.z, r5.x
mul_sat r2.z, r2.x, r2
abs r2.x, r5
cmp r2.z, -r2.x, c1.y, r2
mad r3, r2.z, r0, r3
dp3 r2.x, r4, c3.w
add r0.x, r8.w, -r2
rcp r0.z, r0.x
add r0.y, -r8.w, r2
mad_sat r0.y, r0, r0.z, c1.z
abs r0.x, r0
add r3, -r4, r3
cmp r0.x, -r0, c1.y, r0.y
mad r0, r0.x, r3, r4
add r0, r0, -r1
mad r10, r6.w, r0, r1
endif
mov_pp oC0, r10
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
ConstBuffer "$Globals" 32
Vector 16 [_MainTex_TexelSize]
BindCB  "$Globals" 0
"ps_4_0
eefiecedcoapepcdabpmihnnhdnjnmclpcpkfcbkabaaaaaanibbaaaaadaaaaaa
cmaaaaaaieaaaaaaliaaaaaaejfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcbibbaaaa
eaaaaaaaegaeaaaafjaaaaaeegiocaaaaaaaaaaaacaaaaaafkaaaaadaagabaaa
aaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaa
gfaaaaadpccabaaaaaaaaaaagiaaaaacalaaaaaadcaaaaanpcaabaaaaaaaaaaa
egiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaaaaaaaaaagaeaaaaaaaaaaaaalaea
egbebaaaabaaaaaaefaaaaajpcaabaaaabaaaaaaogakbaaaaaaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaaaaaaaaaegaabaaaaaaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaadcaaaaanpcaabaaaacaaaaaaegiecaaa
aaaaaaaaabaaaaaaaceaaaaaaaaaaaaaaaaamalpaaaaaaaaaaaamadpegbebaaa
abaaaaaaefaaaaajpcaabaaaadaaaaaaogakbaaaacaaaaaaeghobaaaaaaaaaaa
aagabaaaaaaaaaaaefaaaaajpcaabaaaacaaaaaaegaabaaaacaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaaaaaaaaahpcaabaaaaaaaaaaadgajbaaaaaaaaaaa
dgajbaaaadaaaaaaaaaaaaahpcaabaaaadaaaaaaegaobaaaadaaaaaaegaobaaa
acaaaaaaaaaaaaahpcaabaaaaaaaaaaadgajbaaaabaaaaaaegaobaaaaaaaaaaa
dcaaaaanpcaabaaaabaaaaaaegiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaaaaaa
aaaapaeaaaaaaaaaaaaagamaegbebaaaabaaaaaaefaaaaajpcaabaaaaeaaaaaa
egaabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaa
abaaaaaaogakbaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaah
pcaabaaaaaaaaaaaegaobaaaaaaaaaaadgajbaaaaeaaaaaaaaaaaaahpcaabaaa
aaaaaaaadgajbaaaacaaaaaaegaobaaaaaaaaaaaaaaaaaahpcaabaaaaaaaaaaa
dgajbaaaabaaaaaaegaobaaaaaaaaaaadcaaaaanpcaabaaaabaaaaaaegiecaaa
aaaaaaaaabaaaaaaaceaaaaaaaaaaaaaaaaalamaaaaaaaaaaaaapamaegbebaaa
abaaaaaaefaaaaajpcaabaaaacaaaaaaegaabaaaabaaaaaaeghobaaaaaaaaaaa
aagabaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaaogakbaaaabaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaaaaaaaaahpcaabaaaaaaaaaaaegaobaaaaaaaaaaa
dgajbaaaacaaaaaaaaaaaaahpcaabaaaaaaaaaaadgajbaaaabaaaaaaegaobaaa
aaaaaaaadiaaaaakocaabaaaaaaaaaaafgaobaaaaaaaaaaaaceaaaaaaaaaaaaa
aaaaaadoaaaaaadoaaaaaadodccaaaajbcaabaaaaaaaaaaaakaabaaaaaaaaaaa
abeaaaaaaaaaiadoabeaaaaaaaaaialpbaaaaaakccaabaaaaaaaaaaajgahbaaa
aaaaaaaaaceaaaaamdpfkidomdpfkidomdpfkidoaaaaaaaadcaaaaanpcaabaaa
abaaaaaaegiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaaialpaaaaaaaaaaaaiadp
aaaaaaaaegbebaaaabaaaaaaefaaaaajpcaabaaaacaaaaaaegaabaaaabaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaaogakbaaa
abaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaabaaaaaakecaabaaaaaaaaaaa
egacbaaaacaaaaaaaceaaaaamdpfkidomdpfkidomdpfkidoaaaaaaaaaaaaaaai
icaabaaaaaaaaaaackaabaiaebaaaaaaaaaaaaaabkaabaaaaaaaaaaaefaaaaaj
pcaabaaaaeaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
baaaaaakbcaabaaaafaaaaaaegacbaaaaeaaaaaaaceaaaaamdpfkidomdpfkido
mdpfkidoaaaaaaaaaaaaaaaiccaabaaaafaaaaaackaabaiaebaaaaaaaaaaaaaa
akaabaaaafaaaaaabiaaaaahecaabaaaaaaaaaaackaabaaaaaaaaaaaakaabaaa
afaaaaaaaocaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaabkaabaaaafaaaaaa
dhaaaaajecaabaaaaaaaaaaackaabaaaaaaaaaaaabeaaaaaaaaaaaaadkaabaaa
aaaaaaaaaaaaaaaipcaabaaaagaaaaaaegaobaiaebaaaaaaacaaaaaaegaobaaa
aeaaaaaadcaaaaajpcaabaaaagaaaaaakgakbaaaaaaaaaaaegaobaaaagaaaaaa
egaobaaaacaaaaaaaaaaaaahhcaabaaaacaaaaaaegacbaaaabaaaaaaegacbaaa
acaaaaaadcaaaaanhcaabaaaacaaaaaaegacbaiaebaaaaaaaeaaaaaaaceaaaaa
aaaaaaeaaaaaaaeaaaaaaaeaaaaaaaaaegacbaaaacaaaaaadiaaaaalhcaabaaa
acaaaaaaegacbaiaibaaaaaaacaaaaaaaceaaaaaaaaaaadpaaaaaadpaaaaaadp
aaaaaaaabaaaaaakecaabaaaaaaaaaaaegacbaaaacaaaaaaaceaaaaamdpfkido
mdpfkidomdpfkidoaaaaaaaadcaaaaajecaabaaaaaaaaaaackaabaaaaaaaaaaa
abeaaaaaaaaaeaeaabeaaaaamnmmmmlnaaaaaaaipcaabaaaacaaaaaaegaobaia
ebaaaaaaabaaaaaaegaobaaaagaaaaaaaaaaaaaiccaabaaaaaaaaaaabkaabaaa
aaaaaaaaakaabaiaebaaaaaaafaaaaaabaaaaaakicaabaaaaaaaaaaaegacbaaa
abaaaaaaaceaaaaamdpfkidomdpfkidomdpfkidoaaaaaaaaaaaaaaaiccaabaaa
afaaaaaadkaabaiaebaaaaaaaaaaaaaaakaabaaaafaaaaaabiaaaaahicaabaaa
aaaaaaaadkaabaaaaaaaaaaaakaabaaaafaaaaaaaoaaaaahccaabaaaaaaaaaaa
bkaabaaaaaaaaaaabkaabaaaafaaaaaaaacaaaahccaabaaaaaaaaaaabkaabaaa
aaaaaaaaabeaaaaaaaaaiadpdhaaaaajccaabaaaaaaaaaaadkaabaaaaaaaaaaa
abeaaaaaaaaaaaaabkaabaaaaaaaaaaadcaaaaajpcaabaaaabaaaaaafgafbaaa
aaaaaaaaegaobaaaacaaaaaaegaobaaaabaaaaaaaaaaaaahpcaabaaaacaaaaaa
egaobaaaaeaaaaaaegaobaaaaeaaaaaadcaaaaampcaabaaaadaaaaaaegaobaaa
adaaaaaaaceaaaaaaaaaaaeaaaaaaaeaaaaaaaeaaaaaaaeaegaobaaaacaaaaaa
diaaaaakocaabaaaafaaaaaaagajbaaaadaaaaaaaceaaaaaaaaaaaaaklkkckdo
klkkckdoklkkckdobaaaaaakccaabaaaaaaaaaaajgahbaaaafaaaaaaaceaaaaa
mdpfkidomdpfkidomdpfkidoaaaaaaaaaocaaaahccaabaaaaaaaaaaackaabaaa
aaaaaaaabkaabaaaaaaaaaaadiaaaaahccaabaaaaaaaaaaabkaabaaaaaaaaaaa
abeaaaaaaaaaaadpdcaaaaanpcaabaaaagaaaaaaegiecaaaaaaaaaaaabaaaaaa
aceaaaaaaaaamalpaaaaaaaaaaaamadpaaaaaaaaegbebaaaabaaaaaaefaaaaaj
pcaabaaaahaaaaaaegaabaaaagaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
efaaaaajpcaabaaaagaaaaaaogakbaaaagaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaaaaaaaaahpcaabaaaaiaaaaaaegaobaaaagaaaaaaegaobaaaahaaaaaa
dcaaaaampcaabaaaacaaaaaaegaobaaaaiaaaaaaaceaaaaaaaaaaaeaaaaaaaea
aaaaaaeaaaaaaaeaegaobaaaacaaaaaadcaaaaanpcaabaaaaiaaaaaaegaobaaa
acaaaaaaaceaaaaaklkkckdoklkkckdoklkkckdoklkkckdoegaobaiaebaaaaaa
aeaaaaaadiaaaaakhcaabaaaacaaaaaaegacbaaaacaaaaaaaceaaaaaklkkckdo
klkkckdoklkkckdoaaaaaaaabaaaaaakecaabaaaaaaaaaaaegacbaaaacaaaaaa
aceaaaaamdpfkidomdpfkidomdpfkidoaaaaaaaadcaaaaanpcaabaaaacaaaaaa
egiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaaaaaaaaaaialpaaaaaaaaaaaaiadp
egbebaaaabaaaaaaefaaaaajpcaabaaaajaaaaaaegaabaaaacaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaacaaaaaaogakbaaaacaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaahocaabaaaafaaaaaaagajbaaa
acaaaaaaagajbaaaajaaaaaadcaaaaanocaabaaaafaaaaaaagajbaiaebaaaaaa
aeaaaaaaaceaaaaaaaaaaaaaaaaaaaeaaaaaaaeaaaaaaaeafgaobaaaafaaaaaa
diaaaaalocaabaaaafaaaaaafgaobaiaibaaaaaaafaaaaaaaceaaaaaaaaaaaaa
aaaaaadpaaaaaadpaaaaaadpbaaaaaakicaabaaaaaaaaaaajgahbaaaafaaaaaa
aceaaaaamdpfkidomdpfkidomdpfkidoaaaaaaaadcaaaaajicaabaaaaaaaaaaa
dkaabaaaaaaaaaaaabeaaaaaaaaaeaeaabeaaaaamnmmmmlnaocaaaahecaabaaa
aaaaaaaadkaabaaaaaaaaaaackaabaaaaaaaaaaadcaaaaajpcaabaaaaiaaaaaa
kgakbaaaaaaaaaaaegaobaaaaiaaaaaaegaobaaaaeaaaaaaaaaaaaaipcaabaaa
aeaaaaaaegaobaaaaeaaaaaaegaobaiaebaaaaaaajaaaaaadcaaaaanpcaabaaa
adaaaaaaegaobaaaadaaaaaaaceaaaaaklkkckdoklkkckdoklkkckdoklkkckdo
egaobaiaebaaaaaaaiaaaaaadcaaaaajpcaabaaaadaaaaaafgafbaaaaaaaaaaa
egaobaaaadaaaaaaegaobaaaaiaaaaaaaaaaaaaipcaabaaaabaaaaaaegaobaaa
abaaaaaaegaobaiaebaaaaaaadaaaaaadcaaaaajpcaabaaaabaaaaaaagaabaaa
aaaaaaaaegaobaaaabaaaaaaegaobaaaadaaaaaadcaaaaanpcaabaaaaiaaaaaa
egiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaagaeaaaaaaaaaaaaalaeaaaaaaaaa
egbebaaaabaaaaaaefaaaaajpcaabaaaakaaaaaaegaabaaaaiaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaaiaaaaaaogakbaaaaiaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaahpcaabaaaagaaaaaadgajbaaa
agaaaaaadgajbaaaakaaaaaaaaaaaaahpcaabaaaagaaaaaadgajbaaaaiaaaaaa
egaobaaaagaaaaaadcaaaaanpcaabaaaaiaaaaaaegiecaaaaaaaaaaaabaaaaaa
aceaaaaaaaaapaeaaaaaaaaaaaaagamaaaaaaaaaegbebaaaabaaaaaaefaaaaaj
pcaabaaaakaaaaaaegaabaaaaiaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
efaaaaajpcaabaaaaiaaaaaaogakbaaaaiaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaaaaaaaaahpcaabaaaagaaaaaaegaobaaaagaaaaaadgajbaaaakaaaaaa
aaaaaaahpcaabaaaagaaaaaadgajbaaaahaaaaaaegaobaaaagaaaaaaaaaaaaah
pcaabaaaagaaaaaadgajbaaaaiaaaaaaegaobaaaagaaaaaadcaaaaanpcaabaaa
ahaaaaaaegiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaalamaaaaaaaaaaaaapama
aaaaaaaaegbebaaaabaaaaaaefaaaaajpcaabaaaaiaaaaaaegaabaaaahaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaahaaaaaaogakbaaa
ahaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaaaaaaaahpcaabaaaagaaaaaa
egaobaaaagaaaaaadgajbaaaaiaaaaaaaaaaaaahpcaabaaaagaaaaaadgajbaaa
ahaaaaaaegaobaaaagaaaaaadiaaaaakocaabaaaaaaaaaaafgaobaaaagaaaaaa
aceaaaaaaaaaaaaaaaaaaadoaaaaaadoaaaaaadodccaaaajccaabaaaafaaaaaa
akaabaaaagaaaaaaabeaaaaaaaaaiadoabeaaaaaaaaaialpbaaaaaakccaabaaa
aaaaaaaajgahbaaaaaaaaaaaaceaaaaamdpfkidomdpfkidomdpfkidoaaaaaaaa
baaaaaakecaabaaaaaaaaaaaegacbaaaajaaaaaaaceaaaaamdpfkidomdpfkido
mdpfkidoaaaaaaaaaaaaaaaiicaabaaaaaaaaaaackaabaiaebaaaaaaaaaaaaaa
bkaabaaaaaaaaaaaaaaaaaaiccaabaaaaaaaaaaaakaabaiaebaaaaaaafaaaaaa
bkaabaaaaaaaaaaaaaaaaaaiecaabaaaafaaaaaackaabaiaebaaaaaaaaaaaaaa
akaabaaaafaaaaaabiaaaaahecaabaaaaaaaaaaackaabaaaaaaaaaaaakaabaaa
afaaaaaaaocaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaackaabaaaafaaaaaa
dhaaaaajecaabaaaaaaaaaaackaabaaaaaaaaaaaabeaaaaaaaaaaaaadkaabaaa
aaaaaaaadcaaaaajpcaabaaaaeaaaaaakgakbaaaaaaaaaaaegaobaaaaeaaaaaa
egaobaaaajaaaaaaaaaaaaaipcaabaaaaeaaaaaaegaobaiaebaaaaaaacaaaaaa
egaobaaaaeaaaaaabaaaaaakecaabaaaaaaaaaaaegacbaaaacaaaaaaaceaaaaa
mdpfkidomdpfkidomdpfkidoaaaaaaaaaaaaaaaiicaabaaaaaaaaaaackaabaia
ebaaaaaaaaaaaaaaakaabaaaafaaaaaabiaaaaahecaabaaaaaaaaaaackaabaaa
aaaaaaaaakaabaaaafaaaaaaaoaaaaahccaabaaaaaaaaaaabkaabaaaaaaaaaaa
dkaabaaaaaaaaaaaaacaaaahccaabaaaaaaaaaaabkaabaaaaaaaaaaaabeaaaaa
aaaaiadpdhaaaaajccaabaaaaaaaaaaackaabaaaaaaaaaaaabeaaaaaaaaaaaaa
bkaabaaaaaaaaaaadcaaaaajpcaabaaaacaaaaaafgafbaaaaaaaaaaaegaobaaa
aeaaaaaaegaobaaaacaaaaaaaaaaaaaipcaabaaaacaaaaaaegaobaiaebaaaaaa
abaaaaaaegaobaaaacaaaaaadcaaaaajpcaabaaaabaaaaaafgafbaaaafaaaaaa
egaobaaaacaaaaaaegaobaaaabaaaaaaaaaaaaaibcaabaaaaaaaaaaaakaabaia
ebaaaaaaaaaaaaaabkaabaaaafaaaaaadbaaaaaibcaabaaaaaaaaaaaabeaaaaa
mnmmemdoakaabaiaibaaaaaaaaaaaaaadhaaaaajpccabaaaaaaaaaaaagaabaaa
aaaaaaaaegaobaaaabaaaaaaegaobaaaadaaaaaadoaaaaab"
}
}
 }
}
Fallback Off
}