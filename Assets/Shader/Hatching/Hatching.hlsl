void hatching_float(half2 uv, half intensity, half4 hatch0, half4 hatch1, out half3 OutHatch) {
    
    half3 overbright = max(0, intensity - 1.0);

    half3 weightsA = saturate((intensity * 6.0) + half3(-0, -1, -2));
    half3 weightsB = saturate((intensity * 6.0) + half3(-3, -4, -5));

    weightsA.xy -= weightsA.yz;
    weightsA.z -= weightsB.x;
    weightsB.xy -= weightsB.yz;

    half3 hatch0out = hatch0 * weightsA;
    half3 hatch1out = hatch1 * weightsB;

    OutHatch = overbright + hatch0out.r + hatch0out.g + hatch0out.b + hatch1out.r + hatch1out.g + hatch1out.b;

  }
