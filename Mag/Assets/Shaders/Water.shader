Shader "Water"
{
    Properties
    {
        _DrawPosition ("Draw Position", Vector) = (-1,-1,0,0)
        _Draw("Draw", Range(0,1)) = 0
    }

    SubShader
    {
        Lighting Off
        Blend One Zero

        Pass
        {
            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            float4 _DrawPosition;
            int _Draw;

            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float4 previousColor = tex2D(_SelfTexture2D, IN.localTexcoord.xy);
                float alpha = 1 - smoothstep(0, .2, distance(IN.localTexcoord.xy, _DrawPosition));
                if (_Draw == 1) 
                { 
                    if (!(previousColor.a > 0 && alpha < previousColor.a))
                        previousColor.a = alpha;
                //float4 Color = smoothstep(0, .2, distance(IN.localTexcoord.xy, _DrawPosition));
                    return previousColor;
                }
                else
                    if (previousColor.a > 0 && alpha < previousColor.a)
                        previousColor.a = previousColor.a * 0.99 - 0.005;
                return previousColor;
            }
            ENDCG
        }
    }
}