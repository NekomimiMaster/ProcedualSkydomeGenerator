#ifndef EXAMPLE_INCLUDED
#define EXAMPLE_INCLUDED

//Random fixed2 => fixed2 [-0.99…, 0.99…]
fixed2 randomF2F2(fixed2 uv)
{
	fixed2 result = fixed2( dot(uv, fixed2(217.13, 451.77)), dot(uv, fixed2(269.5, 383.3)));
	result = 2.0 * frac(sin(result) * 4657.3179123);
	result = result - 1.0;
	return result;
}

//seed
//Random fixed2 => fixed2 [-0.99…, 0.99…]
fixed2 randomF2F2(fixed2 uv, int seed)
{
	fixed2 result = fixed2( dot(uv, fixed2(217.13, 451.77 + seed)), dot(uv, fixed2(269.5, 383.3)));
	result = 2.0 * frac(sin(result) * 4657.3179123);
	result = result - 1.0;
	return result;
}

float PerlinNoise(fixed2 i)
{
    fixed2 uv = frac(i); //グリッド内UV(少数部を取り出す)
    fixed2 grid = floor(i); //グリッド(整数部を取り出す)
    fixed2 u = uv * uv * (3.0 - 2.0 * uv); //SmoothStep()
    
    //グリッドを引数にしてランダムを得る
    fixed2 v00 = randomF2F2( grid + fixed2(0, 0));
    fixed2 v01 = randomF2F2( grid + fixed2(0, 1));
    fixed2 v10 = randomF2F2( grid + fixed2(1, 0));
    fixed2 v11 = randomF2F2( grid + fixed2(1, 1));
    
    //四隅のランダムな勾配ベクトルと、UV座標を基準としたベクトルの内積をとる
    //fixed2()を引いて、ベクトルの原点を揃える
    //v0001 v1011 は内積の結果をx方向でlerp 最後にy方向でlerpする
    //ランダム関数の作るベクトルとuvベクトルの長さが異なるので、ノイズの結果は0.5オフセットする
    float v0001 = lerp(dot(v00, uv - fixed2(0, 0)), dot(v10, uv - fixed2(1, 0)), u.x);
    float v1011 = lerp(dot(v01, uv - fixed2(0, 1)), dot(v11, uv - fixed2(1, 1)), u.x);
    float noise = lerp(v0001, v1011, u.y) + 0.5;
    
    return noise;
}

//seed
float PerlinNoise(fixed2 i, int seed)
{
    fixed2 uv = frac(i); //グリッド内UV(少数部を取り出す)
    fixed2 grid = floor(i); //グリッド(整数部を取り出す)
    fixed2 u = uv * uv * (3.0 - 2.0 * uv); //SmoothStep()
    
    //グリッドを引数にしてランダムを得る
    fixed2 v00 = randomF2F2( grid + fixed2(0, 0), seed);
    fixed2 v01 = randomF2F2( grid + fixed2(0, 1), seed);
    fixed2 v10 = randomF2F2( grid + fixed2(1, 0), seed);
    fixed2 v11 = randomF2F2( grid + fixed2(1, 1), seed);
    
    //四隅のランダムな勾配ベクトルと、UV座標を基準としたベクトルの内積をとる
    //fixed2()を引いて、ベクトルの原点を揃える
    //v0001 v1011 は内積の結果をx方向でlerp 最後にy方向でlerpする
    //ランダム関数の作るベクトルとuvベクトルの長さが異なるので、ノイズの結果は0.5オフセットする
    float v0001 = lerp(dot(v00, uv - fixed2(0, 0)), dot(v10, uv - fixed2(1, 0)), u.x);
    float v1011 = lerp(dot(v01, uv - fixed2(0, 1)), dot(v11, uv - fixed2(1, 1)), u.x);
    float noise = lerp(v0001, v1011, u.y) + 0.5;
    
    return noise;
}

float fBm(fixed2 uv)
{
    //uv座標を二乗して広げてゆく
    //大きなノイズほど間隔が広くなり、小さなノイズほど感覚が狭まる
    //大きなノイズが大まかな特徴となり、細かい特徴は小さなノイズが作る
    fixed2 p = uv;
    float fbm = 0; //[0,1]

    fbm = fbm + 0.5000 * PerlinNoise(p);
    p = p * 2.01;
    fbm = fbm + 0.2500 * PerlinNoise(p);
    p = p * 2.02;
    fbm = fbm + 0.1250 * PerlinNoise(p);
    p = p * 2.03;
    fbm = fbm + 0.0625 * PerlinNoise(p);
    p = p * 2.01;

    return fbm;
}

//seed
float fBm(fixed2 uv, int seed)
{
    fixed2 p = uv;
    float fbm = 0; //[0,1]

    fbm = fbm + 0.5000 * PerlinNoise(p, seed);
    p = p * 2.01;
    fbm = fbm + 0.2500 * PerlinNoise(p, seed);
    p = p * 2.02;
    fbm = fbm + 0.1250 * PerlinNoise(p, seed);
    p = p * 2.03;
    fbm = fbm + 0.0625 * PerlinNoise(p, seed);
    p = p * 2.01;

    return fbm;
}


#endif