uniform sampler3D s_texture0;
uniform sampler3D s_texture1;
uniform sampler3D s_texture2;
uniform sampler3D s_texture3;
uniform vec3 pos;
uniform vec3 iViewDir;

varying vec3 vTexCoord;


// transfer function
vec4 transfer(float I)
{
	vec4 clr;
	float s = 0.75;
	if(I<0.3)
		clr = vec4(s , s , 1.0 , 1.0);
	else
	if(I<0.7)
		clr = vec4(s , 1.0 , s,1.0);
	else
		clr = vec4(1.0 , s,s,1.0) ;
		
	return clr * I * 1.5;
	
}

void main()
{
	vec3 pos_anomalia = vec3(0,0,0);
	vec3 q = (vTexCoord-0.5)* 256;
	float s = dot(q-pos,iViewDir);
	float dist = length(pos + iViewDir*s - q);
	if( s>=0 && s<75 && dist <5)
	{
		gl_FragColor = vec4(0.5,0.5,1,1);
	}
	else
	{
		dist = length(pos-q);
		if( dist<10)
			gl_FragColor = vec4(0.5,0.5,1,1-dist/10);
		else
		{
			dist = length(pos_anomalia-q);
			if( dist<10)
				gl_FragColor = vec4(1,0.5,0.5,1-dist/10);
			else
			{
				gl_FragColor = texture3D(s_texture0, vTexCoord);
			//	gl_FragColor.a *= gl_FragColor.a;
				gl_FragColor.a *= gl_FragColor.a * 0.1;
			}
		}
	}
}

void main2()
{
	vTexCoord = fract(vTexCoord);
	float I;
	float z = vTexCoord.z;
	if(z<0.25)
	{
		vTexCoord.z*=4.0;
		I = texture3D(s_texture0, vTexCoord).r;
	}
	else
	if(z<0.5)
	{
		vTexCoord.z = (vTexCoord.z-0.25)*4.0;
		I = texture3D(s_texture1, vTexCoord).r;
	}
	else
	if(z<0.75)
	{
		vTexCoord.z = (vTexCoord.z-0.5)*4.0;
		I = texture3D(s_texture2, vTexCoord).r;
	}
	else
	{
		vTexCoord.z = (vTexCoord.z-0.75)*4.0;
		I = texture3D(s_texture3, vTexCoord).r;
	}
	
	/*
	float I = texture3D(s_texture3, vTexCoord).r;
	float ambient = 0.5;
	float diffuse = 0.8;
	float gx = texture3D(s_texture0, vTexCoord+vec3(1.0/256.0, 0.0 , 0.0)).r;
	float gy = texture3D(s_texture0, vTexCoord+vec3(0.0, 1.0/256.0, 0.0)).r;
	float gz = texture3D(s_texture0, vTexCoord+vec3(0.0 , 0.0, 1.0/256.0)).r;
	vec3 N = normalize(vec3(gx,gy,gz));
	vec3 L = normalize(vTexCoord*256.0 - vec3(100.0 , 300.0 , 10.0));
	//gl_FragColor = texture3D(s_texture, vTexCoord) * (ambient + abs(dot(N,L))*diffuse);
	gl_FragColor = transfer(I*(ambient + abs(dot(N,L))*diffuse));
	*/
	//gl_FragColor = vec4(vTexCoord , 1.0);
	gl_FragColor = I;
	
}
