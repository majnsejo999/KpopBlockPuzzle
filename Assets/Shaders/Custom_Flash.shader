Shader "Custom/Flash"
{
  Properties
  {
    _MainTex ("Texture", 2D) = "white" {}
    _CoverTrans ("Cover Transparent", Range(0, 1)) = 0
    _Angle ("Angle", float) = 75
    _Width ("Width", float) = 0.25
    _Interval ("Interval", float) = 5
    _StartTime ("Start Time", float) = 2
    _PlayTime ("Play Time", float) = 0.6
    _OffSet ("Offset", float) = 0.3
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      Blend SrcAlpha OneMinusSrcAlpha
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      //uniform float4 _Time;
      uniform sampler2D _MainTex;
      uniform int _CoverTrans;
      uniform float _Angle;
      uniform float _Width;
      uniform float _Interval;
      uniform float _StartTime;
      uniform float _PlayTime;
      uniform float _OffSet;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float4 tmpvar_1;
          tmpvar_1.w = 1;
          tmpvar_1.xyz = in_v.vertex.xyz;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_1));
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 texCol_1;
          float4 outp_2;
          float4 tmpvar_3;
          tmpvar_3 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          texCol_1 = tmpvar_3;
          int interval_4;
          interval_4 = int(_Interval);
          int beginTime_5;
          beginTime_5 = int(_StartTime);
          float brightness_6;
          brightness_6 = 0;
          float tmpvar_7;
          tmpvar_7 = (0.0174444 * _Angle);
          float tmpvar_8;
          tmpvar_8 = (_Time.y - float((int((_Time.y / float(interval_4))) * interval_4)));
          if((tmpvar_8>float(beginTime_5)))
          {
              float xProjL_9;
              float x0_10;
              float xPointRightBound_11;
              float xPointLeftBound_12;
              x0_10 = ((tmpvar_8 - float(beginTime_5)) / _PlayTime);
              xProjL_9 = ((in_f.xlv_TEXCOORD0.y / (sin(tmpvar_7) / cos(tmpvar_7))) * 0.5);
              xPointLeftBound_12 = ((x0_10 - _Width) - xProjL_9);
              xPointRightBound_11 = (x0_10 - xProjL_9);
              xPointLeftBound_12 = (xPointLeftBound_12 + _OffSet);
              xPointRightBound_11 = (xPointRightBound_11 + _OffSet);
              if(((in_f.xlv_TEXCOORD0.x>xPointLeftBound_12) && (in_f.xlv_TEXCOORD0.x<xPointRightBound_11)))
              {
                  brightness_6 = ((_Width - (2 * abs((in_f.xlv_TEXCOORD0.x - ((xPointLeftBound_12 + xPointRightBound_11) / 2))))) / _Width);
              }
          }
          float tmpvar_13;
          tmpvar_13 = max(brightness_6, 0);
          brightness_6 = tmpvar_13;
          if((_CoverTrans<1))
          {
              if((texCol_1.w>0.8))
              {
                  outp_2 = (texCol_1 + (float4(0.1, 0.1, 0.1, 1) * tmpvar_13));
              }
              else
              outp_2 = texCol_1;
          }
          else
          {
              outp_2 = (texCol_1 + float4(tmpvar_13, tmpvar_13, tmpvar_13, tmpvar_13));
          }
          out_f.color = outp_2;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
