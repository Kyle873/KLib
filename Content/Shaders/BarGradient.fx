sampler s0;

float3 colorStart;
float3 colorEnd;

float4 BarGradient(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, coords);

	if (color.a > 0)
		color.rgb = colorStart.rgb + coords.x * (colorEnd.rgb - colorStart.rgb);

    return color;
}

technique Technique1
{
    pass Pass1
    {
		PixelShader = compile ps_2_0 BarGradient();
    }
}
