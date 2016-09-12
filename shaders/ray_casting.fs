uniform sampler3D s_texture0;
uniform sampler3D s_texture1;
uniform sampler3D s_texture2;
uniform sampler3D s_texture3;
uniform vec3 iLookFrom;
uniform vec3 iViewDir;
uniform vec3 iDx;
uniform vec3 iDy;
uniform float voxel_scale;
uniform float voxel_opacity;
uniform float voxel_step0;
uniform float voxel_step;
uniform int game_status;

varying vec3 vTexCoord;

// transfer function
vec4 transfer(float I)
{
	vec4 clr;
	float t0 = 0.3;
	float t1 = 0.7;
	if(I<t0)
		clr = vec4(0.0 , 0.0 , I , 1.0);
	else
	if(I<t1)
		clr = vec4(0.0 , I , 0.0 , 1.0);
	else
		clr = vec4(I,0.0 , 0.0 , 1.0);
	return 0.1*clr + 0.9*vec4(I,I,I,1.0);
	
}


float opDisplace( vec3 p )
{
	vec3 q = mod(p+8,16)-8;
	vec3 center = vec3(0,0,0);
    float d1 = length(q-center);
    float d2 = 0.5*sin(15.0*p.x)*sin(15.0*p.y)*sin(15.0*p.z);
    return d1+d2;
}

float3 tex3d(vec3 pos)
{
	if(opDisplace(pos)<1)
		return vec3(1.0 ,0.3 ,0.3);

	pos += vec3(128.0,128.0,128.0);
	float k = voxel_scale/256.0;
	return texture3D(s_texture0,pos.xzy*k).rgb;
}

void main()
{
	float cant_total = 40.0;
	//float voxel_step = 1;
	float voxel_step0 = 5.5;
	
	vec2 uv = vTexCoord.xy*0.5;
	// computo la direccion del rayo
	// D = N + Dy*y + Dx*x;
	vec3 rd = normalize(iViewDir + iDy*uv.y + iDx*uv.x);
	vec3 ro = iLookFrom + rd*voxel_step0;
	float3 S = vec3(0.0,0.0,0.0);
	float k = 1.0;
	
	// ray marching
	for (int i = 0; i < cant_total; i++) {
		S += tex3d(ro)*k;
		ro += rd*voxel_step;
		//k*=voxel_opacity;
	}
	S /= cant_total;
	
	if(game_status)
		S.rg *= 1.5;
	
	gl_FragColor = vec4( S, 1.0 );
	
}


