»ËShader "Hidden/FXAA Preset 2" {
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
#extension GL_ARB_shader_texture_lod : enable
uniform sampler2D _MainTex;
uniform vec4 _MainTex_TexelSize;
varying vec2 xlv_TEXCOORD0;
void main ()
{
  vec2 rcpFrame_1;
  rcpFrame_1 = _MainTex_TexelSize.xy;
  vec3 tmpvar_2;
  bool doneP_4;
  bool doneN_5;
  float lumaEndP_6;
  float lumaEndN_7;
  vec2 offNP_8;
  vec2 posP_9;
  vec2 posN_10;
  float gradientN_11;
  float lengthSign_12;
  float lumaS_13;
  float lumaN_14;
  vec4 tmpvar_15;
  tmpvar_15.zw = vec2(0.0, 0.0);
  tmpvar_15.xy = (xlv_TEXCOORD0 + (vec2(0.0, -1.0) * _MainTex_TexelSize.xy));
  vec4 tmpvar_16;
  tmpvar_16 = texture2DLod (_MainTex, tmpvar_15.xy, 0.0);
  vec4 tmpvar_17;
  tmpvar_17.zw = vec2(0.0, 0.0);
  tmpvar_17.xy = (xlv_TEXCOORD0 + (vec2(-1.0, 0.0) * _MainTex_TexelSize.xy));
  vec4 tmpvar_18;
  tmpvar_18 = texture2DLod (_MainTex, tmpvar_17.xy, 0.0);
  vec4 tmpvar_19;
  tmpvar_19 = texture2DLod (_MainTex, xlv_TEXCOORD0, 0.0);
  vec4 tmpvar_20;
  tmpvar_20.zw = vec2(0.0, 0.0);
  tmpvar_20.xy = (xlv_TEXCOORD0 + (vec2(1.0, 0.0) * _MainTex_TexelSize.xy));
  vec4 tmpvar_21;
  tmpvar_21 = texture2DLod (_MainTex, tmpvar_20.xy, 0.0);
  vec4 tmpvar_22;
  tmpvar_22.zw = vec2(0.0, 0.0);
  tmpvar_22.xy = (xlv_TEXCOORD0 + (vec2(0.0, 1.0) * _MainTex_TexelSize.xy));
  vec4 tmpvar_23;
  tmpvar_23 = texture2DLod (_MainTex, tmpvar_22.xy, 0.0);
  float tmpvar_24;
  tmpvar_24 = ((tmpvar_16.y * 1.96321) + tmpvar_16.x);
  lumaN_14 = tmpvar_24;
  float tmpvar_25;
  tmpvar_25 = ((tmpvar_18.y * 1.96321) + tmpvar_18.x);
  float tmpvar_26;
  tmpvar_26 = ((tmpvar_19.y * 1.96321) + tmpvar_19.x);
  float tmpvar_27;
  tmpvar_27 = ((tmpvar_21.y * 1.96321) + tmpvar_21.x);
  float tmpvar_28;
  tmpvar_28 = ((tmpvar_23.y * 1.96321) + tmpvar_23.x);
  lumaS_13 = tmpvar_28;
  float tmpvar_29;
  tmpvar_29 = max (tmpvar_26, max (max (tmpvar_24, tmpvar_25), max (tmpvar_28, tmpvar_27)));
  float tmpvar_30;
  tmpvar_30 = (tmpvar_29 - min (tmpvar_26, min (
    min (tmpvar_24, tmpvar_25)
  , 
    min (tmpvar_28, tmpvar_27)
  )));
  float tmpvar_31;
  tmpvar_31 = max (0.0416667, (tmpvar_29 * 0.125));
  if ((tmpvar_30 < tmpvar_31)) {
    tmpvar_2 = tmpvar_19.xyz;
  } else {
    float tmpvar_32;
    tmpvar_32 = min (0.75, (max (0.0, 
      ((abs((
        ((((tmpvar_24 + tmpvar_25) + tmpvar_27) + tmpvar_28) * 0.25)
       - tmpvar_26)) / tmpvar_30) - 0.25)
    ) * 1.33333));
    vec4 tmpvar_33;
    tmpvar_33.zw = vec2(0.0, 0.0);
    tmpvar_33.xy = (xlv_TEXCOORD0 - _MainTex_TexelSize.xy);
    vec4 tmpvar_34;
    tmpvar_34 = texture2DLod (_MainTex, tmpvar_33.xy, 0.0);
    vec4 tmpvar_35;
    tmpvar_35.zw = vec2(0.0, 0.0);
    tmpvar_35.xy = (xlv_TEXCOORD0 + (vec2(1.0, -1.0) * _MainTex_TexelSize.xy));
    vec4 tmpvar_36;
    tmpvar_36 = texture2DLod (_MainTex, tmpvar_35.xy, 0.0);
    vec4 tmpvar_37;
    tmpvar_37.zw = vec2(0.0, 0.0);
    tmpvar_37.xy = (xlv_TEXCOORD0 + (vec2(-1.0, 1.0) * _MainTex_TexelSize.xy));
    vec4 tmpvar_38;
    tmpvar_38 = texture2DLod (_MainTex, tmpvar_37.xy, 0.0);
    vec4 tmpvar_39;
    tmpvar_39.zw = vec2(0.0, 0.0);
    tmpvar_39.xy = (xlv_TEXCOORD0 + _MainTex_TexelSize.xy);
    vec4 tmpvar_40;
    tmpvar_40 = texture2DLod (_MainTex, tmpvar_39.xy, 0.0);
    vec3 tmpvar_41;
    tmpvar_41 = (((
      (((tmpvar_16.xyz + tmpvar_18.xyz) + tmpvar_19.xyz) + tmpvar_21.xyz)
     + tmpvar_23.xyz) + (
      ((tmpvar_34.xyz + tmpvar_36.xyz) + tmpvar_38.xyz)
     + tmpvar_40.xyz)) * vec3(0.111111, 0.111111, 0.111111));
    float tmpvar_42;
    tmpvar_42 = ((tmpvar_34.y * 1.96321) + tmpvar_34.x);
    float tmpvar_43;
    tmpvar_43 = ((tmpvar_36.y * 1.96321) + tmpvar_36.x);
    float tmpvar_44;
    tmpvar_44 = ((tmpvar_38.y * 1.96321) + tmpvar_38.x);
    float tmpvar_45;
    tmpvar_45 = ((tmpvar_40.y * 1.96321) + tmpvar_40.x);
    bool tmpvar_46;
    tmpvar_46 = (((
      abs((((0.25 * tmpvar_42) + (-0.5 * tmpvar_25)) + (0.25 * tmpvar_44)))
     + 
      abs((((0.5 * tmpvar_24) - tmpvar_26) + (0.5 * tmpvar_28)))
    ) + abs(
      (((0.25 * tmpvar_43) + (-0.5 * tmpvar_27)) + (0.25 * tmpvar_45))
    )) >= ((
      abs((((0.25 * tmpvar_42) + (-0.5 * tmpvar_24)) + (0.25 * tmpvar_43)))
     + 
      abs((((0.5 * tmpvar_25) - tmpvar_26) + (0.5 * tmpvar_27)))
    ) + abs(
      (((0.25 * tmpvar_44) + (-0.5 * tmpvar_28)) + (0.25 * tmpvar_45))
    )));
    float tmpvar_47;
    if (tmpvar_46) {
      tmpvar_47 = -(_MainTex_TexelSize.y);
    } else {
      tmpvar_47 = -(_MainTex_TexelSize.x);
    };
    lengthSign_12 = tmpvar_47;
    if (!(tmpvar_46)) {
      lumaN_14 = tmpvar_25;
    };
    if (!(tmpvar_46)) {
      lumaS_13 = tmpvar_27;
    };
    float tmpvar_48;
    tmpvar_48 = abs((lumaN_14 - tmpvar_26));
    gradientN_11 = tmpvar_48;
    float tmpvar_49;
    tmpvar_49 = abs((lumaS_13 - tmpvar_26));
    lumaN_14 = ((lumaN_14 + tmpvar_26) * 0.5);
    float tmpvar_50;
    tmpvar_50 = ((lumaS_13 + tmpvar_26) * 0.5);
    lumaS_13 = tmpvar_50;
    bool tmpvar_51;
    tmpvar_51 = (tmpvar_48 >= tmpvar_49);
    if (!(tmpvar_51)) {
      lumaN_14 = tmpvar_50;
    };
    if (!(tmpvar_51)) {
      gradientN_11 = tmpvar_49;
    };
    if (!(tmpvar_51)) {
      lengthSign_12 = -(tmpvar_47);
    };
    float tmpvar_52;
    if (tmpvar_46) {
      tmpvar_52 = 0.0;
    } else {
      tmpvar_52 = (lengthSign_12 * 0.5);
    };
    posN_10.x = (xlv_TEXCOORD0.x + tmpvar_52);
    float tmpvar_53;
    if (tmpvar_46) {
      tmpvar_53 = (lengthSign_12 * 0.5);
    } else {
      tmpvar_53 = 0.0;
    };
    posN_10.y = (xlv_TEXCOORD0.y + tmpvar_53);
    gradientN_11 = (gradientN_11 * 0.25);
    posP_9 = posN_10;
    vec2 tmpvar_54;
    if (tmpvar_46) {
      vec2 tmpvar_55;
      tmpvar_55.y = 0.0;
      tmpvar_55.x = rcpFrame_1.x;
      tmpvar_54 = tmpvar_55;
    } else {
      vec2 tmpvar_56;
      tmpvar_56.x = 0.0;
      tmpvar_56.y = rcpFrame_1.y;
      tmpvar_54 = tmpvar_56;
    };
    lumaEndN_7 = lumaN_14;
    lumaEndP_6 = lumaN_14;
    doneN_5 = bool(0);
    doneP_4 = bool(0);
    posN_10 = (posN_10 + (tmpvar_54 * vec2(-1.5, -1.5)));
    posP_9 = (posP_9 + (tmpvar_54 * vec2(1.5, 1.5)));
    offNP_8 = (tmpvar_54 * vec2(2.0, 2.0));
    for (int i_3 = 0; i_3 < 8; i_3++) {
      if (!(doneN_5)) {
        vec4 tmpvar_57;
        tmpvar_57 = texture2DGradARB (_MainTex, posN_10, offNP_8, offNP_8);
        lumaEndN_7 = ((tmpvar_57.y * 1.96321) + tmpvar_57.x);
      };
      if (!(doneP_4)) {
        vec4 tmpvar_58;
        tmpvar_58 = texture2DGradARB (_MainTex, posP_9, offNP_8, offNP_8);
        lumaEndP_6 = ((tmpvar_58.y * 1.96321) + tmpvar_58.x);
      };
      bool tmpvar_59;
      if (doneN_5) {
        tmpvar_59 = bool(1);
      } else {
        tmpvar_59 = (abs((lumaEndN_7 - lumaN_14)) >= gradientN_11);
      };
      doneN_5 = tmpvar_59;
      bool tmpvar_60;
      if (doneP_4) {
        tmpvar_60 = bool(1);
      } else {
        tmpvar_60 = (abs((lumaEndP_6 - lumaN_14)) >= gradientN_11);
      };
      doneP_4 = tmpvar_60;
      if ((tmpvar_59 && tmpvar_60)) {
        break;
      };
      if (!(tmpvar_59)) {
        posN_10 = (posN_10 - offNP_8);
      };
      if (!(tmpvar_60)) {
        posP_9 = (posP_9 + offNP_8);
      };
    };
    float tmpvar_61;
    if (tmpvar_46) {
      tmpvar_61 = (xlv_TEXCOORD0.x - posN_10.x);
    } else {
      tmpvar_61 = (xlv_TEXCOORD0.y - posN_10.y);
    };
    float tmpvar_62;
    if (tmpvar_46) {
      tmpvar_62 = (posP_9.x - xlv_TEXCOORD0.x);
    } else {
      tmpvar_62 = (posP_9.y - xlv_TEXCOORD0.y);
    };
    bool tmpvar_63;
    tmpvar_63 = (tmpvar_61 < tmpvar_62);
    float tmpvar_64;
    if (tmpvar_63) {
      tmpvar_64 = lumaEndN_7;
    } else {
      tmpvar_64 = lumaEndP_6;
    };
    lumaEndN_7 = tmpvar_64;
    if ((((tmpvar_26 - lumaN_14) < 0.0) == ((tmpvar_64 - lumaN_14) < 0.0))) {
      lengthSign_12 = 0.0;
    };
    float tmpvar_65;
    tmpvar_65 = (tmpvar_62 + tmpvar_61);
    float tmpvar_66;
    if (tmpvar_63) {
      tmpvar_66 = tmpvar_61;
    } else {
      tmpvar_66 = tmpvar_62;
    };
    float tmpvar_67;
    tmpvar_67 = ((0.5 + (tmpvar_66 * 
      (-1.0 / tmpvar_65)
    )) * lengthSign_12);
    float tmpvar_68;
    if (tmpvar_46) {
      tmpvar_68 = 0.0;
    } else {
      tmpvar_68 = tmpvar_67;
    };
    float tmpvar_69;
    if (tmpvar_46) {
      tmpvar_69 = tmpvar_67;
    } else {
      tmpvar_69 = 0.0;
    };
    vec2 tmpvar_70;
    tmpvar_70.x = (xlv_TEXCOORD0.x + tmpvar_68);
    tmpvar_70.y = (xlv_TEXCOORD0.y + tmpvar_69);
    vec4 tmpvar_71;
    tmpvar_71 = texture2DLod (_MainTex, tmpvar_70, 0.0);
    vec3 tmpvar_72;
    tmpvar_72.x = -(tmpvar_32);
    tmpvar_72.y = -(tmpvar_32);
    tmpvar_72.z = -(tmpvar_32);
    tmpvar_2 = ((tmpvar_72 * tmpvar_71.xyz) + ((tmpvar_41 * vec3(tmpvar_32)) + tmpvar_71.xyz));
  };
  vec4 tmpvar_73;
  tmpvar_73.w = 0.0;
  tmpvar_73.xyz = tmpvar_2;
  gl_FragData[0] = tmpvar_73;
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
eefiecedaffpdldohodkdgpagjklpapmmnbhcfmlabaaaaaaoeabaaaaadaaaaaa
cmaaaaaaiaaaaaaaniaaaaaaejfdeheoemaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaaebaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafaepfdejfeejepeoaafeeffiedepepfceeaaklkl
epfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaa
fdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklklfdeieefcaeabaaaa
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
def c1, 0.00000000, -1.00000000, 1.00000000, 1.96321082
def c2, 0.12500000, 0.04166667, 0.11111111, 0.25000000
def c3, -0.25000000, 1.33333337, 0.75000000, -0.50000000
def c4, 0.50000000, -1.50000000, 1.50000000, 2.00000000
defi i0, 8, 0, 1, 0
dcl_texcoord0 v0.xy
mov r0.xy, c0
mov r2.xy, c0
mov r0.z, c1.x
mad r0.xy, c1.yxzw, r0, v0
texldl r1.xyz, r0.xyzz, s0
mov r0.xy, c0
mad r1.w, r1.y, c1, r1.x
mov r2.z, c1.x
mad r2.xy, c1.xzzw, r2, v0
texldl r4.xyz, r2.xyzz, s0
mov r2.xy, c0
mov r2.z, c1.x
mad r2.xy, c1.zxzw, r2, v0
texldl r3.xyz, r2.xyzz, s0
mad r4.w, r4.y, c1, r4.x
mad r3.w, r3.y, c1, r3.x
min r6.y, r3.w, r4.w
mov r0.z, c1.x
mad r0.xy, c1, r0, v0
texldl r0.xyz, r0.xyzz, s0
mad r0.w, r0.y, c1, r0.x
min r6.x, r0.w, r1.w
min r6.x, r6, r6.y
max r2.w, r0, r1
max r5.w, r3, r4
max r5.w, r2, r5
mov r2.z, c1.x
mov r2.xy, v0
texldl r2.xyz, r2.xyzz, s0
mad r2.w, r2.y, c1, r2.x
max r5.w, r2, r5
mul r6.y, r5.w, c2.x
min r6.x, r2.w, r6
max r6.y, r6, c2
add r5.w, r5, -r6.x
add r6.x, r5.w, -r6.y
cmp_pp r6.y, r6.x, c1.z, c1.x
cmp r5.xyz, r6.x, r5, r2
if_gt r6.y, c1.x
add r0.xyz, r0, r1
add r0.xyz, r0, r2
add r0.xyz, r0, r3
add r0.xyz, r0, r4
mad r9.w, r1, c4.x, -r2
mov r5.xy, c0
mad r5.xy, c1.yzzw, r5, v0
mov r5.z, c1.x
texldl r7.xyz, r5.xyzz, s0
mul r5.x, r4.w, c3.w
mad r6.w, r7.y, c1, r7.x
mad r8.w, r6, c2, r5.x
mad r9.w, r3, c4.x, r9
mov r5.z, c1.x
add r5.xy, v0, c0
texldl r8.xyz, r5.xyzz, s0
mov r5.xy, c0
mad r5.xy, c1.zyzw, r5, v0
mov r5.z, c1.x
texldl r6.xyz, r5.xyzz, s0
mul r5.x, r3.w, c3.w
mad r7.w, r6.y, c1, r6.x
mad r5.y, r7.w, c2.w, r5.x
mad r5.x, r8.y, c1.w, r8
mad r8.w, r5.x, c2, r8
mad r9.x, r5, c2.w, r5.y
mul r9.z, r0.w, c3.w
mov r5.z, c1.x
add r5.xy, v0, -c0
texldl r5.xyz, r5.xyzz, s0
mad r9.y, r5, c1.w, r5.x
mad r9.z, r9.y, c2.w, r9
mad r7.w, r7, c2, r9.z
add r1.xyz, r5, r6
add r1.xyz, r1, r7
add r1.xyz, r1, r8
add r0.xyz, r0, r1
abs r9.z, r9.w
abs r7.w, r7
add r7.w, r7, r9.z
abs r8.w, r8
add r8.w, r7, r8
abs r7.w, r9.x
mul r9.x, r1.w, c3.w
mad r9.x, r9.y, c2.w, r9
mad r9.z, r0.w, c4.x, -r2.w
mad r6.w, r6, c2, r9.x
mad r9.y, r4.w, c4.x, r9.z
abs r9.x, r9.y
abs r6.w, r6
add r6.w, r6, r9.x
add r7.w, r6, r7
add r6.w, r7, -r8
cmp r6.w, r6, c1.z, c1.x
abs_pp r9.x, r6.w
cmp r9.z, -r9.x, r3.w, r4.w
cmp r9.x, -r9, r1.w, r0.w
mul r1.xyz, r0, c2.z
add r2.z, r7.w, -r8.w
add r9.w, -r2, r9.z
add r9.y, -r2.w, r9.x
add r0.y, r2.w, r9.x
add r0.x, r2.w, r9.z
abs r9.w, r9
abs r9.y, r9
add r10.x, r9.y, -r9.w
cmp r2.x, r10, c1.z, c1
abs_pp r2.x, r2
cmp r0.z, r2, -c0.y, -c0.x
mul r0.y, r0, c4.x
mul r0.x, r0, c4
cmp r0.x, -r2, r0, r0.y
add r0.y, r0.w, r1.w
add r0.y, r0, r3.w
add r0.y, r0, r4.w
cmp r1.w, -r2.x, r9, r9.y
cmp r0.z, -r2.x, -r0, r0
mul r2.x, r0.z, c4
cmp r2.y, r2.z, r2.x, c1.x
cmp r2.x, r2.z, c1, r2
add r3.y, v0, r2
add r3.x, v0, r2
mad r0.y, r0, c2.w, -r2.w
mov r2.y, c0
mov r2.x, c1
mov r0.w, r0.x
mov r4.x, c0
mov r4.y, c1.x
cmp r4.xy, r2.z, r4, r2
mad r2.xy, r4, c4.y, r3
mad r3.xy, r4, c4.z, r3
mul r3.zw, r4.xyxy, c4.w
rcp r2.z, r5.w
abs r0.y, r0
mad r0.y, r0, r2.z, c3.x
mov r4.z, r0.x
max r0.y, r0, c1.x
mov r4.w, r0.x
mul r0.x, r0.y, c3.y
mul r1.w, r1, c2
min r2.z, r0.x, c3
mov_pp r4.x, c1
mov_pp r4.y, c1.x
loop aL, i0
if_eq r4.x, c1.x
texldd r0.xy, r2, s0, r3.zwzw, r3.zwzw
mad r4.z, r0.y, c1.w, r0.x
endif
if_eq r4.y, c1.x
texldd r0.xy, r3, s0, r3.zwzw, r3.zwzw
mad r4.w, r0.y, c1, r0.x
endif
add r0.y, -r0.w, r4.w
add r0.x, r4.z, -r0.w
abs r0.y, r0
abs r0.x, r0
add r0.y, -r1.w, r0
add r0.x, r0, -r1.w
cmp r0.x, r0, c1.z, c1
cmp r0.y, r0, c1.z, c1.x
add_pp_sat r4.y, r4, r0
add_pp_sat r4.x, r4, r0
mul_pp r0.x, r4, r4.y
break_gt r0.x, c1.x
add r0.xy, r2, -r3.zwzw
abs_pp r5.x, r4
cmp r2.xy, -r5.x, r0, r2
add r0.xy, r3.zwzw, r3
abs_pp r5.x, r4.y
cmp r3.xy, -r5.x, r0, r3
endloop
add r0.y, -v0.x, r3.x
add r0.x, -v0.y, r3.y
cmp r1.w, -r6, r0.x, r0.y
add r0.y, v0.x, -r2.x
add r0.x, v0.y, -r2.y
cmp r0.x, -r6.w, r0, r0.y
add r0.y, r0.x, -r1.w
cmp r2.x, r0.y, r4.w, r4.z
add r2.x, -r0.w, r2
cmp r0.y, r0, r1.w, r0.x
add r0.w, r2, -r0
add r1.w, r0.x, r1
cmp r2.x, r2, c1, c1.z
cmp r0.w, r0, c1.x, c1.z
add_pp r0.w, r0, -r2.x
abs_pp r0.x, r0.w
cmp r0.x, -r0, c1, r0.z
rcp r0.w, r1.w
mad r0.y, r0, -r0.w, c4.x
mul r0.y, r0, r0.x
cmp r0.x, -r6.w, r0.y, c1
cmp r0.y, -r6.w, c1.x, r0
add r0.x, v0, r0
mov r0.z, c1.x
add r0.y, v0, r0
texldl r0.xyz, r0.xyzz, s0
mad r1.xyz, r2.z, r1, r0
mad r5.xyz, -r2.z, r0, r1
endif
mov oC0.xyz, r5
mov oC0.w, c1.x
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
ConstBuffer "$Globals" 32
Vector 16 [_MainTex_TexelSize]
BindCB  "$Globals" 0
"ps_4_0
eefiecedogndihbcdlobcanplnflemfocekcmgonabaaaaaakibeaaaaadaaaaaa
cmaaaaaaieaaaaaaliaaaaaaejfdeheofaaaaaaaacaaaaaaaiaaaaaadiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaeeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcoibdaaaa
eaaaaaaapkaeaaaafjaaaaaeegiocaaaaaaaaaaaacaaaaaafkaaaaadaagabaaa
aaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaa
gfaaaaadpccabaaaaaaaaaaagiaaaaacajaaaaaadcaaaaanpcaabaaaaaaaaaaa
egiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaaaaaaaaaaialpaaaaialpaaaaaaaa
egbebaaaabaaaaaaeiaaaaalpcaabaaaabaaaaaaegaabaaaaaaaaaaaeghobaaa
aaaaaaaaaagabaaaaaaaaaaaabeaaaaaaaaaaaaaeiaaaaalpcaabaaaaaaaaaaa
ogakbaaaaaaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaabeaaaaaaaaaaaaa
eiaaaaalpcaabaaaacaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaaabeaaaaaaaaaaaaadcaaaaanpcaabaaaadaaaaaaegiecaaaaaaaaaaa
abaaaaaaaceaaaaaaaaaiadpaaaaaaaaaaaaaaaaaaaaiadpegbebaaaabaaaaaa
eiaaaaalpcaabaaaaeaaaaaaegaabaaaadaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaaabeaaaaaaaaaaaaaeiaaaaalpcaabaaaadaaaaaaogakbaaaadaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaaabeaaaaaaaaaaaaadcaaaaajicaabaaa
aaaaaaaabkaabaaaabaaaaaaabeaaaaahnekpldpakaabaaaabaaaaaadcaaaaaj
icaabaaaabaaaaaabkaabaaaaaaaaaaaabeaaaaahnekpldpakaabaaaaaaaaaaa
dcaaaaajicaabaaaacaaaaaabkaabaaaacaaaaaaabeaaaaahnekpldpakaabaaa
acaaaaaadcaaaaajicaabaaaadaaaaaabkaabaaaaeaaaaaaabeaaaaahnekpldp
akaabaaaaeaaaaaadcaaaaajicaabaaaaeaaaaaabkaabaaaadaaaaaaabeaaaaa
hnekpldpakaabaaaadaaaaaaddaaaaahbcaabaaaafaaaaaadkaabaaaaaaaaaaa
dkaabaaaabaaaaaaddaaaaahccaabaaaafaaaaaadkaabaaaadaaaaaadkaabaaa
aeaaaaaaddaaaaahbcaabaaaafaaaaaabkaabaaaafaaaaaaakaabaaaafaaaaaa
ddaaaaahbcaabaaaafaaaaaadkaabaaaacaaaaaaakaabaaaafaaaaaadeaaaaah
ccaabaaaafaaaaaadkaabaaaaaaaaaaadkaabaaaabaaaaaadeaaaaahecaabaaa
afaaaaaadkaabaaaadaaaaaadkaabaaaaeaaaaaadeaaaaahccaabaaaafaaaaaa
ckaabaaaafaaaaaabkaabaaaafaaaaaadeaaaaahccaabaaaafaaaaaadkaabaaa
acaaaaaabkaabaaaafaaaaaaaaaaaaaibcaabaaaafaaaaaaakaabaiaebaaaaaa
afaaaaaabkaabaaaafaaaaaadiaaaaahccaabaaaafaaaaaabkaabaaaafaaaaaa
abeaaaaaaaaaaadodeaaaaahccaabaaaafaaaaaabkaabaaaafaaaaaaabeaaaaa
klkkckdnbnaaaaahccaabaaaafaaaaaaakaabaaaafaaaaaabkaabaaaafaaaaaa
bpaaaeadbkaabaaaafaaaaaaaaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaa
egacbaaaabaaaaaaaaaaaaahhcaabaaaaaaaaaaaegacbaaaacaaaaaaegacbaaa
aaaaaaaaaaaaaaahhcaabaaaaaaaaaaaegacbaaaaeaaaaaaegacbaaaaaaaaaaa
aaaaaaahhcaabaaaaaaaaaaaegacbaaaadaaaaaaegacbaaaaaaaaaaaaaaaaaah
bcaabaaaabaaaaaadkaabaaaaaaaaaaadkaabaaaabaaaaaaaaaaaaahbcaabaaa
abaaaaaadkaabaaaadaaaaaaakaabaaaabaaaaaaaaaaaaahbcaabaaaabaaaaaa
dkaabaaaaeaaaaaaakaabaaaabaaaaaadcaaaaakbcaabaaaabaaaaaaakaabaaa
abaaaaaaabeaaaaaaaaaiadodkaabaiaebaaaaaaacaaaaaaaoaaaaaibcaabaaa
abaaaaaaakaabaiaibaaaaaaabaaaaaaakaabaaaafaaaaaaaaaaaaahbcaabaaa
abaaaaaaakaabaaaabaaaaaaabeaaaaaaaaaialodeaaaaahbcaabaaaabaaaaaa
akaabaaaabaaaaaaabeaaaaaaaaaaaaadiaaaaahbcaabaaaabaaaaaaakaabaaa
abaaaaaaabeaaaaaklkkkkdpddaaaaahbcaabaaaabaaaaaaakaabaaaabaaaaaa
abeaaaaaaaaaeadpaaaaaaajgcaabaaaabaaaaaaagbbbaaaabaaaaaaagibcaia
ebaaaaaaaaaaaaaaabaaaaaaeiaaaaalpcaabaaaafaaaaaajgafbaaaabaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaaabeaaaaaaaaaaaaadcaaaaanpcaabaaa
agaaaaaaegiecaaaaaaaaaaaabaaaaaaaceaaaaaaaaaiadpaaaaialpaaaaialp
aaaaiadpegbebaaaabaaaaaaeiaaaaalpcaabaaaahaaaaaaegaabaaaagaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaaabeaaaaaaaaaaaaaeiaaaaalpcaabaaa
agaaaaaaogakbaaaagaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaabeaaaaa
aaaaaaaaaaaaaaaigcaabaaaabaaaaaaagbbbaaaabaaaaaaagibcaaaaaaaaaaa
abaaaaaaeiaaaaalpcaabaaaaiaaaaaajgafbaaaabaaaaaaeghobaaaaaaaaaaa
aagabaaaaaaaaaaaabeaaaaaaaaaaaaaaaaaaaahhcaabaaaadaaaaaaegacbaaa
afaaaaaaegacbaaaahaaaaaaaaaaaaahhcaabaaaadaaaaaaegacbaaaagaaaaaa
egacbaaaadaaaaaaaaaaaaahhcaabaaaadaaaaaaegacbaaaaiaaaaaaegacbaaa
adaaaaaaaaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaadaaaaaa
diaaaaahhcaabaaaaaaaaaaaagaabaaaabaaaaaaegacbaaaaaaaaaaadcaaaaaj
ccaabaaaabaaaaaabkaabaaaafaaaaaaabeaaaaahnekpldpakaabaaaafaaaaaa
dcaaaaajecaabaaaabaaaaaabkaabaaaahaaaaaaabeaaaaahnekpldpakaabaaa
ahaaaaaadcaaaaajbcaabaaaadaaaaaabkaabaaaagaaaaaaabeaaaaahnekpldp
akaabaaaagaaaaaadcaaaaajccaabaaaadaaaaaabkaabaaaaiaaaaaaabeaaaaa
hnekpldpakaabaaaaiaaaaaadiaaaaahecaabaaaadaaaaaadkaabaaaaaaaaaaa
abeaaaaaaaaaaalpdcaaaaajecaabaaaadaaaaaabkaabaaaabaaaaaaabeaaaaa
aaaaiadockaabaaaadaaaaaadcaaaaajecaabaaaadaaaaaackaabaaaabaaaaaa
abeaaaaaaaaaiadockaabaaaadaaaaaadiaaaaahbcaabaaaaeaaaaaadkaabaaa
abaaaaaaabeaaaaaaaaaaalpdcaaaaakccaabaaaaeaaaaaadkaabaaaabaaaaaa
abeaaaaaaaaaaadpdkaabaiaebaaaaaaacaaaaaadiaaaaahecaabaaaaeaaaaaa
dkaabaaaadaaaaaaabeaaaaaaaaaaalpdcaaaaajccaabaaaaeaaaaaadkaabaaa
adaaaaaaabeaaaaaaaaaaadpbkaabaaaaeaaaaaaaaaaaaajecaabaaaadaaaaaa
ckaabaiaibaaaaaaadaaaaaabkaabaiaibaaaaaaaeaaaaaadiaaaaahccaabaaa
aeaaaaaadkaabaaaaeaaaaaaabeaaaaaaaaaaalpdcaaaaajccaabaaaaeaaaaaa
akaabaaaadaaaaaaabeaaaaaaaaaiadobkaabaaaaeaaaaaadcaaaaajccaabaaa
aeaaaaaabkaabaaaadaaaaaaabeaaaaaaaaaiadobkaabaaaaeaaaaaaaaaaaaai
ecaabaaaadaaaaaackaabaaaadaaaaaabkaabaiaibaaaaaaaeaaaaaadcaaaaaj
ccaabaaaabaaaaaabkaabaaaabaaaaaaabeaaaaaaaaaiadoakaabaaaaeaaaaaa
dcaaaaajccaabaaaabaaaaaaakaabaaaadaaaaaaabeaaaaaaaaaiadobkaabaaa
abaaaaaadcaaaaakbcaabaaaadaaaaaadkaabaaaaaaaaaaaabeaaaaaaaaaaadp
dkaabaiaebaaaaaaacaaaaaadcaaaaajbcaabaaaadaaaaaadkaabaaaaeaaaaaa
abeaaaaaaaaaaadpakaabaaaadaaaaaaaaaaaaajccaabaaaabaaaaaabkaabaia
ibaaaaaaabaaaaaaakaabaiaibaaaaaaadaaaaaadcaaaaajecaabaaaabaaaaaa
ckaabaaaabaaaaaaabeaaaaaaaaaiadockaabaaaaeaaaaaadcaaaaajecaabaaa
abaaaaaabkaabaaaadaaaaaaabeaaaaaaaaaiadockaabaaaabaaaaaaaaaaaaai
ccaabaaaabaaaaaackaabaiaibaaaaaaabaaaaaabkaabaaaabaaaaaabnaaaaah
ccaabaaaabaaaaaabkaabaaaabaaaaaackaabaaaadaaaaaadhaaaaanecaabaaa
abaaaaaabkaabaaaabaaaaaabkiacaiaebaaaaaaaaaaaaaaabaaaaaaakiacaia
ebaaaaaaaaaaaaaaabaaaaaadhaaaaajicaabaaaaaaaaaaabkaabaaaabaaaaaa
dkaabaaaaaaaaaaadkaabaaaabaaaaaadhaaaaajicaabaaaabaaaaaabkaabaaa
abaaaaaadkaabaaaaeaaaaaadkaabaaaadaaaaaaaaaaaaaibcaabaaaadaaaaaa
dkaabaiaebaaaaaaacaaaaaadkaabaaaaaaaaaaaaaaaaaaiccaabaaaadaaaaaa
dkaabaiaebaaaaaaacaaaaaadkaabaaaabaaaaaaaaaaaaahicaabaaaaaaaaaaa
dkaabaaaacaaaaaadkaabaaaaaaaaaaadiaaaaahicaabaaaaaaaaaaadkaabaaa
aaaaaaaaabeaaaaaaaaaaadpaaaaaaahicaabaaaabaaaaaadkaabaaaacaaaaaa
dkaabaaaabaaaaaadiaaaaahicaabaaaabaaaaaadkaabaaaabaaaaaaabeaaaaa
aaaaaadpbnaaaaajecaabaaaadaaaaaaakaabaiaibaaaaaaadaaaaaabkaabaia
ibaaaaaaadaaaaaadhaaaaajicaabaaaaaaaaaaackaabaaaadaaaaaadkaabaaa
aaaaaaaadkaabaaaabaaaaaadhaaaaalicaabaaaabaaaaaackaabaaaadaaaaaa
akaabaiaibaaaaaaadaaaaaabkaabaiaibaaaaaaadaaaaaadhaaaaakecaabaaa
abaaaaaackaabaaaadaaaaaackaabaaaabaaaaaackaabaiaebaaaaaaabaaaaaa
diaaaaahbcaabaaaadaaaaaackaabaaaabaaaaaaabeaaaaaaaaaaadpdhaaaaaj
ccaabaaaadaaaaaabkaabaaaabaaaaaaabeaaaaaaaaaaaaaakaabaaaadaaaaaa
abaaaaahbcaabaaaadaaaaaabkaabaaaabaaaaaaakaabaaaadaaaaaaaaaaaaah
dcaabaaaaeaaaaaabgafbaaaadaaaaaaegbabaaaabaaaaaadiaaaaahicaabaaa
abaaaaaadkaabaaaabaaaaaaabeaaaaaaaaaiadodgaaaaaigcaabaaaadaaaaaa
aceaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaadgaaaaagjcaabaaaadaaaaaa
agiecaaaaaaaaaaaabaaaaaadhaaaaajdcaabaaaadaaaaaafgafbaaaabaaaaaa
egaabaaaadaaaaaaogakbaaaadaaaaaadcaaaaampcaabaaaaeaaaaaaegaebaaa
adaaaaaaaceaaaaaaaaamalpaaaamalpaaaamadpaaaamadpegaebaaaaeaaaaaa
aaaaaaahmcaabaaaadaaaaaaagaebaaaadaaaaaaagaebaaaadaaaaaadgaaaaaf
pcaabaaaafaaaaaaegaobaaaaeaaaaaadgaaaaafdcaabaaaagaaaaaapgapbaaa
aaaaaaaadgaaaaaimcaabaaaagaaaaaaaceaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
aaaaaaaadgaaaaafbcaabaaaahaaaaaaabeaaaaaaaaaaaaadaaaaaabcbaaaaah
ccaabaaaahaaaaaaakaabaaaahaaaaaaabeaaaaaaiaaaaaaadaaaeadbkaabaaa
ahaaaaaabpaaaaadckaabaaaagaaaaaaejaaaaanpcaabaaaaiaaaaaaegaabaaa
afaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaogakbaaaadaaaaaaogakbaaa
adaaaaaadcaaaaajccaabaaaahaaaaaabkaabaaaaiaaaaaaabeaaaaahnekpldp
akaabaaaaiaaaaaabcaaaaabdgaaaaafccaabaaaahaaaaaaakaabaaaagaaaaaa
bfaaaaabbpaaaaaddkaabaaaagaaaaaaejaaaaanpcaabaaaaiaaaaaaogakbaaa
afaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaogakbaaaadaaaaaaogakbaaa
adaaaaaadcaaaaajecaabaaaahaaaaaabkaabaaaaiaaaaaaabeaaaaahnekpldp
akaabaaaaiaaaaaabcaaaaabdgaaaaafecaabaaaahaaaaaabkaabaaaagaaaaaa
bfaaaaabaaaaaaaiicaabaaaahaaaaaadkaabaiaebaaaaaaaaaaaaaabkaabaaa
ahaaaaaabnaaaaaiicaabaaaahaaaaaadkaabaiaibaaaaaaahaaaaaadkaabaaa
abaaaaaadmaaaaahecaabaaaagaaaaaackaabaaaagaaaaaadkaabaaaahaaaaaa
aaaaaaaiicaabaaaahaaaaaadkaabaiaebaaaaaaaaaaaaaackaabaaaahaaaaaa
bnaaaaaiicaabaaaahaaaaaadkaabaiaibaaaaaaahaaaaaadkaabaaaabaaaaaa
dmaaaaahicaabaaaagaaaaaadkaabaaaagaaaaaadkaabaaaahaaaaaaabaaaaah
icaabaaaahaaaaaadkaabaaaagaaaaaackaabaaaagaaaaaabpaaaeaddkaabaaa
ahaaaaaadgaaaaafdcaabaaaagaaaaaajgafbaaaahaaaaaaacaaaaabbfaaaaab
dcaaaaandcaabaaaaiaaaaaaegaabaiaebaaaaaaadaaaaaaaceaaaaaaaaaaaea
aaaaaaeaaaaaaaaaaaaaaaaaegaabaaaafaaaaaadhaaaaajdcaabaaaafaaaaaa
kgakbaaaagaaaaaaegaabaaaafaaaaaaegaabaaaaiaaaaaadcaaaaamdcaabaaa
aiaaaaaaegaabaaaadaaaaaaaceaaaaaaaaaaaeaaaaaaaeaaaaaaaaaaaaaaaaa
ogakbaaaafaaaaaadhaaaaajmcaabaaaafaaaaaapgapbaaaagaaaaaakgaobaaa
afaaaaaaagaebaaaaiaaaaaaboaaaaahbcaabaaaahaaaaaaakaabaaaahaaaaaa
abeaaaaaabaaaaaadgaaaaafdcaabaaaagaaaaaajgafbaaaahaaaaaabgaaaaab
aaaaaaaidcaabaaaadaaaaaaegaabaiaebaaaaaaafaaaaaaegbabaaaabaaaaaa
dhaaaaajicaabaaaabaaaaaabkaabaaaabaaaaaaakaabaaaadaaaaaabkaabaaa
adaaaaaaaaaaaaaidcaabaaaadaaaaaaogakbaaaafaaaaaaegbabaiaebaaaaaa
abaaaaaadhaaaaajbcaabaaaadaaaaaabkaabaaaabaaaaaaakaabaaaadaaaaaa
bkaabaaaadaaaaaadbaaaaahccaabaaaadaaaaaadkaabaaaabaaaaaaakaabaaa
adaaaaaadhaaaaajecaabaaaadaaaaaabkaabaaaadaaaaaaakaabaaaagaaaaaa
bkaabaaaagaaaaaaaaaaaaaiicaabaaaacaaaaaadkaabaiaebaaaaaaaaaaaaaa
dkaabaaaacaaaaaadbaaaaahicaabaaaacaaaaaadkaabaaaacaaaaaaabeaaaaa
aaaaaaaaaaaaaaaiicaabaaaaaaaaaaadkaabaiaebaaaaaaaaaaaaaackaabaaa
adaaaaaadbaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaaabeaaaaaaaaaaaaa
caaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaadkaabaaaacaaaaaadhaaaaaj
icaabaaaaaaaaaaadkaabaaaaaaaaaaaabeaaaaaaaaaaaaackaabaaaabaaaaaa
aaaaaaahecaabaaaabaaaaaadkaabaaaabaaaaaaakaabaaaadaaaaaadhaaaaaj
icaabaaaabaaaaaabkaabaaaadaaaaaadkaabaaaabaaaaaaakaabaaaadaaaaaa
aoaaaaahecaabaaaabaaaaaaabeaaaaaaaaaialpckaabaaaabaaaaaadcaaaaaj
ecaabaaaabaaaaaadkaabaaaabaaaaaackaabaaaabaaaaaaabeaaaaaaaaaaadp
diaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaackaabaaaabaaaaaadhaaaaaj
ecaabaaaabaaaaaabkaabaaaabaaaaaaabeaaaaaaaaaaaaadkaabaaaaaaaaaaa
aaaaaaahbcaabaaaadaaaaaackaabaaaabaaaaaaakbabaaaabaaaaaaabaaaaah
icaabaaaaaaaaaaadkaabaaaaaaaaaaabkaabaaaabaaaaaaaaaaaaahccaabaaa
adaaaaaadkaabaaaaaaaaaaabkbabaaaabaaaaaaeiaaaaalpcaabaaaadaaaaaa
egaabaaaadaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaaabeaaaaaaaaaaaaa
dcaaaaamhcaabaaaaaaaaaaaegacbaaaaaaaaaaaaceaaaaadjiooddndjiooddn
djiooddnaaaaaaaaegacbaaaadaaaaaadcaaaaakhcaabaaaacaaaaaaagaabaia
ebaaaaaaabaaaaaaegacbaaaadaaaaaaegacbaaaaaaaaaaabfaaaaabdgaaaaaf
hccabaaaaaaaaaaaegacbaaaacaaaaaadgaaaaaficcabaaaaaaaaaaaabeaaaaa
aaaaaaaadoaaaaab"
}
}
 }
}
Fallback "Hidden/FXAA II"
}