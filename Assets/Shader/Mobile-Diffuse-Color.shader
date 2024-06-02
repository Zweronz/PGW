Shader "Mobile/Diffuse-Color" {
	Properties {
		_ColorRili ("Rili Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 150
	
	CGPROGRAM
	#pragma surface surf Lambert noforwardadd
	
	sampler2D _MainTex;
	float4 _ColorRili;
	
	struct Input {
		float2 uv_MainTex;
	};
	
	void surf (Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = c.rgb * _ColorRili;
		o.Alpha = c.a;
	}
	ENDCG
	}
	
	Fallback "Mobile/VertexLit"
	}